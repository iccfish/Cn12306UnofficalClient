using System;
using System.Drawing;
using System.Linq;

using TOBA.UI.Controls.Query.ResultSubItems;

namespace TOBA.UI.Controls.Query
{
    using ComponentOwl.BetterListView;

    using TOBA.Query.Entity;

    internal class QueryResultListViewItem : BetterListViewItem
    {
        QueryResultItem _resultItem, _originalItem;
        Font[]          _fonts;
        bool            _showStartEndStation;

        /// <summary>
        /// 获得查询结果列
        /// </summary>
        public QueryResultItem ResultItem
        {
            get { return _resultItem; }
            set
            {
                if (_resultItem == value)
                    return;

                _resultItem = value;

                SubItems.Clear();
                SubItems.AddRange(CreateSubItems(value, _fonts, _showStartEndStation, _originalItem).Skip(1));

                var header = (TrainCodeSubItem)SubItems[0];
                header.ResultItem = value;
            }
        }

        /// <summary>
        /// 创建 <see cref="QueryResultListViewItem" />  的新实例(QueryResultListViewItem)
        /// </summary>
        public QueryResultListViewItem(QueryResultItem resultItem, Font[] font, bool showStartEndStation = false, QueryResultItem originalItem = null)
            : base(CreateSubItems(resultItem, font, showStartEndStation, originalItem), 0)
        {
            _showStartEndStation    = showStartEndStation;
            _resultItem             = resultItem;
            _originalItem           = originalItem;
            UseItemStyleForSubItems = false;
            RefreshIcon();
            _fonts = font;
        }

        static BetterListViewSubItem[] CreateSubItems(QueryResultItem result, Font[] font, bool showStartEndStation, QueryResultItem originalItem = null)
        {
            var array = new BetterListViewSubItem[]
                {
                    new TrainCodeSubItem(result, font),
                    showStartEndStation ? new StartOrEndStation(result, font, true) : null,
                    new TrainFromStationNameSubItem(result, font),
                    new TrainToStationNameSubItem(result, font),
                    showStartEndStation ? new StartOrEndStation(result, font, false) : null,
                    new TrainToStationDateSubItem(result, font)
                }.ExceptNull().
                ToArray();

            //var baseTicketPriceIndex = array.Length;

            array = array.Concat(Configuration.QueryViewConfiguration.Instance.SeatOrder.Select(s => new TicketCellSubItem(result, s, font))).Concat(new[] { new TrainMemoSubItem(result, font) }).ToArray();
            return array;
        }

        /// <summary>
        /// 刷新图标
        /// </summary>
        public void RefreshIcon()
        {
            if (ResultItem.AlmostIllegalResult)
            {
                ImageKey = "i";
                return;
            }

            if (Configuration.QueryViewConfiguration.Instance.UseStatusIcon)
            {
                if (ResultItem.NoTicketNeeded) ImageKey = "f";
                else if (!ResultItem.IsAvailable)
                {
                    ImageKey = ResultItem.BeginSellTime == null ? "n" : "s";
                }
                else
                {
                    ImageKey = "o";
                }
            }
            else
            {
                if (ResultItem.IsFuXing)
                    ImageKey = "fx";
                else if (ResultItem.IsSmartD)
                    ImageKey = "zn";
                else
                {
                    var code = ResultItem.TrainClass;
                    if (code == 'K' || code == 'G' || code == 'D' || code == 'C' || code == 'T' || code == 'Z')
                    {
                        ImageKey = char.ToLower(code).ToString();
                    }
                    else if (code == 'L') ImageKey        = "l";
                    else if (code == 'Y') ImageKey        = "y";
                    else if (char.IsDigit(code)) ImageKey = "p";
                    else ImageKey                         = "u";
                }
            }
        }
    }
}
