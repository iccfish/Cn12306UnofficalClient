using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TOBA.Entity;
using TOBA.Workers;

namespace TOBA.UI.Controls.Query
{
	using Data;

	internal partial class SellTimeTip : ControlBase, IOperation
	{
		public SellTimeTip()
		{
			InitializeComponent();
		}

		QueryParam _query;

		public void AttachQuery(QueryParam query)
		{
			_query = query;
			_query.PropertyChanged += _query_PropertyChanged;
			RefreshSellTimeInfo();
		}

		void _query_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!e.IsPropertyOf(_query, s => s.DepartureDate) && !e.IsPropertyOf(_query, s => s.FromStationCode) && !e.IsPropertyOf(_query, s => s.QueryStudentTicket))
				return;
			RefreshSellTimeInfo();
		}

		void RefreshSellTimeInfo()
		{
			pictureBox2.Image = Properties.Resources.tick_16;
			BackColor = SystemColors.Window;
			ForeColor = SystemColors.WindowText;

			var code = _query.FromStationCode;
			var date = _query.DepartureDate;

			if (string.IsNullOrEmpty(code) || ParamData.SellTimeMap == null)
			{
				desc.Text = "欢迎使用<b>订票助手.NET</b>";
				return;
			}

			//是否是学生票区间？
			DateTime? beginSellDay = null;
			if (_query.QueryStudentTicket)
			{
				if (!ParamData.IsDateInStudentRange(date))
				{
					BackColor = Color.FromArgb(0xFF, 0xD6, 0xCF);
					ForeColor = Color.DarkRed;
					desc.Text = "<font color='DarkRed'>您所选的日期不在学生票的区间中，不可购买学生票</font>";
					pictureBox2.Image = Properties.Resources.warning_16;

					return;
				}
				beginSellDay = ParamData.GetBeginSellTime(date, _query.QueryStudentTicket);

				if (beginSellDay.Value <= DateTime.Now.Date)
					beginSellDay = null;
			}
			else
			{
				var startDate = ParamData.GetBeginSellTime(date);

				if (startDate > DateTime.Now)
				{
					beginSellDay = startDate;
				}
			}

			pictureBox2.Image = Properties.Resources.clock_16;
			desc.Text = "<i>正在检索站点起售信息...</i>";
			var task = Task.Factory.StartNew(() => SameCityStationResolveWorker.Instance.Resolve(code));
			task.ContinueWith(_ => LoadSellTimes(beginSellDay, _.Result));
		}

		void LoadSellTimes(DateTime? day, HashSet<string> list)
		{
			if (IsDisposed)
				return;

			if (InvokeRequired)
			{
				AppContext.HostForm.Invoke(LoadSellTimes, day, list);
				return;
			}

			if (list == null)
			{
				pictureBox2.Image = Properties.Resources.cou_16_warning;
				desc.Text = "<i>检索失败..</i>";
			}
			else
			{
				var selltimes = GetSellTimeList(list);
				if (selltimes == null)
				{
					desc.Text = "查不到站点信息";
					return;
				}

				var selltimedesc = "车票起售时间】" + (day.HasValue ? "<b>" + day.Value.ToString("yyyy年MM月dd日") + "</b> " : "") + (selltimes.Count == 0 ? "<i>未知</i>" : string.Join(" ", selltimes.Select(s => "<b><font color='royalblue'>" + s.Item1 + "</font></b> <font color='green'><u>" + Translate24Hto12H(s.Item2) + "</u></font>")));

				if (day.HasValue)
				{
					pictureBox2.Image = Properties.Resources.cou_16_warning;
					BackColor = Color.FromArgb(0xFF, 0xFF, 0xD4);
					ForeColor = Color.DarkGoldenrod;

					desc.Text = "【<strong>" + _query.DepartureDate.ToString("yyyy年MM月dd日") + "</strong> " + selltimedesc;
				}
				else
				{
					desc.Text = "【" + selltimedesc;
				}
			}
		}

		List<Tuple<string, string>> GetSellTimeList(IEnumerable<string> list)
		{
			var times = list.Select(s => ParamData.TrainStationMap.GetValue(s)).ExceptNull().Select(s => Tuple.Create(s.Name, ParamData.SellTimeMap.GetValue(s.Code)))
						.Where(s => !string.IsNullOrEmpty(s.Item2))
						.ToList();

			return times;
		}

		string Translate24Hto12H(string time)
		{
			return Regex.Replace(time, @"(\d{2}):(\d{2})", _ =>
			{
				if (!_.Success)
					return _.Value;
				var h = _.Groups[1].Value.ToInt32();
				return (h > 12 ? ("下午" + (h - 12)) : (h == 12 ? "中午" : "上午") + h) + ":" + _.Groups[2].Value;
			});
		}
	}
}
