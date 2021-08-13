using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Data;

	internal partial class TrainPrompt : ControlBase
	{
		public TrainPrompt()
		{
			InitializeComponent();

			lstStations.KeyUp += lstStations_KeyUp;
			lstStations.KeyDown += lstStations_KeyDown;
			lstStations.Click += lstStations_DoubleClick;
			lstStations.LostFocus += lstStations_LostFocus;
		}

		void lstStations_LostFocus(object sender, EventArgs e)
		{
			OnLostFocus(e);
		}

		void lstStations_DoubleClick(object sender, EventArgs e)
		{
			//SendKeys.SendAsync("{ENTER}");
			_stationControl.Focus();
			if (lstStations.SelectedItem != null)
			{
				_stationControl.Text = (lstStations.SelectedItem as Entity.TrainStation).Name;
				_stationControl.Code = (lstStations.SelectedItem as Entity.TrainStation).Code;
			}
			SendKeys.Send("{TAB}");
		}

		void lstStations_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue >= 'A' && e.KeyValue <= 'z')
			{
				//路由到主窗口
				_stationControl.Focus();
				SendKeys.Send(((char)e.KeyValue).ToString());
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Right)
			{
				_stationControl.Focus();
				SendKeys.Send("{RIGHT}");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Left)
			{
				_stationControl.Focus();
				SendKeys.Send("{LEFT}");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Up && lstStations.SelectedIndex == 0)
			{
				_stationControl.Focus();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				_stationControl.Focus();
				_stationControl.Text = (lstStations.SelectedItem as Entity.TrainStation).Name;
				_stationControl.Code = (lstStations.SelectedItem as Entity.TrainStation).Code;
				SendKeys.Send("{TAB}");
				e.Handled = true;
			}
		}

		void lstStations_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private Controls.Query.TrainStation _stationControl;
		/// <summary>
		/// 获得或设置车站控件
		/// </summary>
		public Controls.Query.TrainStation StationControl
		{
			get { return _stationControl; }
			set
			{
				if (_stationControl != null)
				{
					_stationControl.TextChanged -= _stationControl_TextChanged;
				}
				_stationControl = value;
				if (_stationControl != null)
				{
					_stationControl.TextChanged += _stationControl_TextChanged;
				}
				_stationControl_TextChanged(null, null);
			}
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			_stationControl_TextChanged(null, null);

		}

		public IntPtr GetListIntPtr()
		{
			return lstStations.Handle;
		}

		void _stationControl_TextChanged(object sender, EventArgs e)
		{
			if (_stationControl == null || Session == null) return;

			List<Entity.TrainStation> stations;
			SuspendLayout();
			lstStations.BeginUpdate();
			lstStations.Items.Clear();
			if (_stationControl.Text.IsNullOrEmpty())
			{
				var frequency = StationControl.StationType == "from" ? Session.UserProfile.Configuration.UserStationFrequencyFrom : Session.UserProfile.Configuration.UserStationFrequencyTo;

				if (frequency.Count > 0)
				{
					//再插入用户历史记录
					frequency.Select(s => ParamData.TrainStationMap.GetValue(s.Code)).ExceptNull().ForEach(s => lstStations.Items.Add(s));
					if (lstStations.Items.Count > 0)
					{
						lstStations.Items.Add(Entity.TrainStation.EmptyStation);
					}
				}
				//插入热门车站
				ParamData.DefaultCityCode.Select(s => ParamData.TrainStationMap.GetValue(s)).ExceptNull().ForEach(s => lstStations.Items.Add(s));
			}
			else
			{
				//查询
				var query = _stationControl.Text;
				var list = ParamData.TrainStationList.Where(s => s.IsMatch(query)).ToArray();
				Array.Sort(list, (x, y) =>
								{
									if (x.SortOrder == y.SortOrder) return x.CompareTo(y);
									return x.SortOrder - y.SortOrder;
								});
				list.ForEach(s => lstStations.Items.Add(s));
			}
			if (lstStations.Items.Count > 0)
			{
				lstStations.SelectedIndex = 0;
			}
			lstStations.EndUpdate();
			ResumeLayout();
		}

		/// <summary>
		/// 设置焦点
		/// </summary>
		public void SetFocus()
		{
			lstStations.Focus();
		}

		/// <summary>
		/// Gets the selected station.
		/// </summary>
		/// <value>
		/// The selected station.
		/// </value>
		public Entity.TrainStation SelectedStation
		{
			get { return lstStations.SelectedItem as Entity.TrainStation; }
		}
	}
}
