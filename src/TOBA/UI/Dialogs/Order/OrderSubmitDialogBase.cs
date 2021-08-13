using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TOBA.Entity;
using TOBA.Order;
using TOBA.Query.Entity;
using TOBA.QueryResumeManager;
using TOBA.Service;
using TOBA.UI.Controls;

namespace TOBA.UI.Dialogs.Order
{
	using AutoVc;

	using Configuration;

	using Controls.Misc;
	using Controls.Vc;

	using DevComponents.DotNetBar;

	using FSLib.Extension;

	using Platform.SlideVc;

	using System.Diagnostics;
	using System.Drawing;
	using System.Threading.Tasks;

	using TOBA.Query;

	internal class OrderSubmitDialogBase : DialogBase, IOrderSubmitEventSource
	{

		private bool _preventCloseFlag = false;

		/// <summary>
		/// 是否在排队中
		/// </summary>
		public bool InQueue { get; private set; }

		public char OriginalSeat { get; private set; }

		private bool _cancelQueueConfirm;

		IVerifyCodeControl _verifyCodeBox;

		protected OrderSubmitDialogBase(Session session, QueryResultItem train, char seat, IEnumerable<PassengerInTicket> passengers, QueryParam queryParam, bool byAuto)
		{
			InitSession(session);
			Train = train;
			OriginalSeat = seat;
			Seat = train.FindCorrectSeat(seat); ;
			PassengerInTickets = queryParam.PrepareTicketInfoForPassengers(train, passengers, Seat);
			Query = queryParam;

			if (Program.IsRunning)
			{
				Icon = MainForm.Instance.Icon;
				if (byAuto)
					QueryResumeManager.Controller.Instance[session].Register(this);
			}
			Session.Logout += Session_Logout;
			Session.QueueOrderCancelled += Session_QueueOrderCancelled;
			CaptchaLoaded += OrderSubmitDialogBase_CaptchaLoaded;
			OnOrderDialogOpen(this);

			//冲突检测
			OrderDialogOpen += OrderSubmitDialogBase_OrderDialogOpen;
			FormClosing += (s, e) =>
			{
				if (PreventCloseFlag)
				{
					this.ShowToast("窗口正在忙碌...请等待当前操作完成后再关闭。");
					e.Cancel = true;
					return;
				}

				IsFormClosed = true;
				_safeTimer?.Stop();
				Session.Logout -= Session_Logout;
				OrderDialogOpen -= OrderSubmitDialogBase_OrderDialogOpen;
				Session.QueueOrderCancelled -= Session_QueueOrderCancelled;
			};

			//安全期
			InitSafeTimer();
		}

		private void Session_QueueOrderCancelled(object sender, EventArgs e)
		{
			if (!InQueue)
				return;

			IsFormClosed = true;
			PreventCloseFlag = false;
			Close();
		}

		public OrderSubmitDialogBase()
		{

		}

		#region Overrides of Form

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Form.FormClosed"/> 事件。
		/// </summary>
		/// <param name="e">一个 <see cref="T:System.Windows.Forms.FormClosedEventArgs"/>，其中包含事件数据。</param>
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			BreakOperation = true;
			Session.Logout -= Session_Logout;
			CaptchaLoaded -= OrderSubmitDialogBase_CaptchaLoaded;
			OnSubmitClosed();
		}

		#endregion


