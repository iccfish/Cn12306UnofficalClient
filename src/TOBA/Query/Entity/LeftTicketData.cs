namespace TOBA.Query.Entity
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using FSLib.Extension.Drawing;
	using System.Linq;
	using System.Runtime.CompilerServices;

	using FSLib.Extension;

	using Newtonsoft.Json;

	internal class LeftTicketData : IComparable<LeftTicketData>, INotifyPropertyChanged
	{
		public const int LargeQuantityOfTicket = int.MaxValue;

		private KnownColor _memoTextColorName;

		/// <summary>
		/// 创建 <see cref="LeftTicketData" />  的新实例(LeftTicketData)
		/// </summary>
		public LeftTicketData(int count, double? price, char code)
		{
			Count = count;
			Price = price;
			Code = code;
			NoTicket = count == 0;
			NotNeed = false;
		}

		int Weight => NoTicket ? -2 : NotAvailable ? -999 : NotSell ? -1 : NotNeed ? 0 : TicketForCompute;

		/// <summary>
		/// 比较当前对象和同一类型的另一对象。
		/// </summary>
		/// <returns>
		/// 一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此对象小于 <paramref name="other"/> 参数。零此对象等于 <paramref name="other"/>。大于零此对象大于 <paramref name="other"/>。
		/// </returns>
		/// <param name="other">与此对象进行比较的对象。</param>
		public int CompareTo(LeftTicketData other)
		{
			//无席别-无票-未卖-不需要-车票
			return Weight - other.Weight;
		}

		private int? _approximateCount;

		/// <summary>
		/// 大概数字
		/// </summary>
		public int? ApproximateCount
		{
			get => _approximateCount;
			set
			{
				if (value == _approximateCount) return;
				_approximateCount = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TicketForCompute));
			}
		}

		private char _code;

		/// <summary>
		/// 席别代码
		/// </summary>
		public char Code
		{
			get => _code;
			private set
			{
				if (value == _code) return;
				_code = value;
				OnPropertyChanged();
			}
		}

		public bool ContainsApproximateData => Count == LargeQuantityOfTicket;

		private int? _count;

		public int? Count
		{
			get => _count;
			set
			{
				if (value == _count) return;
				_count = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ContainsApproximateData));
				OnPropertyChanged(nameof(TicketForCompute));
			}
		}

		private bool _hasDiscount;

		/// <summary>
		/// 是否有折扣
		/// </summary>
		public bool HasDiscount
		{
			get => _hasDiscount;
			set
			{
				if (value == _hasDiscount) return;
				_hasDiscount = value;
				OnPropertyChanged();
			}
		}

		private string _memoText;

		/// <summary>
		/// 备注文本，优先显示
		/// </summary>
		public string MemoText
		{
			get => _memoText;
			set
			{
				if (value == _memoText) return;
				_memoText = value;
				OnPropertyChanged();
			}
		}

		private Brush _memoTextColor;

		/// <summary>
		/// 文本颜色，默认绿色
		/// </summary>
		[JsonIgnore]
		public Brush MemoTextColor
		{
			get => _memoTextColor;
			private set
			{
				if (Equals(value, _memoTextColor)) return;
				_memoTextColor = value;
				OnPropertyChanged();
			}
		}

		public KnownColor MemoTextColorName
		{
			get { return _memoTextColorName; }
			set
			{
				_memoTextColorName = value;
				MemoTextColor = BrushUtility.FromKnownName(value);
			}
		}

		private bool _notAvailable;

		public bool NotAvailable
		{
			get => _notAvailable;
			set
			{
				if (value == _notAvailable) return;
				_notAvailable = value;
				OnPropertyChanged();
			}
		}

		private bool _noTicket;

		public bool NoTicket
		{
			get => _noTicket;
			set
			{
				if (value == _noTicket) return;
				_noTicket = value;
				OnPropertyChanged();
			}
		}

		private bool _notNeed;

		public bool NotNeed
		{
			get => _notNeed;
			set
			{
				if (value == _notNeed) return;
				_notNeed = value;
				OnPropertyChanged();
			}
		}

		private bool _notSell;

		public bool NotSell
		{
			get => _notSell;
			set
			{
				if (value == _notSell) return;
				_notSell = value;
				OnPropertyChanged();
			}
		}

		private double? _price;

		public double? Price
		{
			get => _price;
			set
			{
				if (Nullable.Equals(value, _price)) return;
				_price = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// 用于参考计算的票数
		/// </summary>
		public int TicketForCompute => ApproximateCount ?? Count ?? 0;

		private int _hbLevel;

		public int HbLevel
		{
			get => _hbLevel;
			set
			{
				if (value == _hbLevel) return;
				_hbLevel = value;
				OnPropertyChanged();
			}
		}

		private string _hbInfo;

		public string HbInfo
		{
			get => _hbInfo;
			set
			{
				if (value == _hbInfo) return;
				_hbInfo = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}
