using System;

namespace TOBA.UI.Controls.Query
{
	using Data;

	using FSLib.Extension;

	using Popup;

	using System.ComponentModel;
	using System.Reflection;
	using System.Windows.Forms;

	using Win32;

	internal class TrainStation : TextBox, IRequireSessionInit, INotifyPropertyChanged
	{
		/// <summary>
		/// 车站类型（发站还是到站）
		/// </summary>
		public string StationType
		{
			get { return _stationType; }
			set
			{
				if (value == _stationType)
					return;
				_stationType = value;
				OnPropertyChanged("StationType");
			}
		}

		/// <summary>
		/// 获得或设置城市编码
		/// </summary>
		[Bindable(true)]
		[Obfuscation(Exclude = false, Feature = "-rename")]
		public string Code
		{
			get { return _code; }
			set
			{
				if (value == _code)
					return;
				_code = value ?? "";
				OnPropertyChanged("Code");
			}
		}

		//	UI.Dialogs.Query.TrainPrompt _prompt;
		Session _operationContext;

		/// <summary>
		/// 创建 <see cref="TrainStation" />  的新实例(TrainStation)
		/// </summary>
		public TrainStation()
		{
			InitPopup();
		}


		#region 自动提示

		TrainPrompt _promptControl;
		Timer _hideTimer;
		Popup _promptPopup;
		string _stationType;
		string _code;

		/// <summary>
		/// 初始化弹窗
		/// </summary>
		void InitPopup()
		{
			_promptControl = new TrainPrompt() { StationControl = this, Width = Width };
			_promptPopup = new Popup(_promptControl)
			{
				AutoClose = false,
				FocusOnOpen = false,
				ShowingAnimation = PopupAnimations.TopToBottom | PopupAnimations.Slide,
				HidingAnimation = PopupAnimations.BottomToTop | PopupAnimations.Slide
			};
			_hideTimer = new Timer() { Interval = 100 };
			_hideTimer.Tick += (s, e) =>
			{
				if (!_promptPopup.ContainsFocus && !ContainsFocus)
				{
					//自动赋值
					if (!Text.IsNullOrEmpty())
					{
						var st = ParamData.TrainStationLookupByName.GetValue(Text);
						if (st == null)
						{
							//取第一个预选值
							st = _promptControl.SelectedStation;
							if (st == null) Text = "";
							else
							{
								Text = st.Name;
								Code = st.Code;
							}
						}
						else
						{
							Code = st.Code;
							Text = st.Name;
						}
					}
					else
					{
						Code = "";
					}
					_promptPopup.Close();
				}
				_hideTimer.Enabled = false;
			};
			_promptControl.LostFocus += (s, e) => CountDownClosePopup();

		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
			{
				_promptControl.SetFocus();
				SendKeys.Send(e.KeyCode == Keys.Down ? "{DOWN}" : "{UP}");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			CountDownClosePopup();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			CancelClosePopup();
			_promptPopup.Show(this);
			SelectAll();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			SelectAll();
		}

		/// <summary>
		/// 设置依旧保持焦点，阻止关闭弹窗
		/// </summary>
		internal void CancelClosePopup()
		{
			_hideTimer.Enabled = false;
		}

		/// <summary>
		/// 设置开始关闭弹窗
		/// </summary>
		internal void CountDownClosePopup()
		{
			_hideTimer.Enabled = true;
		}

		#region Overrides of TextBox

		/// <summary>
		/// 处理 Windows 消息。
		/// </summary>
		/// <param name="m">一个 Windows 消息对象。</param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x020A)
			{
				UnsafeNativeMethods.SendMessage(_promptControl.GetListIntPtr(), m.Msg, m.WParam, m.LParam);
				m.Result = IntPtr.Zero;
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		#endregion

		#endregion

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session
		{
			get { return _operationContext; }
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			_operationContext = session;
			_promptControl.InitSession(session);
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
