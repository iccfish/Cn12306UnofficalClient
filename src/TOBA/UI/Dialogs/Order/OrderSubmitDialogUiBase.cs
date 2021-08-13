using System;
using System.Collections.Generic;
using System.Drawing;

namespace TOBA.UI.Dialogs.Order
{
	using AutoVc;

	using Controls.Common;

	using Data;

	using Entity;

	using System.Threading.Tasks;

	using TOBA.Order;
	using TOBA.Query;
	using TOBA.Query.Entity;

	internal partial class OrderSubmitDialogUiBase : OrderSubmitDialogBase
	{
		public bool DisableSkipVc { get; set; } = false;


		/// <summary>
		/// 获得当前的状态栏
		/// </summary>
		protected TipBarMessage Status => ts;

		public OrderSubmitDialogUiBase()
		{
			InitializeComponent();
		}

		protected OrderSubmitDialogUiBase(Session session, QueryResultItem train, char seat, IEnumerable<PassengerInTicket> passengers, QueryParam queryParam, bool byAuto)
			: base(session, train, seat, passengers, queryParam, byAuto)
		{
			InitializeComponent();

			Load += (s, e) =>
					{
						InitUI();
					};
		}

		void InitUI()
		{
			CaptchaExpired += (s, e) =>
			{
				ts.Text = "由于验证码冲突，当前页验证码已失效。记得刷新再填哦~";
				ts.ApplyColorSchema(RowStyleType.PinkRed);
				ts.Image = Properties.Resources.warning_16;
			};

			InitTrainInfoDisplay();
		}

		protected override void AttachWorker(SubmitOrderBase orderWorker, QueueOrderWorker queueWorker)
		{
			base.AttachWorker(orderWorker, queueWorker);

			orderWorker.TicketCountChanged += (_1, _2) =>
			{
				var str = $"实时余票 {ParamData.SeatTypeFull.GetValue(orderWorker.TicketCountSeat)} {orderWorker.TicketCount} 张";
				if (orderWorker.TicketCountNoSeat >= 0)
					str += "，无座 " + orderWorker.TicketCountNoSeat + " 张";
				this.Invoke(() => ShowTip(str));
			};
		}


		protected virtual void InitTrainInfoDisplay()
		{
			Gif.SetLoadingImage(pbAnimate);
			lblTrainCode.Text = Train.Code;
			lblDate.Text = Train.FromStation.DepartureTime.Value.ToString("MM-dd");
			lblFrom.Text = Train.FromStation.StationName;
			lblTo.Text = Train.ToStation.StationName;
			lblTimeInfo.Text = lblTimeInfo.Text.FormatWith(
				Train.FromStation.DepartureTime.Value.ToString("HH:mm"),
				Train.ToStation.ArriveTime.Value.ToString("HH:mm"),
				Train.ElapsedTime.ToString("dd'天'hh'时'mm'分'"));

		}

		/// <summary>
		/// 显示错误信息
		/// </summary>
		/// <param name="error"></param>
		protected virtual void ShowError(string error)
		{

		}

		protected virtual void ShowTip(string tip)
		{

		}

		/// <summary>
		/// 隐藏错误
		/// </summary>
		protected virtual void HideError()
		{

		}

		/// <summary>
		/// 订票失败
		/// </summary>
		/// <param name="error">错误信息</param>
		/// <param name="isQueue">是排队后的信息还是排队前信息</param>
		protected override async Task OrderFailedAsync(string error, bool isQueue)
		{
			Gif.SetFailedImage(pbAnimate);
			ts.ApplyColorSchema(RowStyleType.Red);


			var msg = (isQueue ? "出票失败" : "提交失败") + "：" + error + MessageTranslator.GetAdditionalInfo(error);
			ts.Text = msg;
			ts.Image = Properties.Resources.cou_16_block;
			ShowError(msg);

			await base.OrderFailedAsync(error, isQueue).ConfigureAwait(true);
		}

		/// <summary>
		/// 排队信息变化
		/// </summary>
		/// <param name="message"></param>
		/// <param name="slowQueueWarning"></param>
		/// <returns></returns>
		protected override async Task SubmitProgressChangedAsync(string message, bool slowQueueWarning)
		{
			ts.Text = message;
			if (slowQueueWarning)
			{
				ts.ForeColor = Color.Red;
			}

			await base.SubmitProgressChangedAsync(message, slowQueueWarning).ConfigureAwait(true);
		}

