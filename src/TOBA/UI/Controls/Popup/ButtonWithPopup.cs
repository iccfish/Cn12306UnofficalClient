namespace TOBA.UI.Controls.Popup
{
	using Entity;

	using FSLib.Extension;

	using System;
	using System.Windows.Forms;

	/// <summary>
	/// 点击会触发弹出面板的按钮
	/// </summary>
	public class ButtonWithPopup : System.Windows.Forms.Button
	{
		/// <summary>
		/// 创建 <see cref="ButtonWithPopup" />  的新实例(ButtonWithPopup)
		/// </summary>
		public ButtonWithPopup()
		{
			ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
			HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop;
			AnimationDuration = 100;
		}

		/// <summary>
		/// 显示动画
		/// </summary>
		public PopupAnimations ShowingAnimation { get; set; }

		/// <summary>
		/// 隐藏动画
		/// </summary>
		public PopupAnimations HidingAnimation { get; set; }

		/// <summary>
		/// 获得或设置动画时间
		/// </summary>
		public int AnimationDuration { get; set; }

		/// <summary>
		/// 获得或设置要弹出的控件
		/// </summary>
		public Control PopupControl { get; set; }

		/// <summary>
		/// 获得当前的弹窗控件
		/// </summary>
		public Popup Popup
		{
			get
			{
				if (_popup == null && PopupControl != null)
				{
					_popup = new Popup(PopupControl)
					{
						ShowingAnimation = ShowingAnimation,
						HidingAnimation = HidingAnimation,
						AnimationDuration = AnimationDuration
					};
					OnInitPopup(new GeneralEventArgs<Popup>(_popup));
				}
				return _popup;
			}
		}

		/// <summary>
		/// 初始化弹窗时触发
		/// </summary>
		public event EventHandler<GeneralEventArgs<Popup>> InitPopup;

		/// <summary>
		/// 引发 <see cref="InitPopup" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnInitPopup(GeneralEventArgs<Popup> ea)
		{
			var handler = InitPopup;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 正在弹出
		/// </summary>
		public event EventHandler<RequireCancelEventArgs> Popuping;

		/// <summary>
		/// 引发 <see cref="Popuping" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnPopuping(RequireCancelEventArgs ea)
		{
			var handler = Popuping;
			if (handler != null)
				handler(this, ea);
		}

		#region Overrides of Button

		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnClick(EventArgs e)
		{
			Popup.Show(this);

			base.OnClick(e);
		}

		#endregion

		Popup _popup;
	}
}
