
// namespaces...

namespace TOBA.Data
{
	using Autofac;

	using Configuration;

	using Entity;

	using Platform;

	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;

	// public classes...
	/// <summary>
	/// 预置的程序参数
	/// </summary>
	internal class ParamData
	{
		internal static void Init()
		{
			var conf = AppContext.ExtensionManager.GlobalKernel.Resolve<IWeb12306ConfProvider>().Current;
			if (conf == null)
			{
				StudentTicketDate = new List<Tuple<DateTime, DateTime>>();

				void AddRange(int sm, int sd, int em, int ed)
				{
					var year = DateTime.Now.Month > em ? DateTime.Now.Year : DateTime.Now.Year + 1;
					StudentTicketDate.Add(Tuple.Create(new DateTime(year, sm, sd), new DateTime(year, em, ed)));
				}

				AddRange(6, 1, 9, 30);
				AddRange(12, 1, 12, 31);
				AddRange(1, 1, 1, 31);


			}
			else
			{
				StudentTicketDate = conf.StudentDate.SplitPage(2).Select(x => Tuple.Create(x[0], x[1])).ToList();
				MaxSellDaysStu = conf.StuControl;
				MaxSellDays = conf.OtherControl;
			}


		}

		/// <summary>
		/// 点触验证码的区域偏移
		/// </summary>
		public static Size TouchClickPointOffset = new Size(0, -30);

		/// <summary>
		/// 性别代码列表
		/// </summary>
		public static readonly List<Sex> SexList = new List<Sex>
		{
			new Sex() {Code = "M"},
			new Sex() {Code = "F"}
		};

		/// <summary>
		/// 身份证件映射关系
		/// </summary>
		public static readonly Dictionary<char, string> PassengerIdType = new Dictionary<char, string>()
		{
			{'1', "二代身份证"},
			{'2', "一代身份证"},
			{'C', "港澳通行证"},
			{'G', "台湾通行证"},
			{'B', "护照"}
		};

		/// <summary>
		/// 席别映射表
		/// </summary>
		public static readonly Dictionary<char, string> SeatType = new Dictionary<char, string>()
		{
			{'9', "商务座"},
			{'P', "特等座"},
			{'M', "一等座"},
			{'O', "二等座"},
			{'6', "高级软卧"},
			{'4', "软卧"},
			{'3', "硬卧"},
			{'2', "软座"},
			{'B', "混编硬座"},
			{'1', "硬座"},
			{'0', "无座"},
			//{ 'S', "一等包座"},
			//{ 'Q', "观光座"},
			{'F', "动卧"},
			{'*', "其它"},
			{'H', "其它"}
		};

		/// <summary>
		/// 席别映射表
		/// </summary>
		public static readonly Dictionary<char, string> SeatTypeFull = new Dictionary<char, string>()
		{
			{'A', "高级动卧"},
			{'F', "动卧"},
			{'9', "商务座"},
			{'E', "特等软座"},
			{'P', "特等座"},
			{'M', "一等座"},
			{'O', "二等座"},
			{'7', "一等软座"},
			{'8', "二等软座"},
			{'6', "高级软卧"},
			{'5', "包厢硬卧"},
			{'4', "软卧"},
			{'3', "硬卧"},
			{'2', "软座"},
			{'1', "硬座"},
			{'0', "无座"},
			{'S', "一等包座"},
			{'B', "混编硬座"},
			{'Q', "观光座"},
			{'H', "一人软包"},
			{'G',"二人软包" },
			{'I',"一等双软" },
			{'J',"二等双软" },
			{'C',"混编硬卧" },
			{'K',"混编软座" },
			{'L',"混编软卧" },
			{'*', "其它"}
		};

		/// <summary>
		/// 席别类型反查字典
		/// </summary>
		public static readonly Dictionary<string, char> SeatTypeReverseMap = SeatTypeFull.ToDictionary(s => s.Value, s => s.Key);