		/// <inheritdoc />
		protected override async Task OrderInitSuccessAsync(bool? needVc)
		{
			Gif.SetWaitingImage(pbAnimate);
			if (SubmitOrderWorker.QueueWarning)
			{
				ts.ApplyColorSchema(RowStyleType.PinkRed);
				ts.Image = Properties.Resources.warning_16;
				ts.Text = "继续无视长龙购买彩票。出现异常情况请在选项中关闭“忽略排队人数错误”。";
			}
			else
			{
				ts.ApplyColorSchema(RowStyleType.Blue);
				ts.Image = Properties.Resources.tick_16;
				ts.Text = "准备就绪";
			}

			if (needVc == null)
			{
				await SetAllowOperationAsync(true, true).ConfigureAwait(true);
			}
		}


		protected override async Task PrepareSuccessAsync(bool queueWarning)
		{
			if (IsFormClosed)
				return;

			await base.PrepareSuccessAsync(queueWarning).ConfigureAwait(true);
		}

		/// <summary>
		/// 订单排队成功
		/// </summary>
		/// <returns></returns>
		protected override async Task SubmitOrderSuccessAsync(bool async, string orderid)
		{
			Gif.SetLoadingImage(pbAnimate);
			//vc.EnableAutoVc = false;
			if (async)
			{
				ts.Text = "订单提交成功，正在排队中...";
			}

			await base.SubmitOrderSuccessAsync(async, orderid).ConfigureAwait(true);
		}


		/// <summary>
		/// 返回是否可以忽略验证码
		/// </summary>
		/// <returns></returns>
		protected override async Task<bool> CanSkipVcAsync()
		{
			if (DisableSkipVc)
				return false;

			return await base.CanSkipVcAsync().ConfigureAwait(true);
		}

		protected override async Task<bool> BeginSubmitOrderAsync()
		{
			//DisableSkipVc = true;
			HideError();
			ts.Image = Properties.Resources._16px_loading_1;
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Text = "正在提交订单...";

			return await base.BeginSubmitOrderAsync().ConfigureAwait(true);
		}

		protected override async Task QueueSucceedAsync(string orderid)
		{
			Gif.SetSuccessImage(pbAnimate);
			VerifyCodeBox.EnableAutoVc = false;
			HideError();

			var txt = "订票成功了耶~订单号为 " + orderid.DefaultForEmpty("(不知道..T_T)") + "....赶快去付款哦....";
			ts.Text = txt;
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Image = Properties.Resources.tick_16;

			await base.QueueSucceedAsync(orderid).ConfigureAwait(true);
		}


		protected override async Task PrepareFailed(string error)
		{
			Gif.SetFailedImage(pbAnimate);
			ShowError(error);
			await base.PrepareFailed(error).ConfigureAwait(true);
		}

		protected override Task OnOrderContextInitializeAsync()
		{
			Gif.SetLoadingImage(pbAnimate);
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Text = "正在初始化上下文...";

			return base.OnOrderContextInitializeAsync();
		}

		protected override async Task PrepareOrderAsync()
		{
			ts.Image = Properties.Resources._16px_loading_1;
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Text = "正在准备订单...";

			await base.PrepareOrderAsync().ConfigureAwait(true);
		}

		#region Overrides of OrderSubmitDialogBase

		protected override async Task OnVcLoad()
		{
			ts.Image = Properties.Resources.loading_16;
			ts.ApplyColorSchema(RowStyleType.RoyalBlue);
			ts.Text = "正在努力地拖验证码回来...";
			await base.OnVcLoad().ConfigureAwait(true);
		}

		protected override async Task OnVcLoadComplete()
		{
			Gif.SetWaitingImage(pbAnimate);
			ts.Image = Properties.Resources.tick_16;
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Text = "验证码加载成功，快来玩连连看吧...";

			await base.OnVcLoadComplete().ConfigureAwait(true);
		}

		/// <summary>
		/// 验证码加载失败
		/// </summary>
		protected override async Task OnVcLoadFailed()
		{
			ts.Image = Properties.Resources.warning_16;
			ts.ApplyColorSchema(RowStyleType.Red);
			ts.Text = "即便是使出了吃奶的劲儿，没想到臭不要脸的验证码还是加载失败了...难道是被踢了？";

			await base.OnVcLoadFailed().ConfigureAwait(true);
		}

