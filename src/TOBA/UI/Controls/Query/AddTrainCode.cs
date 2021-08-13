using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Data;

	using FSLib.Extension;

	using Popup;

	using System;
	using System.Drawing;
	using System.Linq;
	using System.Text.RegularExpressions;

	using TOBA.Entity;
	using TOBA.Query.Entity;

	internal partial class AddTrainCode : ControlBase
	{
		public QueryParam QueryParam { get; set; }

		public AddTrainCode(Session session, QueryParam queryParam)
		{
			InitializeComponent();
			InitSession(session);
			QueryParam = queryParam;
			btnOk.Enabled = false;
			txtCode.KeyUp += txtCode_KeyUp;
			Load += AddTrainCode_Load;

			ilTab.Images.AddRange(new[] { Properties.Resources.pencil_16, Properties.Resources.bookmark_16 });

			VisibleChanged += (s, e) =>
			{
				if (!Visible || QueryParam == null)
					return;

				tcMain.SelectedTab = QueryParam.LastQueryResult == null ? tabAdd : tabSelect;
				TryLoadTrains();
			};
			InitTrainSelector();
			linkLabel1.LinkClicked += linkLabel1_LinkClicked;
			txtCode.TextChanged += textBox1_TextChanged;
			btnOk.Click += btnOk_Click;

			//List Event
			radListAll.CheckedChanged += (s, e) => TryLoadTrains();

		}

		void InitTrainSelector()
		{
			btnCheckAll.Click += (s, e) => FilterTrainSelected(_ => true);
			btnCheckNone.Click += (s, e) => FilterTrainSelected(_ => false);
			btnCheckC.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'C');
			btnCheckD.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'D');
			btnCheckG.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'G');
			btnCheckK.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'K');
			btnCheckT.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'T');
			btnCheckZ.Click += (s, e) => FilterTrainSelected(_ => _.Code[0] == 'Z');

			btnCheck.Click += (s, e) =>
			{
				var trains = lstTrain.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => x.Tag as QueryResultItem).ToArray();
				foreach (var train in trains)
				{
					OnRequestAddCode(new GeneralEventArgs<string>(train.Code));
				}
				(Parent as Popup).Close();
			};
		}

		void FilterTrainSelected(Func<QueryResultItem, bool> predicate)
		{
			using (lstTrain.CreateBatchOperationDispatcher())
			{
				foreach (var listViewItem in lstTrain.Items.Cast<ListViewItem>())
				{
					listViewItem.Checked = predicate(listViewItem.Tag as QueryResultItem);
				}
			}
		}


		void TryLoadTrains()
		{
			if (QueryParam == null || QueryParam.LastQueryResult == null)
			{
				pQueryTip.Visible = true;
				return;
			}

			pQueryTip.Visible = false;

			lstTrain.Items.Clear();
			var result = QueryParam.LastQueryResult;
			var items = (radListAll.Checked ? result.OriginalList.ToArray() : result.ToArray()).Select(s =>
			  {
				  var item = new ListViewItem(s.Code)
				  {
					  Tag = s
				  };
				  item.SubItems.AddRange(new[]
										  {
											s.FromStation.StationName,
											s.ToStation.StationName,
											s.FromStation.DepartureTime.Value.ToString("HH:mm"),
											s.ToStation.ArriveTime.Value.ToString("HH:mm"),
											s.ElapsedTime.ToFriendlyDisplay()
										  });
				  return item;
			  }).ToArray();
			lstTrain.Items.AddRange(items);
		}

		void AddTrainCode_Load(object sender, EventArgs e)
		{
			ParamData.FrequencyTrainCodeMap.ForEach(s =>
			{
				var linkLabel = new LinkLabel()
				{
					Text = s.Key,
					Tag = s.Key
				};
				linkLabel.Click += linkLabel_Click;
				pFrq.Controls.Add(linkLabel);
			});
		}

		void linkLabel_Click(object sender, EventArgs e)
		{
			txtCode.Text = (sender as LinkLabel).Tag as string;
			btnOk_Click(null, null);
		}

		void txtCode_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (btnOk.Enabled)
					btnOk_Click(null, null);
			}
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			btnOk.Enabled = false;
			lblTip.Image = Properties.Resources.block_16;
			lblTip.ForeColor = Color.Red;
			if (txtCode.Text.IsNullOrEmpty())
			{
				lblTip.Text = "    不可为空";
				return;
			}

			try
			{
				new Regex(txtCode.Text);
				btnOk.Enabled = true;
				lblTip.Image = Properties.Resources.tick_16;
				lblTip.Text = "    有效";
				lblTip.ForeColor = Color.Green;
			}
			catch (Exception)
			{
				lblTip.Text = "    无效";
			}
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (e.Link != null)
			{
				Shell.StartUrl("http://www.fishlee.net/soft/12306/faq.html#reg");
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			txtCode.Text = "";
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			var code = txtCode.Text.ToUpper();
			if (Regex.IsMatch(code, @"^(([a-z?\d]+)[\s;\/\|,.]?)+$", RegexOptions.IgnoreCase))
			{
				var array = Regex.Split(code, @"[\s;\/\|,.]");
				for (var i = 0; i < array.Length; i++)
				{
					OnRequestAddCode(new GeneralEventArgs<string>(array[i]));
				}
			}
			else
				OnRequestAddCode(new GeneralEventArgs<string>(txtCode.Text));
			(Parent as Popup).Close();
		}

		/// <summary>
		/// 请求添加代码
		/// </summary>
		public event EventHandler<GeneralEventArgs<string>> RequestAddCode;

		/// <summary>
		/// 引发 <see cref="RequestAddCode" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestAddCode(GeneralEventArgs<string> ea)
		{
			var handler = RequestAddCode;
			if (handler != null)
				handler(this, ea);
		}
	}
}
