using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Account
{
	using DevComponents.DotNetBar;

	using System.Threading.Tasks;

	using TOBA.Account;

	internal partial class AccountMobileCheck : Office2007Form, IRequireSessionInit
	{
		BindMobileService _service;

		public AccountMobileCheck(Session session)
		{
			InitializeComponent();
			Icon = Properties.Resources.icon_mobile;
			InitSession(session);

			txtMobile.Text = session.UserKeyData.MobileNumber;
			btnModify.Click += (s, e) =>
			{
				using (var dlg = new ModifyMobileCode(session))
				{
					if (dlg.ShowDialog(this) != DialogResult.OK)
						return;

					txtMobile.Text = dlg.MobileNo;
				}

				Refresh();
			};

			btnGetCode.Click += async (s, e) =>
			{
				btnGetCode.Enabled = false;

				ToastNotification.Show(this, "正在请求发送验证码...", Properties.Resources.xfsm_switch, -1);
				var success = await _service.GetMobileCodeAsync(txtMobile.Text);
				ToastNotification.Close(this);

				if (success.IsNullOrEmpty())
				{
					DelayButton();
				}
				else
				{
					ToastNotification.Show(this, $"操作失败：{success}", Properties.Resources.cou_16_block);
				}
			};

			btnApply.Click += async (s, e) =>
			{
				if (txtCode.Text.IsNullOrEmpty())
				{
					ToastNotification.Show(this, "请输入验证码", Properties.Resources.cou_16_block);
					return;
				}

				btnApply.Enabled = false;

				ToastNotification.Show(this, "正在提交核验...", Properties.Resources.xfsm_switch, -1);
				var success = await _service.CheckMobileCodeAsync(txtMobile.Text, txtCode.Text);
				ToastNotification.Close(this);

				if (success.IsNullOrEmpty())
				{
					this.Information("手机号核验成功！");
					DialogResult = DialogResult.OK;

					Close();
					return;
				}
				ToastNotification.Show(this, $"操作失败：{success}", Properties.Resources.cou_16_block);
				btnApply.Enabled = true;
			};
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; private set; }

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			Session = session;
			UiUtility.AttachContext(this, session);
			_service = new BindMobileService(session);
		}

		private void AccountMobileCheck_Load(object sender, EventArgs e)
		{
			Refresh();
		}

		async void Refresh()
		{
			btnApply.Enabled = false;
			btnGetCode.Enabled = false;
			btnModify.Enabled = false;
			btnGetCode.Visible = false;

			ToastNotification.Show(this, "正在刷新手机号核验状态，请稍等...", Properties.Resources.xfsm_switch, -1);
			if (!await _service.RefreshStatusAsync())
			{
				ToastNotification.Close(this);
				this.Information("未能刷新状态，请重试。");
				Close();
			}
			ToastNotification.Close(this);

			if (Session.IsMobileChecked == true)
			{
				lblStatus.Text = "已通过核验";
				lblStatus.ForeColor = Color.Green;

				return;
			}
			lblStatus.ForeColor = Color.Red;
			lblStatus.Text = "未通过核验或状态未知";
			btnModify.Enabled = true;

			ToastNotification.Show(this, "正在初始化手机号核验，请稍等...", Properties.Resources.xfsm_switch, -1);

			var result = await _service.GetMobileCheckIsDoubleAsync();
			ToastNotification.Close(this);
			if (result == null)
			{
				this.Information("貌似出现了网络错误，请重试。");
				Close();
			}

			if (result == true)
			{
				pDesc.Text = "当前验证模式为双向验证模式。<br />请使用您的手机编辑短信 <font color='red'><b>999</b></font> 至 <font color='red'><b>12306</b></font>，<br />在12306回复您的短信后，将收到的短信验证码填入下面的编辑框中。";
			}
			else
			{
				btnGetCode.Visible = true;
			}

			btnApply.Enabled = true;
		}

		async void DelayButton()
		{
			var count = 120;
			btnGetCode.Enabled = false;

			while (count-- >= 0)
			{
				btnGetCode.Text = $"{count}";
				await Task.Delay(1000);
			}

			btnGetCode.Text = "";
			btnGetCode.Enabled = true;
		}
	}
}
