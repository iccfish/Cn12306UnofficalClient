using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform.HttpConf
{
	using System;
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	using Otn.Entity;

	using WebLib;

	class Web12306ConfProvider : IWeb12306ConfProvider
	{
		private IWeb12306Conf _current;

		/// <summary>
		/// 获得当前配置
		/// </summary>
		public IWeb12306Conf Current
		{
			get => _current;
			private set
			{
				if (value == _current)
					return;

				_current = value;
				OnCurrentConfUpdated();
			}
		}

		private NetClient _client;

		public Web12306ConfProvider()
		{
			_client = new NetClient();
		}


		/// <summary>
		/// 刷新当前配置
		/// </summary>
		/// <returns></returns>
		public async Task<(bool success, string message)> RefreshAsync()
		{

			var (ok, msg, conf) = await UpdateAsync();
			if (ok)
				Current = conf;

			return (ok, msg);
		}


		/// <summary>
		/// 刷新当前配置
		/// </summary>
		/// <returns></returns>
		public async Task<(bool success, string message, HttpConf conf)> UpdateAsync()
		{
			const string uri = "/index/otn/login/conf";

			var ctx = await _client.RunRequestLoopAsync(_ => _client.Create<OtnWebResponse<HttpConf>>(HttpMethod.Post, uri));
			var ret = ctx.Result;

			return (ctx.IsValid(), ctx.GetErrorMsg(), ret?.Data);
		}

		/// <summary>
		/// 当前配置已更新
		/// </summary>
		public event EventHandler CurrentConfUpdated;

		protected virtual void OnCurrentConfUpdated() { CurrentConfUpdated?.Invoke(this, EventArgs.Empty); }
	}
}
