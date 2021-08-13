namespace TOBA.Platform.DeviceFingerprint
{
	using Autofac;

	using Configuration;

	using Extension;

	using FSLib.Network.Http;

	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using WebLib;

	class FingerprintExtension : AbstractExtension, IExtension, IRequestInspector
	{

		/// <summary>
		/// 插件ID
		/// </summary>
		public override string Id { get; } = "ifish.mock.device.fingerprint";

		/// <summary>
		/// 名称
		/// </summary>
		public override string Name { get; } = "DeviceFingerprint Mock Service";

		/// <summary>
		/// 连接插件
		/// </summary>
		public override void Connect()
		{
			var service = AppContext.ExtensionManager.GlobalKernel.Resolve<IFingerprintService>();
			service.Initialize();
		}

		/// <summary>
		/// 请求对一个Session进行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void SessionInit(Session session)
		{
			base.SessionInit(session);

			var context = new HostContext(session);
			context.ClearFinterprintInfo();
		}

		/// <summary>
		/// 校验请求
		/// </summary>
		/// <param name="context"></param>
		public async void ValidateResponse(HttpContext context, HttpRequestMessage request, HttpResponseMessage response, HttpRequestContent requestContent, HttpResponseContent responseContent, CookieCollection cookies)
		{
			if (!ApiConfiguration.Instance.EnableHttpZf)
				return;

			var session = (context.Client as NetClient).Session;
			if (session == null)
				return;

			if (!(responseContent is ResponseStringContent) || !context.IsSuccess || response.ContentType.IndexOf("text/html") == -1)
			{
				return;
			}

			//检测dynamicjs
			var service = AppContext.ExtensionManager.GlobalKernel.Resolve<IFingerprintService>();
			if (!service.IsInitialized)
				return;

			var m = Regex.Match((responseContent as ResponseStringContent).Result, @"(/otn/HttpZF/[^""]*?)['""]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			if (!m.Success)
			{
				return;
			}

			var jsUrl = m.GetGroupValue(1);
			var host = new HostContext(session)
			{
				ResourceUrl = jsUrl
			};
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
			Task.Factory.StartNew(() =>
			{
				if (ProcessFp(host))
					return;

				lock (_processingSessions)
				{
					if (_processingSessions.Contains(session))
						return;

					_processingSessions.Add(session);
				}

				do
				{
					if (!session.IsLogined)
						return;
					Thread.Sleep(5000);
				} while (!ProcessFp(host));

				lock (_processingSessions)
				{
					_processingSessions.Remove(session);
				}
			});
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
		}

		private HashSet<Session> _processingSessions = new HashSet<Session>();

		bool ProcessFp(HostContext host)
		{
			var service = AppContext.ExtensionManager.GlobalKernel.Resolve<IFingerprintService>();
			if (!service.IsInitialized)
				return false;

			var fp = host.FingerprintInfo;
			if (fp != null && fp.Expire > DateTime.Now)
				return true;

			//CreateFpInfo
			return CreateFpInfo(host, service) || CreateFpInfo(host, service, Properties.Resources.httpzf);
		}

		private bool CreateFpInfo(HostContext host, IFingerprintService service, string jsContent = null)
		{
			var (success, msg) = service.InitTagStatus(host, jsContent);

			if (!success)
			{
				Events.OnWarning(this, new EventInfoArgs(msg));
				return false;
			}

			var (succd, msg1) = service.BuildDeviceFingerprint(host);
			if (!succd)
			{
				Events.OnWarning(this, new EventInfoArgs(msg1));
				return false;
			}

			if (host.FingerprintInfo == null)
				return false;

			//set cookie
			host.FingerprintInfo.SetToNetClient(host.Session.NetClient);
			var (succd1, msg2) = service.PostFpInfo(host);
			if (!succd1)
			{
				Events.OnWarning(this, new EventInfoArgs(msg2));
				return false;
			}

			host.SetFingerprintInfoToSession();
			return true;
		}
	}
}