		protected override async Task OnAutoVcBegin(int count)
		{
			await base.OnAutoVcBegin(count).ConfigureAwait(true);

			ts.Image = Properties.Resources._16px_loading_1;
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Text = $"第{(count)}次自动识别中...";
		}

		protected override async Task OnAutoVcFailed(int count)
		{
			await base.OnAutoVcFailed(count).ConfigureAwait(true);

			ts.Text = $"第{count}次自动识别失败";
			ts.Image = Properties.Resources.block_16;
			ts.ApplyColorSchema(RowStyleType.Red);
		}

		protected override async Task OnAutoVcGiveUp()
		{
			ts.Image = Properties.Resources.warning_16;
			ts.Text = "未能自动识别验证码，请手动输入";
			ts.ApplyColorSchema(RowStyleType.Red);

			await base.OnAutoVcGiveUp().ConfigureAwait(true);
		}


		protected override async Task OnAutoVcSuccess(int count, IVerifyCodeRecognizeResult code)
		{
			ts.Text = "第" + count + "次自动识别成功";
			ts.Image = Properties.Resources.tick_16;
			ts.ApplyColorSchema(RowStyleType.Green);

			await base.OnAutoVcSuccess(count, code).ConfigureAwait(true);
		}


		#endregion
		#region Overrides of OrderSubmitDialogBase

		protected override async Task<bool> OnOrderResubmitAsync(string msg)
		{
			return await base.OnOrderResubmitAsync(msg).ConfigureAwait(true);
		}


		protected override async Task OnCaptchaErrorAsync()
		{
			//一次验证码失败后，禁用自动跳过验证码
			DisableSkipVc = true;
			ts.Image = Properties.Resources.block_16;
			ts.ApplyColorSchema(RowStyleType.Red);
			ts.Text = "验证码错误，请重试";

			//ts.Image = Properties.Resources.block_16;
			//ts.

			if (VerifyCodeBox.EnableAutoVc)
			{
				OnOperationPerformed();
			}

			await base.OnCaptchaErrorAsync().ConfigureAwait(true);

		}

		protected override async Task QueueBeginAsync()
		{
			ts.Image = Properties.Resources._16px_loading_1;
			ts.Text = "正在排队中...";
			ts.ApplyColorSchema(RowStyleType.Blue);

			await base.QueueBeginAsync().ConfigureAwait(true);
		}

		protected override async Task<bool> CheckLoginStateAsync()
		{
			ts.Image = Properties.Resources._16px_loading_1;
			ts.ApplyColorSchema(RowStyleType.Blue);
			ts.Text = "正在检测登录状态....";

			return await base.CheckLoginStateAsync().ConfigureAwait(true);
		}

		protected override async Task<bool> OnReloginAsync()
		{
			ts.Image = Properties.Resources.warning_16;
			ts.ApplyColorSchema(RowStyleType.Red);
			ts.Text = "已经被12306踢出去了...立刻重新登录以反咬他们！";

			return await base.OnReloginAsync().ConfigureAwait(true);

		}

		#endregion

		#region 安全期计算

		string _safeTimeTemplate = "等待安全期，否则可能因洪荒之力过于强大无法提交订单...(约 {0} 秒)";
		string _safeTimeTemplateInSubmit = "正在准备提交订单，还需要等待洪荒之力封印 (约 {0} 秒)";

		protected override async Task SafeTimeTick(bool insubmit, int resetTime)
		{
			ts.Text = (insubmit ? _safeTimeTemplateInSubmit : _safeTimeTemplate).FormatWith((resetTime * 1.0 / 1000).ToString("#0.00"));
			await base.SafeTimeTick(insubmit, resetTime).ConfigureAwait(true);
		}

		protected override Task SafeTimeEnd()
		{
			ts.ApplyColorSchema(RowStyleType.Green);
			ts.Image = Properties.Resources.tick_16;
			ts.Text = "安全期已到...现在提交订单应该不会有意外情况发生了.....";

			return base.SafeTimeEnd();
		}

		/// <summary>
		/// 开始安全期....
		/// </summary>
		/// <returns></returns>
		protected override Task SafeTimeBegin()
		{
			ts.Image = Properties.Resources.alarm_clock;

			return base.SafeTimeBegin();
		}

		#endregion
	}
}
