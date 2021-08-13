using System.Windows.Forms;

namespace TOBA.Configuration
{
	using Newtonsoft.Json;

	using System.Drawing;


	internal class QueryViewConfiguration : ConfigurationBase
	{
		static QueryViewConfiguration _instance;
		Color _approximateValidBackColor;

		private FontStyle _approximateValidStyle;
		private Color _approximateValidTextColor;
		Color _discountPriceColor;
		bool _doubleClickOnEmptyAreaAddQuery;
		bool _enableSelltip;

		Color _endPointTextColor;
		bool _hideExtraFilterOption;

		private bool _hideResultColumnIfNotSelected = true;
		Point _mainFormLocation;
		Size _mainFormSize;
		FormWindowState _mainFormWindowState;
		Color _notAvailableBackColor;
		FontStyle _notAvailableFontStyle;
		Color _notAvailableTextColor;

		private string _notAvailableTicketText = "";
		Color _noTicketBackColor;
		FontStyle _noTicketFontStyle;
		Color _noTicketTextColor;
		FontStyle _notNeedFontStyle;
		Color _notNeedTextBackColor;
		Color _notNeedTextColor;
		FontStyle _notSellFontStyle;
		Color _notSellTextBackColor;
		Color _notSellTextColor;

		private string _notSellTicketText = "*";
		int _queryResultListGridLine;
		char[] _seatOrder;

		bool _showGroups;
		bool _showPriceWhenAvailable;

		private bool _showStartAndEndStation = true;
		bool _sortAsc;
		string _sortingInfo;

		private Color _startEndStationColor = Color.Chocolate;
		bool _useStatusIcon;
		Color _validBackColor;
		FontStyle _validFontStyle;
		Color _validTextColor;
		int _viewSplitterHeight;

		/// <summary>
		/// 创建 <see cref="QueryViewConfiguration" />  的新实例(QueryViewConfiguration)
		/// </summary>
		public QueryViewConfiguration()
		{
			_viewSplitterHeight = 250;
			_noTicketTextColor = Color.Gray;
			//_noTicketBackColor = Color.FromArgb(ConfigObject.GetConfig("NoTicketBackColor", Color.FromArgb(0xEB, 0xEB, 0xEB).ToArgb()));
			_noTicketBackColor = SystemColors.Window;
			_noTicketFontStyle = FontStyle.Italic;

			//9PMO6432B10 B-混编硬座
			_seatOrder = "9PMO64F3210*".ToCharArray();
			_showGroups = true;
			_useStatusIcon = false;

			//未到预售期
			_notSellFontStyle = FontStyle.Italic;
			//_notSellTextBackColor = Color.FromArgb(ConfigObject.GetConfig("NotSellTextBackColor", Color.FromArgb(0xFD, 0xE4, 0xE4).ToArgb()));
			_notSellTextBackColor = SystemColors.Window;
			_notSellTextColor = Color.FromArgb(0xDE, 0x75, 0x75);

			//票可用
			_validFontStyle = FontStyle.Bold;
			//_validBackColor = GetConfigColor("ValidBackColor", Color.FromArgb(0xDE, 0xDE, 0xFB));
			_validBackColor = SystemColors.Window;
			_validTextColor = Color.Blue;

			//可能有票可用
			_approximateValidStyle = FontStyle.Bold | FontStyle.Italic;
			//_validBackColor = GetConfigColor("ValidBackColor", Color.FromArgb(0xDE, 0xDE, 0xFB));
			_approximateValidTextColor = SystemColors.Window;
			_approximateValidTextColor = Color.CadetBlue;


			//无需要的票
			_notAvailableFontStyle = FontStyle.Italic;
			_notAvailableTextColor = Color.Gray;
			//_notAvailableBackColor = GetConfigColor("NotAvailableBackColor", Color.FromArgb(0xE5, 0xE5, 0xE5));
			_notAvailableBackColor = SystemColors.Window;

			//无需要的票
			_notNeedFontStyle = FontStyle.Italic;
			//_notNeedTextBackColor = GetConfigColor("NotNeedTextBackColor", Color.FromArgb(0xEA, 0xFF, 0xEA));
			_notNeedTextBackColor = SystemColors.Window;
			_notNeedTextColor = Color.Gray;

			_endPointTextColor = Color.Red;
			_discountPriceColor = Color.BlueViolet;

			_sortingInfo = "TS,0";
			_showPriceWhenAvailable = true;
			_hideExtraFilterOption = false;
			_queryResultListGridLine = 1;

			//主窗口状态
			_mainFormWindowState = FormWindowState.Normal;
			DoubleClickOnEmptyAreaAddQuery = true;

			EnableSelltip = true;
		}

