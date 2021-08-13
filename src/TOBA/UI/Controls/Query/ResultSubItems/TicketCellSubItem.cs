using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TOBA.Entity.Web;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using TOBA.Configuration;
	using TOBA.Query.Entity;

	internal class TicketCellSubItem : SubItemBase
	{

		/// <summary>
		/// 创建 <see cref="TicketCellSubItem"/>  的新实例(TicketCellSubItem)
		/// </summary>
		public TicketCellSubItem(QueryResultItem resultItem, char code, Font[] font)
			: base(resultItem, font)
		{
			Code = code;
			Bind();
		}

		void Bind()
		{
			var resultItem = ResultItem;
			var data = resultItem.TicketCount.GetTicketData(Code);

			if (data == null || data.NotAvailable)
			{
				Text = QueryViewConfiguration.Instance.NotAvailableTicketText;
			}
			else if (data.NotSell)
			{
				Text = QueryViewConfiguration.Instance.NotSellTicketText;
			}
			else if (data.NoTicket)
			{
				//未到预售期的不显示
				if (resultItem.BeginSellTime != null || resultItem.IsLimitSell || resultItem.ControlFlag > 0)
				{
					Text = "";
				}
				else if (resultItem.IsSeatBackupAvailable(Code))
				{
					Text = "候补";
				}
				else
					Text = QueryViewConfiguration.Instance.NoTicketText;
			}
			else if (data.ApproximateCount.HasValue)
			{
				Text = "≈" + data.ApproximateCount.ToString();
			}
			else if (data.Count == LeftTicketData.LargeQuantityOfTicket)
			{
				Text = QueryViewConfiguration.Instance.LargeTicketText;
			}
			else
			{
				Text = (data.Count ?? 0).ToString("#0");
			}
			if (data == null || data.NotNeed)
			{
				ForeColor = Configuration.QueryViewConfiguration.Instance.NotNeedTextColor;
				//BackColor = Configuration.QueryViewConfiguration.Instance.NotNeedTextBackColor;
				Font = Fonts[3];
			}
			else if (data.NotAvailable)
			{
				ForeColor = Configuration.QueryViewConfiguration.Instance.NotAvailableTextColor;
				//BackColor = Configuration.QueryViewConfiguration.Instance.NotAvailableBackColor;
				Font = Fonts[4];
			}
			else if (data.NoTicket || !resultItem.IsAvailable)
			{
				if (data.NotSell || resultItem.BeginSellTime != null)
				{
					//未到预售期
					ForeColor = Configuration.QueryViewConfiguration.Instance.NotSellTextColor;
					//BackColor = Configuration.QueryViewConfiguration.Instance.NotSellTextBackColor;
					Font = Fonts[2];
				}
				else if (resultItem.IsSeatBackupAvailable(Code))
				{
					if (data.HbLevel == -1)
					{
						//未认证
						ForeColor = Color.Gray;
						Font = Fonts[2];
					}
					else if (data.HbLevel == 3)
					{
						ForeColor = Color.Green;
						Font = Fonts[0];
					}
					else if (data.HbLevel == 2)
					{
						ForeColor = Color.RoyalBlue;
						Font = Fonts[0];
					}
					else if (data.HbLevel == 1)
					{
						ForeColor = Color.Crimson;
						Font = Fonts[0];
					}
					else if (data.HbLevel == 4)
					{
						//人太多
						ForeColor = Color.Gray;
						Font = Fonts[2];
					}
					else
					{
						ForeColor = Configuration.QueryViewConfiguration.Instance.BackupTicketColor;
						Font = Fonts[2];

						data.AddPropertyChangedEventHandler(_ => _.HbLevel, (sender, args) => { Bind(); });
					}

				}
				else
				{
					ForeColor = Configuration.QueryViewConfiguration.Instance.NoTicketTextColor;
					//BackColor = Configuration.QueryViewConfiguration.Instance.NoTicketBackColor;
					Font = Fonts[1];
				}
			}
			else
			{
				ForeColor = Configuration.QueryViewConfiguration.Instance.ValidTextColor;
				//BackColor = Configuration.QueryViewConfiguration.Instance.ValidBackColor;
				Font = Fonts[0];
			}
		}

		/// <summary>
		/// 获得席别编码
		/// </summary>
		public char Code { get; private set; }

		protected override void RefreshStyle()
		{
		}

		public override int CompareTo(SubItemBase other)
		{
			return SortWeight - (other as TicketCellSubItem).SortWeight;
		}

		/// <summary>
		/// 获得排序的权重
		/// </summary>
		public int SortWeight
		{
			get
			{
				var data = ResultItem.TicketCount.GetTicketData(Code);
				if (data == null || data.NotAvailable) return 999993;
				if (data.NoTicket) return 999992;
				if (data.NotSell) return 999991;
				if (data.NotNeed) return 999990;
				return data.Count ?? data.ApproximateCount ?? 999993;
			}
		}
	}
}
