using System;
using System.Collections.Generic;
using System.Linq;

using TOBA.Configuration;
using TOBA.Entity;
using TOBA.Otn.Workers;
using TOBA.Profile;
using TOBA.WebLib;
using TOBA.Workers;

namespace TOBA
{
	using Account;
	using Account.Entities;

	using Autofac;

	using BackupOrder;

	using FSLib.Extension;

	using Order;

	using Platform;
	using Platform.HttpConf;

	using Query.Entity;

	using System.ComponentModel;
	using System.Threading.Tasks;

	/// <summary>
	/// 上下文操作环境
	/// </summary>
	internal class Session : ISession, INotifyPropertyChanged, IDisposable
	{
		static readonly object _lockObject = new object();

		bool _inloading = false;

		private bool? _isMobileChecked;

		private bool _isPassengerLoaded;
		bool? _isUserVerified;
		string _lastVerifyCode;

		private string _loginNotification;
		NetClient _netClient;
		string _password;
		private readonly ILifetimeScope _serviceKernel;
		bool _temporaryMode;
		UserKeyData _userKeyData;
		string _userName;
		UserProfile _userProfile;
		VerifyCodeCheckWorker _vccWorker;

		public Session(string userName, bool tempMode, bool shadowMode = false, NetClient netClient = null)
		{
			//_vccWorker = new VerifyCodeCheckWorker() { Session = this };
			//_vccWorker.StartAliveCheck();
			UserName = userName;
			TemporaryMode = tempMode;
			ShadowMode = shadowMode;
			NetClient = netClient;

			UserKeyData = UserKeyDataMap.Current[userName];
			if (UserKeyData == null)
			{
				UserKeyData = new UserKeyData();
				if (!tempMode)
				{
					UserKeyDataMap.Current[userName] = UserKeyData;
				}
			}
			UserProfile = new UserProfile(UserName, TemporaryMode, shadowMode);
			SessionData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

			//候补订单
			BackupOrderCart = new BackupCart(this);

			_serviceKernel = AppContext.ExtensionManager.GlobalKernel.BeginLifetimeScope(c =>
			{
				c.RegisterInstance(this).AsImplementedInterfaces().AsSelf();
				c.RegisterInstance(NetClient).AsImplementedInterfaces().AsSelf();
				c.RegisterInstance(BackupOrderCart).AsImplementedInterfaces().AsSelf();
				c.RegisterInstance(UserProfile).AsImplementedInterfaces().AsSelf();
			});
		}

		/// <summary>
		/// 用户被强制退出登录
		/// </summary>
		public static event EventHandler<GeneralEventArgs<bool>> ForceLogout;

		/// <summary>
		/// 用户被注销
		/// </summary>
		public static event EventHandler Logout;

		/// <summary>
		/// 手机核验状态发生变化
		/// </summary>
		public static event EventHandler MobileCheckStateChanged;

		/// <summary>
		/// 订单发生变化
		/// </summary>
		public event EventHandler OrderChanged;

		/// <summary>
		/// 退票成功
		/// </summary>
		public static event EventHandler<OrderRefundEventArgs> OrderRefundSuccess;

		/// <summary>
		/// 订单提交失败
		/// </summary>
		public static event EventHandler<OrderSubmitEventArgs> OrderSubmitFailed;


		/// <summary>
		/// 订单成功提交
		/// </summary>
		public static event EventHandler<OrderSubmitEventArgs> OrderSubmitSuccess;


		/// <summary>
		/// 联系人加载完成
		/// </summary>
		public event EventHandler PassengerLoadComplete;

		public static event EventHandler PreInputedVcMissed;
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// 当用户请求显示特定面板的时候触发
		/// </summary>
		public event EventHandler<GeneralEventArgs<UI.PanelIndex>> RequestShowPanel;

		/// <summary>
		/// 当有用户登录时引发此事件
		/// </summary>
		public static event EventHandler UserLogined;

		public static event EventHandler UserVerificationStateChanged;

		private static void OnMobileCheckStateChanged(object sender)
		{
			MobileCheckStateChanged?.Invoke(sender, EventArgs.Empty);
		}

		#region ISession 成员


		INetClient ISession.NetClient
		{
			get { return NetClient; }
		}

		#endregion


		/// <summary>
		/// 引发 <see cref="PassengerLoadComplete" /> 事件
		/// </summary>
		protected virtual void OnPassengerLoadComplete() => PassengerLoadComplete?.Invoke(this, EventArgs.Empty);

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// 请求加载联系人
		/// </summary>
		public void AccquireLoadPassengers()
		{
			if (!IsLogined)
				return;

			TaskManager.Instance.EnqueueTask("加载【" + UserKeyData.DisplayName + "】的联系人", LoadPassengers);
		}