		public Color ApproximateValidBackColor
		{
			get => _approximateValidBackColor;
			set
			{
				if (value == _approximateValidBackColor)
					return;

				_approximateValidBackColor = value;
				OnPropertyChanged(nameof(ApproximateValidBackColor));
			}
		}

		/// <summary>
		/// 可预订票文本样式
		/// </summary>
		[JsonProperty("approximateValidStyle")]
		public FontStyle ApproximateValidFontStyle
		{
			get => _approximateValidStyle;
			set
			{
				if (value == _approximateValidStyle)
					return;
				_approximateValidStyle = value;
				OnPropertyChanged(nameof(ValidFontStyle));
			}
		}

		/// <summary>
		/// 大概可预订文本颜色
		/// </summary>
		[JsonProperty("approximateColorValid")]
		public Color ApproximateValidTextColor
		{
			get => _approximateValidTextColor;
			set
			{
				if (value.Equals(_approximateValidTextColor))
					return;
				_approximateValidTextColor = value;
				OnPropertyChanged(nameof(ApproximateValidTextColor));
			}
		}

		/// <summary>
		/// 折扣价格颜色
		/// </summary>
		public Color DiscountPriceColor
		{
			get => _discountPriceColor;
			set
			{
				if (value.Equals(_discountPriceColor))
					return;
				_discountPriceColor = value;
				OnPropertyChanged(nameof(DiscountPriceColor));
			}
		}

		public bool DoubleClickOnEmptyAreaAddQuery
		{
			get => _doubleClickOnEmptyAreaAddQuery;
			set
			{
				if (value.Equals(_doubleClickOnEmptyAreaAddQuery)) return;
				_doubleClickOnEmptyAreaAddQuery = value;
				OnPropertyChanged(nameof(DoubleClickOnEmptyAreaAddQuery));
			}
		}

		/// <summary>
		/// 获得或设置是否允许起售提醒
		/// </summary>
		public bool EnableSelltip
		{
			get => _enableSelltip;
			set
			{
				if (value.Equals(_enableSelltip)) return;
				_enableSelltip = value;
				OnPropertyChanged(nameof(EnableSelltip));
			}
		}

		/// <summary>
		/// 终点站或始发站文字颜色
		/// </summary>
		public Color EndPointTextColor
		{
			get => _endPointTextColor;
			set
			{
				if (value.Equals(_endPointTextColor))
					return;
				_endPointTextColor = value;
				OnPropertyChanged(nameof(EndPointTextColor));
			}
		}

		private Color _endPointDifferentColor = Color.DarkGoldenrod;

		/// <summary>
		/// 终点站或始发站文字颜色
		/// </summary>
		public Color EndPointDifferentColor
		{
			get => _endPointDifferentColor;
			set
			{
				if (value.Equals(_endPointDifferentColor))
					return;
				_endPointDifferentColor = value;
				OnPropertyChanged(nameof(EndPointDifferentColor));
			}
		}



		public bool HideExtraFilterOption
		{
			get => _hideExtraFilterOption;
			set
			{
				if (value.Equals(_hideExtraFilterOption)) return;
				_hideExtraFilterOption = value;
				OnPropertyChanged(nameof(HideExtraFilterOption));
			}
		}

		/// <summary>
		/// 如果未选择席别，则隐藏结果中的列
		/// </summary>
		public bool HideResultColumnIfNotSelected
		{
			get => _hideResultColumnIfNotSelected;
			set
			{
				if (value == _hideResultColumnIfNotSelected)
					return;
				_hideResultColumnIfNotSelected = value;
				OnPropertyChanged(nameof(HideResultColumnIfNotSelected));
			}
		}

		/// <summary>
		/// 获得当前的配置实例
		/// </summary>
		public static QueryViewConfiguration Instance => _instance ?? (_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<QueryViewConfiguration>("main", "Query", "QueryView"));

		/// <summary>
		/// 大量车票文本
		/// </summary>
		public string LargeTicketText { get; set; } = "有";

		public Point MainFormLocation
		{
			get => _mainFormLocation;
			set
			{
				if (value.Equals(_mainFormLocation))
					return;
				_mainFormLocation = value;
				OnPropertyChanged(nameof(MainFormLocation));
			}
		}

		public Size MainFormSize
		{
			get => _mainFormSize;
			set
			{
				if (value.Equals(_mainFormSize))
					return;
				_mainFormSize = value;
				OnPropertyChanged(nameof(MainFormSize));
			}
		}

		/// <summary>
		/// 获得或设置出窗口状态
		/// </summary>
		public System.Windows.Forms.FormWindowState MainFormWindowState
		{
			get => _mainFormWindowState;
			set
			{
				if (value == _mainFormWindowState)
					return;
				_mainFormWindowState = value;
				OnPropertyChanged(nameof(MainFormWindowState));
			}
		}