		/// <summary>
		/// 学生票的预售期限
		/// </summary>
		public static List<Tuple<DateTime, DateTime>> StudentTicketDate { get; set; }

		/// <summary>
		/// 乘客类型
		/// </summary>
		public static readonly Dictionary<int, string> PassengerType = new Dictionary<int, string>
		{
			{1, "成人"},
			{2, "儿童"},
			{3, "学生"},
			{4, "残军"}
		};

		/// <summary>
		/// 车票类型
		/// </summary>
		public static readonly Dictionary<int, Entity.TicketType> TicketType = new Dictionary<int, TicketType>
		{
			{1, 1},
			{2, 2},
			{3, 3},
			{4, 4}
		};

		public static readonly Dictionary<string, string> FrequencyTrainCodeMap =
			new Dictionary<string, string>
			{
				{"任意车次", ".*"},
				{"任意高铁", "G.*"},
				{"任意动车", "D.*"},
				{"任意城铁", "C.*"},
				{"任意直达", "Z.*"},
				{"任意特快", "T.*"},
				{"任意快车", "K.*"},
				{"任意普客", @"\d*"},
				{"任意临客", "L.*"},
				{"任意G/D/C", "[GDC].*"},
				{"任意直达特快", "[TZ].*"}
			};

		/// <summary>
		/// 默认提示的车站编码
		/// </summary>
		public static readonly List<string> DefaultCityCode = new List<string>()
		{
			"BJP",
			"SHH",
			"TJP",
			"CQW",
			"CSQ",
			"CCT",
			"CDW",
			"FZS",
			"GZQ",
			"GIW",
			"HBB",
			"HFH",
			"HZH",
			"VUQ",
			"JNK",
			"KMM",
			"LSO",
			"LZJ",
			"NNZ",
			"NJH",
			"NCG",
			"SYT",
			"SJP",
			"TYV",
			"WMR",
			"WHN",
			"XXO",
			"XAY",
			"YIJ",
			"ZZF",
			"SZQ"
		};

		static Dictionary<string, TrainStation> _trainStationMap;
		static Dictionary<string, TrainStation> _trainStationLookupByName;

		/// <summary>
		/// 系统内置的时间段选择
		/// </summary>
		public static readonly int[][] BuildInTimeRange = new[]
		{
			new[] {0, 6},
			new[] {6, 12},
			new[] {12, 18},
			new[] {18, 24}
		};

		/// <summary>
		/// 每单最多联系人个数
		/// </summary>
		public static readonly int MaxPassengersPerOrder = 5;

		static Dictionary<string, string> _countryMap;

		static List<HashSet<char>> _compMap = new List<HashSet<char>>()
		{
			new HashSet<char> {'P', 'E'},
			new HashSet<char> {'M', '7'},
			new HashSet<char> {'O', '8'},
			new HashSet<char> {'6', 'A'},
			new HashSet<char> {'9', 'H'},
			new HashSet<char> {'3', '5', 'J'},
			new HashSet<char> {'4', 'I'},
			new HashSet<char> {'1', 'B'}
		};

		private static Dictionary<char, HashSet<char>> _compMapCache = new Dictionary<char, HashSet<char>>();

		/// <summary>
		/// 是否在系统维护中
		/// </summary>
		public static bool IsSystemMaintenance
		{
			get
			{
				//var hour = DateTime.Now.Hour;
				//var apiConf = ApiConfiguration.Instance;

				//return hour > apiConf.MaxOpenHour || hour < apiConf.MinOpenHour;
				return false;
			}
		}


		private static List<TrainStation> _trainStationList;

		/// <summary>
		/// 获得停靠站列表
		/// </summary>
		public static List<Entity.TrainStation> TrainStationList
		{
			get => _trainStationList;
			set => _trainStationList = value.GroupBy(s => s.Code).Select(s => s.First()).ToList();
		}

		/// <summary>
		/// 车站映射
		/// </summary>
		public static Dictionary<string, Entity.TrainStation> TrainStationMap
		{
			get { return _trainStationMap ??= TrainStationList.ToDictionary(s => s.Code, StringComparer.OrdinalIgnoreCase); }
		}

