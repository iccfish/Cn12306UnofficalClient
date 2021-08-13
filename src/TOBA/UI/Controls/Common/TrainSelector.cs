using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Common
{
	using System.Collections;
	using System.Diagnostics;
	using FSLib.Extension;
	using TOBA.Query.Entity;

	internal partial class TrainSelector : UserControl
	{

		private bool _ignorePreSelectFlag;

		private IEnumerable<QueryResultItem> _items;

		TrainItemComparer _comparer;

		public TrainSelector()
		{
			InitializeComponent();
			lstTrain.ColumnClick += LstTrain_ColumnClick;
			lstTrain.ListViewItemSorter = _comparer = new TrainItemComparer();
		}

		public event EventHandler IgnorePreSelectFlagChanged;

		public event EventHandler ItemsChanged;

		private void LstTrain_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (_comparer.SortIndex == -1)
			{
				_comparer.SortIndex = e.Column;
				_comparer.SortDirection = 1;
			}
			else
			{
				_comparer.SortIndex = e.Column;
				_comparer.SortDirection = -_comparer.SortDirection;
			}

			using (lstTrain.CreateBatchOperationDispatcher())
			{
				lstTrain.Sort();
			}
		}

		protected virtual void OnIgnorePreSelectFlagChanged()
		{
			IgnorePreSelectFlagChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnItemsChanged()
		{
			ItemsChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 重新加载项目
		/// </summary>
		public void ReloadItems()
		{
			using (lstTrain.CreateBatchOperationDispatcher())
			{
				lstTrain.SuspendLayout();
				lstTrain.Items.Clear();
				if (_items == null)
					return;

				var result = _items;
				var items = result.Select(s =>
				{
					var item = new ListViewItem(s.Code)
					{
						Tag = s,
						Checked = !IgnorePreSelectFlag && s.Selected > 0
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
				lstTrain.ResumeLayout();
			}
		}

		/// <summary>
		/// 根据条件表达式设置检测标记
		/// </summary>
		/// <param name="func"></param>
		public void SetCheckFlag([NotNull] Func<QueryResultItem, bool> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));

			using (lstTrain.CreateBatchOperationDispatcher())
			{
				foreach (var item in lstTrain.Items.OfType<ListViewItem>())
				{
					item.Checked = func(item.Tag as QueryResultItem);
				}
			}
		}

		/// <summary>
		/// 获得选中的车次
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IEnumerable<QueryResultItem> CheckedTrains => lstTrain.CheckedItems.OfType<ListViewItem>().Select(s => s.Tag as QueryResultItem);

		/// <summary>
		/// 是否忽略预先选择标记
		/// </summary>
		public bool IgnorePreSelectFlag
		{
			get { return _ignorePreSelectFlag; }
			set
			{
				if (_ignorePreSelectFlag == value)
					return;
				_ignorePreSelectFlag = value;
				ReloadItems();
				OnIgnorePreSelectFlagChanged();
			}
		}

		/// <summary>
		/// 获得或设置项目集合
		/// </summary>
		public IEnumerable<QueryResultItem> Items
		{
			get { return _items; }
			set
			{
				if (value == _items)
					return;

				_items = value;
				ReloadItems();
				OnItemsChanged();
			}
		}
		class TrainItemComparer : IComparer
		{
			public int SortIndex { get; set; } = -1;

			public int SortDirection { get; set; } = 1;

			/// <summary>
			/// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
			/// </summary>
			/// <returns>
			/// 一个带符号整数，它指示 <paramref name="x"/> 与 <paramref name="y"/> 的相对值，如下表所示。值含义小于零<paramref name="x"/> 小于 <paramref name="y"/>。零<paramref name="x"/> 等于 <paramref name="y"/>。大于零<paramref name="x"/> 大于 <paramref name="y"/>。
			/// </returns>
			/// <param name="x">要比较的第一个对象。</param><param name="y">要比较的第二个对象。</param><exception cref="T:System.ArgumentException"><paramref name="x"/> 和 <paramref name="y"/> 都不实现 <see cref="T:System.IComparable"/> 接口。- 或 -<paramref name="x"/> 和 <paramref name="y"/> 的类型不同，它们都无法处理与另一个进行的比较。</exception>
			public int Compare(object x, object y)
			{
				if (SortIndex == -1)
					return 0;

				var lvx = x as ListViewItem;
				var lvy = y as ListViewItem;
				Debug.WriteLine(SortIndex);
				if (SortIndex < 3)
					return StringComparer.OrdinalIgnoreCase.Compare(lvx.SubItems[SortIndex].Text, lvy.SubItems[SortIndex].Text) * SortDirection;

				Debug.Assert(lvx != null, "lvx != null");
				var ltx = (QueryResultItem)lvx.Tag;
				Debug.Assert(lvy != null, "lvy != null");
				var lty = (QueryResultItem)lvy.Tag;

				if (SortIndex == 3)
				{
					return CompareDateTime(ltx.FromStation.DepartureTime?.TimeOfDay, lty.FromStation.DepartureTime?.TimeOfDay) * SortDirection;
				}
				if (SortIndex == 4)
				{
					return CompareDateTime(ltx.ToStation.ArriveTime?.TimeOfDay, lty.ToStation.ArriveTime?.TimeOfDay) * SortDirection;
				}

				return CompareDateTime(ltx.ElapsedTime, lty.ElapsedTime) * SortDirection;
			}
			int CompareDateTime(TimeSpan? dtx, TimeSpan? dty)
			{
				if (dtx == null)
					return dty == null ? 0 : -1;
				else if (dty == null)
					return 1;
				else
					return dtx.Value < dty.Value ? -1 : 1;
			}
		}
	}
}
