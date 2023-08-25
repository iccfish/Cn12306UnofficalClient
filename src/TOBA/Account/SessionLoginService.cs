namespace TOBA.Account
{
	using Autofac;

	using Configuration;

	using FSLib.Network.Http;

	using Passport;

	using Platform.SlideVc;
	using Platform.Sm4;

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using TOBA.Otn;
	using TOBA.Profile;

	class SessionLoginService : ISessionLoginService
	{
		private Dictionary<string, string> _passportInfo;

		public event EventHandler StateChanged;
		public bool NeedVcLogin { get; set; } = false;

		/// <summary>
		/// 引发 <see cref="StateChanged"/> 事件
		/// </summary>
		protected virtual void OnStateChanged()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		async Task<bool> LoginViaOtnAsync()
		{
			CfSessionId = null;
			Sig = null;
			SlideVcToken = null;

			//var result = await Session.NetClient.VerifyRandCodeAsync(RandCodeType.SjRand, RandCode, null, _ => State = $"[{_}] 正在检查验证码……").ConfigureAwait(true);
			if (NeedVcLogin)
			{
				var result = await Session.NetClient.RunRequestLoopAsync(
						_ =>
						{
							var ctx = Session.NetClient.Create(
								HttpMethod.Get,
								"/passport/captcha/captcha-check",
								"login/init",
								new
								{
									callback = "jQuery19109564546343775517_1546669788548",
									answer = RandCode,
									rand = "sjrand",
									login_site = "E",
									_ = DateTime.Now.ToJsTicks()
								},
								new { result_code = 0, result_message = "" }
							);
							if (ApiConfiguration.Instance.EnableVerifycodeSubmitDelay && NetworkConfiguration.Current.VcSubmitDelay > 0)
							{
								var delay = NetworkConfiguration.Current.VcSubmitDelay - (int)(DateTime.Now - Session.NetClient.LastVcLoadTime).TotalMilliseconds;
								if (delay > 0)
									ctx.SendDelay(new TimeSpan(0, 0, 0, 0, delay));
							}

							return ctx;
						},
						_ => State = $"[{_}] 正在检查验证码……"
					).
					ConfigureAwait(true);
				if (!result.IsValid())
				{
					State = result.GetExceptionMessage("登录失败，请重试(无法校验验证码)");
					return false;
				}

				Session.LastCheckRandCodeTime = DateTime.Now;
				if (result.Result.result_code != 4)
				{
					State = result.Result.result_message ?? "验证码错误";
					return false;
				}

				//现在必须滑动验证码
				var slideSvc = Session.ServiceContainer.Resolve<ISlideVcService>();
				var (need, token) = await slideSvc.GetSlideToken(UserName, NeedSlideVcLogin);
				if (need == null)
				{
					State = "[LOGIN_SLIDE_TOKEN_FAILED] 滑动验证码加载失败";
				}
				else
				{
					SlideVcToken = token;
					NeedSlideVcLogin = need.Value;
				}

				if (NeedSlideVcLogin)
					return false;
			}

			if (!await CheckLoginVerify())
				return false;
			if (!NeedSlideVcLogin && !NeedSmsLogin)
			return await WebLoginAsync();
			return false;
		}

		async Task<bool> CheckLoginVerify()
		{
			var ctx = Session.NetClient.Post("/passport/web/checkLoginVerify", new { username = UserName, appid = "otn" }, result: new { result_code = 0, result_message = "", login_check_code = 0 });
			await ctx.SendAsync();
			if (!ctx.IsValid())
			{
				State = ctx.GetExceptionMessage("[LF-CLV] 登录失败，请重试(无法拉取验证方式)");
				return false;
			}
			var result = ctx.Result;
			NeedSlideVcLogin = result.login_check_code is 1 or 2;
			NeedSmsLogin     = result.login_check_code is 1 or 3;
			if (NeedSlideVcLogin)
			{
				var slideSvc = Session.ServiceContainer.Resolve<ISlideVcService>();
				var (need, token) = await slideSvc.GetSlideToken(UserName, NeedSlideVcLogin);
				if (need == null)
				{
					State = "[LOGIN_SLIDE_TOKEN_FAILED] 滑动验证码加载失败";
				}
				else
				{
					SlideVcToken     = token;
					NeedSlideVcLogin = need.Value;
				}
			}
			return true;
		}
		async Task<bool> WebLoginAsync()
		{
			var sm4srv = AppContext.ExtensionManager.GlobalKernel.Resolve<ISm4CryptoService>();

			var form = new Dictionary<string, string>();
			form.Add("appid",    "otn");
			form.Add("username", UserName);
			form.Add("password", $"@{sm4srv.CryptoEcbBase64(Password)}");
			if (NeedSmsLogin)
				form.Add("randCode", RandCode);
			else if (!RandCode.IsNullOrEmpty())
			form.Add("answer", RandCode);
			form.Add("checkMode", VcType == VcType.None ? "" : (int)VcType + "");

			form["sessionId"]                     = CfSessionId  ?? "";
			form["sig"]                           = Sig          ?? "";
			form["if_check_slide_passcode_token"] = SlideVcToken ?? "";
			form["scene"]                         = NeedSlideVcLogin ? "nc_login" : "";

			//form["randCode_validate"] = Utility.CreateCaptchaValidate(vccode);
			if (Session.DynamicJsData != null)
				form[Session.DynamicJsData.Key] = Session.DynamicJsData.EncodedValue;
			if (VcType is VcType.Sms && (DateTime.Now - SmsTime).TotalMilliseconds < ApiConfiguration.Instance.SmsVerificationTimeWait)
			{
				var waitTime = (int)((DateTime.Now - SmsTime).TotalMilliseconds - ApiConfiguration.Instance.SmsVerificationTimeWait);
				if (waitTime > 0)
					await Task.Delay(waitTime);
			}
			var context = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create<string>(
						HttpMethod.Post,
						"/passport/web/login",
						"login/init",
						form),
					_ => State = $"[{_}] 正在登录中……")
				.ConfigureAwait(true);
			if (!context.IsValid())
			{
				State = context.GetExceptionMessage("登录失败，请重试(提交登录请求)");
				return false;
			}

			var loginResult = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(
				context.Result,
				new
				{
					result_message = "",
					result_code    = 0,
					uamtk          = "",
					mobile         = ""
				});
			//loginAddress	登录地址

			if (loginResult.result_code != 0)
			{
				var errorMsg = loginResult.result_message ?? "未知错误";

				switch (loginResult.result_code)
				{
					case 91:
						errorMsg = $"需验证手机号，请用尾号{loginResult.mobile}的手机发送“666”到12306";
						break;
					case 101:
						errorMsg = "需要更新密码，请登录12306重置密码后登录";
						break;
				}
				errorMsg = Msg.Translate(errorMsg);
				if (errorMsg.IndexOf("验证码") != -1)
				{
					State = "验证码错误";
					return false;
				}

				if (errorMsg.IndexOf("网络繁忙") != -1)
				{
					State = "页面验证出错，重试中.";
					return false;
				}

				State = errorMsg.DefaultForEmpty("未知错误消息");
				return false;
			}

			var uamService = AppContext.ExtensionManager.GlobalKernel.Resolve<IUamAuthService>();
			var (uamOk, uamMsg, userName) = await uamService.AuthTkAsync(Session.NetClient, _ => State = _).ConfigureAwait(true);

			if (uamOk != true)
			{
				State = uamMsg;
				return false;
			}
			else
			{
				Session.UserKeyData.DisplayName = userName;
			}

			return true;
		}

		async Task<bool> LoginViaNativeAsync()
		{
			if (Session.HttpConf.IsLoginPassCode)
			{
				var result = await Session.NetClient.RunRequestLoopAsync(
						_ =>
						{
							var ctx = Session.NetClient.Create(
								HttpMethod.Post,
								"passcodeNew/checkRandCodeAnsyn",
								"login/init",
								new
								{
									randCode = RandCode,
									rand = "sjrand"
								},
								new { data = new { result = 0, msg = "" } }
							);
							if (ApiConfiguration.Instance.EnableVerifycodeSubmitDelay && NetworkConfiguration.Current.VcSubmitDelay > 0)
							{
								var delay = NetworkConfiguration.Current.VcSubmitDelay - (int)(DateTime.Now - Session.NetClient.LastVcLoadTime).TotalMilliseconds;
								if (delay > 0)
									ctx.SendDelay(new TimeSpan(0, 0, 0, 0, delay));
							}

							return ctx;
						},
						_ => State = $"[{_}] 正在检查验证码……"
					).
					ConfigureAwait(true);
				if (!result.IsValid() || result.Result.data == null)
				{
					State = result.GetExceptionMessage("登录失败，请重试(无法校验验证码)");
					return false;
				}

				Session.LastCheckRandCodeTime = DateTime.Now;
				if (result.Result.data.result != 1)
				{
					State = result.Result.data.msg ?? "验证码错误";
					return false;
				}
			}

			var form = new Dictionary<string, string>();
			form.Add("loginUserDTO.user_name", UserName);
			form.Add("userDTO.password", Password);
			if (Session.HttpConf.IsLoginPassCode)
				form.Add("randCode", RandCode);
			//form["randCode_validate"] = Utility.CreateCaptchaValidate(vccode);
			if (Session.DynamicJsData != null)
				form[Session.DynamicJsData.Key] = Session.DynamicJsData.EncodedValue;
			form["myversion"] = "undefined";

			var context = await Session.NetClient.RunRequestLoopAsync(_ => Session.NetClient.Create(
					HttpMethod.Post,
					"login/loginAysnSuggest",
					"login/init",
					form,
					new { data = new Dictionary<string, string>(), messages = new List<string>() }),
				_ => State = $"[{_}] 正在登录中……").ConfigureAwait(true);
			var loginResult = context.Result;
			if (!context.IsValid() || loginResult.data == null)
			{
				State = context.GetExceptionMessage("登录失败，请重试(提交登录请求)");
				return false;
			}

			//loginAddress	登录地址

			if (loginResult.data?.GetValue("loginCheck") != "Y")
			{
				var errorMsg = loginResult.messages?.JoinAsString(",") ?? "未知错误";
				errorMsg = Msg.Translate(errorMsg);
				if (errorMsg.IndexOf("验证码") != -1)
				{
					State = "验证码错误";
					return false;
				}

				if (errorMsg.IndexOf("网络繁忙") != -1)
				{
					State = "页面验证出错，重试中.";
					return false;
				}

				State = errorMsg.DefaultForEmpty("未知错误消息");
				return false;
			}
			//userLogin
			await Session.NetClient.Create<string>(HttpMethod.Post, "login/userLogin", "login/init", new
			{
				_json_att = Session.Attributes ?? ""
			}).SendAsync().ConfigureAwait(true);

			return true;
		}

		public async Task<bool> CompleteVcAsync()
		{

			var result = await WebLoginAsync();
			if (result)
				await PostLoginAsync();
			return result;
		}

		public DateTime SmsTime { get; set; }
		public VcType VcType { get; set; }
		/// <summary>
		/// 登录
		/// </summary>
		/// <returns></returns>
		public async Task<bool> LoginAsync()
		{
			if (Session == null)
				throw new InvalidOperationException();

			//OTN?
			if (Session.HttpConf.IsUamLogin)
			{
				if (!await LoginViaOtnAsync().ConfigureAwait(true))
					return false;
			}
			else
			{
				//loginasyncsuggest
				if (!await LoginViaNativeAsync().ConfigureAwait(true))
					return false;
			}

			await PostLoginAsync();

			return true;
		}

		async Task PostLoginAsync()
		{
			//登录成功
			if (Session.UserName != UserName)
			{
				var oldSession = Session;

				var client = Session.NetClient;
				Session = new Session(UserName, TempMode, RunTime.SessionManager.IsLogined(UserName))
				{
					NetClient = client,
					UserData = oldSession.UserData,
					SessionData = oldSession.SessionData
				};
				client.Session = Session;
			}

			Session.Password = Password;
			if (!IsRelogin)
			{
				if (StorePwd)
					Session.UserKeyData.Password = Password;
				else
				{
					Session.UserKeyData.Password = null;
				}
			}

			Session.IsPassengerLoaded = false;
			Session.UserKeyData.LastLoginTime = DateTime.Now;
			Session.UserKeyData.LoginTimes++;
			//Session.UserData = ckResult.Result.Data;
			//Session.Attributes = ckResult.Result.Attributes;
			if (!Session.TemporaryMode)
				UserKeyDataMap.Current.Save();

			//重置环境参数
			//Session.SessionData.Clear();

			State = "登录成功";
		}

		/// <summary>
		/// 准备登录
		/// </summary>
		/// <returns></returns>
		public async Task<bool?> PrepareLoginAsync()
		{
			var ctx = SynchronizationContext.Current;
			Trace.TraceInformation(ctx.GetType().FullName);
			//初始化会话
			if (Session == null)
				Session = new Session("$tmp$", true, false);

			Session.NetClient.ClearOtnPort();
			Session.NetClient?.Clear();
			//Clear Df
			AppContext.ExtensionManager.EnumerateExtension(_ => _.SessionInit(Session));
			await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create<string>(HttpMethod.Get, "/index/index.html", "resources/login.html"),
					_ => State = $"{_} 正在准备..."
				).
				ConfigureAwait(true);
			var indexCtx = await Session.NetClient.RunRequestLoopAsync(
						_ => Session.NetClient.Create<string>(HttpMethod.Get, "resources/login.html", "resources/login.html"),
						_ => State = $"{_} 正在准备..."
					).
					ConfigureAwait(true);
			if (!indexCtx.IsValid())
			{
				State = "页面加载失败";
				return false;
			}

			var loginJsFile = Regex.Match(indexCtx.Result, @"js/login.*?\.js");
			if (!loginJsFile.Success)
			{
				State = "[IDX0001] 未能解析登录页";
				return false;
			}

			var jsCtx = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create<string>(HttpMethod.Get, $"resources/{loginJsFile.Value}", "resources/login.html"),
					_ => State = $"{_} 正在解析APPKEY..."
				).ConfigureAwait(true);
			if (!jsCtx.IsValid())
			{
				State = "[IDX0002] 未能解析APPKEY";
				return false;
			}

			SlideAppId = Regex.Match(jsCtx.Result, @"appKey\s*=\s*['""](.*?)['""]", RegexOptions.IgnoreCase).GetGroupValue(1);
			if (SlideAppId.IsNullOrEmpty())
			{
				State = "[IDX0003] 未能解析APPKEY";
				return false;
			}
			//检测是否需要滑动验证码
			var needSlideCheck = Regex.Match(jsCtx.Result, @"url:\s*popup_passport_captcha_check[\s\S]*?slide_passport\(\s*(true|false)\s*\)[\s\S]*?error", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			if (needSlideCheck.Success)
			{
				NeedSlideVcLogin = needSlideCheck.GetGroupValue(1) != "true";
			}

			indexCtx = await Session.NetClient.RunRequestLoopAsync(
						_ => Session.NetClient.Create<string>(HttpMethod.Get, "index12306/getLoginBanner", "resources/login.html"),
						_ => State = $"{_} 正在准备..."
					).
					ConfigureAwait(true);
			if (!indexCtx.IsValid())
			{
				State = "[LF1] 页面加载失败";
				return false;
			}
			//初始化HttpConf
			if (!await Session.InitHttpConfAsync(true))
			{
				State = "HTTPCONF更新失败，请重试.";
				return false;
			}

			//进入登录页
			if (Session.HttpConf.IsUamLogin)
			{
				var initTask = await Session.NetClient.RunRequestLoopAsync(
						_ => Session.NetClient.Create(HttpMethod.Post, "/passport/web/auth/uamtk-static", "resources/login.html", new { appid = "otn" }, new { result_message = "", result_code = 1 }),
						_ => State = $"{_} 正在准备登录信息..."
					).
					ConfigureAwait(true);

				if (!initTask.IsValid())
				{
					State = initTask.GetExceptionMessage("网络错误，请重试。");
					return false;
				}

				if (initTask.Result.result_code == 0)
				{
					//已登录
					return null;
				}
			}

			return true;
		}

		/// <summary>
		/// 获得此次登录是否引发了登录冲突
		/// </summary>
		public bool LoginConflict { get; private set; }

		/// <summary>
		/// 获得或设置当前的密码
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 获得或设置当前的验证码
		/// </summary>
		public string RandCode { get; set; }

		/// <summary>
		/// 获得或设置当前的会话
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 获得或设置当前的状态
		/// </summary>
		public string State { get; private set; }

		/// <summary>
		/// 是否保存密码
		/// </summary>
		public bool StorePwd { get; set; }

		/// <summary>
		/// 获得或设置是否是临时模式
		/// </summary>
		public bool TempMode { get; set; }

		/// <summary>
		/// 获得或设置当前的用户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 获得或设置是否是重新登录
		/// </summary>
		public bool IsRelogin { get; set; }

		public bool NeedSlideVcLogin { get; private set; }

		public bool NeedSmsLogin { get; private set; }
		/// <inheritdoc />
		public string SlideAppId { get; private set; }

		public string SlideVcToken { get; private set; }

		public string CfSessionId { get; set; }

		public string Sig { get; set; }
	}
}