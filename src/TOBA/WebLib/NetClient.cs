//-# define APPEND_DEBUG_HEADER

using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;

using TOBA.Entity;

namespace TOBA.WebLib
{
	using Configuration;

	using Entity.Web;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using Otn;

	using System.Collections.Generic;
	using System.Data;
	using System.Diagnostics;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	internal class NetClient : HttpClient, INetClient
	{


		readonly object _lockObject = new object();

		public NetClient()
			: this(null)
		{
		}

		public NetClient(Session session)
		{
			HttpHandler = new NetClientHandler() { NetClient = this };
			Session = session;
			Setting.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4385.0 Safari/537.36";
			ClearOtnPort();
		}

		public string AppID { get; set; } = "otn";

		public string AppTk { get; set; }

		public string Uamtk { get; set; }

		#region Overrides of HttpClient

		/// <summary>
		/// 引发 <see cref="HttpClient.HttpContextCreated" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		public override void OnHttpContextCreated(WebEventArgs ea)
		{
			base.OnHttpContextCreated(ea);
			ea.Context.RequestValidateResponse += CheckResponse;
			ea.Context.ValidateResponseHeader += Context_ValidateResponseHeader;
		}

		#endregion

		/// <summary>
		/// 引发 <see cref="HttpClient.RequestFailed" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		public override void OnRequestFailed(WebEventArgs ea)
		{
			base.OnRequestFailed(ea);
			if (!RunTime.PreProcessWebMessage(this, ea.Context))
				ea.Cancelled = true;
		}


		/// <summary>
		/// 引发 <see cref="HttpClient.RequestSuccess" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		public override void OnRequestSuccess(WebEventArgs ea)
		{
			base.OnRequestSuccess(ea);
			if (!RunTime.PreProcessWebMessage(this, ea.Context))
				ea.Cancelled = true;
		}

		/// <summary>
		/// 获得当前关联的上下文环境
		/// </summary>
		public Session Session { get; set; }
		#region 服务器环境保持
		private ExportedSession _currentOtnPort;
		internal void StoreOtnPort()
		{
			_currentOtnPort = new ExportedSession();
			_currentOtnPort.FromSession(Session, this);
		}
		internal void ClearOtnPort()
		{
			_currentOtnPort = null;
		}
		internal void ResetOtnPort()
		{
			_currentOtnPort?.ToSession(Session, this);
		}
		#endregion

		#region 实际业务操作逻辑



		/// <summary>
		/// 验证当前会话是否有效
		/// </summary>
		/// <returns></returns>
		public bool? VerifySessionValid(int recursiveCount = 2, bool noOtnPortCheck = false)
		{
			var count = 1;
			var lastValid = (bool?)null;
			//var uamService = ServcieManager.Instance.GlobalKernel.Get<IUamAuthService>();

			if (noOtnPortCheck)
				recursiveCount = 1;
			do
			{
				if (count > 1)
					Thread.Sleep(300);

				//uam校验

				//var (success, errorMsg, userName) = uamService.AuthTkAsync(this, null).Result;
				//if (success != true)
				//	continue;

				var r = Create<string>(HttpMethod.Get, "index/initMy12306", "leftTicket/init");
				if (noOtnPortCheck)
					r.ContextData.Add("no_otn_portcheck", true);
#if APPEND_DEBUG_HEADER
				r.AddRequestHeader("Via", $"Fish-VerifySessionValid-Step-{count}-of-{recursiveCount}-OtnCheck-{noOtnPortCheck}");
#endif
				r.Send();

				if (!r.IsValid() || r.IsRedirection)
				{
					if (r.Exception is ForceLogoutException)
					{
						lastValid = false;
						break;
					}

					continue;
				}

				//if (!r.Result.Data.Flag)
				//{
				//	//冲突登录
				//	if (r.Result.GetErrorMessages().IndexOf("已失效", StringComparison.CurrentCulture) != -1)
				//	{
				//		if (Session != null)
				//		{
				//			var ea = new GeneralEventArgs<bool>(false);
				//			Session.OnForceLogout(Session, ea);
				//		}
				//		return false;
				//	}
				//	lastValid = false;

				//	continue; //无效
				//}

				//if (!string.IsNullOrEmpty(r.Result.Attributes) && Session != null)
				//{
				//	Session.Attributes = r.Result.Attributes;
				//}
				return true;
			} while (count++ < recursiveCount);

			return lastValid;
		}

		/// <summary>
		/// 检测登录状态
		/// </summary>
		/// <returns></returns>
		public bool? CheckLoginState()
		{
			var ctx = Create<byte[]>(HttpMethod.Get, "index/initMy12306", "leftTicket/init").Send();
			//不成功，则假定是网络问题。
			if (!ctx.IsSuccess)
				return null;

			if (ctx.IsRedirection && ctx.Redirection.Current.PathAndQuery.IndexOf("login") != -1)
			{
				Events.OnWarning(this, new EventInfoArgs("警告：账户【{1}】尝试检测登录状态时被重定向，这意味着已经被系统强行退出。重定向地址={0}".FormatWith(ctx.Redirection.Current, Session.UserName)));
			}
			else
			{
				Events.OnMessage(this, new EventInfoArgs($"信息：检测登录状态，结果正常。用户：{Session.UserName}"));
			}

			return !ctx.IsRedirection;
		}

		/// <summary>
		/// 异步注销
		/// </summary>
		/// <returns></returns>
		public async Task LogoutAsync()
		{
			await RunRequestLoopAsync(_ => Create<string>(HttpMethod.Get, "login/loginOut", "")).ConfigureAwait(true);
		}

		/// <summary>
		/// 校验验证码
		/// </summary>
		/// <param name="type"></param>
		/// <param name="code"></param>
		/// <param name="token"></param>
		/// <param name="tryIndicator"></param>
		/// <returns></returns>
		public string VerifyRandCode(RandCodeType type, string code, string token, Action<int> tryIndicator = null)
		{
			return VerifyRandCodeAsync(type, code, token, tryIndicator).Result;
		}

		/// <summary>
		/// 校验验证码
		/// </summary>
		/// <param name="type"></param>
		/// <param name="code"></param>
		/// <param name="token"></param>
		/// <param name="tryIndicator"></param>
		/// <returns></returns>
		public async Task<string> VerifyRandCodeAsync(RandCodeType type, string code, string token, Action<int> tryIndicator = null)
		{
			if (type == RandCodeType.SjRand)
			{
				var url = "/passport/captcha/captcha-check";
				var referUrl = "";
				object data = new
				{
					answer = code,
					rand = "sjrand",
					login_site = "E"
				};

				//var sendDelay

				var result = await RunRequestLoopAsync(
					_ =>
					{
						var ctx = Create(
							HttpMethod.Post,
							url,
							referUrl,
							data,
							new { result_code = 0, result_message = "" }
						);
						if (ApiConfiguration.Instance.EnableVerifycodeSubmitDelay && NetworkConfiguration.Current.VcSubmitDelay > 0)
						{
							var delay = NetworkConfiguration.Current.VcSubmitDelay - (int)(DateTime.Now - LastVcLoadTime).TotalMilliseconds;
							if (delay > 0)
								ctx.SendDelay(new TimeSpan(0, 0, 0, 0, delay));
						}

						return ctx;
					},
					tryIndicator
				).ConfigureAwait(true);

				if (!result.IsValid())
				{
					return "登录失败，请重试(无法校验验证码)";
				}
				Session.LastCheckRandCodeTime = DateTime.Now;
				if (result.Result.result_code != 4)
				{
					return result.Result.result_message ?? "验证码错误";
				}
			}
			else
			{
				var url = "passcodeNew/checkRandCodeAnsyn";
				var referUrl = "";
				object data = null;

				switch (type)
				{
					case RandCodeType.Randp:
						referUrl = "confirmPassenger/initDc";
						data = new
						{
							randCode = code,
							rand = "randp",
							REPEAT_SUBMIT_TOKEN = token,
							_json_att = Session.Attributes
						}; break;
					case RandCodeType.RandpResign:
						referUrl = "confirmPassenger/initGc";
						data = new
						{
							randCode = code,
							rand = "randp",
							REPEAT_SUBMIT_TOKEN = token,
							_json_att = Session.Attributes
						}; break;
					default:
						break;
				}

				//var sendDelay

				var result = await RunRequestLoopAsync(
					_ =>
					{
						var ctx = Create(
							HttpMethod.Post,
							url,
							referUrl,
							data,
							new { data = new { result = 0 } }
						);
						if (ApiConfiguration.Instance.EnableVerifycodeSubmitDelay && NetworkConfiguration.Current.VcSubmitDelay > 0)
						{
							var delay = NetworkConfiguration.Current.VcSubmitDelay - (int)(DateTime.Now - LastVcLoadTime).TotalMilliseconds;
							if (delay > 0)
								ctx.SendDelay(new TimeSpan(0, 0, 0, 0, delay));
						}

						return ctx;
					},
					tryIndicator
				).ConfigureAwait(true);

				if (!result.IsValid())
				{
					return "登录失败，请重试(无法校验验证码)";
				}
				Session.LastCheckRandCodeTime = DateTime.Now;
				if ((result.Result.data?.result ?? 0) == 0)
				{
					return "验证码错误";
				}
			}


			return null;
		}


		static Random _random = new Random();
		private DateTime _lastVcLoadTime;

		void CreateVerifyCodeTask64(WebLib.RandCodeType type, Action<Image, HttpContext> callback, Action<HttpContext> failCallback)
		{
			var netctx = Create(HttpMethod.Get, "/passport/captcha/captcha-image64?login_site=E&module=login&rand=sjrand&1546670759129&callback=jQuery19109564546343775517_1546669788561&_=1546669788578", "resources/login.html", result: new { image = "", result_code = 0, result_message = "" });
			if (ApiConfiguration.Instance.EnableVerifycodeLoadDelay && Session.LastCheckRandCodeTime.HasValue)
			{
				var timespan = DateTime.Now - Session.LastCheckRandCodeTime.Value;
				if (timespan.TotalMilliseconds < NetworkConfiguration.Current.ReloadVcCodeDelay)
					netctx.SendDelay(new TimeSpan(0, 0, 0, 0, (int)(NetworkConfiguration.Current.ReloadVcCodeDelay - timespan.TotalMilliseconds)));
			}
			netctx.SendAsPromise().Done((x, y) =>
			{
				var ctx = y.Result;
				Image img = null;
				if (!ctx.Result.image.IsNullOrEmpty())
				{
					try
					{
						var ms = new MemoryStream(Convert.FromBase64String(ctx.Result.image));
						img = Image.FromStream(ms);
						img.Tag = ms;
					}
					catch (Exception e)
					{
						failCallback(y.Result);
						return;
					}
				}
				//0-正常，3-过于频繁
				if (ctx.Result.result_code != 0)
				{
					failCallback(y.Result);
				}
				else
				{
					LastVcLoadTime = DateTime.Now;
					callback(img, ctx);
				}

			}).Fail((x, y) =>
			{
				failCallback(y.Result);
			});
		}

		public void CreateVerifyCodeTask(WebLib.RandCodeType type, Action<Image, HttpContext> callback, Action<HttpContext> failCallback)
		{
			var url = "";
			var referUrl = "";

			switch (type)
			{
				case RandCodeType.SjRand:
					if (Session.HttpConf.IsUamLogin)
					{
						CreateVerifyCodeTask64(type, callback, failCallback);
						return;
					}
					url = string.Format("passcodeNew/getPassCodeNew?module=login&rand=sjrand&{0}", _random.NextDouble());
					referUrl = "leftTicket/init";
					break;
				case RandCodeType.Randp:
					url = $"passcodeNew/getPassCodeNew?module=passenger&rand=randp&{_random.NextDouble()}";
					referUrl = "confirmPassenger/initDc";
					break;
				case RandCodeType.RandpResign:
					url = $"passcodeNew/getPassCodeNew?module=passenger&rand=randp&{_random.NextDouble()}";
					referUrl = "confirmPassenger/initGc";
					break;
				default:
					break;
			}

			var netctx = Create<Image>(HttpMethod.Get, url, referUrl);
			if (ApiConfiguration.Instance.EnableVerifycodeLoadDelay && Session.LastCheckRandCodeTime.HasValue)
			{
				var timespan = DateTime.Now - Session.LastCheckRandCodeTime.Value;
				if (timespan.TotalMilliseconds < NetworkConfiguration.Current.ReloadVcCodeDelay)
					netctx.SendDelay(new TimeSpan(0, 0, 0, 0, (int)(NetworkConfiguration.Current.ReloadVcCodeDelay - timespan.TotalMilliseconds)));
			}
			netctx.SendAsPromise().Done((x, y) =>
			{
				var ctx = y.Result;
				//刷新过快？
				if ((ctx.ResponseContent as ResponseBinaryContent).Result.Length == 1492)
				{
					failCallback(y.Result);
				}
				else
				{
					LastVcLoadTime = DateTime.Now;
					ctx.Result.Tag = (ctx.ResponseContent as ResponseImageContent).ResultStream;
					callback(ctx.Result, ctx);
				}
			}).Fail((x, y) =>
			{
				failCallback(y.Result);
			});
		}

		/// <summary>
		/// 获得用户的联系人列表
		/// </summary>
		/// <returns></returns>
		public Entity.Web.PassengerList GetPassengers(out string msg)
		{
			msg = null;

			var task = RunRequestLoop(_ => Create<string>(HttpMethod.Post, "confirmPassenger/getPassengerDTOs", "leftTicket/init"), null);
			if (task == null || !task.IsSuccess) return new Entity.Web.PassengerList();

			Session.IsPassengerLoaded = true;

			//HACK: 对没有乘客性别的联系人强制为成人
			var data = task.Result.Replace("passenger_type\":\"\"", "passenger_type\":\"1\"");

			try
			{
				var anonymousType = JsonConvert.DeserializeAnonymousType(data, new
				{
					data = new { normal_passengers = new PassengerList(), exMsg = "" }
				});
				if (anonymousType?.data?.normal_passengers == null)
				{
					msg = anonymousType?.data?.exMsg ?? "未知错误";
					return null;
				}

				return anonymousType.data.normal_passengers;
			}
			catch (Exception e)
			{
				msg = $"数据错误：{e.Message}";

				return null;
			}
		}

		/// <summary>
		/// 获得用于修改邮箱和手机号的验证码
		/// </summary>
		/// <returns></returns>
		public async Task<string> GetMobileCode4EmailPwdAsync()
		{
			var ctx = await RunRequestLoopAsync(_ => Create(HttpMethod.Post, "userSecurity/getMobileCode4pwdemail", "userSecurity/loginPwd", new { type = 1 }, new { data = new { errorMsg = "" } }), null).ConfigureAwait(true);

			if (!ctx.IsValid())
			{
				return "获得手机验证码失败";
			}

			if (ctx.Result.data == null || ctx.Result.data?.errorMsg.IsNullOrEmpty() != true)
			{
				return ctx.Result.data?.errorMsg ?? "未知错误";
			}

			return null;
		}

		/// <summary>
		/// 注销登录
		/// </summary>
		public void Logout()
		{
			Create<string>(HttpMethod.Get, "login/loginOut", "login/init").Send();
		}

		#endregion

		#region 请求操作

		/// <summary>
		/// 动态JS数据
		/// </summary>
		public DynamicJsData DynamicJsData { get; set; }

		public DateTime LastVcLoadTime
		{
			get
			{
				return _lastVcLoadTime;
			}

			set
			{
				_lastVcLoadTime = value;
			}
		}

		private Task<bool> _resetOtnPortTask = null;

		bool IsUnexpectedLogout(HttpContext context)
		{
			var response = context.Response;
			var cookies = response.Cookies;
			var data = context.ContextData;

			if (data?.ContainsKey("no_otn_portcheck") == true)
				return false;

			//变更 2018年8月13日：取消 Cookies 座位掉线标记，因为貌似不掉线这个也有可能出现
			//if (cookies != null && _currentOtnPort != null && cookies.Cast<Cookie>().Any(s => s.Name == "BIGipServerotn" || s.Name == "__NRF"))
			//	return true;

			if (response.Redirection != null)
			{
				var newLocation = response.Redirection.Current.ToString();

				if (Regex.IsMatch(newLocation, @"/otn/(passport\?redirect=|login/init)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
				{
					return true;
				}
			}

			return false;
		}

		private void Context_ValidateResponseHeader(object sender, EventArgs e)
		{
			var ctx = (HttpContext)sender;
			var response = ctx.Response;

			if (string.Compare(response.ResponseUri.Host, "kyfw.12306.cn", StringComparison.OrdinalIgnoreCase) == -1)
				return;

			var redirection = response.Redirection;
			if (redirection != null && Regex.IsMatch(redirection.Current.ToString(), @"(mormhweb/logFiles|otn/login/error)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				//系统错误
				throw new SystemBusyException();
			}

			//如果返回了OTN信息，则重置OTN避免掉线
			if (IsUnexpectedLogout(ctx))
			{
				if (ApiConfiguration.Instance.EnableLogoutRescure)
				{
					Task<bool> task;
					lock (_lockObject)
					{
						task = _resetOtnPortTask;
						if (task == null)
						{
							Debug.WriteLine("OTN重定向已检测到。启动检测任务。");

							_resetOtnPortTask = Task<bool>.Factory.StartNew(() =>
							{
								//先校验原始信息, 最多三次, 只要成功一次即可
								for (int i = 0; i < 3; i++)
								{
									var ret = VerifySessionValid(noOtnPortCheck: true);
									if (ret == true)
									{
										return false;
									}
								}

								if (ctx.ContextData?.ContainsKey("not_important") == true)
								{
									if (_currentOtnPort != null)
									{
										ResetOtnPort();
										return true;
									}
								}
								else
								{
									var count = 0;
									var errorCount = 0;
									ExportedSession otnSession;
									while (count++ < 5 && (otnSession = _currentOtnPort) != null)
									{
										var client = new NetClient();
										otnSession.ToSession(null, client);
										var ret = client.VerifySessionValid(noOtnPortCheck: true);
										if (ret != null)
										{
											if (ret == true)
											{
												_currentOtnPort = new ExportedSession(Session, this);
												ResetOtnPort();
												return true;
											}
											else
											{
												if (errorCount++ >= 3)
												{
													break;
												}
											}
											count--;
										}
										Thread.Sleep(100);
									}
								}

								return false;
							});
							task = _resetOtnPortTask;
						}
					}

					task.Wait();
					_resetOtnPortTask = null;
					if (task.Result)
					{
						Debug.WriteLine("已挽救OTNPORT");
						throw new Exception("掉线重定向已拦截，请重试");
					}
				}

				throw new ForceLogoutException();
			}
		}


		/// <summary>
		/// 检测请求并抛出异常
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void CheckResponse(object sender, EventArgs e)
		{
			var ctx = (HttpContext)sender;
			var response = ctx.Response;
			var content = ctx.ResponseContent;
			var request = ctx.Request;
			var cookies = response.Cookies;
			var redirection = response.Redirection;

			//IP被封？
			if (string.Compare(response.ResponseUri.Host, "kyfw.12306.cn", StringComparison.OrdinalIgnoreCase) == -1)
				return;

			if (
				response.ContentType?.IndexOf("application/json") >= 0 &&
				content is ResponseBinaryContent rbc &&
				rbc.StringResult.IndexOf("\":[\"网络繁忙\"]", StringComparison.OrdinalIgnoreCase) != -1
			)
				throw new SystemBusyException();

			if (content is ResponseBinaryContent && redirection == null)
			{
				var allowEmptyResult = false;
				allowEmptyResult |= response.ResponseUri.PathAndQuery.IndexOf("login/userLogin") != -1;
				allowEmptyResult |= response.ResponseUri.PathAndQuery.IndexOf("dynamicJs/") != -1;

				if (!allowEmptyResult && response.ContentLength == 0)
				{
					throw new EmptyResponseException();
				}
			}


			//更新服务器信息
			if ((response.Status == HttpStatusCode.Found || response.Status == HttpStatusCode.OK) && ((response.Headers["X-Cache"].IsNullOrEmpty() || response.Headers["X-Cache"].IndexOf("MISS") != -1) && response.Headers["Age"].IsNullOrEmpty() && !response.Headers["Date"].IsNullOrEmpty()))
			{
				var dt = response.Headers["Date"].ToDateTimeNullable();
				if (dt.HasValue)
					RunTime.UpdateServerTimeOffset(dt.Value);
			}

			//系统维护？
			//if (content is ResponseStringContent && ((!ctx.Request.Host.IsNullOrEmpty() && ctx.Request.Host.IndexOf("12306.cn") != -1) || ctx.Request.Uri.Host.IndexOf("12306.cn") != -1) && ((content as ResponseStringContent).Result.IndexOf("系统维护") != -1))
			//{
			//	throw new Entity.SystemClosedException();
			//}

			////登录冲突
			//if ((content as ResponseStringContent)?.Result.IndexOf("noticeSessionCollect = 'Y'") > 0)
			//{
			//	Session.OnLoginConflict(Session);
			//	throw new EmptyResponseException();
			//}

			if (ApiConfiguration.Instance.EnableDynamicJs && content is ResponseStringContent && (ctx.IsSuccess && response.ContentType.IndexOf("text/html") != -1))
			{
				//检测dynamicjs
				var m = Regex.Match((content as ResponseStringContent).Result, @"/otn/(dynamicJs\/(.+?))['""]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				if (m.Success)
				{
					var id = m.GetGroupValue(2);
					var url = m.GetGroupValue(1);
					var data = new DynamicJsData()
					{
						Id = id,
						SourceUrl = url
					};

					var jsCtx = Create<string>(HttpMethod.Get, url, request.Uri.ToString()).Send();
					if (jsCtx.IsValid())
					{
						var jscontent = jsCtx.Result;
						var mcs = Regex.Matches(jscontent, @"/otn/(dynamicJs\/(.+?))['""]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
						var km = Regex.Match(jscontent, @"gc\(\)\s*{.*?['""]([^""']+)['""]", RegexOptions.Singleline);
						if (mcs.Count >= 1 && km.Success)
						{
							data.PingUrl = mcs[0].GetGroupValue(1);
							data.NeedImmediatePingback = mcs.Count > 1;
							data.Key = km.GetGroupValue(1);
							data.EncodedValue = Base32.Encode(data.Key);
							data.ValidateData = $"{data.Key},-,{Base32.Encode(data.Key)}:::myversion,-,undefined";

							if (data.NeedImmediatePingback)
							{
								Create<string>(HttpMethod.Post, data.PingUrl, request.Uri.ToString()).Send();
							}

							DynamicJsData = data;
						}
					}

				}
			}

			AppContext.ExtensionManager.WebRequestInspectors?.ForEach(s => s.ValidateResponse(ctx, request, response, request.RequestData, content, cookies));
		}

		/// <summary>
		/// 启用跟踪
		/// </summary>
		public static void EnableTrace()
		{
			GlobalEvents.RequestEnd += (s, e) =>
			{
				if (e?.Response?.Content is ResponseBinaryContent)
				{
					Events.OnMessage(null, new EventInfoArgs((e.Response.Content as ResponseBinaryContent).StringResult));
				}
			};
		}

		bool IsKnownErrors(HttpContext context)
		{
			var ex = context.Exception;
			if (ex != null)
			{
				var extype = ex.GetType();

				if (extype == typeof(ForceLogoutException))
					return true;
			}

			return false;
		}

		/// <summary>
		/// 保证执行成功
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		/// <param name="retryIndicate"></param>
		/// <returns></returns>
		public HttpContext<T> RunRequestLoop<T>(Func<int, HttpContext<T>> action, Action<int> retryIndicate = null, int? retryCount = null) where T : class
		{
			var nc = NetworkConfiguration.Current;

			var retry = 0;
			var max = retryCount ?? (nc.AutoRetryOnNetworkError ? nc.RetryMaxCount + 1 : 1);
			var sleep = nc.RetrySleepTime;

			while (retry < max)
			{
				retryIndicate?.Invoke(retry);

				var ctx = action(retry);
				ctx.Send();

				if (ctx.IsValid() || IsKnownErrors(ctx) || ++retry >= max)
				{
					return ctx;
				}

				//如果是已知的错误类型，则提前返回


				Thread.Sleep(sleep);
			}

			throw new Exception("看起来不会到这里啊！");
		}

		public async Task<HttpContext<T>> RunRequestVieAsync<T>(Func<int, HttpContext<T>> action, int count = 3)
			where T : class
		{
			var list = new List<HttpContext<T>>(count);
			var sleepTime = 0;
			for (int i = 0; i < count; i++)
			{
				var ctx = action(i);
				if (sleepTime > 0)
					ctx.SendDelay(new TimeSpan(sleepTime * 10000));
				sleepTime += 1000;
			}

			var tasks = list.Select(s => Task.Factory.StartNew(() =>
			{
				s.Send();
				return s;
			})).ToArray();

			var result = await Task.Factory.StartNew(() =>
			  {
				  var idx = Task.WaitAny(tasks);
				  return tasks[idx].Result;
			  }).ConfigureAwait(true);

			return result;
		}

		/// <summary>
		/// 保证执行成功
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		/// <param name="retryIndicate"></param>
		/// <returns></returns>
		public async Task<HttpContext<T>> RunRequestLoopAsync<T>(Func<int, HttpContext<T>> action, Action<int> retryIndicate = null) where T : class
		{
			var nc = NetworkConfiguration.Current;

			var retry = 0;
			var max = nc.AutoRetryOnNetworkError ? ApiConfiguration.Instance.NetworkMaxRetry + 1 : 1;
			var sleep = nc.RetrySleepTime;

			Trace.TraceInformation($"[001] {Thread.CurrentThread.ManagedThreadId}");

			while (retry < max)
			{
				retryIndicate?.Invoke(retry);

				var ctx = action(retry);
				await ctx.SendAsync().ConfigureAwait(true);

				if (ctx.IsValid() || IsKnownErrors(ctx) || ++retry >= max)
				{
					return ctx;
				}
				await Task.Delay(sleep).ConfigureAwait(true);
			}

			throw new Exception("看起来不会到这里啊！");
		}

		#endregion
	}
}