		public T GetSessionData<T>(string key)
		{
			var obj = SessionData.GetValue(key);
			return obj == null ? default(T) : (T)obj;
		}

		public void RemoveSessionData(string key)
		{
			SessionData.TryRemove(key, out var tmp);
		}

		/// <summary>
		/// 加载用户的联系人
		/// </summary>
		public void LoadPassengers()
		{
			if (_inloading || !IsLogined) return;
			_inloading = true;

			try
			{
				var data = NetClient.GetPassengers(out _);
				if (data != null)
				{
					UserProfile.Passengers = data;
				}
			}
			finally
			{
				_inloading = false;
			}

			OnPassengerLoadComplete();
		}

		/// <summary>
		/// 加载用户的联系人
		/// </summary>
		public async Task LoadPassengersAsync()
		{
			if (_inloading || !IsLogined) return;

			await Task.Factory.StartNew(LoadPassengers).ConfigureAwait(true);
		}


		/// <summary>
		/// 引发 <see cref="ForceLogout" /> 事件
		/// </summary>
		public static void OnForceLogout(object sender, GeneralEventArgs<bool> ea)
		{
			var handler = ForceLogout;
			if (handler != null)
				handler(sender, ea);
		}

		///// <summary>
		///// 报告登录冲突
		///// </summary>
		///// <param name="sender"></param>
		//public static void OnLoginConflict(object sender)
		//{
		//	LoginConflict?.Invoke(sender, EventArgs.Empty);
		//}

		/// <summary>
		/// 引发 <see cref="Logout" /> 事件
		/// </summary>
		public static void OnLogout(object sender, LogoutReason reason)
		{
			var session = sender as Session;
			if (session?.IsLogined != true)
				return;

			session.IsLogined = false;
#pragma warning disable 4014
			session.NetClient.LogoutAsync();
#pragma warning restore 4014
			session.UserProfile.QueryParams.PersistentQueryState(reason != LogoutReason.UserManually);
			session.UserProfile.QueryParams.RequestStopAll();
			foreach (var queryParam in session.UserProfile.QueryParams.Where(s => s.Resign).ToArray())
			{
				queryParam.IsPersistent = false;
				queryParam.IsLoaded = false;
			}

			var handler = Logout;
			handler?.Invoke(sender, EventArgs.Empty);
		}

		public async Task<bool> BeenForceLogout()
		{
			var ea = new GeneralEventArgs<bool>(false);
			OnForceLogout(this, ea);
			if (!ea.Data)
			{
				var service = ServiceContainer.Resolve<ISessionReloginService>();
				ea.Data = await service.ReloginAsync().ConfigureAwait(true);
			}

			if (!ea.Data)
				OnLogout(this, LogoutReason.AccoutKicked);

			return ea.Data;
		}


		///// <summary>
		///// 引发 <see cref="MayNeedRelogin" /> 事件
		///// </summary>
		///// <param name="sender">引发此事件的源对象</param>
		//public static void OnMayNeedRelogin(object sender, GeneralEventArgs<bool> e)
		//{
		//	var handler = MayNeedRelogin;
		//	if (handler != null)
		//		handler(sender, e);
		//}

