using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

using TOBA.Configuration;
using TOBA.Service;

namespace TOBA.UI.Dialogs.Order
{
	using Data;

	using Entity;

	using TOBA.Order;
	using TOBA.Query.Entity;
	using TOBA.WebLib;

	internal partial class FastSubmitOrder : OrderSubmitDialogUiBase
	{
		QueueOrderWorker _queueWorker;
		SubmitOrderBase _submitWoker;

		public FastSubmitOrder(Session session, QueryResultItem train, char seat, IEnumerable<PassengerInTicket> passengers, QueryParam queryParam, bool byAuto)
			: base(session, train, seat, passengers, queryParam, byAuto)
		{
			InitializeComponent();

			//vc.InitSession(session);
			tc.InitSession(session);
			tc.ResizeParent(this, ProgramConfiguration.Instance.CaptchaZoom, ClientSize.Width - tc.Width - tc.Location.X - 10 - 10);

			//VerifyCodeBox = vc;
			AttachVerifyCodeControl(tc);
			VerifyCodeBox.RandType = RandCodeType.Randp;
			//imglist.Images.Add(FSLib.Windows.Utility.Get24PxImageFrom16PxImg(Properties.Resources.user_16));

			PostInitialize();

			tc.EnableAutoVc = ProgramConfiguration.Instance.AutoSubmitOrderVcCode && Query.EnableAutoVC && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine?.IsLoggedIn == true;

			//TODO: 快速下单模式
			_submitWoker = Query.Resign || Query.TemporaryDisableFastSubmitOrder || !ApiConfiguration.Instance.EnableAutoSubmitAPI ? (SubmitOrderBase)new SubmitOrderWorker(Session, Train, Query) { Passengers = PassengerInTickets.ToArray() } : new AutoSubmitWorker(Session, Train, Query, PassengerInTickets.ToArray()) { };
			_queueWorker = new QueueOrderWorker(Session)
			{
				TourFlag = "dc"
			};

			AttachWorker(_submitWoker, _queueWorker);

			Load += async (x, y) =>
			{
				await PrepareOrderAsync().ConfigureAwait(true);
			};
		}

		protected override void HideError()
		{
			lblErrMsg.Visible = false;
		}

		protected override void InitTrainInfoDisplay()
		{
			lblError.Text = PassengerInTickets.Select(s => $"<b><font color=\"#049B59\">{s.Name}</font></b> ({ParamData.GetTicketType(s.TicketType).DisplayName}/{ParamData.SeatTypeFull[Seat]}/{ParamData.PassengerIdType[s.IdType]})").JoinAsString(" ");

			base.InitTrainInfoDisplay();
		}

		protected override void ShowError(string msg)
		{
			lblErrMsg.ForeColor = Color.Red;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = msg;
		}

		protected override void ShowTip(string tip)
		{
			lblErrMsg.ForeColor = Color.SteelBlue;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = tip;
		}
	}
}