		/// <summary>
		/// 车站映射
		/// </summary>
		public static Dictionary<string, Entity.TrainStation> TrainStationLookupByName => _trainStationLookupByName ??= TrainStationList.ToDictionary(s => s.Name, StringComparer.OrdinalIgnoreCase);

		/// <summary>
		/// 获得或设置起售时间映射
		/// </summary>
		public static Dictionary<string, string> SellTimeMap { get; set; }

		/// <summary>
		/// 获得国家映射
		/// </summary>
		public static Dictionary<string, string> CountryMap => _countryMap ??= ResourceLoader.LoadResourceFile<Dictionary<string, string>>("country.json");

		/// <summary>
		/// 根据证件名获得编码
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static char GetPassengerIdTypeCodeByName(string name)
		{
			if (name.IndexOf("二代") != -1)
				return '1';
			if (name.IndexOf("一代") != -1)
				return '2';
			if (name.IndexOf("港澳") != -1)
				return 'C';
			if (name.IndexOf("台湾") != -1)
				return 'G';
			if (name.IndexOf("护照") != -1)
				return 'B';

			return '\0';
		}

		public static string GetSeatTypeName(char seatCode, string defaultName = "未知席别，请报告作者！")
		{
			return SeatTypeFull.GetValue(seatCode).DefaultForEmpty(defaultName);
		}

		/// <summary>
		/// 判断指定的日期是否在预售期中
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static bool IsDateInStudentRange(DateTime date)
		{
			return StudentTicketDate.FindIndex(s => s.Item1 <= date && s.Item2 >= date) != -1;
		}

		public static int MaxSellDaysStu { get; set; } = 30;

		public static int MaxSellDays { get; set; } = 30;

		/// <summary>
		/// 获得最大的预售期
		/// </summary>
		/// <returns></returns>
		public static DateTime GetMaxTicketDate(bool student)
		{
			if (!student)
			{
				var dt = DateTime.Now.Date;
				return dt.AddDays(MaxSellDays - 1);
			}

			return DateTime.Now.Date.AddDays(MaxSellDaysStu - 1);
		}

		public static DateTime GetBeginSellTime(DateTime dt, bool student = false)
		{
			dt = dt.Date;

			if (student && IsDateInStudentRange(dt))
				return dt.AddDays(-(MaxSellDaysStu - 1));
			return dt.AddDays(-(MaxSellDays - 1));
		}

		/// <summary>
		/// 确认指定日期是否出售普通票
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public static bool IsCommonTicketSell(DateTime date)
		{
			return date <= GetMaxTicketDate(false);
		}

		public static int GetPassengerTypeByName(string name)
		{
			if (name.IndexOf("成人") != -1)
				return 1;
			if (name.IndexOf("儿童") != -1)
				return 1;
			if (name.IndexOf("学生") != -1)
				return 1;
			if (name.IndexOf("残军") != -1)
				return 1;
			return 0;
		}

		public static TicketType GetTicketType(int type)
		{
			return TicketType.GetValue(type);
		}

		/// <summary>
		/// 根据车站电码获得车站名
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public static string GetStationNameByCode(string code)
		{
			return TrainStationMap.GetValue(code).SelectValue(s => s.Name);
		}

		/// <summary>
		/// 查找相似的站点
		/// </summary>
		/// <param name="station"></param>
		/// <returns></returns>
		public static List<TrainStation> FindSimilarStation(string station)
		{
			string extract = null;

			if (string.IsNullOrEmpty(station))
				return null;

			foreach (var trainStation in TrainStationList)
			{
				if ((trainStation.Name.IndexOf(station) == 0 || station.IndexOf(trainStation.Name) == 0) && (extract == null || extract.Length > trainStation.Name.Length))
					extract = trainStation.Name;
			}

			if (string.IsNullOrEmpty(extract))
				return null;

			var result = new List<TrainStation>();
			foreach (var trainStation in TrainStationList)
			{
				if (trainStation.Name.IndexOf(extract) == 0 || extract.IndexOf(trainStation.Name) == 0)
					result.Add(trainStation);
			}

			return result;
		}

