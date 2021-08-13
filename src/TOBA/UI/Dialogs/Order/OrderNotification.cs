using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TOBA.Entity;
using TOBA.Query.Entity;

namespace TOBA.UI.Dialogs.Order
{

	using Data;

	using FSLib.Network.Http;

	using System.Threading.Tasks;
	using System.Web;

	using TOBA.Configuration;
	using TOBA.MailNotification;

	using WebNotification;

	internal partial class OrderNotification : DialogBase
	{
		private readonly QueryParam _query;
		private readonly QueryResultItem _train;
		private readonly IEnumerable<PassengerInTicket> _passengers;
		private readonly string _error;
		private readonly string _orderid;

		public OrderNotification(Session session, QueryParam query, QueryResultItem train, IEnumerable<PassengerInTicket> passengers, string error, string orderid)
		{
			_query = query;
			_train = train;
			_passengers = passengers;
			_error = error;
			_orderid = orderid;
			InitializeComponent();

			InitSession(session);
			Icon = Properties.Resources.icon_warning;
			imglist.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.user_16));

			lblTrainCode.Text = train.Code;
			lblDate.Text = train.FromStation.DepartureTime.Value.ToString("M月d日");
			lblFrom.Text = train.FromStation.StationName;
			lblTo.Text = train.ToStation.StationName;
			lblTimeInfo.Text = lblTimeInfo.Text.FormatWith(
				train.FromStation.DepartureTime.Value.ToString("HH:mm"),
				train.ToStation.ArriveTime.Value.ToString("HH:mm"),
				train.ElapsedTime.ToString("dd'天'hh'时'mm'分'"));

			if (passengers != null)
			{
				lstPas.Items.AddRange(passengers.Select(s => new ListViewItem(new[]
			{
				ParamData.GetTicketType(s.TicketType).DisplayName,
				ParamData.SeatTypeFull[s.SeatType],
				s.Name,
				ParamData.PassengerIdType[s.IdType],
				s.IdNo
			})
				{ ImageIndex = 0 }).ToArray());
			}

			if (string.IsNullOrEmpty(error))
			{
				Gif.SetSuccessImage(pbAnimate);
				lblError.ForeColor = Color.RoyalBlue;
				lblError.Text = "订票成功！订单号为：" + orderid.DefaultForEmpty("<未知>") + "，请尽快付款！如果订单没有立刻查询到，请刷新订单列表！";

				btnOK.Click += btnOK_Click;
				btnOK.Text = "乖~~戳我去付款啦~~要截图留念请速度并打码哦 ლ(^o^ლ)　";

				Operation.Instance.PlayMusicForSuccess();
			}
			else
			{
				lblError.Text = error;
				btnOK.Text = "同学不哭，站起来擦干眼泪继续刷 ლ(╹◡╹ლ)";
				Gif.SetFailedImage(pbAnimate);
				btnOK.Click += (s, e) => Close();

				if (error.IndexOf("未完成") != -1)
				{
					session.OnRequestShowPanel(PanelIndex.Orders);
					session.OnOrderChanged();
				}
			}

			if (ProgramConfiguration.Instance.OrderDlgCenterMainform)
			{
				UiUtility.PlaceFormAtCenter(this, show: false);
			}
			else
			{
				StartPosition = FormStartPosition.CenterScreen;
			}
			FormPlacementManager.Instance.Control(this);

			Load += async (s, e) =>
					{
						WindowState = FormWindowState.Normal;
						BringToFront();
						Activate();

						if (_error.IsNullOrEmpty())
						{
							await CheckNotification().ConfigureAwait(true);
							await SendWebNotification().ConfigureAwait(true);
						}
					};
		}

		void btnOK_Click(object sender, EventArgs e)
		{
			//付款
			MainForm.Instance.SelectedSession = Session;
			Session.OnRequestShowPanel(PanelIndex.Orders);
			Session.OnOrderChanged();
			Close();
		}

		async Task SendWebNotification()
		{
			var preText = tipEx.Text + "\n";
			var wnc = WebNotifyConfiguration.Instance;
			if (!wnc.Enabled)
				return;

			tipEx.Visible = true;
			tipEx.Text = preText + "(正在发送WEB通知...)";

			if (wnc.UrlTemplate.IsNullOrEmpty())
			{
				tipEx.Text = "客官，你选择了发送WEB通知，但是设置真的有点儿不对劲。";
				return;
			}

			var notier = new WebNotifier();
			var url = ReplaceTemplate(wnc.UrlTemplate, true);
			var data = ReplaceTemplate(wnc.Body ?? "", wnc.RequestContentType == ContentType.FormUrlEncoded);

			var result = await notier.SendAsync(url, data).ConfigureAwait(true);
			if (result.IsNullOrEmpty())
			{
				tipEx.ForeColor = Color.Green;
				tipEx.Text = preText + "(WEB通知发送成功)";
			}
			else
			{
				tipEx.Text = preText + $"(WEB通知发送失败：{result})";
				tipEx.ForeColor = Color.Red;
			}
		}

		async Task CheckNotification()
		{
			var cfg = MailConfiguration.Instance;
			//未开启通知，跳过
			if (!cfg.Enabled)
				return;

			var targetMail = cfg.Receivers ?? new[] { cfg.LoginCredential };
			if ((targetMail?.Length ?? 0) == 0)
			{
				tipEx.Text = "邮件发送失败：未设置收件人邮箱";
				tipEx.ForeColor = Color.Red;
				return;
			}

			var targetMailDesc = targetMail.JoinAsString(";");

			tipEx.Text = "正在通知邮件：" + targetMailDesc;
			tipEx.Visible = true;
			lt.Visible = true;
			lt.Text = "正在发送邮件通知...";

			//发送邮件
			var title = GetMailTitle();
			var body = GetMailBody();
			var success = false;
			var err = "";

			if (!success && cfg.Enabled)
			{
				err = await MailSender.Instance.SendEmailAsync(title, body, targetMail).ConfigureAwait(true);
				success |= err.IsNullOrEmpty();
			}

			lt.Visible = false;
			if (success)
			{
				tipEx.Text = $"已经成功发送邮件通知到：{targetMailDesc}";
				tipEx.ForeColor = Color.Green;
			}
			else
			{
				tipEx.Text = $"邮件发送失败：{err}，目标邮箱：{targetMailDesc}";
				tipEx.ForeColor = Color.Red;
			}
		}

		public string GetMailTitle()
		{
			return ReplaceTemplate(MailConfiguration.Instance.Title);
		}
		public string GetMailBody()
		{
			return ReplaceTemplate(MailConfiguration.Instance.Body);
		}

		public string ReplaceTemplate(string template, bool encode = false)
		{
			string EncodeStr(string str)
			{
				if (encode)
					return HttpUtility.UrlEncode(str);
				return str;
			}

			return template.TemplateTagReplace(new[] { "$date$", "$from$", "$to$", "$code$", "$order$", "$acc$", "$pas$", "$ordertime$", "$paytime$" },
				new[]
				{
					_query.CurrentDepartureDate.ToShortDateString(), _train.FromStation.StationName,
					_train.ToStation.StationName,
					_train.Code,
					_orderid,
					Session.UserName,
					_passengers?.Select(s => s.Name).JoinAsString(";"),
					DateTime.Now.ToString("HH时mm分"),
					DateTime.Now.AddMinutes(30).ToString("HH时mm分")
				}.Select(EncodeStr).ToArray());
		}
	}
}
