using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TOBA.Configuration;
using Timer = System.Timers.Timer;

namespace TOBA.QueryResumeManager
{
	internal class Controller
	{
		#region 单例模式

		static Controller _instance;
		static readonly object _lockObject = new object();

		public static Controller Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new Controller();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		ConcurrentDictionary<ISession, ControllerSession> _sessions = new ConcurrentDictionary<ISession, ControllerSession>();
		Timer _timer;

		private Controller()
		{
			Session.Logout += Session_Logout;

			_timer = new Timer();
			_timer.AutoReset = true;
			_timer.Interval = 1000;
			_timer.Elapsed += (s, e) => CheckSubmitTimerLoop();
			_timer.Start();
		}

		void CheckSubmitTimerLoop()
		{
			if (!AutoResumeRefreshConfiguration.Instance.LimitSubmitTimeNoPerformTime)
				return;

			lock (_lockObject)
			{
				_sessions.Values.ToArray().ForEach(s => s.CheckTimeoutSubmit());
			}
		}

		void Session_Logout(object sender, EventArgs e)
		{
			var session = sender as Session;
			lock (_lockObject)
			{
				ControllerSession ctlSession;
				if (_sessions.TryRemove(session, out ctlSession) && ctlSession != null)
				{
					//cleanup
				}
			}
		}

		public ControllerSession this[ISession session]
		{
			get
			{
				lock (_lockObject)
				{
					ControllerSession ctlSession;
					if (!_sessions.TryGetValue(session, out ctlSession))
					{
						ctlSession = new ControllerSession((Session)session);
						_sessions.TryAdd(session, ctlSession);
					}
					return ctlSession;
				}
			}
		}

	}
}
