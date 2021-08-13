using DevComponents.DotNetBar;

using System;
using System.Drawing;

using TOBA.Configuration;
using TOBA.UI.Dialogs;

namespace TOBA.UI.Controls.Option
{
	internal partial class ThemeConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		StyleManager _sm;

		public ThemeConfig()
		{
			InitializeComponent();

			if (Program.IsRunning)
			{
				if (MainForm.Instance == null)
				{
					Enabled = false;
				}
				else
				{
					_sm = MainForm.Instance.StyleManager;
					Load += ThemeConfig_Load;
				}
			}
		}

		void ThemeConfig_Load(object sender, EventArgs e)
		{
			var names = Enum.GetNames(typeof(eStyle));
			var current = _sm.ManagerStyle.ToString();

			cb.Items.AddRange(names);
			cb.SelectedItem = current;
			cb.SelectedValueChanged += (x, y) =>
			{
				_sm.ManagerStyle = (eStyle)Enum.Parse(typeof(eStyle), cb.SelectedItem.ToString());
				_sm.ResetManagerColorTint();
				cp.SelectedColor = _sm.ManagerColorTint;
				ProgramConfiguration.Instance.GlobalColorHint = null;
				ProgramConfiguration.Instance.DnbGlobalStyle = _sm.ManagerStyle;
			};

			cp.SelectedColor = ProgramConfiguration.Instance.GlobalColorHint.HasValue ? ProgramConfiguration.Instance.GlobalColorHint.Value : Color.White;
			cp.ColorPreview += (s, x) =>
			{
				_sm.ManagerColorTint = x.Color;
			};
			cp.SelectedColorChanged += (s, x) =>
			{
				_sm.ManagerColorTint = cp.SelectedColor;
				ProgramConfiguration.Instance.GlobalColorHint = _sm.ManagerColorTint;
			};
			cp.PopupClose += (x, y) =>
			{
				if (ProgramConfiguration.Instance.GlobalColorHint == null)
					_sm.ResetManagerColorTint();
				else _sm.ManagerColorTint = ProgramConfiguration.Instance.GlobalColorHint.Value;
			};

			cbp.Items.AddRange(new object[] { "自动选择(推荐)", "左侧", "右侧", "顶部", "底部" });
			cbp.SelectedIndex = ProgramConfiguration.Instance.MainTabPosition.HasValue ? ((int)ProgramConfiguration.Instance.MainTabPosition.Value + 1) : 0;
			cbp.SelectedIndexChanged += (x, y) =>
			{
				if (cbp.SelectedIndex == 0)
					ProgramConfiguration.Instance.MainTabPosition = null;
				else ProgramConfiguration.Instance.MainTabPosition = (eTabStripAlignment)(cbp.SelectedIndex - 1);
			};

			btnResetColorHint.Click += (x, y) =>
			{
				_sm.ResetManagerColorTint();
				cp.SelectedColor = _sm.ManagerColorTint;
				ProgramConfiguration.Instance.GlobalColorHint = null;
			};
		}
		#region Overrides of UserControl

		/// <returns>
		/// 与该控件关联的文本。
		/// </returns>
		public override string DisplayText
		{
			get { return "主题设置"; }
		}


		/// <summary>
		/// 获得32PX大图像
		/// </summary>
		public override Image BigImage
		{
			get { return Properties.Resources.testtube_32; }
		}


		/// <summary>
		/// 获得图片
		/// </summary>
		public override Image Image
		{
			get { return Properties.Resources.testtube_16; }
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{

			return base.Save();
		}

		#endregion
	}
}
