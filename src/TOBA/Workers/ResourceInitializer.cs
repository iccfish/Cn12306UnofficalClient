using System;

namespace TOBA.Workers
{
	using Configuration;

	using Data;

	using System.Diagnostics;

	using UI.Dialogs.Misc;

	internal class ResourceInitializer
	{
		private readonly IStartup _startup;

		/// <summary>
		/// 获得是否初始化完成
		/// </summary>
		public bool Succeed { get; protected set; }

		/// <summary>
		/// 创建 <see cref="ResourceInitializer" />  的新实例(ResourceInitializer)
		/// </summary>
		public ResourceInitializer(IStartup startup)
		{
			_startup = startup;
			Succeed = false;
		}

		public virtual void Run()
		{
			_startup.Update("正在初始化资源版本....");
			//检测版本
			Trace.TraceInformation("正在获得网站版本");

			var version = WebLib.NetworkTaskManager.GetResourceVersion(out var retCode, out var err);
			if (string.IsNullOrEmpty(version))
			{
				throw new ApplicationException(retCode == -2 ? "您的网络IP可能被暂时封禁，请稍后重试或使用代理服务器" : "初始化网站版本信息时发生错误: " + err);
			}

			var forceUpdate = version != Configuration.ProgramConfiguration.Instance.LastCachedResourceVersion;

			if (forceUpdate || !RemoteResourceManager.LoadTrainStationListFromCache())
			{
				_startup.Update("正在更新车站信息...");

				var text = WebLib.NetworkTaskManager.GetCityNameJsContent(out var errMsg);
				if (string.IsNullOrEmpty(text))
				{
					throw new ApplicationException($"初始化网站版本信息时发生错误: {errMsg}");
				}

				try
				{
					ParamData.TrainStationList = RemoteResourceManager.ParseCityName(text);
				}
				catch (Exception ex)
				{
					throw new ApplicationException("初始化网站版本信息时发生错误: " + ex.Message, ex);
				}

				//缓存资源
				RemoteResourceManager.SaveTrainStationList();
			}

			if (forceUpdate || !RemoteResourceManager.LoadSellTimeFromCache())
			{
				_startup.Update("正在更新起售信息...");

				try
				{
					ParamData.SellTimeMap = WebLib.NetworkTaskManager.GetSellTimeMap();
					if (ParamData.SellTimeMap == null)
						throw new ApplicationException("获得了空的起售时间");
				}
				catch (Exception ex)
				{
					throw new ApplicationException("初始化起售信息时发生错误", ex);
				}

				//缓存资源
				RemoteResourceManager.SaveSellTimeMap();
			}

			ProgramConfiguration.Instance.LastCachedResourceVersion = version;
		}
	}
}
