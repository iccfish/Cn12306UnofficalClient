using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Configuration;

	using Entity;

	internal partial class QueryConfiguration : ControlBase
	{
		public QueryConfiguration(Session session = null)
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			InitSession(session);
			Load += QueryConfiguration_Load;
		}

		void QueryConfiguration_Load(object sender, EventArgs evg)
		{
			chkOClockRefresh.DataBindings.Add("Checked", QueryParam.AutoPreSubmitConfig, "EnableOClockRefresh", false, DataSourceUpdateMode.OnPropertyChanged);
			chkWaitToHourIfExist.DataBindings.Add("Checked", QueryParam.AutoPreSubmitConfig, "AutoWaitToSell", false, DataSourceUpdateMode.OnPropertyChanged);
			chkEnableSCSLoop.Enabled = QueryParam.IsSameCityStationLoopAvailable;
			chkEnableSCSLoop.Checked = QueryParam.EnableSameCityStationLoop;

			chkIgnoreIllegal.DataBindings.Add(nameof(CheckBox.Checked), QueryParam, nameof(QueryParam.IgnoreIllegalData), false, DataSourceUpdateMode.OnPropertyChanged);

			chkStuAsCommon.Enabled = !QueryParam.QueryStudentTicket;
			chkStuAsCommon.DataBindings.Add(nameof(CheckBox.Checked), QueryParam, nameof(QueryParam.SubmitStudentAsCommon), false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoTrain.DataBindings.Add(nameof(CheckBox.Checked), QueryParam.AutoPreSubmitConfig, nameof(AutoPreSubmitConfiguration.AutoSelectTrain), false, DataSourceUpdateMode.OnPropertyChanged);


			if (ApiConfiguration.Instance.DisableIllegalDetect || !TOBA.Configuration.QueryConfiguration.Current.IgnoreAlmostIllegalResult)
			{
				chkIgnoreIllegal.Visible = false;
			}

			var queryparamChanged = new PropertyChangedEventHandler((_, __) =>
			{
				AppContext.HostForm.Invoke(() =>
				{
					if (__.PropertyName == nameof(QueryParam.IsSameCityStationLoopAvailable))
						chkEnableSCSLoop.Enabled = QueryParam.IsSameCityStationLoopAvailable;
					else if (__.PropertyName == nameof(QueryParam.EnableSameCityStationLoop))
						chkEnableSCSLoop.Checked = QueryParam.EnableSameCityStationLoop;
					else if (__.PropertyName == "QueryStudentTicket")
						chkStuAsCommon.Enabled = !QueryParam.QueryStudentTicket;
				});
			});
			TOBA.Configuration.QueryConfiguration.Current.PropertyChanged += Current_PropertyChanged;
			QueryParam.PropertyChanged += queryparamChanged;
			Disposed += (s, e) =>
			{
				QueryParam.PropertyChanged -= queryparamChanged;
				TOBA.Configuration.QueryConfiguration.Current.PropertyChanged -= Current_PropertyChanged;
			};
		}

		private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(TOBA.Configuration.QueryConfiguration.IgnoreAlmostIllegalResult))
				chkIgnoreIllegal.Visible = !ApiConfiguration.Instance.DisableIllegalDetect && TOBA.Configuration.QueryConfiguration.Current.IgnoreAlmostIllegalResult;
		}

		/// <summary>
		/// 获得或设置查询参数
		/// </summary>
		public QueryParam QueryParam { get; set; }
	}
}