		/// <summary>
		/// 无可用票背景色
		/// </summary>
		[JsonProperty("bgNotAvailable")]
		public Color NotAvailableBackColor
		{
			get => _notAvailableBackColor;
			set
			{
				if (value.Equals(_notAvailableBackColor))
					return;
				_notAvailableBackColor = value;
				OnPropertyChanged(nameof(NotAvailableBackColor));
			}
		}

		/// <summary>
		/// 无可用票样式
		/// </summary>
		[JsonProperty("styleNotAvailable")]
		public FontStyle NotAvailableFontStyle
		{
			get => _notAvailableFontStyle;
			set
			{
				if (value == _notAvailableFontStyle)
					return;
				_notAvailableFontStyle = value;
				OnPropertyChanged(nameof(NotAvailableFontStyle));
			}
		}

		/// <summary>
		/// 无可用票文字颜色
		/// </summary>
		[JsonProperty("colorNotAvailable")]
		public Color NotAvailableTextColor
		{
			get => _notAvailableTextColor;
			set
			{
				if (value.Equals(_notAvailableTextColor))
					return;
				_notAvailableTextColor = value;
				OnPropertyChanged(nameof(NotAvailableTextColor));
			}
		}

		/// <summary>
		/// 无此席别文本
		/// </summary>
		public string NotAvailableTicketText
		{
			get => _notAvailableTicketText;
			set
			{
				if (value == _notAvailableTicketText) return;
				_notAvailableTicketText = value;
				OnPropertyChanged(nameof(NotAvailableTicketText));
			}
		}

		/// <summary>
		/// 无票背景颜色
		/// </summary>
		public System.Drawing.Color NoTicketBackColor
		{
			get => _noTicketBackColor;
			set
			{
				if (value.Equals(_noTicketBackColor))
					return;
				_noTicketBackColor = value;
				OnPropertyChanged(nameof(NoTicketBackColor));
			}
		}

		[JsonProperty("styleNoTicket")]
		public FontStyle NoTicketFontStyle
		{
			get => _noTicketFontStyle;
			set
			{
				if (value == _noTicketFontStyle)
					return;
				_noTicketFontStyle = value;
				OnPropertyChanged(nameof(NoTicketFontStyle));
			}
		}

		public string NoTicketText { get; set; } = "无";

		public string BackupTicketText { get; set; } = "候补";

		public Color BackupTicketColor { get; set; } = Color.Brown;

		/// <summary>
		/// 无票文本颜色
		/// </summary>
		public System.Drawing.Color NoTicketTextColor
		{
			get => _noTicketTextColor;
			set
			{
				if (value.Equals(_noTicketTextColor))
					return;
				_noTicketTextColor = value;
				OnPropertyChanged(nameof(NoTicketTextColor));
			}
		}

		/// <summary>
		/// 无需要的票样式
		/// </summary>
		[JsonProperty("styleNotNeed")]
		public FontStyle NotNeedFontStyle
		{
			get => _notNeedFontStyle;
			set
			{
				if (value == _notNeedFontStyle)
					return;
				_notNeedFontStyle = value;
				OnPropertyChanged(nameof(NotNeedFontStyle));
			}
		}

		/// <summary>
		/// 无需要的票背景色
		/// </summary>
		[JsonProperty("bgNotNeed")]
		public Color NotNeedTextBackColor
		{
			get => _notNeedTextBackColor;
			set
			{
				if (value.Equals(_notNeedTextBackColor))
					return;
				_notNeedTextBackColor = value;
				OnPropertyChanged(nameof(NotNeedTextBackColor));
			}
		}

		/// <summary>
		/// 无需要的票文字色
		/// </summary>
		[JsonProperty("colorNotNeed")]
		public Color NotNeedTextColor
		{
			get => _notNeedTextColor;
			set
			{
				if (value.Equals(_notNeedTextColor))
					return;
				_notNeedTextColor = value;
				OnPropertyChanged(nameof(NotNeedTextColor));
			}
		}

		/// <summary>
		/// 未到预售期文本样式
		/// </summary>
		[JsonProperty("styleNotSell")]
		public FontStyle NotSellFontStyle
		{
			get => _notSellFontStyle;
			set
			{
				if (Equals(value, _notSellFontStyle))
					return;
				_notSellFontStyle = value;
			}
		}

		/// <summary>
		/// 未到预售期背景色
		/// </summary>
		[JsonProperty("btNotSell")]
		public Color NotSellTextBackColor
		{
			get => _notSellTextBackColor;
			set
			{
				if (value.Equals(_notSellTextBackColor))
					return;
				_notSellTextBackColor = value;
				OnPropertyChanged(nameof(NotSellTextBackColor));
			}
		}