		/// <summary>
		/// 获得适用于系统查询用的时间段
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public static KeyValuePair<int, int> GetPreferedTimeRangeForQuery(int start, int end)
		{
			var found = BuildInTimeRange.FirstOrDefault(s => start.IsValueInRange(s[0], s[1]) && end.IsValueInRange(s[0], s[1]));
			return found == null ? new KeyValuePair<int, int>(0, 24) : new KeyValuePair<int, int>(found[0], found[1]);
		}

		/// <summary>
		/// 获得兼容席别列表
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public static HashSet<char> GetSeatCompatibleMap(char code)
		{
			return _compMapCache.GetValue(code, c => { return _compMap.FirstOrDefault(s => s.Contains(code)) ?? new HashSet<char>() { c }; });
		}

		/// <summary>
		/// 获得指定席别可以选择的席位
		/// </summary>
		/// <param name="seat"></param>
		/// <returns></returns>
		public static SeatSubType[] GetSeatSubTypeEntityList(char seat) => GetSeatSubTypeList(seat)?.Select(s => new SeatSubType(s)).ToArray();

		/// <summary>
		/// 获得指定席别可以选择的席位
		/// </summary>
		/// <param name="seat"></param>
		/// <returns></returns>
		public static SubType[] GetSeatSubTypeList(char seat)
		{
			if (seat == '9' || seat == 'P')
			{
				//特等，商务
				return new[] { SubType.A, SubType.C, SubType.F };
			}

			if (seat == 'M')
			{
				//一等
				return new[] { SubType.A, SubType.C, SubType.D, SubType.F };
			}

			if (seat == 'O')
			{
				//二等
				return new[] { SubType.A, SubType.B, SubType.C, SubType.D, SubType.F };
			}

			if (seat == '6' || seat == '4')
			{
				//高级软卧，软卧
				return new[] { SubType.X, SubType.S };
			}

			if (seat == '3')
			{
				//硬卧
				return new[] { SubType.X, SubType.Z, SubType.S };
			}

			//软座，硬座，动卧待定。

			return null;
		}

		internal static readonly Dictionary<SubType, string> SeatSubTypeDisplayName = new Dictionary<SubType, string>()
		{
			[SubType.A] = "A(靠窗)",
			[SubType.B] = "B(中间)",
			[SubType.C] = "C(过道)",
			[SubType.D] = "D(过道)",
			[SubType.F] = "F(靠窗)",
			[SubType.X] = "下铺",
			[SubType.Z] = "中铺",
			[SubType.S] = "上铺"
		};

		internal static readonly Dictionary<SubType, string> SeatSubTypeDisplayNameSimple = new Dictionary<SubType, string>()
		{
			[SubType.A] = "A",
			[SubType.B] = "B",
			[SubType.C] = "C",
			[SubType.D] = "D",
			[SubType.F] = "F",
			[SubType.X] = "下",
			[SubType.Z] = "中",
			[SubType.S] = "上"
		};

		/// <summary>
		/// 学历
		/// </summary>
		internal static readonly Dictionary<int, string> Education = new Dictionary<int, string>()
		{
			[0] = "高中及以下",
			[1] = "大专",
			[2] = "本科",
			[3] = "硕士及以上"
		};

		internal static readonly Dictionary<int, string> Marriage = new Dictionary<int, string>()
		{
			[0] = "未婚",
			[1] = "已婚",
			[2] = "离异",
			[3] = "丧偶"
		};

		internal static readonly Dictionary<int, string> Annuals = new Dictionary<int, string>()
		{
			[1] = "10万以下",
			[2] = "10~20万",
			[3] = "20~50万",
			[4] = "50~100万",
			[5] = "100万以上"
		};

