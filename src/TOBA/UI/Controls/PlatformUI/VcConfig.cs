namespace TOBA.UI.Controls.PlatformUI
{
	using AutoVc;

	using Configuration;

	using Dialogs;
	using Dialogs.Common;

	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using TOBA.Service;

	internal partial class VcConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public VcConfig()
		{
			InitializeComponent();

			if (Program.IsRunning)
			{
				DisplayText = "远程打码";
				Image = Properties.Resources.key_16;
				BigImage = Properties.Resources.onebit_04;
			}
			this.HandleDestroyed += VcConfig_HandleDestroyed;
			btnLogin.Click += btnLogin_Click;
			btnRefresh.Click += btnRefresh_Click;
			lnkUU.LinkClicked += lnkUU_LinkClicked;
		}

		void lnkUU_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Shell.StartUrl(VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.ErrorCodeQueryUrl);
		}

		void VcConfig_HandleDestroyed(object sender, EventArgs ee)
		{
			VerifyCodeRecognizeServiceLoader.StateChanged -= _ext_StateChanged;
		}

		void VcConfig_Load(object sender, System.EventArgs ee)
		{
			var p = ProgramConfiguration.Instance;

			chkEnableLoginVc.AddDataBinding(p, s => s.Checked, s => s.AutoEnterLoginVcCode);
			chkEnableOrderVc.AddDataBinding(p, s => s.Checked, s => s.AutoSubmitOrderVcCode);

			var cfg = AutoVcConfig.Instance;
			txtUserName.AddDataBinding(cfg, s => s.Text, s => s.UserName);
			txtPassword.AddDataBinding(cfg, s => s.Text, s => s.Pasword);
			chkAutoReloadIfNotRecognized.AddDataBinding(cfg, s => s.Checked, s => s.AutoReloadIfNotSuccess);
			iiFailLimit.Value = cfg.MaxGiveupFailed;
			iiFailLimit.ValueChanged += (s, e) => cfg.MaxGiveupFailed = iiFailLimit.Value;
			cbVcConflict.SelectedIndex = (int)cfg.VcResultConflict;
			cbVcConflict.SelectedIndexChanged += (s, e) => cfg.VcResultConflict = (AutoVcConflictResult)cbVcConflict.SelectedIndex;

			RefreshEngineList();
			RefreshStatus();

			VerifyCodeRecognizeServiceLoader.StateChanged += _ext_StateChanged;
			cbEngine.SelectedIndexChanged += (s, e) =>
			{
				if (cbEngine.SelectedIndex == -1)
					return;
				if (cbEngine.SelectedItem == VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine)
				{
					return;
				}

				var engine = cbEngine.SelectedItem as IVerifyCodeRecognizeService;
				VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine = engine;

				if (engine != null)
				{
					engine.UserName = cfg.UserName;
					engine.Password = cfg.Pasword;

					Task.Factory.StartNew(() =>
					{
						engine.DoLogin();
					});
				}
			};
		}

		void RefreshEngineList()
		{
			var engines = AppContext.ExtensionManager.VerifyCodeRecogniseService;
			var currentItems = cbEngine.Items.OfType<IVerifyCodeRecognizeService>().ToArray();

			//add not existing.
			if (cbEngine.Items.Count == 0)
			{
				cbEngine.Items.Add("");
			}
			engines.Except(currentItems).ForEach(s =>
			{
				cbEngine.Items.Add(s);
			});
			if (cbEngine.Items.Count == 1)
			{
				cbEngine.Items[0] = "(没有已安装的远程打码引擎...)";
			}
			else
			{
				cbEngine.Items[0] = "(禁用远程打码)";
			}
			if (cbEngine.Items.Count > 0 && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null)
			{
				cbEngine.SelectedItem = VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine;
			}
			else
			{
				cbEngine.SelectedIndex = 0;
			}
		}

		void _ext_StateChanged(object sender, System.EventArgs e)
		{
			Invoke(new Action(RefreshStatus));
		}

		void btnLogin_Click(object sender, System.EventArgs e)
		{
			var cfg = AutoVcConfig.Instance;
			var engine = VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine;

			engine.UserName = cfg.UserName;
			engine.Password = cfg.Pasword;

			using (var dlg = new YetAnotherWaitingDialog())
			{
				dlg.WorkCallback = () =>
				{
					engine.DoLogin();
					if (engine.IsLoggedIn)
						engine.RefreshScore();
				};
				dlg.ShowDialog();
			}
			RefreshStatus();
		}

		void RefreshStatus()
		{
			var ext = VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine;

			if (ext == null)
			{
				scoreDesc.Text = "(没有活动的远程打码引擎)";
				btnLogin.Enabled = btnRefresh.Enabled = false;
				pState.Visible = false;
				pTip.Visible = true;
				return;
			}

			pTip.Visible = false;
			pState.Visible = true;
			btnLogin.Enabled = true;
			btnRefresh.Enabled = ext.IsLoggedIn;
			if (!ext.IsLoggedIn)
			{
				scoreDesc.Text = string.Format("您尚未登录..{0}", (ext.ErrorCode != 0 ? "错误码：<b>" + ext.ErrorCode + "</b>" : ""));
			}
			else
			{
				scoreDesc.Text = string.Format("您已登录...当前题分：<font color='red'>{0}</font>", (ext.Score > 0 ? ext.Score + "" : "无法获得，错误码：" + ext.ErrorCode));
			}
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			using (var dlg = new YetAnotherWaitingDialog())
			{
				dlg.WorkCallback = () =>
				{
					if (VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.IsLoggedIn)
						VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.RefreshScore();
				};
				dlg.ShowDialog();
			}
			RefreshStatus();
		}

		private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Shell.StartUrl(VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.WebUrl);
		}
	}
}
