using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Common;

	using Data;

	using Entity;

	using Popup;

	using TOBA.Query;
	using TOBA.Query.Entity;

	internal partial class TrainStopQuery : ControlBase
	{
		DataGridViewButtonExtendColumn _colAddQuery;
		int _first, _end;

		private QueryParam _query;
		private DataGridViewTextBoxCell _sellTimeCell;

		public TrainStopQuery(Session session)
		{
			InitializeComponent();

			InitSession(session);

			MouseEnter += (s, e) =>
			{
				ContainsMouse = true;
			};
			MouseLeave += (s, e) => ContainsMouse = false;
			dgv.DataBindingComplete += (s, e) =>
			{
				CheckQueryExist();
			};
			dgv.CellContentClick += dgv_CellContentClick;
		}

		void CheckQueryExist()
		{
			dgv.Rows.Cast<DataGridViewRow>().ForEach(s =>
			{
				var item = s.DataBoundItem as TrainStopInfo;

				//起售时间
				var station = ParamData.TrainStationLookupByName.GetValue(item.StationName);
				_sellTimeCell = ((DataGridViewTextBoxCell)s.Cells[2]);
				if (station != null)
				{
					var sellTime = ParamData.SellTimeMap?.GetValue(station.Code);
					if (sellTime != null)
					{
						_sellTimeCell.Value = sellTime;
					}
					else
					{
						_sellTimeCell.Value = "<无>";
					}
				}
				else
				{
					_sellTimeCell.Value = "<无此站>";
				}


				if (Query != null && Session?.IsLogined == true)
				{
					//可用？
					if (item.StationName == Train.FromStation.StationName || item.StationName == Train.ToStation.StationName)
					{

						s.Cells.Cast<DataGridViewCell>().ForEach(x => x.Style.ForeColor = Color.Red);
						if (Query == null || dgv.Columns.Count >= 7)
							(s.Cells[6] as DataGridViewButtonExtendCell).ButtonVisible = false;
					}

					if (Session.UserProfile.QueryParams.Any(x => Query.DepartureDate.Date == x.DepartureDate.Date && (Utility.IsStationInclude(item.StationName, x.FromStationName) || Utility.IsStationInclude(x.ToStationName, item.StationName))))
					{
						//不管包含了起始还是终点，均假设不需要再设为查询了
						if (dgv.Columns.Count >= 7)
							(s.Cells[6] as DataGridViewButtonExtendCell).Enabled = false;
					}
				}
				//else
				//{
				//	(s.Cells[5] as DataGridViewButtonExtendCell).ButtonVisible = false;
				//}

				if (!item.IsEnabled)
				{
					//不到的站
					s.Cells.Cast<DataGridViewCell>().ForEach(x => x.Style.ForeColor = Color.Gray);
				}
			});
		}

		void CreateNewQuery(string from, string to, DateTime depDate)
		{
			var q = Query.Clone() as QueryParam;
			q.FromStationName = from;
			q.FromStationCode = ParamData.TrainStationLookupByName.GetValue(from).SelectValue(s => s.Code);
			q.ToStationCode = ParamData.TrainStationLookupByName.GetValue(to).SelectValue(s => s.Code);
			q.ToStationName = to;
			q.DepartureDate = depDate;

			if (q.FromStationCode.IsNullOrEmpty() || q.ToStationCode.IsNullOrEmpty())
			{
				Information("很抱歉，未能找到对应站点的电报编码，请手动创建查询。");
				return;
			}


			q.IsLoaded = true;
			Session.UserProfile.QueryParams.Add(q);
			//q.OnRequireSave();
		}

		void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			var columnIndex = e.ColumnIndex;
			var rowIndex = e.RowIndex;

			if (rowIndex < 0)
				return;

			var row = dgv.Rows[rowIndex];
			var data = Train.TicketStationInfo[rowIndex];
			var depDate = ((DateTime)((row.Cells[4] as DataGridViewTextBoxCell).Value)).Date;

			if (columnIndex == 6)
			{
				if (!(row.Cells[6] as DataGridViewButtonExtendCell).Enabled) return;
				if (rowIndex < _first)
				{
					//开始-结束
					CreateNewQuery(data.StationName, Query.ToStationName, depDate);
				}
				else if (rowIndex > _end)
				{
					CreateNewQuery(Query.FromStationName, data.StationName, Query.DepartureDate);
				}
				else if (rowIndex != _first && rowIndex != _end)
				{
					//中间
					if (Question("亲，车站【" + data.StationName + "】是不是出发站？点是将会新建查票【" + data.StationName + " -> " + Query.ToStationName + "】，点否将会新建查票【" + Query.FromStationName + " -> " + data.StationName + "】....", true))
					{
						//开始-结束
						CreateNewQuery(data.StationName, Query.ToStationName, depDate);
					}
					else
					{
						CreateNewQuery(Query.FromStationName, data.StationName, Query.DepartureDate);
					}
				}
				(Parent as Popup).Close();
			}
		}

		void LoadStopInfo()
		{
			dgv.DataSource = Train.TicketStationInfo;
			//查找起始和终点
			_first = Train.TicketStationInfo.FindIndex(s => s.IsEnabled);
			_end = Train.TicketStationInfo.FindLastIndex(s => s.IsEnabled);

			var hasAc = Train.TicketStationInfo.Any(s => s.ServiceType == 1) ? " 有空调" : "";
			var className = Train.TicketStationInfo.FirstOrDefault(s => !s.TrainClassName.IsNullOrEmpty())?.TrainClassName ?? "";
			pInfo.Text += $" {className}  {hasAc}";
			pTools.Enabled = true;
		}

		void ReloadColumn()
		{
			var shouldHaveColumn = Session?.IsLogined == true && Query != null;

			if (!(shouldHaveColumn ^ _colAddQuery != null))
			{
				return;
			}

			if (shouldHaveColumn)
			{
				_colAddQuery = new DataGridViewButtonExtendColumn();
				_colAddQuery.HeaderText = "";
				_colAddQuery.Name = "colAddQuery";
				_colAddQuery.ReadOnly = true;
				_colAddQuery.Text = "创建查询";
				_colAddQuery.UseColumnTextForButtonValue = true;
				_colAddQuery.Width = 70;
				dgv.Columns.Add(_colAddQuery);
			}
			else
			{
				dgv.Columns.Remove(_colAddQuery);
				_colAddQuery = null;
			}
		}

		void worker_LoadComplete(object sender, EventArgs e)
		{
			var worker = sender as QueryTrainStopInfoWorker;
			worker.LoadComplete -= worker_LoadComplete;
			worker.LoadFailed -= worker_LoadFailed;

			pLoading.Visible = false;
			Train.TicketStationInfo = worker.Result;
			LoadStopInfo();
		}

		void worker_LoadFailed(object sender, EventArgs e)
		{
			var worker = sender as QueryTrainStopInfoWorker;
			worker.LoadComplete -= worker_LoadComplete;
			worker.LoadFailed -= worker_LoadFailed;

			pLoading.ForeColor = Color.Red;
			lblStatus.Text = "查询失败...o(>_<)o ~~";
			pictureBox1.Image = Properties.Resources.block_16;
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			ReloadColumn();
		}

		public void ShowTrain(QueryResultItem train)
		{
			Train = train;
			pTools.Enabled = false;
			if (train == null)
			{
				pLoading.Visible = false;
				dgv.DataSource = null;
				return;
			}

			dgv.DataSource = null;

			pInfo.Text = $"<b><font color='Red'>{train.Code}</font></b> <b>{train.StartStation.StationName}</b>-&gt;<b>{train.EndStation.StationName}</b>   ";
			if (train.IsTicketStationInfoLoaded)
			{
				LoadStopInfo();
			}
			else
			{
				var worker = new QueryTrainStopInfoWorker
				{
					Session = Session
				};
				worker.LoadComplete += worker_LoadComplete;
				worker.LoadFailed += worker_LoadFailed;

				pLoading.ForeColor = SystemColors.ControlText;
				pictureBox1.Image = Properties.Resources._16px_loading_1;
				lblStatus.Text = "正在查询中...ヽ(≧Д≦)ノ";
				pLoading.BringToFront();
				pLoading.Show();

				//获得数据
				worker.Query(train);
			}
		}

		public bool ContainsMouse { get; private set; }

		public QueryParam Query
		{
			get { return _query; }
			set
			{
				_query = value;
				ReloadColumn();
			}
		}

		/// <summary>
		/// 获得当前正在显示的车次
		/// </summary>
		public QueryResultItem Train { get; private set; }
	}
}