		protected virtual void OnUserEnterReady()
		{
			UserEnterReady?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 完成初始化
		/// </summary>
		protected virtual void PostInitialize()
		{
			if (ProgramConfiguration.Instance.OrderDlgCenterMainform)
			{
				UiUtility.PlaceFormAtCenter(this, show: false);
			}
			else
			{
				StartPosition = FormStartPosition.CenterScreen;
			}
			FormPlacementManager.Instance.Control(this);

			Layer = new MsgLayer();
			Controls.Add(Layer);
			Layer.BringToFront();

			//空格键快速提交
			KeyDown += (s, e) =>
			{
				if (SubmitButton?.Enabled == true && !InQueue)
				{
					if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
					{
						SubmitButton.PerformClick();
						e.Handled = true;
					}
				}
			};

			KeyPreview = true;
		}

		/// <summary>
		/// 已自动重试的次数
		/// </summary>
		protected int AutoRetryCount { get; private set; }

		/// <summary>
		/// 获得或设置是否中断操作
		/// </summary>
		protected virtual bool BreakOperation { get; private set; }

		protected virtual bool PreventCloseFlag
		{
			get { return _preventCloseFlag || SubmitOrderWorker?.IsBusy == true; }
			set { _preventCloseFlag = value; }
		}

		/// <summary>
		/// 是否已经附加自动重刷控制器
		/// </summary>
		public bool AutoResumeAttached { get; set; }

		/// <summary>
		/// 是否已经关闭
		/// </summary>
		public bool IsFormClosed { get; protected set; }

		/// <summary>
		/// 最后输入的验证码
		/// </summary>
		public IVerifyCodeRecognizeResult LastVcCode { get; protected set; }
		public MsgLayer Layer { get; private set; }

		public OrderSubmitEventArgs OrderEventArgs { get; protected set; }

		/// <summary>
		/// 订单里的乘客
		/// </summary>
		public IEnumerable<PassengerInTicket> PassengerInTickets { get; protected set; }

		public virtual QueryParam Query { get; protected set; }

		public virtual char Seat { get; protected set; }

		private ButtonX _submitButton;

		/// <summary>
		/// 获得或设置提交用的按钮
		/// </summary>
		public ButtonX SubmitButton
		{
			get { return _submitButton; }
			set
			{
				if (value == null || value == _submitButton)
					return;

				if (_submitButton != null)
					_submitButton.Click -= _submitButton_Click;
				_submitButton = value;
				if (_submitButton != null)
					_submitButton.Click += _submitButton_Click;
			}
		}

		private async void _submitButton_Click(object sender, EventArgs e)
		{
			if (InQueue)
			{
				if (!_cancelQueueConfirm)
				{
					_cancelQueueConfirm = true;
					SubmitButton.Text = "确认取消";
					this.ShowToast("请再次点击“确认取消”以确定取消排队");
				}
				else
				{
					SubmitButton.Text = "正在取消...";
					SubmitButton.Enabled = false;

					var (ret, msg) = await QueueOrderWorker.CancelQueueOrderAsync().ConfigureAwait(true);

					SubmitButton.Enabled = ret == CancelQueueStatus.Failed || ret == CancelQueueStatus.NetworkError;
					SubmitButton.Text = msg;


					if (ret == CancelQueueStatus.ForceLogout)
					{
						DevComponents.DotNetBar.MessageBoxEx.Show(this, "登录已失效，尽快重新登录查看排队结果，请留意是否有未完成订单。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						if (!await Session.BeenForceLogout().ConfigureAwait(true))
							Close();
					}
				}
			}
			else
				await TryBeginSubmitOrderAsync().ConfigureAwait(true);
		}

		public virtual QueryResultItem Train { get; protected set; }

		#region Implementation of IOrderSubmitEventSource

		private event EventHandler InitSubmitFailed;

		event EventHandler IOrderSubmitEventSource.InitSubmitFailed
		{
			add { this.InitSubmitFailed += value; }
			remove { this.InitSubmitFailed -= value; }
		}

		/// <summary>
		/// 引发 <see cref="InitSubmitFailed" /> 事件
		/// </summary>
		protected virtual void OnInitSubmitFailed()
		{
			var handler = InitSubmitFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private event EventHandler AutoVcFailed;

		event EventHandler IOrderSubmitEventSource.AutoVcFailed
		{
			add { this.AutoVcFailed += value; }
			remove { this.AutoVcFailed -= value; }
		}

		/// <summary>
		/// 引发 <see cref="AutoVcFailed" /> 事件
		/// </summary>
		protected virtual void OnAutoVcFailed()
		{
			var handler = AutoVcFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private event EventHandler _submitFailed;

		event EventHandler IOrderSubmitEventSource.SubmitFailed
		{
			add { this._submitFailed += value; }
			remove { this._submitFailed -= value; }
		}

		/// <summary>
		/// 引发 <see cref="SubmitFailed" /> 事件
		/// </summary>
		protected virtual void OnSubmitFailed()
		{
			var handler = _submitFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private event EventHandler QueueFailedNoTicket;

		event EventHandler IOrderSubmitEventSource.QueueFailedNoTicket
		{
			add { this.QueueFailedNoTicket += value; }
			remove { this.QueueFailedNoTicket -= value; }
		}

		/// <summary>
		/// 引发 <see cref="QueueFailedNoTicket" /> 事件
		/// </summary>
		protected virtual void OnQueueFailedNoTicket()
		{
			var handler = QueueFailedNoTicket;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private event EventHandler QueueFailedElse;

		event EventHandler IOrderSubmitEventSource.QueueFailedElse
		{
			add { this.QueueFailedElse += value; }
			remove { this.QueueFailedElse -= value; }
		}

		/// <summary>
		/// 引发 <see cref="QueueFailedElse" /> 事件
		/// </summary>
		protected virtual void OnQueueFailedElse()
		{
			var handler = QueueFailedElse;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 准备完成，等待输入
		/// </summary>
		private event EventHandler UserEnterReady;

		/// <summary>
		/// 错误信息
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// 准备完成，等待输入
		/// </summary>
		event EventHandler IOrderSubmitEventSource.UserEnterReady
		{
			add { this.UserEnterReady += value; }
			remove { this.UserEnterReady -= value; }
		}

		private event EventHandler OperationPerformed;

		event EventHandler IOrderSubmitEventSource.OperationPerformed
		{
			add { this.OperationPerformed += value; }
			remove { this.OperationPerformed -= value; }
		}

		/// <summary>
		/// 引发 <see cref="OperationPerformed" /> 事件
		/// </summary>
		protected virtual void OnOperationPerformed()
		{
			if (MediaConfiguration.Instance.StopMusicIfUserOperated)
				MainForm.Instance.StopPlayTicketMusic(true);

			var handler = OperationPerformed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 取消提交
		/// </summary>
		void IOrderSubmitEventSource.Cancel()
		{
			AppContext.MainForm.UiInvoke(() =>
			{
				BreakOperation = true;
				base.Close();
			});
		}

		private event EventHandler SubmitClosed;

		event EventHandler IOrderSubmitEventSource.SubmitClosed
		{
			add { this.SubmitClosed += value; }
			remove { this.SubmitClosed -= value; }
		}

		/// <summary>
		/// 引发 <see cref="SubmitClosed" /> 事件
		/// </summary>
		protected virtual void OnSubmitClosed()
		{
			var handler = SubmitClosed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		#endregion

		#region 自动识别验证码

		protected virtual void AttachVerifyCodeControl(IVerifyCodeControl control)
		{
			if (_verifyCodeBox != null)
				throw new InvalidOperationException();

			_verifyCodeBox = control;
			_verifyCodeBox.EnableAutoVc = VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null && ProgramConfiguration.Instance.AutoSubmitOrderVcCode && Query.EnableAutoVC;

			_verifyCodeBox.AutoVcFailed += async (o, args) =>
			{
				await OnAutoVcFailed(_verifyCodeBox.AutoVcCount).ConfigureAwait(true);
			};
			_verifyCodeBox.AutoVcGiveUp += async (o, args) =>
			 {
				 await OnAutoVcGiveUp().ConfigureAwait(true);
			 };
			_verifyCodeBox.BeginAutoVc += async (o, args) =>
			{
				await OnAutoVcBegin(_verifyCodeBox.AutoVcCount).ConfigureAwait(true);
			};
			_verifyCodeBox.EndAutoVc += async (o, args) =>
			{
				await OnAutoVcSuccess(_verifyCodeBox.AutoVcCount, _verifyCodeBox.AutoVcCode).ConfigureAwait(true);
			};
			_verifyCodeBox.VerifyCodeOnLoad += async (o, args) => await OnVcLoad().ConfigureAwait(true);
			_verifyCodeBox.VerifyCodeLoadComplete += async (o, args) =>
			{
				await OnVcLoadComplete().ConfigureAwait(true);
				OnCaptchaLoaded(this);
			};
			_verifyCodeBox.VerifyCodeLoadFailed += async (o, args) =>
			{
				await OnVcLoadFailed().ConfigureAwait(true);

				//验证码加载失败，则检查一次登录状态
				if (await Session.CheckOnlineStatusIfNeed().ConfigureAwait(true) == false)
				{
					await PrepareFailed("此次登录即便封印了洪荒之力也依然被踢了……赶紧重新登录...").ConfigureAwait(true);
				}
			};
			_verifyCodeBox.VerifyCodeEnterComplete += async (s, e) =>
			 {
				 await TryBeginSubmitOrderAsync().ConfigureAwait(true);
			 };

			//默认禁用
			_verifyCodeBox.Enabled = false;
		}

		/// <summary>
		/// 获得当前的验证码控件
		/// </summary>
		public IVerifyCodeControl VerifyCodeBox => _verifyCodeBox;

		protected virtual async Task OnAutoVcSuccess(int count, IVerifyCodeRecognizeResult code)
		{
			await TryBeginSubmitOrderAsync().ConfigureAwait(true);
		}

		protected virtual async Task OnVcLoad()
		{
			OnUserEnterReady();
		}

		/// <summary>
		/// 验证码加载成功
		/// </summary>
		protected virtual async Task OnVcLoadComplete()
		{
			await SetAllowOperationAsync(true, true).ConfigureAwait(true);
			SetFocus();
			StartSafeTimeCountDown();
		}

		/// <summary>
		/// 验证码加载失败
		/// </summary>
		protected virtual async Task OnVcLoadFailed()
		{
			SetFocus();
			await SetAllowOperationAsync(true, true).ConfigureAwait(true);
		}

		/// <summary>
		/// VC过时
		/// </summary>
		protected virtual async Task OnVcExpired()
		{

		}

		protected virtual async Task OnAutoVcFailed(int count)
		{

		}

		protected virtual async Task OnAutoVcBegin(int count)
		{

		}

		protected virtual async Task OnAutoVcGiveUp()
		{
			if (AutoResumeAttached)
				OnAutoVcFailed();
		}

		#endregion

		#region 订单逻辑

		/// <summary>
		/// 获得当前的订单工作类
		/// </summary>
		public SubmitOrderBase SubmitOrderWorker { get; private set; }

		/// <summary>
		/// 当前的排队工作类
		/// </summary>
		public QueueOrderWorker QueueOrderWorker { get; private set; }

		/// <summary>
		/// 获得上次错误信息
		/// </summary>
		public string LastErrorMessage { get; private set; }

		protected virtual void AttachWorker([NotNull] SubmitOrderBase orderWorker, [NotNull] QueueOrderWorker queueWorker)
		{
			if (orderWorker == null)
				throw new ArgumentNullException(nameof(orderWorker));
			if (queueWorker == null)
				throw new ArgumentNullException(nameof(queueWorker));

			if (SubmitOrderWorker != null)
			{
				throw new InvalidOperationException();
			}

			SubmitOrderWorker = orderWorker;
			QueueOrderWorker = queueWorker;

			//初始化信息
			if (PassengerInTickets != null && SubmitOrderWorker.Passengers == null)
			{
				SubmitOrderWorker.Passengers = PassengerInTickets.ToArray();
			}
			OrderEventArgs = SubmitOrderWorker.CreateEventArgs();


			//绑定事件
			orderWorker.PrepareFailed += async (s, e) =>
			 {
				 if (IsFormClosed)
					 return;

				 await PrepareFailed(orderWorker.Error).ConfigureAwait(true);
			 };
			orderWorker.PrepareFinished += async (s, e) =>
			 {
				 if (IsFormClosed)
					 return;

				 await PrepareSuccessAsync(orderWorker.QueueWarning).ConfigureAwait(true);
			 };
			//提交失败
			orderWorker.SubmitOrderFailed += async (s, e) =>
			{
				await OrderFailedAsync(orderWorker.Error, false).ConfigureAwait(true);
			};
			orderWorker.SubmitOrderSuccess += async (s, e) =>
			{
				await SubmitOrderSuccessAsync(orderWorker.Async, orderWorker.OrderID).ConfigureAwait(true);
			};
			QueueOrderWorker.QueryOrderQueueFailed += async (s, e) =>
			{
				InQueue = false;
				_cancelQueueConfirm = false;
				if (SubmitButton != null)
				{
					SubmitButton.ColorTable = eButtonColor.BlueWithBackground;
					SubmitButton.Image = Properties.Resources.cou_32_accept;
					SubmitButton.Text = "重新提交";
				}
				await OrderFailedAsync(QueueOrderWorker.Error.ToString(), true).ConfigureAwait(true);
			};
			QueueOrderWorker.QueryOrderQueueSuccess += async (s, e) =>
			{
				InQueue = false;
				_cancelQueueConfirm = false;
				if (SubmitButton != null)
				{
					SubmitButton.Image = Properties.Resources.cou_32_accept;
					SubmitButton.Text = "订票成功";
					SubmitButton.Enabled = false;
				}
				await QueueSucceedAsync(QueueOrderWorker.OrderID).ConfigureAwait(true);
			};
			QueueOrderWorker.QueueMessageChanged += async (s, e) =>
			{
				await SubmitProgressChangedAsync(QueueOrderWorker.QueueMessage, QueueOrderWorker.SlowQueueWarning).ConfigureAwait(true);
			};

		}

		/// <summary>
		/// _订单排队成功_
		/// ---
		/// 
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual async Task SubmitOrderSuccessAsync(bool async, string orderid)
		{
			if (async)
			{
				PreventCloseFlag = true;
				await QueueBeginAsync().ConfigureAwait(true);
				QueueOrderWorker.RunQueryOrderQueue();
			}
			else
			{
				PreventCloseFlag = false;
				//非异步模式，不需要排队
				await QueueSucceedAsync(orderid).ConfigureAwait(true);
			}
		}

		protected virtual async Task QueueBeginAsync()
		{
			InQueue = true;
			if (SubmitButton != null)
			{
				SubmitButton.Text = "取消排队";
				SubmitButton.Image = Properties.Resources.cou_32_delete;
				SubmitButton.Enabled = true;
				SubmitButton.ColorTable = eButtonColor.OrangeWithBackground;
			}
		}

		/// <summary>
		/// 排队信息变化
		/// </summary>
		/// <param name="message"></param>
		/// <param name="slowQueueWarning">是否是慢速队列</param>
		/// <returns></returns>
		protected virtual async Task SubmitProgressChangedAsync(string message, bool slowQueueWarning)
		{

		}

		protected virtual async Task PrepareOrderAsync()
		{
			await SetAllowOperationAsync(false, true).ConfigureAwait(true);
			SubmitOrderWorker.Prepare();
		}


		private SlideVcControl _slideVc;
		/// <summary>
		/// 订单准备成功
		/// </summary>
		/// <param name="queueWarning">是否有排队警告</param>
		/// <returns></returns>
		protected virtual async Task PrepareSuccessAsync(bool queueWarning)
		{
			PreventCloseFlag = false;
			SetFocus();

			VerifyCodeBox.SetIfNeedVc(SubmitOrderWorker.NeedVc);

			if (await CanSkipVcAsync().ConfigureAwait(true))
			{
				//TODO 这里将延迟的流程延迟到提交订单中了。
				//     现在的延迟不高

				//if (SubmitOrderWorker.NeedVcTime > 0)
				//{
				//	_safeTime = SubmitOrderWorker.NeedVcTime - (SubmitOrderWorker.VcBaseTime == null ? 0 : (int)(DateTime.Now - SubmitOrderWorker.VcBaseTime.Value).TotalMilliseconds);
				//	//_safeTime = Math.Min(ApiConfiguration.Instance.MinimalVcWaitTimeRunTime, _safeTime);
				//	_delaySubmit = true;
				//	Debug.WriteLine($"[VCTIME] {SubmitOrderWorker.NeedVcTime} [BASETIME] {SubmitOrderWorker.VcBaseTime?.ToString("O")} [DELAYTIME] {_safeTime}");

				//	StartSafeTimeCountDown();
				//}
				//else 
				if (PassengerInTickets?.Count() > 0 && PassengerInTickets.All(s => s.SeatType != '\0'))
					await BeginSubmitOrderAsync().ConfigureAwait(true);
				else
				{
					await SafeTimeEnd().ConfigureAwait(true);
				}
			}
			else if (SubmitOrderWorker.NeedSlideVc)
			{
				//需要滑动验证码
				if (_slideVc == null)
				{
					_slideVc = new SlideVcControl(Session, SubmitOrderWorker.SlideVcToken, SlideAppId.Order);
					_slideVc.SlideOk += async (sender, args) =>
					{
						await SetAllowOperationAsync(true, true);
						SubmitOrderWorker.SlideSig = _slideVc.Sig;
						SubmitOrderWorker.SlideCSessionId = _slideVc.CfSessionId;
						_submitButton.Visible = true;
						_slideVc.Visible = false;
						await TryBeginSubmitOrderAsync();
					};
					VerifyCodeBox.Parent.Controls.Add(_slideVc);
					_slideVc.Location = VerifyCodeBox.Location + new Size(0, VerifyCodeBox.Size.Height - _slideVc.Height);
					_slideVc.BringToFront();
				}
				else
				{
					_slideVc.Token = SubmitOrderWorker.SlideVcToken;
				}
				VerifyCodeBox.Visible = false;
				_submitButton.Visible = false;
				await OrderInitSuccessAsync(true).ConfigureAwait(true);
			}
			else if (Session.LastVerifyCode != null)
			{
				VerifyCodeBox.ValidateImage = Properties.Resources.img_preinput;
				SubmitOrderWorker.RandCode = Session.LastVerifyCode;

				await TryBeginSubmitOrderAsync().ConfigureAwait(true);
			}
			else
			{
				VerifyCodeBox.Visible = false;
				if (_slideVc != null) _slideVc.Visible = false;
				_submitButton.Visible = true;
				await OrderInitSuccessAsync(SubmitOrderWorker.NeedVc).ConfigureAwait(true);
			}
		}

		/// <summary>
		/// 完成初始化（含验证码倒计时）
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OrderInitSuccessAsync(bool? needVc)
		{

		}

		protected virtual async Task PrepareFailed(string error)
		{
			PreventCloseFlag = false;
			LastErrorMessage = error;

			var ma = new MessageAnalyzer(error);

			if (ma.NeedRelogin)
			{
				if (Query.Resign)
				{
					this.Information("登录已被强退，请重新登录并重新改签。");
					Close();
				}
				else
				{
					if (await OnReloginAsync().ConfigureAwait(true))
					{
						await OnOrderContextInitializeAsync().ConfigureAwait(true);
						await PrepareOrderAsync().ConfigureAwait(true);
					}
					else
					{
						Close();
					}
				}

				return;
			}

			if (ma.CaptchaError)
			{
				await OnCaptchaErrorAsync().ConfigureAwait(true);

				return;
			}
			if (ma.TicketNotEnough)
			{
				//排队人数过多/排队人数已超过剩余票数
				//余票不足
				//没有足够的票
				RecentlySubmitFailedTokenStorageProvider.Instance.AddDisableTicketData(Train);
			}

			if (ma.NeedRollbackOrderCommitMethod)
				Query.TemporaryDisableFastSubmitOrder = !Query.TemporaryDisableFastSubmitOrder;

			if (ma.TicketNotEnough || ma.DataExpired || ma.ErrorElse || ma.NeedRetry)
			{
				//订单提交失败
				if (AutoResumeAttached)
				{
					Error = SubmitOrderWorker.Error;
					OnInitSubmitFailed();

					if (BreakOperation)
						return;
				}

				if (ma.DataExpired)
				{
					await OnInformationAsync("余票信息已经过期，请重新查询最新的余票信息 o(>_<)o ~~").ConfigureAwait(true);
					Close();

					return;

				}

				if (ma.NeedRollbackOrderCommitMethod)
				{
					await OnInformationAsync("当前订单提交已失效，请返回后重试。").ConfigureAwait(true);
					Close();

					return;

				}

				//自动禁用
				if (await OnOrderResubmitAsync(error).ConfigureAwait(true))
					await PrepareOrderAsync().ConfigureAwait(true);
				else
				{
					Close();
				}

				return;
			}

			//身份信息有误/账户信息有误/订单要处理/订单排队中
			new OrderNotification(Session, Query, Train, PassengerInTickets, ma.Message, null).Show(AppContext.HostForm);

			Close();
		}

		/// <summary>
		/// 返回是否可以忽略验证码
		/// </summary>
		/// <returns></returns>
		protected virtual async Task<bool> CanSkipVcAsync()
		{
			//滑动验证
			if (SubmitOrderWorker.NeedSlideVc)
				return false;

			//不需要验证码
			if (SubmitOrderWorker.NeedVc != true)
				return true;

			if (Query.Resign)
				return true;

			return ApiConfiguration.Instance.FastSubmitOrderSkipVc && !SubmitOrderWorker.Async;
		}

		/// <summary>
		/// 返回是否可以提交订单
		/// </summary>
		/// <returns></returns>
		protected virtual async Task<bool> CanSubmitAsync()
		{
			if (PassengerInTickets != null && PassengerInTickets.Any() && PassengerInTickets.All(s => s.SeatType != '\0'))
			{
				SubmitOrderWorker.Passengers = PassengerInTickets.ToArray();
				if (OrderEventArgs != null)
				{
					OrderEventArgs.OrderSubmitContext.Passengers = SubmitOrderWorker.Passengers;
				}
			}
			if (Session.LastVerifyCode != null)
			{
				SubmitOrderWorker.RandCode = Session.LastVerifyCode;
				Session.LastVerifyCode = null;
			}
			else
				SubmitOrderWorker.RandCode = VerifyCodeBox.Code;

			if (SubmitOrderWorker.NeedSlideVc)
			{
				SubmitOrderWorker.SlideCSessionId = _slideVc?.CfSessionId;
				SubmitOrderWorker.SlideSig = _slideVc?.Sig;
				if (SubmitOrderWorker.SlideSig.IsNullOrEmpty())
				{
					return false;
				}
			}
			else if (SubmitOrderWorker.RandCode.IsNullOrEmpty() && (SubmitOrderWorker.NeedVc != null && !await CanSkipVcAsync().ConfigureAwait(true)))
			{
				this.ShowToast("请输入验证码啊大侠!....", backColor: Color.DarkRed, glowColor: eToastGlowColor.Red);
				return false;
			}

			if (SubmitButton?.Enabled == false && !_delaySubmit)
				return false;
			_delaySubmit = false;

			if (_safeTimer.Enabled || _safeStartTime != null)
			{
				//等待完成提交
				if (SubmitButton != null)
				{
					SubmitButton.Enabled = false;
				}
				_delaySubmit = true;
				return false;
			}

			return true;
		}

		protected virtual async Task<bool> TryBeginSubmitOrderAsync()
		{
			if (!await CanSubmitAsync().ConfigureAwait(true))
				return false;

			return await BeginSubmitOrderAsync().ConfigureAwait(true);
		}

		protected virtual async Task<bool> BeginSubmitOrderAsync()
		{
			TopMost = false;
			await SetAllowOperationAsync(false, false).ConfigureAwait(true);
			OnOperationPerformed();
			SubmitOrderWorker.BeginSubmitOrder();

			return true;
		}

		/// <summary>
		/// 设置是否允许操作界面
		/// </summary>
		/// <param name="enabled"></param>
		/// <returns></returns>
		protected virtual async Task SetAllowOperationAsync(bool enabled, bool onPrepare)
		{
			if (SubmitButton != null)
				SubmitButton.Enabled = enabled;
		}

		protected virtual async Task<bool> CheckLoginStateAsync()
		{
			return await Task<bool>.Factory.StartNew(() => Session.NetClient.VerifySessionValid(2) == true).ConfigureAwait(true);
		}

		protected virtual async Task<bool> OnReloginAsync()
		{
			return await Session.BeenForceLogout().ConfigureAwait(true);
		}

		protected virtual async Task<bool> OnOrderResubmitAsync(string msg)
		{
			return await OnConfirmAsync("哦NO，没买着…" + msg + "\n是否重试？").ConfigureAwait(true);
		}

		protected virtual async Task<bool> OnConfirmAsync(string msg)
		{
			//return this.Question(msg);
			return await Layer.Confirm(msg).ConfigureAwait(true);
		}

		protected virtual async Task OnInformationAsync(string msg)
		{
			//this.Information(msg);
			await Layer.Information(msg).ConfigureAwait(true);
		}

		protected virtual async Task OnCaptchaErrorAsync()
		{
			if (SubmitOrderWorker.NeedSlideVc)
			{
				_slideVc.Reload();
			}
			else
			{
				if (VerifyCodeBox?.AutoVcCode == LastVcCode)
				{
					VerifyCodeBox?.MarkError(LastVcCode);
				}

				VerifyCodeBox?.LoadVerifyCode();
			}
			await SetAllowOperationAsync(false, true).ConfigureAwait(true);
		}

		/// <summary>
		/// 请求订单重试
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnRequireRetryAsync()
		{
			await PrepareOrderAsync().ConfigureAwait(true);
		}

		/// <summary>
		/// 订票失败
		/// </summary>
		/// <param name="error">错误信息</param>
		/// <param name="isQueue">是排队后的信息还是排队前信息</param>
		protected virtual async Task OrderFailedAsync(string error, bool isQueue)
		{
			PreventCloseFlag = false;
			if (OrderEventArgs != null)
			{
				OrderEventArgs.OrderSubmitContext.OrderID = null;
				OrderEventArgs.OrderSubmitContext.Message = error;
				Session.OnOrderSubmitFailed(OrderEventArgs.OrderSubmitContext.Session, OrderEventArgs);
			}
			LastErrorMessage = error;

			if (error?.IndexOf("出票失败") >= 0)
			{
				ApiConfiguration.Instance.MinimalVcWaitTimeRunTime += 100;
				Trace.TraceInformation($"Increase MinimalVcWaitTimeRunTime to {ApiConfiguration.Instance.MinimalVcWaitTimeRunTime}");
			}

			var ma = new MessageAnalyzer(error);

			if (ma.NeedRelogin)
			{
				if (await OnReloginAsync().ConfigureAwait(true))
				{
					await OnOrderContextInitializeAsync().ConfigureAwait(true);
					await PrepareOrderAsync().ConfigureAwait(true);
				}
				else
				{
					Close();
				}
				return;
			}

			if (ma.NeedVc)
			{
				this.ShowToast("请输入验证码");
				await SetAllowOperationAsync(false, true).ConfigureAwait(true);
				VerifyCodeBox.SetIfNeedVc(SubmitOrderWorker.NeedVc);
				return;
			}
			if (ma.CaptchaError)
			{
				await OnCaptchaErrorAsync().ConfigureAwait(true);
				return;
			}
			if (ma.NeedRetry && AutoRetryCount++ < OrderConfiguration.Instance.AutoRetryCountLimit)
			{
				await OnOrderContextInitializeAsync().ConfigureAwait(true);
				await OnRequireRetryAsync().ConfigureAwait(true);
				return;
			}
			if (ma.TicketNotEnough)
			{
				//排队人数过多/排队人数已超过剩余票数
				//余票不足
				//没有足够的票
				RecentlySubmitFailedTokenStorageProvider.Instance.AddDisableTicketData(Train);
			}

			if (ma.NeedRollbackOrderCommitMethod)
				Query.TemporaryDisableFastSubmitOrder = !Query.TemporaryDisableFastSubmitOrder;

			if (ma.TicketNotEnough || ma.ErrorElse || ma.NeedRetry)
			{
				//订单提交失败
				if (AutoResumeAttached)
				{
					if (ma.TicketNotEnough)
					{
						if (isQueue)
							OnQueueFailedNoTicket();
						else OnSubmitFailed();
					}
					else
					{
						if (isQueue)
							OnQueueFailedElse();
						else OnSubmitFailed();
					}

					if (BreakOperation)
						return;
				}

				if (ma.NeedRollbackOrderCommitMethod)
				{
					await OnInformationAsync("当前订单提交已失效，请返回后重试。").ConfigureAwait(true);
					Close();

					return;

				}

				//请求确认
				if (await OnOrderResubmitAsync(error).ConfigureAwait(true))
					await PrepareOrderAsync().ConfigureAwait(true);
				else Close();

				return;
			}

			//身份信息有误/账户信息有误/订单要处理/订单排队中
			UiUtility.PlaceFormAtCenter(new OrderNotification(Session, Query, Train, PassengerInTickets, ma.Message, null));
			Close();
		}

		/// <summary>
		/// 订单提交已成功
		/// </summary>
		/// <param name="orderid"></param>
		/// <returns></returns>
		protected virtual async Task QueueSucceedAsync(string orderid)
		{
			PreventCloseFlag = false;
			//new OrderSuccess(orderid.DefaultForEmpty("(不知道..T_T)")) { Session = Session }.Show();
			UiUtility.PlaceFormAtCenter(new OrderNotification(Session, Query, Train, PassengerInTickets, null, orderid));

			Session.OnRequestShowPanel(PanelIndex.NotComplete);
			if (Query.Resign)
				Query.IsLoaded = false;

			if (OrderEventArgs != null)
			{
				OrderEventArgs.OrderSubmitContext.OrderID = orderid ?? "";
				OrderEventArgs.OrderSubmitContext.Message = "";
				Session.OnOrderSubmitSuccess(OrderEventArgs.OrderSubmitContext.Session, OrderEventArgs);
			}

			if (AutoResumeAttached)
			{
				Controller.Instance[Session].GetContext(this).IsSubmitSuccess = true;

			}

			Close();
		}

		protected virtual async Task OnOrderContextInitializeAsync()
		{
			await SetAllowOperationAsync(false, true).ConfigureAwait(true);
			await Session.GetService<ITicketOrderInitializerService>().InitializeAsync().ConfigureAwait(true);
		}

		#endregion

		#region 冲突检测和操作


		private static void OnOrderDialogOpen(object sender)
		{
			OrderDialogOpen?.Invoke(sender, EventArgs.Empty);
		}

		private void OrderSubmitDialogBase_OrderDialogOpen(object sender, EventArgs e)
		{
			if (sender != this && ((OrderSubmitDialogBase)sender).Session == Session && !PreventCloseFlag && !InQueue)
			{
				PreventCloseFlag = false;
				IsFormClosed = true;
				Close();
			}
		}

		void Session_Logout(object sender, EventArgs e)
		{
			if (sender == Session)
			{
				Close();
			}
		}


		static event EventHandler CaptchaLoaded;

		/// <summary>
		/// 验证码已失效
		/// </summary>
		public event EventHandler CaptchaExpired;

		/// <summary>
		/// 引发 <see cref="CaptchaExpired" /> 事件
		/// </summary>
		protected virtual void OnCaptchaExpired()
		{
			OnVcExpired();
			var handler = CaptchaExpired;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="CaptchaLoaded" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		static void OnCaptchaLoaded(object sender)
		{
			var handler = CaptchaLoaded;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}



		void OrderSubmitDialogBase_CaptchaLoaded(object sender, EventArgs e)
		{
			if (sender == this || (sender as OrderSubmitDialogBase).Session != Session)
				return;

			VerifyCodeBox?.SetExpires();
			OnCaptchaExpired();
		}

		static event EventHandler OrderDialogOpen;

		#endregion

		#region 安全期计数

		Timer _safeTimer;
		DateTime? _safeStartTime;
		bool _delaySubmit;
		private int _safeTime;

		/// <summary>
		/// 初始化定时器
		/// </summary>
		void InitSafeTimer()
		{
			_safeTime = NetworkConfiguration.Current.VcSubmitDelay;
			_safeTimer = new Timer() { Interval = 50, Enabled = false };
			_safeTimer.Tick += async (s, e) =>
			{
				var ts = (DateTime.Now - _safeStartTime.Value);
				if (ts.TotalMilliseconds >= _safeTime)
				{
					_safeTimer.Stop();
					await SafeTimeEnd().ConfigureAwait(true);
				}
				else
				{
					await SafeTimeTick(_delaySubmit, _safeTime - (int)ts.TotalMilliseconds).ConfigureAwait(true);
				}
			};
		}

		/// <summary>
		/// 开始安全期....
		/// </summary>
		/// <returns></returns>
		protected virtual async Task SafeTimeBegin()
		{

		}

		protected virtual async Task SafeTimeEnd()
		{
			_safeStartTime = null;
			await OrderInitSuccessAsync(SubmitOrderWorker.NeedVc).ConfigureAwait(true);
			if (_delaySubmit)
			{
				await TryBeginSubmitOrderAsync().ConfigureAwait(true);
			}
		}

		protected virtual async Task SafeTimeTick(bool insubmit, int resetTime)
		{

		}

		/// <summary>
		/// 启动安全计时
		/// </summary>
		protected async void StartSafeTimeCountDown()
		{
			if (NetworkConfiguration.Current.VcSubmitDelay <= 0)
				return;

			_safeStartTime = DateTime.Now;
			_safeTimer.Enabled = true;

			await SafeTimeBegin().ConfigureAwait(true);
		}

		#endregion
	}
}