		internal static readonly Dictionary<int, string> Ocupations = new Dictionary<int, string>()
		{
			[1] = "国家机关/党群组织/企业/事业单位",
			[2] = "农/林/牧/渔/水利业生产人员",
			[3] = "商业/服务性人员",
			[4] = "军人",
			[5] = "生产/运输设备操作人员及有关人员",
			[6] = "专业技术人员",
			[7] = "办事人员和有关人员",
			[8] = "学生",
			[9] = "其他从业人员"
		};

		internal static readonly Dictionary<int, string> Provinces = new Dictionary<int, string>()
		{
			[11] = "北京",
			[12] = "天津",
			[13] = "河北",
			[14] = "山西",
			[15] = "内蒙古",
			[21] = "辽宁",
			[22] = "吉林",
			[23] = "黑龙江",
			[31] = "上海",
			[32] = "江苏",
			[33] = "浙江",
			[34] = "安徽",
			[35] = "福建",
			[36] = "江西",
			[37] = "山东",
			[41] = "河南",
			[42] = "湖北",
			[43] = "湖南",
			[44] = "广东",
			[45] = "广西",
			[46] = "海南",
			[50] = "重庆",
			[51] = "四川",
			[52] = "贵州",
			[53] = "云南",
			[54] = "西藏",
			[61] = "陕西",
			[62] = "甘肃",
			[63] = "青海",
			[64] = "宁夏",
			[65] = "新疆"
		};

		internal static bool IsSysOpen
		{
			get
			{
				var (isOpen, _) = GetSystemMaintenanceTime();
				return isOpen;
			}
		}

		/// <summary>
		/// 获得当前是否是系统休息时间，以及距离开放还有多久
		/// </summary>
		/// <returns></returns>
		internal static (bool isSystemMaintenance, int toOpenTime) GetSystemMaintenanceTime()
		{
			var time = RunTime.ServerTime;
			var timeValue = time.Hour * 60 + time.Minute;
			var cfg = ApiConfiguration.Instance;

			if (timeValue >= cfg.SystemOpenTime && timeValue <= cfg.SystemCloseTime)
				return (true, 0);

			var diff = timeValue < cfg.SystemOpenTime ? cfg.SystemOpenTime - timeValue : cfg.SystemOpenTime + 24 * 60 - timeValue;

			return (false, diff * 60 * 1000);
		}

		/// <summary>
		/// 获得当前是否是系统休息时间，以及距离开放还有多久
		/// </summary>
		/// <returns></returns>
		internal static (bool isSystemMaintenance, int toOpenTime) GetSubmitOrderOpenTime()
		{
			var time = RunTime.ServerTime;
			var timeValue = time.Hour * 60 + time.Minute;
			var cfg = ApiConfiguration.Instance;

			if (timeValue >= cfg.OrderSubmitOpenTime && timeValue <= cfg.OrderSubmitCloseTime)
				return (true, 0);

			var diff = timeValue < cfg.OrderSubmitOpenTime ? cfg.OrderSubmitOpenTime - timeValue : cfg.OrderSubmitOpenTime + 24 * 60 - timeValue;

			return (false, diff * 60 * 1000);
		}

		internal static Dictionary<RunningMode, string> ModeName { get; } = new Dictionary<RunningMode, string>()
		{
			[RunningMode.PreSell] = "起售",
			[RunningMode.CatchLeak] = "捡漏",
			[RunningMode.Professional] = "熟练"
		};

		public static Dictionary<string, string> BackNames = new Dictionary<string, string>()
		{
			{"33000010", "支付宝"},
			{"01020000", "中国工商银行"},
			{"01030000", "中国农业银行"},
			{"01040000", "中国银行"},
			{"01050000", "中国建设银行"},
			{"03080000", "招商银行"},
			{"00011000", "银联或发卡行"},
			{"00011001", "中铁银通公司"},
			{"01009999", "邮储银行"}
		};

		public static string GetBankName(string code) => BackNames.GetValue(code).DefaultForEmpty("发卡行");
	}
}
