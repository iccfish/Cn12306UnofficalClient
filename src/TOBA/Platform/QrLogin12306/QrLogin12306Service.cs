namespace TOBA.Platform.QrLogin12306
{
	using Account;

	using Autofac;

	using DeviceFingerprint;

	using FSLib.Network.Http;

	using System;
	using System.Drawing;
	using System.IO;
	using System.Threading.Tasks;

	class QrLogin12306Service : IQrLogin12306Service
	{
		public async Task<Session> InitQrLoginAsync()
		{
			var session = new Session("$tmp$", true, false);
			var loginService = AppContext.ExtensionManager.GlobalKernel.Resolve<ISessionLoginService>();
			loginService.Session = session;
			var ok = await loginService.PrepareLoginAsync();

			if (ok == false)
				return null;

			return session;
		}

		public async Task<(int code, string message, Image qrImg, string uuid)> CreateQrImageAsync(Session session)
		{
			var url = "/passport/web/create-qr64";
			var refer = "/otn/resources/login.html";
			var nc = session.NetClient;

			var qrCtx = await nc.RunRequestLoopAsync(_ => nc.Create(HttpMethod.Post, url, refer, new { appid = "otn" }, new { image = "", result_code = 0, result_message = "", uuid = "" }));

			if (qrCtx.IsValid())
			{
				return (
					qrCtx.Result.result_code,
					qrCtx.Result.result_message,
					qrCtx.Result.result_code == 0 ? Image.FromStream(new MemoryStream(Convert.FromBase64String(qrCtx.Result.image))) : null,
					qrCtx.Result.uuid
				);
			}

			return (-1, qrCtx.GetExceptionMessage("网络错误"), null, null);
		}

		public async Task<(LoginQrState? state, string message, string uamtk)> CheckLoginStateAsync(Session session, string uuid)
		{
			var url = "/passport/web/checkqr";
			var refer = "/otn/resources/login.html";
			var nc = session.NetClient;
			var df = session.GetService<IFingerprintService>().GetFingerprintInfoFromSession(session);

			var data = new { appid = "otn", uuid, RAIL_DEVICEID = df?.Dfp, RAIL_EXPIRATION = df?.Expiration };
			var qrCtx = await nc.RunRequestLoopAsync(_ => nc.Create(HttpMethod.Post, url, refer, data, new { result_code = LoginQrState.Valid, result_message = "", uamtk = "" }));

			if (qrCtx.IsValid())
			{
				return (
					qrCtx.Result.result_code,
					qrCtx.Result.result_message,
					qrCtx.Result.uamtk
				);
			}

			return (LoginQrState.SystemError, qrCtx.GetExceptionMessage("网络错误"), null);
		}
	}
}