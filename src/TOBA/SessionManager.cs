using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TOBA.Entity;

namespace TOBA
{
	using Configuration;

	using Data;

	using System.Threading.Tasks;

	internal class SessionManager : List<Session>
	{
		static string _sessionFilePath;

		static SessionManager()
		{
			_sessionFilePath = Path.Combine(Profile.Root.Path, "sessions.json");
			Session.Logout += (s, e) =>
			{
				var sessManager = RunTime.SessionManager;
				var sess = s as Session;
				sess?.Unload();
				if (sessManager.Contains(sess))
				{
					sessManager.Remove(sess);
					sessManager.SaveSessionsToFile();
				}
			};
			Session.UserLogined += (s, e) =>
			{
				var sessManager = RunTime.SessionManager;
				var sess = s as Session;
				if (!sessManager.Contains(sess))
				{
					sessManager.Add(sess);
					sessManager.SaveSessionsToFile();
				}
				if (!sess.UserProfile.IsPassengerLoaded)
					sess.AccquireLoadPassengers();
			};
		}

		private void AppContext_MainProgramLoaded(object sender, EventArgs e)
		{
			if (_waitLogin?.Count > 0 && ParamData.IsSysOpen)
			{
				foreach (var session in _waitLogin)
				{
					AppContext.MainForm.Login(session.UserName);
				}
			}

			_waitLogin = null;
		}

		public SessionManager()
		{
			System.Threading.ThreadPool.QueueUserWorkItem(_ => KeepOnline());
			AppContext.MainProgramLoaded += AppContext_MainProgramLoaded;
		}

		int _keepOnlineInex;

		private static double KeepAliveInterval => NetworkConfiguration.Current.CheckLoginStateInterval;

		/// <summary>
		/// 保持用户在线
		/// </summary>
		void KeepOnline()
		{
			while (true)
			{
				Session session;
				lock (this)
				{
					if (_keepOnlineInex >= Count) _keepOnlineInex = 0;
					session = _keepOnlineInex >= Count ? null : this[_keepOnlineInex++];
				}

				if (session != null && (session.LastHeartBeatTime == null || (DateTime.Now - session.LastHeartBeatTime.Value).TotalMinutes > KeepAliveInterval))
				{
					session.LastHeartBeatTime = DateTime.Now;
					if (session.NetClient.VerifySessionValid() == false)
					{
						var sess = session;
						AppContext.DispatchEvents(() => ForceLogoutNotify(sess));
					}
				}
				System.Threading.Thread.Sleep(10 * 1000);
			}
		}

		async Task ForceLogoutNotify(Session sess)
		{
			await sess.BeenForceLogout().ConfigureAwait(true);
		}

		/// <summary>
		/// 获得或设置指定的用户名是否已经登录
		/// </summary>
		public bool IsLogined(string name, params Session[] exceptSessions)
		{
			return Find(s => s.UserName == name && exceptSessions?.Contains(s) != true) != null;
		}

		/// <summary>
		/// 卸载所有会话
		/// </summary>
		public void UnloadAllSessions()
		{
			this.ForEach(s => s.Unload());
		}

		/// <summary>
		/// 保存会话信息到文件
		/// </summary>
		void SaveSessionsToFile()
		{
			var sessinfo = this.Where(s => !s.TemporaryMode).Select(s => new ExportedSession(s)).Where(s => s != null).ToArray();
			sessinfo.SaveToFile(_sessionFilePath);
		}

		List<ExportedSession> _waitLogin = new List<ExportedSession>();

		/// <summary>
		/// 尝试恢复会话
		/// </summary>
		public void TryRecoverSession(Action<string> progressAction)
		{
			if (!File.Exists(_sessionFilePath)) return;

			try
			{
				var buffer = File.ReadAllText(_sessionFilePath);

				var list = Newtonsoft.Json.JsonConvert.DeserializeObject<ExportedSession[]>(buffer);
				var valid = new List<ExportedSession>();
				var client = new WebLib.NetClient();
				foreach (var item in list)
				{
					if (IsLogined(item.UserName)) continue;
					if (progressAction != null)
						progressAction(item.UserName);

					item.ToSession(null, client);
					if (client.VerifySessionValid() == true)
					{
						var sess = new Session(item.UserName, false, this.Any(s => s.UserName == item.UserName));
						item.ToSession(sess);
						Session.OnUserLogined(sess);
						valid.Add(item);
					}
					else if (ProgramConfiguration.Instance.KeepLoginStateAfterRestart)
					{
						_waitLogin.Add(item);
					}
				}

				SaveSessionsToFile();
			}
			catch (Exception ex)
			{
				Events.OnWarning(this, new EventInfoArgs()
				{
					Message = "无法从本地存储中恢复用户会话，可能记录文件已经损坏"
				});
			}
		}
	}
}
