using System;

using TOBA.Workers;

namespace TOBA
{
	using Configuration;

	internal class RunTime
	{
		static RunTime()
		{
			SessionManager = new SessionManager();
		}

		public static bool IsInProfessonalMode => ProgramConfiguration.Instance.Mode == RunningMode.Professional;

		/// <summary>
		/// 获得服务器时间和本地时间差异
		/// </summary>
		public static TimeSpan? ServerTimeOffset
		{
			get;
			private set;
		}

		/// <summary>
		/// 获得当前服务器时间
		/// </summary>
		public static DateTime ServerTime
		{
			get => ServerTimeOffset == null || ServerTimeOffset.Value.TotalMinutes >= 1 ? DateTime.Now : DateTime.Now.Add(ServerTimeOffset.Value);
		}

		/// <summary>
		/// 更新服务器时间和本地时间差异
		/// </summary>
		/// <param name="time"></param>
		public static void UpdateServerTimeOffset(DateTime time)
		{
			ServerTimeOffset = time - DateTime.Now;
		}

		/// <summary>
		/// 获得会话列表
		/// </summary>
		public static SessionManager SessionManager { get; private set; }


		public static void AccquireLoadPassengers(Session session)
		{
			TaskManager.Instance.EnqueueTask("加载【" + session.UserKeyData.DisplayName + "】的联系人", session.LoadPassengers);
		}


		public static bool PreProcessWebMessage(WebLib.NetClient client, FSLib.Network.Http.HttpContext context)
		{
			if (context.IsSuccess)
			{
			}
			else
			{
				if (context.Exception != null)
				{
					if (context.Exception is Entity.SystemBusyException)
					{
					}
					if (context.Exception is Entity.SystemClosedException)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}
