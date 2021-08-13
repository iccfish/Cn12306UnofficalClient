namespace TOBA.UI.Controls.Common
{
	using System;
	using System.ComponentModel;

	/// <summary>
	/// Button对象的扩展
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxItemFilter("System.Windows.Forms")]
	[System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	public class ButtonExtend : System.Windows.Forms.Button
	{
		/// <summary>
		/// 创建 <see cref="ButtonExtend" /> 的新实例
		/// </summary>
		public ButtonExtend()
		{
			this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		}

		private bool _ShowShield;
		/// <summary>
		/// 获得或设置是否在按钮上显示盾牌小图标
		/// </summary>
		public bool ShowShield
		{
			get
			{
				return _ShowShield;
			}
			set
			{
				_ShowShield = value;
				if (_ShowShield && this.IsHandleCreated) UiUtility.AddShieldToButton(this);
			}
		}


		#region 重写的底层函数

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"></see> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (ShowShield)
			{
				UiUtility.AddShieldToButton(this);
			}
		}

		#endregion
	}
}