		/// <summary>
		/// 未到预售期文本颜色
		/// </summary>
		[JsonProperty("colorNotSell")]
		public Color NotSellTextColor
		{
			get => _notSellTextColor;
			set
			{
				if (value.Equals(_notSellTextColor))
					return;
				_notSellTextColor = value;
				OnPropertyChanged(nameof(NotSellTextColor));
			}
		}

		/// <summary>
		/// 未起售文本
		/// </summary>
		public string NotSellTicketText
		{
			get => _notSellTicketText;
			set
			{
				if (value == _notSellTicketText) return;
				_notSellTicketText = value;
				OnPropertyChanged(nameof(NotSellTicketText));
			}
		}

		/// <summary>
		/// 获得当前的网格线状态
		/// </summary>
		public int QueryResultListGridLine
		{
			get => _queryResultListGridLine;
			set
			{
				if (value == _queryResultListGridLine) return;
				_queryResultListGridLine = value;
				OnPropertyChanged(nameof(QueryResultListGridLine));
			}
		}


		/// <summary>
		/// 获得席别显示顺序
		/// </summary>
		[JsonProperty("seatOrderv3")]
		public char[] SeatOrder
		{
			get => _seatOrder;
			set
			{
				if (Equals(value, _seatOrder)) return;
				_seatOrder = value;
				OnPropertyChanged(nameof(SeatOrder));
			}
		}

		/// <summary>
		/// 获得或设置是否显示分组
		/// </summary>
		public bool ShowGroups
		{
			get => _showGroups;
			set
			{
				if (value.Equals(_showGroups)) return;
				_showGroups = value;
				OnPropertyChanged(nameof(ShowGroups));
			}
		}

		/// <summary>
		/// 可能的时候显示票价
		/// </summary>
		public bool ShowPriceWhenAvailable
		{
			get => _showPriceWhenAvailable;
			set
			{
				if (value.Equals(_showPriceWhenAvailable))
					return;
				_showPriceWhenAvailable = value;
				OnPropertyChanged(nameof(ShowPriceWhenAvailable));
			}
		}

		/// <summary>
		/// 如果不是始发站或终到站，则显示
		/// </summary>
		public bool ShowStartAndEndStation
		{
			get => _showStartAndEndStation;
			set
			{
				if (value == _showStartAndEndStation) return;
				_showStartAndEndStation = value;
				OnPropertyChanged(nameof(ShowStartAndEndStation));
			}
		}

		/// <summary>
		/// 获得或设置显示排序的列
		/// </summary>
		[JsonProperty("sort_v1")]
		public string SortInfo
		{
			get => _sortingInfo;
			set
			{
				if (value == _sortingInfo) return;
				_sortingInfo = value;
				OnPropertyChanged(nameof(SortInfo));
			}
		}

		public Color StartEndStationColor
		{
			get => _startEndStationColor;
			set
			{
				if (value.Equals(_startEndStationColor)) return;
				_startEndStationColor = value;
				OnPropertyChanged(nameof(StartEndStationColor));
			}
		}

		/// <summary>
		/// 获得或设置是否使用状态做图标
		/// </summary>
		public bool UseStatusIcon
		{
			get => _useStatusIcon;
			set
			{
				if (value.Equals(_useStatusIcon))
					return;
				_useStatusIcon = value;
				OnPropertyChanged(nameof(UseStatusIcon));
			}
		}

		/// <summary>
		/// 可预订背景色
		/// </summary>
		[JsonProperty("bgValid")]
		public Color ValidBackColor
		{
			get => _validBackColor;
			set
			{
				if (value.Equals(_validBackColor))
					return;
				_validBackColor = value;
				OnPropertyChanged(nameof(ValidBackColor));
			}

		}

		/// <summary>
		/// 可预订票文本样式
		/// </summary>
		[JsonProperty("styleValid")]
		public FontStyle ValidFontStyle
		{
			get => _validFontStyle;
			set
			{
				if (value == _validFontStyle)
					return;
				_validFontStyle = value;
				OnPropertyChanged(nameof(ValidFontStyle));
			}
		}

		/// <summary>
		/// 可预订文本颜色
		/// </summary>
		[JsonProperty("colorValid")]
		public Color ValidTextColor
		{
			get => _validTextColor;
			set
			{
				if (value.Equals(_validTextColor))
					return;
				_validTextColor = value;
				OnPropertyChanged(nameof(ValidTextColor));
			}
		}

		public int ViewSplitterHeight
		{
			get => _viewSplitterHeight;
			set
			{
				if (value == _viewSplitterHeight) return;
				_viewSplitterHeight = value;
				OnPropertyChanged(nameof(ViewSplitterHeight));
			}
		}
	}
}