		/// <summary>
		/// 引发 <see cref="OrderChanged" /> 事件
		/// </summary>
		public void OnOrderChanged()
		{
			var handler = OrderChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="OrderRefundSuccess" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnOrderRefundSuccess(object sender, OrderRefundEventArgs ea)
		{
			var handler = OrderRefundSuccess;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 引发 <see cref="OrderSubmitFailed" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnOrderSubmitFailed(object sender, OrderSubmitEventArgs ea)
		{
			var handler = OrderSubmitFailed;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 引发 <see cref="OrderSubmitSuccess" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnOrderSubmitSuccess(object sender, OrderSubmitEventArgs ea)
		{
			var handler = OrderSubmitSuccess;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 引发 <see cref="PreInputedVcMissed" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		public static void OnPreInputedVcMissed(object sender)
		{

			var handler = PreInputedVcMissed;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="RequestShowPanel" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		public virtual void OnRequestShowPanel(UI.PanelIndex index)
		{
			var handler = RequestShowPanel;
			if (handler != null)
				handler(this, new GeneralEventArgs<UI.PanelIndex>(index));
		}

		/// <summary>
		/// 引发 <see cref="UserLogined" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		public static void OnUserLogined(object sender)
		{
			var session = sender as Session;
			if (session != null)
			{
				session.IsLogined = true;
				session.NetClient.StoreOtnPort();
			}

			Statistics.Current.LoginCount++;

			//加载联系人
			session.LoadPassengersAsync();

			var handler = UserLogined;
			handler?.Invoke(sender, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="UserVerificationStateChanged" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		public static void OnUserVerificationStateChanged(object sender)
		{
			var handler = UserVerificationStateChanged;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}

		/// <summary>
		/// 设置会话数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetSessionData<T>(string key, T value)
		{
			SessionData[key] = value;
		}

		public string Attributes { get; set; }

		/// <summary>
		/// 获得用于显示的标题名字
		/// </summary>
		public string DisplayText
		{
			get
			{
				if (string.IsNullOrEmpty(UserKeyData?.DisplayName) || UserKeyData.DisplayName == UserName)
					return UserName;

				return UserKeyData.DisplayName + " <font color='#aaaaaa'>" + UserName + "</font>";
			}
		}

		/// <summary>
		/// 获得用于显示的标题名字
		/// </summary>
		public string DisplayPlainText
		{
			get
			{
				if (string.IsNullOrEmpty(UserKeyData?.DisplayName) || UserKeyData.DisplayName == UserName)
					return UserName;

				return UserKeyData.DisplayName + " (" + UserName + ")";
			}
		}

		/// <summary>
		/// 动态JS数据
		/// </summary>
		public DynamicJsData DynamicJsData
		{
			get { return NetClient.DynamicJsData; }
		}

		/// <summary>
		/// 是否已经登录
		/// </summary>
		public bool IsLogined { get; private set; }

		/// <summary>
		/// 手机是否已经通过校验
		/// </summary>
		public bool? IsMobileChecked
		{
			get { return _isMobileChecked; }
			set
			{
				if (value == _isMobileChecked) return;
				_isMobileChecked = value;
				OnPropertyChanged(nameof(IsMobileChecked));
				OnMobileCheckStateChanged(this);
			}
		}

		/// <summary>
		/// 获得或设置自从登录以来，是否已经加载过联系人。如果没有，可能会无法提交订单
		/// </summary>
		public bool IsPassengerLoaded
		{
			get { return _isPassengerLoaded; }
			set
			{
				if (value == _isPassengerLoaded)
					return;
				_isPassengerLoaded = value;
				OnPropertyChanged(nameof(IsPassengerLoaded));
			}
		}

		public bool? IsUserVerified
		{
			get { return _isUserVerified; }
			set
			{
				if (value.Equals(_isUserVerified))
					return;
				_isUserVerified = value;
				OnPropertyChanged(nameof(IsUserVerified));
				OnUserVerificationStateChanged(this);
			}
		}


		/// <summary>
		/// 获得或设置上次检测验证码的时间
		/// </summary>
		public DateTime? LastCheckRandCodeTime { get; set; }


		/// <summary>
		/// 获得或设置最后心跳包的时间
		/// </summary>
		public DateTime? LastHeartBeatTime { get; set; }

		/// <summary>
		/// 获得或设置最后的验证码
		/// </summary>
		public string LastVerifyCode
		{
			get { return _lastVerifyCode; }
			set
			{
				if (value == _lastVerifyCode) return;

				_lastVerifyCode = value;
				if (value.IsNullOrEmpty())
					LastVerifyCodeInputTime = DateTime.Now;
			}
		}

		public DateTime? LastVerifyCodeInputTime { get; private set; }

		/// <summary>
		/// 获得或设置登录通知
		/// </summary>
		public string LoginNotification
		{
			get { return _loginNotification; }
			set
			{
				if (value == _loginNotification) return;
				_loginNotification = value;
				OnPropertyChanged(nameof(LoginNotification));
			}
		}
		/// <summary>
		/// 获得或设置是否需要检测在线状态
		/// </summary>
		public bool NeedCheckStatus { get; set; }

		/// <summary>
		/// 确认当前登录是否有效
		/// </summary>
		/// <returns></returns>
		public async Task<bool> CheckOnlineStatusIfNeed()
		{
			if (!NeedCheckStatus)
				return true;

			NeedCheckStatus = false;

			var ret = await Task<bool?>.Factory.StartNew(() => NetClient.VerifySessionValid()).ConfigureAwait(true);
			return ret != false;
		}

		/// <summary>
		/// 获得当前使用的客户端
		/// </summary>
		public NetClient NetClient
		{
			get
			{
				_lockObject.LockExecute(() => _netClient == null, () => _netClient = new NetClient(this));
				return _netClient;
			}
			set { _netClient = value; }
		}

		/// <summary>
		/// 获得或设置用户密码（临时保存）
		/// </summary>
		public string Password
		{
			get { return _password.DefaultForEmpty(UserKeyData.SelectValue(s => s.Password)); }
			set
			{
				if (value == _password) return;
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		/// <summary>
		/// 服务容器
		/// </summary>
		public ILifetimeScope ServiceContainer => _serviceKernel;

		/// <summary>
		/// 获得服务
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetService<T>() => _serviceKernel.Resolve<T>();

		/// <summary>
		/// 获得需要在会话中保存的数据字典
		/// </summary>
		public Dictionary<string, object> SessionData { get; internal set; }

		/// <summary>
		/// 是否处于影子模式
		/// </summary>
		public bool ShadowMode { get; set; }

		/// <summary>
		/// 获得或设置当前是否是临时模式
		/// </summary>
		public bool TemporaryMode
		{
			get { return _temporaryMode; }
			set
			{
				if (value.Equals(_temporaryMode)) return;
				_temporaryMode = value;
				OnPropertyChanged(nameof(TemporaryMode));
			}
		}

		public Otn.Entity.CheckUserResult UserData { get; set; }

		/// <summary>
		/// 用户的关键数据
		/// </summary>
		public UserKeyData UserKeyData
		{
			get { return _userKeyData; }
			set
			{
				if (Equals(value, _userKeyData)) return;
				_userKeyData = value;
				OnPropertyChanged(nameof(UserKeyData));
			}
		}

		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			get { return _userName; }
			private set
			{
				if (string.Compare(_userName, value, StringComparison.OrdinalIgnoreCase) == 0) return;
				_userName = value;
			}
		}


		/// <summary>
		/// 获得当前用户的配置
		/// </summary>
		public Profile.UserProfile UserProfile
		{
			get => _userProfile;
			private set
			{
				if (Equals(value, _userProfile)) return;
				_userProfile = value;
				OnPropertyChanged(nameof(UserProfile));
			}
		}

		/// <summary>
		/// 排队订单已取消
		/// </summary>
		public event EventHandler QueueOrderCancelled;

		protected virtual void OnQueueOrderCancelled()
		{
			QueueOrderCancelled?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 通知已经取消排队
		/// </summary>
		public void SetQueueOrderCancelled()
		{
			OnQueueOrderCancelled();
		}

		public string Uamtk
		{
			get => NetClient.Uamtk;
			set => NetClient.Uamtk = value;
		}

		public string Apptk
		{
			get => NetClient.AppTk;
			set => NetClient.AppTk = value;
		}

		/// <summary>
		/// 卸载当前的用户会话，并进行资源释放和保存
		/// </summary>
		public void Unload()
		{
			UserProfile.Save();
		}

		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				//TODO 释放托管资源

			}
			//TODO 释放非托管资源
			Unload();
			_serviceKernel.Dispose();

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion

		public BackupCart BackupOrderCart { get; }

		public HttpConf HttpConf { get; private set; }

		/// <summary>
		/// 重置HTTP配置
		/// </summary>
		public void ResetHttpConf() => HttpConf = null;

		/// <summary>
		/// 初始化HTTP配置
		/// </summary>
		/// <returns></returns>
		public async Task<bool> InitHttpConfAsync(bool force = false)
		{
			if (force)
				HttpConf = null;

			if (HttpConf != null)
				return true;

			var service = GetService<IWeb12306ConfProvider>();
			var (ok, _, conf) = await service.UpdateAsync();

			if (ok)
			{
				HttpConf = conf;
			}

			return ok;
		}

		public event EventHandler HbOrderChanged;

		public virtual void OnHbOrderChanged() { HbOrderChanged?.Invoke(this, EventArgs.Empty); }

		private bool? _faceCheckStatus;

		/// <summary>
		/// 人脸认证状态
		/// </summary>
		public bool? FaceCheckStatus
		{
			get => _faceCheckStatus;
			set
			{
				if (_faceCheckStatus == value)
				{
					return;
				}

				_faceCheckStatus = value;
				OnPropertyChanged(nameof(FaceCheckStatus));
			}
		}

		/// <summary>
		/// 使用指定的查询结果来检测人脸验证
		/// </summary>
		/// <param name="query"></param>
		public async Task<bool?> DetectFaceCheckStatusByQueryResult(Query.Entity.QueryResult query, QueryResultItem train = null, char? seat = null)
		{
			if (!IsLogined)
				return null;

			if (_faceCheckStatus != null)
				return _faceCheckStatus.Value;

			var sec = train ?? query.FirstOrDefault(s => s.SubmitOrderInfo != null && s.TicketCount.Count > 0);
			if (sec == null)
				return null;
			var ss = seat ?? sec.TicketCount.First().Key;

			var serv = GetService<IBackupOrderService>();
			var (passed, _, _) = await serv.CheckFaceAsync(new BackupCartItem() { Seat = ss, Train = sec });

			if (passed != null)
			{
				FaceCheckStatus = passed;

				GetService<IHbInfoProvider>().FillInfo(query);
			}

			return FaceCheckStatus;
		}


	}
}
