using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using ComponentOwl.BetterListView;

	using TOBA.Query.Entity;

	internal abstract class SubItemBase : BetterListViewSubItem
	{
		private QueryResultItem _resultItem;

		/// <summary>
		/// 获得查询结果行
		/// </summary>
		public QueryResultItem ResultItem
		{
			get { return _resultItem; }
			set
			{
				if (_resultItem == value)
					return;

				_resultItem = value;
				RefreshContent();
			}
		}

		/// <summary>
		/// 获得或设置用于表示样式的字体
		/// </summary>
		protected Font[] Fonts { get; private set; }

		/// <summary>
		/// 创建 <see cref="SubItemBase" />  的新实例(SubItemBase)
		/// </summary>
		protected SubItemBase(QueryResultItem resultItem, Font[] font)
		{
			ResultItem = resultItem;
			Fonts = font;
			RefreshStyle();
		}

		/// <summary>
		/// 刷新单元格显示
		/// </summary>
		protected virtual void RefreshContent()
		{

		}

		/// <summary>
		/// 刷新样式
		/// </summary>
		protected virtual void RefreshStyle()
		{
			if (ResultItem.NoTicketNeeded)
			{
				ForeColor = Configuration.QueryViewConfiguration.Instance.NotNeedTextColor;
				BackColor = Configuration.QueryViewConfiguration.Instance.NotNeedTextBackColor;
				Font = Fonts[3];
			}
			else if (!ResultItem.IsAvailable)
			{
				if (ResultItem.BeginSellTime == null)
				{
					ForeColor = Configuration.QueryViewConfiguration.Instance.NoTicketTextColor;
					BackColor = Configuration.QueryViewConfiguration.Instance.NoTicketBackColor;
					Font = Fonts[1];
				}
				else
				{
					//未到预售期
					ForeColor = Configuration.QueryViewConfiguration.Instance.NotSellTextColor;
					BackColor = Configuration.QueryViewConfiguration.Instance.NotSellTextBackColor;
					Font = Fonts[2];
				}
			}
			else
			{
				ForeColor = Configuration.QueryViewConfiguration.Instance.ValidTextColor;
				BackColor = Configuration.QueryViewConfiguration.Instance.ValidBackColor;
				Font = Fonts[0];
			}
		}

		/// <summary>
		/// 向另个单元格做比较
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public virtual int CompareTo(SubItemBase other)
		{
			return string.Compare(Text, other.Text, true);
		}
	}
}
