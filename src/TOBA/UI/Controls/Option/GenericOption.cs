using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Configuration;

	using Dialogs;

	internal partial class GenericOption : OptionConfigForm.AbstractOptionConfigUI
	{
		public GenericOption()
		{
			InitializeComponent();

			Load += GenericOption_Load;
		}

		void GenericOption_Load(object sender, EventArgs e)
		{
			var c = ProgramConfiguration.Instance;

			txtPasswordChar.Text = c.PasswordChar.ToString();
			chkMinimizeToTray.AddDataBinding(c, s => s.Checked, s => s.MinimizeToTray);
			chkAutoRelogin.AddDataBinding(c, s => s.Checked, s => s.AutoRelogin);
			chkAutoRelogin.Visible = RunTime.IsInProfessonalMode;

			nudCheckLoginStateInterval.Value = NetworkConfiguration.Current.CheckLoginStateInterval;
			chkNotifyIfNotMobileChecked.AddDataBinding(c, s => s.Checked, s => s.NotifyIfMobileNotChecked);
			nudCheckLoginStateInterval.ValueChanged += (s, ee) =>
			{
				NetworkConfiguration.Current.CheckLoginStateInterval = (int)nudCheckLoginStateInterval.Value;
			};

			chkConflictLogin.AddDataBinding(c, s => s.Checked, s => s.EnableConflictLogin);
			nudMaxTryReloginCount.IntValue = c.MaxReloginCount;
			nudMaxTryReloginCount.IntValueChanged += (x, y) => c.MaxReloginCount = nudMaxTryReloginCount.IntValue;
			chkAutoShowLoginDlg.AddDataBinding(c, s => s.Checked, s => s.AutoShowLoginDialog);
			chkKeepQuery.AddDataBinding(c, s => s.Checked, s => s.KeepQueryStateAfterRestart);
			chkKeepLogin.AddDataBinding(c, s => s.Checked, s => s.KeepLoginStateAfterRestart);
			chkAutoSendSms.AddDataBinding(c, s => s.Checked, s => s.AutoSendLoginVerifySms);

			chkConflictLogin.Enabled = false;
		}

		#region Overrides of UserControl

		/// <returns>
		/// 与该控件关联的文本。
		/// </returns>
		public override string DisplayText
		{
			get { return "常规选项"; }
		}


		/// <summary>
		/// 获得32PX大图像
		/// </summary>
		public override Image BigImage
		{
			get { return Properties.Resources.onebit_01; }
		}


		/// <summary>
		/// 获得图片
		/// </summary>
		public override Image Image
		{
			get { return Properties.Resources.home_16; }
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			if (txtPasswordChar.Text.Length > 0)
				Configuration.ProgramConfiguration.Instance.PasswordChar = txtPasswordChar.Text[0];

			return base.Save();
		}

		#endregion
	}
}
