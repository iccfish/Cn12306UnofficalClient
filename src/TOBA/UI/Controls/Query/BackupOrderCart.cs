using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{

	using Components;

	using Data;

	using DevComponents.DotNetBar;

	using Entity;

	using TOBA.BackupOrder;

	partial class BackupOrderCart : ControlBase
	{
		public QueryParam QueryParam { get; set; }

		public BackupOrderCart()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			btnSubmit.Click += BtnSubmit_Click;
			btnCancel.Click += (_1, _2) =>
			{
				Session.BackupOrderCart.Items.Clear();
			};

			Disposed += (sender, args) =>
			{
				Session.BackupOrderCart.Items.Added -= Items_Removed;
				Session.BackupOrderCart.Items.Removed -= Items_Removed; ;
				Session.BackupOrderCart.PropertyChanged -= BackupOrderCart_PropertyChanged;
			};
			btnAutoSumit.Click += (sender, args) =>
			{
				if (QueryParam.AutoPreSubmitConfig.Passenger.Count == 0)
				{
					this.FindForm().ShowWarningToastMini("请先添加要提交的联系人（最多3个）");
					return;
				}

				var cart = Session.BackupOrderCart;
				cart.ImportQueryPassengers(QueryParam.AutoPreSubmitConfig.Passenger);
				cart.StartOrStopAutoSubmit();
			};
		}

		private void BackupOrderCart_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var cart = Session.BackupOrderCart;
			switch (e.PropertyName)
			{
				case nameof(BackupCart.InAutoSubmit):
					btnAutoSumit.Text = cart.InAutoSubmit ? "停止提交" : "自动提交";
					btnSubmit.Enabled = !cart.InAutoSubmit;
					lblAutoSubmitStatus.Visible = cart.InAutoSubmit;
					btnSubmit.Image = cart.InAutoSubmit ? Properties.Resources.loading_16_3 : Properties.Resources.cou_16_accept;
					break;
				case nameof(BackupCart.LastSubmitMessage):
					lblAutoSubmitStatus.Text = cart.LastSubmitMessage;
					break;
			}
		}

		private async void BtnSubmit_Click(object sender, EventArgs e)
		{
			//自动添加乘车人
			var cart = Session.BackupOrderCart;
			cart.ImportQueryPassengers(QueryParam?.AutoPreSubmitConfig.Passenger);

			btnSubmit.Enabled = false;
			var uiop = Session.GetService<IBackupOrderUiOperation>();
			await uiop.SubmitOrderRequestAsync();
			btnSubmit.Enabled = true;
		}

		/// <inheritdoc />
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			session.BackupOrderCart.Items.Added += Items_Removed;
			session.BackupOrderCart.Items.Removed += Items_Removed; ;
			Session.BackupOrderCart.PropertyChanged += BackupOrderCart_PropertyChanged;
			SyncCartItems();
		}


		private void Items_Removed(object sender, ItemEventArgs<BackupCartItem> e)
		{
			SyncCartItems();
		}

		void SyncCartItems()
		{
			pContainer.SuspendLayout();
			var items = Session.BackupOrderCart.Items;
			var controls = pContainer.Controls.OfType<ButtonX>().ToList();

			var count = Math.Max(items.Count, controls.Count);
			for (var i = 0; i < count; i++)
			{
				if (i > items.Count - 1)
				{
					//没有了
					controls.OfType<ButtonX>().Skip(i).ToArray().ForEach(_ => controls.Remove(_));
					break;
				}

				var item = items[i];
				if (i > controls.Count - 1)
				{
					//控件未生成
					controls.Add(CreateButton(item));
				}
				else if (controls[i].Tag != item)
				{
					//不匹配，则插入新的
					controls.Insert(i, CreateButton(item));
				}
			}

			pContainer.Controls.Clear();
			pContainer.Controls.AddRange(controls.Cast<Control>().ToArray());
			this.Size = new Size(this.Width, controls.Any() ? controls.Max(_ => _.Height) + Margin.Top + Margin.Bottom : 38);
			pContainer.ResumeLayout(true);

			this.Visible = items.Count > 0;
		}

		private static string[] _rateColors = new[]
		{
			"#575551",
			"#EB0034",
			"#D5AB47",
			"#31933C",
			"#EB2C00"
		};

		ButtonX CreateButton(BackupCartItem item)
		{
			string GetText() => $"<div><b><font color=\"#ED1C24\">{item.Train.QueryResult.Date:MM-dd}</font> <font color=\"#BA1419\">{item.Train.Code}</font> <font color=\"#8066A0\">{ParamData.GetSeatTypeName(item.Seat)}</font> <font color=\"#4BACC6\">{item.Train.FromStation.StationName}-{item.Train.ToStation.StationName}</font></b></div><div><font color=\"{_rateColors[item.SuccessLevel]}\" size =\"-1\">{item.SuccessRateInfoMessage.DefaultForEmpty("候补人数查询中...")}</font></div>";

			var btn = new ButtonX()
			{
				EnableMarkup = true,
				Text = GetText(),
				Tag = item,
				ColorTable = eButtonColor.Flat
			};
			btn.CreateControl();
			btn.Size = btn.PreferredSize;

			item.SuccessRateChanged += (_1, _2) =>
			{
				btn.Text = GetText();
			};

			btn.Click += (_1, _2) => Session.BackupOrderCart.Items.Remove(item);

			if (item.SuccessLevel == 0)
			{
				Session.GetService<IBackupOrderService>().GetSuccessRateAsync(item);
			}

			return btn;
		}

	}
}
