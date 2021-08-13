using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform.SlideVc
{
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	internal interface ISlideVcService
	{
		Task<(bool? needSlide, string token)> GetSlideToken(string userName, bool needSlide);
	}

	class SlideVcService : ISlideVcService
	{
		public SlideVcService(Session session) { Session = session; }


		public Session Session { get; }

		public async Task<(bool? needSlide, string token)> GetSlideToken(string userName, bool needSlide)
		{
			var client = Session.NetClient;

			var ctx = await client.RunRequestLoopAsync(i =>
				client.Create(
					HttpMethod.Post,
					"/passport/web/slide-passcode",
					"/otn/resources/login.html",
					new { username = userName, appid = "otn", slideMode = needSlide ? 1 : 0 },
					new
					{
						if_check_slide_passcode_token = "FFFF0N000000000085DE:1596709200098:0.032837196587888861",
						result_code = 0,
						result_message = ""
					}));
			if (!ctx.IsValid())
				return (null, null);

			return (ctx.Result.result_code != 1, ctx.Result.if_check_slide_passcode_token);

		}
	}
}
