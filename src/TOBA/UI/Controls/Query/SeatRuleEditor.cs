using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Popup;

	using System.Diagnostics;

	using TOBA.Entity;
	using TOBA.Entity.SeatCheckRules;

	internal partial class SeatRuleEditor : UserControl
	{
		Popup _popup;
		MinimumTicketCheckRule _rule;

		public Control OwnerControl { get; set; }


		public SeatRuleEditor()
		{
			InitializeComponent();

			ckMinimum.CheckedChanged += CkMinimum_CheckedChanged;
			iEditorMinimumValue.ValueChanged += (s, e) =>
			{
				if (_rule != null)
					_rule.Count = iEditorMinimumValue.Value;
				OnSettingChanged();
			};
			cbEditorMinimumType.SelectedIndexChanged += (s, e) =>
			{
				if (_rule != null)
					_rule.IsLessThan = cbEditorMinimumType.SelectedIndex == 0;
				OnSettingChanged();
			};
			btnClose.Click += (s, e) =>
			{
				(Parent as Popup).Close();
			};
		}

		void CkMinimum_CheckedChanged(object sender, EventArgs e)
		{
			InitEditor();
			OnSettingChanged();
		}

		void InitEditor()
		{
			if (ckMinimum.Checked)
			{
				pContentMinimum.Enabled = true;
				_rule = QueryParam.AutoPreSubmitConfig.SeatCheckRules.GetValue(SeatCode)?.OfType<MinimumTicketCheckRule>().FirstOrDefault();
				if (_rule == null)
				{
					_rule = new MinimumTicketCheckRule();
					QueryParam.AutoPreSubmitConfig.SeatCheckRules.GetValue(SeatCode, _ => new List<ISeatCheckRule>()).Add(_rule);
				}
				cbEditorMinimumType.SelectedIndex = _rule.IsLessThan ? 0 : 1;
				iEditorMinimumValue.Value = _rule.Count;
			}
			else
			{
				pContentMinimum.Enabled = false;
				if (_rule != null)
				{
					QueryParam.AutoPreSubmitConfig.SeatCheckRules.GetValue(SeatCode)?.Remove(_rule);
				}
			}
		}

		/// <summary>
		/// 设置变更
		/// </summary>
		public event EventHandler SettingChanged;

		protected virtual void OnSettingChanged()
		{
			SettingChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Init(Control ownerControl, char code)
		{
			SeatCode = code;
			OwnerControl = ownerControl;

			if (code == '\0' || ownerControl == null)
				return;

			ckMinimum.Checked = QueryParam.AutoPreSubmitConfig.SeatCheckRules.GetValue(SeatCode)?.OfType<MinimumTicketCheckRule>().Any() == true;
			InitEditor();
		}

		public QueryParam QueryParam { get; set; }

		public char SeatCode { get; set; }

		private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://blog.iccfish.com/?p=2720");
		}
	}
}
