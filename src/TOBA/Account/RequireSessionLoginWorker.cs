namespace TOBA.Account
{
	using FSLib.Network.Http;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	using TOBA.Otn;
	using TOBA.Otn.Entity;
	using TOBA.Profile;

	using WebLib;

	internal class RequireSessionLoginWorker
	{
		internal class RequireEnterVerifyCodeEventArgs : EventArgs
		{
			/// <summary>
			/// 获得关联的临时会话
			/// </summary>
			public Session Session { get; set; }

			/// <summary>
			/// 获得验证码
			/// </summary>
			public string VerifyCode { get; set; }

			/// <summary>
			/// 创建 <see cref="RequireEnterVerifyCodeEventArgs" />  的新实例(RequireEnterVerifyCodeEventArgs)
			/// </summary>
			public RequireEnterVerifyCodeEventArgs(Session session)
			{
				Session = session;
			}
		}

		/// <summary>
		/// 请求输入验证码
		/// </summary>
		public event EventHandler<RequireEnterVerifyCodeEventArgs> RequireEnterVerifyCode;

		/// <summary>
		/// 引发 <see cref="RequireEnterVerifyCode" /> 事件
		/// </summary>
		protected virtual void OnRequireEnterVerifyCode(RequireEnterVerifyCodeEventArgs e)
		{
			var handler = RequireEnterVerifyCode;
			if (handler != null)
				handler(this, e);
		}

		/// <summary>
		/// 获得创建的会话
		/// </summary>
		public Session Session { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		/// <summary>
		/// 获得或设置是否重试登录请求
		/// </summary>
		public bool IsRelogin { get; set; }

		/// <summary>
		/// 是否保存密码
		/// </summary>
		public bool StorePwd { get; set; }

		/// <summary>
		/// 获得或设置是否是临时模式
		/// </summary>
		public bool TempMode { get; set; }

		public string VerifyCode { get; set; }

		public bool Cancelled { get; set; }

		/// <summary>
		/// 状态 0-运行中
		/// </summary>
		public OpearationState State { get; private set; }

		/// <summary>
		/// 状态发生变化
		/// </summary>
		public event EventHandler StateChanged;

		/// <summary>
		/// 引发 <see cref="StateChanged" /> 事件
		/// </summary>
		protected virtual void OnStateChanged()
		{
			var handler = StateChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发状态变化事件
		/// </summary>
		/// <param name="status"></param>
		/// <param name="message"></param>
		protected virtual void TriggerStateChange(OpearationState? status, string message)
		{
			if (status != null) State = status.Value;
			if (message != null) Message = message;

			OnStateChanged();
		}

		public event EventHandler VerifyCodeError;

		/// <summary>
		/// 引发 <see cref="VerifyCodeError" /> 事件
		/// </summary>
		protected virtual void OnVerifyCodeError()
		{
			var handler = VerifyCodeError;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 状态信息
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// 获得本次登录的时候是否发生了登录冲突
		/// </summary>
		public bool LoginConflict { get; private set; }

		/// <summary>
		/// 进行登录
		/// </summary>
		public void DoLogin()
		{
			var session = Session ?? new Session(UserName, TempMode, RunTime.SessionManager.IsLogined(UserName));
			Session = null;
			string vccode = null;

			do
			{
				TriggerStateChange(OpearationState.Running, "正在准备...");
				var initTask = session.NetClient.Create<string>(HttpMethod.Get, "login/init", "login/init").Send();
				if (!initTask.IsRedirection)
				{
					//如果是302，则已经成功

					TriggerStateChange(OpearationState.Running, "正在获得验证码....");
					var vce = new RequireEnterVerifyCodeEventArgs(session);
					OnRequireEnterVerifyCode(vce);
					vccode = vce.VerifyCode;
					if (vccode.IsNullOrEmpty())
					{
						Cancelled = true;
						return;
					}

					TriggerStateChange(OpearationState.Running, "正在检查验证码……");
					var result = session.NetClient.VerifyRandCode(RandCodeType.SjRand,
						vccode, null,
																_ => TriggerStateChange(OpearationState.Running, $"[{_}] 正在检查验证码……"));

					if (!result.IsNullOrEmpty())
					{
						TriggerStateChange(OpearationState.Blocked, "验证码有问题：" + result);
						return;
					}

					TriggerStateChange(OpearationState.Running, "正在登录中……");
					var form = new Dictionary<string, string>();
					form.Add("loginUserDTO.user_name", session.UserName);
					form.Add("userDTO.password", Password);
					form.Add("randCode", vccode);
					//form["randCode_validate"] = Utility.CreateCaptchaValidate(vccode);
					if (session.DynamicJsData != null)
						form[session.DynamicJsData.Key] = session.DynamicJsData.EncodedValue;
					form["myversion"] = "undefined";

					var context = session.NetClient.RunRequestLoop(_ => session.NetClient.Create<string>(
																									 HttpMethod.Post,
																									"login/loginAysnSuggest",
																									"login/init",
																									form),
																	_ => TriggerStateChange(OpearationState.Running, $"[{_}] 正在登录中……"));
					if (context == null || !context.IsSuccess)
					{
						TriggerStateChange(OpearationState.Blocked, "登录失败，请重试(提交登录请求)");
						return;
					}
					var loginResult = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(context.Result, new { data = new Dictionary<string, string>(), messages = new List<string>() });
					//loginAddress	登录地址


					if (loginResult.data == null || loginResult.data.GetValue("loginCheck") != "Y")
					{
						var errorMsg = loginResult.messages.JoinAsString(",");
						errorMsg = Msg.Translate(errorMsg);
						if (errorMsg.IndexOf("验证码") != -1)
						{
							OnVerifyCodeError();
							continue;
						}
						if (errorMsg.IndexOf("网络繁忙") != -1)
						{
							TriggerStateChange(OpearationState.Fail, "页面验证出错，重试中.");
							continue;
						}
						TriggerStateChange(OpearationState.Blocked, (errorMsg.DefaultForEmpty("未知错误消息")));
						return;
					}
					if (loginResult.data.ContainsKey("username"))
						session.UserKeyData.DisplayName = loginResult.data["username"];
					//登录冲突
					if (loginResult.data.GetValue("notifysession") == "Y")
						LoginConflict = true;
				}

				//设置资料
				var ckResult = session.NetClient.RunRequestLoop(
																 _ => session.NetClient.Create<CheckUserResponse>(HttpMethod.Post, "login/checkUser", "leftTicket/init"),
																_ => TriggerStateChange(OpearationState.Running, $"[{_}] 校验状态")
					);
				if (!ckResult.IsValid() || !ckResult.Result.Status)
				{
					TriggerStateChange(OpearationState.Blocked, "登录失败，请重试");
					return;
				}
				if (ckResult.Result.Data?.Flag != true)
				{
					var error = ckResult.Result.Messages.JoinAsString("").DefaultForEmpty("未知错误");
					TriggerStateChange(OpearationState.Blocked, "登录失败: " + error);
					return;
				}
				session.Password = Password;
				if (!IsRelogin)
				{
					if (StorePwd)
						session.UserKeyData.Password = Password;
					else
					{
						session.UserKeyData.Password = null;
					}
				}
				session.IsPassengerLoaded = false;
				session.UserData = ckResult.Result.Data;
				session.Attributes = ckResult.Result.Attributes;
				if (!session.TemporaryMode)
					UserKeyDataMap.Current.Save();

				//userLogin
				session.NetClient.Create<string>(HttpMethod.Post, "login/userLogin", "login/init", new
				{
					_json_att = session.Attributes ?? ""
				}).Send();

				//刷新用户状态
				TriggerStateChange(OpearationState.Running, "正在刷新用户状态信息...");

				var userInfo = session.NetClient.RunRequestLoop(
																 _ => session.NetClient.Create<string>(HttpMethod.Get, "index/initMy12306", "login/init"),
																_ => TriggerStateChange(OpearationState.Running, $"[{_}] 正在刷新用户状态信息...")
					);
				if (!userInfo.IsValid())
				{
					TriggerStateChange(OpearationState.Blocked, "登录失败: 尝试刷新用户信息的时候遇到网络问题，请重试。");
					return;
				}
				var infoMatches = Regex.Matches(userInfo.Result, @"var\s+(user_name|_is_active|_email|_is_needModifyPassword|notify_TWO_[12]|notify_(THREE|FOUR|FIVE|SESSION))\s*=['""]?([^;\r\n]+?)['""]?\s*;\s*", RegexOptions.Singleline | RegexOptions.IgnoreCase).Cast<Match>().ToDictionary(s => s.GetGroupValue(1), s => s.GetGroupValue(3).DecodeFromJsExpression());
				//真实姓名
				session.UserKeyData.DisplayName = infoMatches.GetValue("user_name");
				session.UserKeyData.Email = infoMatches.GetValue("_email");
				//未激活
				//弹窗提示
				var prompt = (string)null;//infoMatches.GetValue("notify_TWO_2")?.Replace("本网站", "12306");
				if (prompt == "null")
					prompt = null;
				if (prompt.IsNullOrEmpty() && infoMatches.GetValue("_is_active") == "N")
				{
					prompt = "您的邮箱尚未激活，建议您尽快登录网页版12306并验证邮箱。";
				}
				if (!prompt.IsNullOrEmpty())
				{
					//您填写的身份信息有误，未能通过国家身份信息管理权威部门核验，请检查您的姓名和身份证件号码填写是否正确。如有疑问，可致电12306客服咨询。
					if (prompt.IndexOf("身份信息有误", StringComparison.OrdinalIgnoreCase) != -1)
					{
						session.IsUserVerified = false;
						prompt = null;
					}
					//根据本网站服务条款，您需要提供真实、准确的本人资料，您注册时的手机号码被多个用户使用，为了保障您的个人信息安全，请您到就近办理客运售票业务的铁路车站完成身份核验，通过后即可在网上购票，本网站同时将核验与您身份信息重复的用户。谢谢您的支持。
				}

				if (!IsRelogin)
				{
					session.LoginNotification = prompt;
					if (infoMatches.GetValue("notify_SESSION") == "Y")
						LoginConflict = true;
				}

				//重置环境参数
				//session.SessionData.Clear();

				TriggerStateChange(OpearationState.Success, "登录成功");
				Session = session;

				break;
			} while (true);

		}
	}
}
