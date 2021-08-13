using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace TOBA.Entity
{

	using Data;

	using Query.Entity;

	/// <summary>
	/// 分析文本的辅助类
	/// </summary>
	internal class ParseUtility
	{
		static Dictionary<string, Regex> _regCache = new Dictionary<string, Regex>();

		static Regex GetReg(string reg, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			var r = _regCache.GetValue(reg, _ =>
			{
				var ropt = opt.HasValue ? opt.Value : RegexOptions.None;
				if (singleLine) ropt |= RegexOptions.Singleline;
				else if (multiLine) ropt |= RegexOptions.Multiline;
				if (ignoreCase) ropt = RegexOptions.IgnoreCase;
				return new Regex(_, ropt);
			});
			return r;
		}

		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		/// <param name="reg"></param>
		/// <param name="input"></param>
		/// <param name="ignoreCase"></param>
		/// <param name="multiLine"></param>
		/// <param name="singleLine"></param>
		/// <param name="opt"></param>
		/// <returns></returns>
		public static Match Match(string reg, string input, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			var r = GetReg(reg, ignoreCase, multiLine, singleLine, opt);

			return r.Match(input);
		}


		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		/// <param name="reg"></param>
		/// <param name="input"></param>
		/// <param name="ignoreCase"></param>
		/// <param name="multiLine"></param>
		/// <param name="singleLine"></param>
		/// <param name="opt"></param>
		/// <returns></returns>
		public static void Match(string reg, string input, Action<string[]> action, Action failCallback = null, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			var m = Match(reg, input, ignoreCase, multiLine, singleLine, opt);
			if (m.Success)
			{
				action(m.Groups.Cast<Group>().Select(s => s.Value).ToArray());
			}
			else
			{
				if (failCallback != null) failCallback();
			}
		}


		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		/// <param name="reg"></param>
		/// <param name="input"></param>
		/// <param name="ignoreCase"></param>
		/// <param name="multiLine"></param>
		/// <param name="singleLine"></param>
		/// <param name="opt"></param>
		/// <returns></returns>
		public static void Match(string reg, string input, Action<string[]> action, string errorMsg = null, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			Match(reg, input, action, () =>
			{
				if (errorMsg.IsNullOrEmpty()) return;
				throw new InvalidOperationException("分析数据时发生错误：无法分析 " + errorMsg);
			}, ignoreCase, multiLine, singleLine, opt);
		}

		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		/// <param name="reg"></param>
		/// <param name="input"></param>
		/// <param name="ignoreCase"></param>
		/// <param name="multiLine"></param>
		/// <param name="singleLine"></param>
		/// <param name="opt"></param>
		/// <returns></returns>
		public static bool IsMatch(string reg, string input, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			var r = GetReg(reg, ignoreCase, multiLine, singleLine, opt);

			return r.IsMatch(input);
		}

		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		public static IEnumerable<Match> Matches(string reg, string input, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			var r = GetReg(reg, ignoreCase, multiLine, singleLine, opt);

			return r.Matches(input).Cast<Match>().Where(s => s.Success);
		}

		/// <summary>
		/// 匹配目标字符串
		/// </summary>
		public static void Matches(string reg, string input, Action<string[]> action, bool ignoreCase = true, bool multiLine = false, bool singleLine = false, RegexOptions? opt = null)
		{
			Matches(reg, input, ignoreCase, multiLine, singleLine, opt).ForEach(s => action(s.Groups.Cast<Group>().Select(m => m.Value).ToArray()));
		}


		///// <summary>
		///// 分析查票结果中的车站信息
		///// </summary>
		///// <param name="content"></param>
		///// <returns></returns>
		//internal static TicketStationInfo ParseQueryResultStationInfo(string content, bool isDepart, DateTime date)
		//{
		//	var model = new TicketStationInfo();
		//	Match(@"(<img.*?(first|last).*?>)?\s*(.*?)\s*<br.*?>\s*([\d]+):([\d]+)", content, _ =>
		//	{
		//		model.StationName = _[3];
		//		model.ArriveTime = isDepart ? (DateTime?)null : new DateTime(date.Year, date.Month, date.Day, _[4].ToInt32(), _[5].ToInt32(), 0);
		//		model.DepartureTime = isDepart ? new DateTime(date.Year, date.Month, date.Day, _[4].ToInt32(), _[5].ToInt32(), 0) : (DateTime?)null;
		//		model.IsFirst = _[2] == "first";
		//		model.IsEnd = _[2] == "last";
		//	}, "无法分析站点信息");
		//	return model;
		//}


		///// <summary>
		///// 分析查票结果中的车次信息
		///// </summary>
		///// <param name="lineContent"></param>
		///// <returns></returns>
		//public static QueryResultItem ParseTicketQuery(string lineContent, DateTime date)
		//{
		//	var now = DateTime.Now.Date;
		//	var args = lineContent.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		//	var m = new QueryResultItem();
		//	Match("id_([^']+)", args[1], _ =>
		//	{
		//		m.Id = _[1];
		//	}, "车次ID");
		//	Match(@"onStopHover\('([^']+)'\)", args[1], _ =>
		//	{
		//		m.TrainStopData = _[1];
		//	}, "停靠站信息ID");
		//	Match(@">([A-Z\d]+)<", args[1], _ =>
		//	{
		//		m.Code = _[1];
		//	}, "车次编号");
		//	m.FromStation = ParseQueryResultStationInfo(args[2], true, date);
		//	m.ToStation = ParseQueryResultStationInfo(args[3], false, date);
		//	Match(@"(\d+):(\d+)", args[4], _ =>
		//	{
		//		m.ElapsedTime = new TimeSpan(0, _[1].ToInt32(), _[2].ToInt32(), 0);
		//	}, "车票历时信息");
		//	//修正到达时间
		//	m.ToStation.ArriveTime = m.FromStation.DepartureTime.Value.Add(m.ElapsedTime);
		//	//余票信息
		//	_querySeatSequence.ForEachWithIndex((i, c) => m.TicketCount.Add(c, ParseLeftTicketData(args[i + 5])));

		//	if (args[16].IndexOf("起售") != -1)
		//	{
		//		Match(@"(0*(\d+)月0*(\d+)日)?(\d+)\s*点起售", args[16], _ =>
		//		{
		//			if (_[0].IsNullOrEmpty())
		//			{
		//				m.BeginSellTime = now.Date.AddHours(_[4].ToInt32());
		//			}
		//			else
		//			{
		//				var mm = _[2].ToInt32(now.Month);
		//				var dd = _[3].ToInt32(now.Day);
		//				var year = now.Year;
		//				if (mm < now.Month)
		//					year++;
		//				m.BeginSellTime = new DateTime(year, mm, dd, _[4].ToInt32(), 0, 0);
		//			}
		//		}, "起售时间");
		//	}

		//	m.IsAvailable = args[16].IndexOf("getSelected") != -1;
		//	if (m.IsAvailable)
		//	{
		//		Match(@"getSelected\('(.*?)'\)", args[16], _ =>
		//		{
		//			m.SubmitOrderInfo = _[1];
		//		}, "预定信息");
		//		//分析余票数据
		//		GetTicketCountFromOrderInfo(m.SubmitOrderInfo, m.TicketCount);
		//	}


		//	return m;
		//}

		/// <summary>
		/// 获得查询结果中一个席别的车票情况
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		static LeftTicketData ParseLeftTicketData(char code, string data)
		{
			if (data.IndexOf("--") != -1)
				return null;
			if (data.IndexOf("无") != -1) return new LeftTicketData(0, null, code);
			if (data.IndexOf("有") != -1) return new LeftTicketData(LeftTicketData.LargeQuantityOfTicket, null, code);
			if (data.IndexOf("*") != -1) return new LeftTicketData(0, null, code)
			{
				NotSell = true
			};


			return new LeftTicketData(data.ToInt32(), null, code);
		}

		static List<KeyValuePair<char, char>> _compMap = new List<KeyValuePair<char, char>>();

		static void ProcessSingleColumn(string data, QueryLeftTicketItem item, char code, Dictionary<char, LeftTicketData> dic)
		{
			var tag = ParseLeftTicketData(code, data);
			if (tag == null)
				return;

			//动卧，4->F
			if (!item.seat_types.IsNullOrEmpty() && item.seat_types.IndexOf(code) == -1)
			{
				var map = ParamData.GetSeatCompatibleMap(code);
				if (map != null)
				{
					var gcode = map.FirstOrDefault(s => item.seat_types.IndexOf(s) != -1);
					if (gcode != 0)
						code = gcode;
				}
			}

			dic[code] = tag;
		}

		//public static readonly Dictionary<string, char> QueryColumnToSeatTypeMap = new Dictionary<string, char>()
		//{
		//	["swz_num"] = '9',
		//	["tz_num"] = 'P',
		//	["zy_num"] = 'M',
		//	["ze_num"] = 'O',
		//	["gr_num"] = '6',
		//	["rw_num"] = '4',
		//	["yw_num"] = '3',
		//	["rz_num"] = '2',
		//	["yz_num"] = '1',
		//	["wz_num"] = '0',
		//	["qt_num"] = '*',
		//	["yb_num"] = '5',
		//	["gg_num"] = 'Q'
		//};



		/// <summary>
		/// 分析余票数据
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static void GetTicketCountFromOrderInfo(QueryLeftTicketItem item, Dictionary<char, LeftTicketData> dic)
		{
			//余票信息
			ProcessSingleColumn(item.swz_num, item, '9', dic);
			ProcessSingleColumn(item.tz_num, item, 'P', dic);
			ProcessSingleColumn(item.zy_num, item, 'M', dic);
			ProcessSingleColumn(item.ze_num, item, 'O', dic);
			ProcessSingleColumn(item.gr_num, item, '6', dic);
			ProcessSingleColumn(item.rw_num, item, '4', dic);
			ProcessSingleColumn(item.yw_num, item, '3', dic);
			ProcessSingleColumn(item.rz_num, item, '2', dic);
			ProcessSingleColumn(item.yz_num, item, '1', dic);
			ProcessSingleColumn(item.wz_num, item, '0', dic);
			ProcessSingleColumn(item.qt_num, item, '*', dic);
			//动卧
			ProcessSingleColumn(item.srrb_num, item, 'F', dic);

			var info = item.yp_info;
			if (Regex.IsMatch(info, @"^[A-Z\d]+$"))
			{
				Matches(@"([\dA-Za-z])(\d{5})(\d{4})", info, _ =>
				{
					var tcode = _[1][0];
					var count = _[3].TrimStart('0').ToInt32();
					var price = _[2][0] == '*' ? null : (double?)(_[2].ToDouble() / 10);

					if (count >= 3000)
					{
						tcode = '0';
						count -= 3000;
					}

					dic[tcode] = new LeftTicketData(count, price, tcode);
				});
			}
		}

		///// <summary>
		///// 将查询解析为车次数据
		///// </summary>
		///// <param name="content"></param>
		///// <param name="date"></param>
		///// <returns></returns>
		//public static QueryResult ParseQueryTrains(string content, DateTime date)
		//{
		//	var r = new QueryResult();
		//	GetReg(@"(?:\\[rn])+", singleLine: true).Split(System.Web.HttpUtility.HtmlDecode(content))
		//		.Select(s => s.Trim())
		//		.Where(s => !s.IsNullOrEmpty())
		//		.Select(s => ParseTicketQuery(s.Trim(), date)).ForEach(r.Add);

		//	return r;
		//}

		/// <summary>
		/// 分析表单域中的提交信息
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		public static NameValueCollection ParseHtmlFields(string html)
		{
			var nvc = new NameValueCollection();
			Matches(@"<input[\s\w\W]*?(name|value)=['""]([\s\w\W]*?)['""][\s\w\W]*?(name|value)=['""]([\s\w\W]*?)['""][\s\w\W]*?\/?>",
					html,
					_ =>
					{
						if (_[2] == "name") nvc[_[4]] = _[2];
						else nvc[_[2]] = _[4];
					});

			return nvc;
		}

		///// <summary>
		///// 分析表单中的SELECT域
		///// </summary>
		///// <param name="html"></param>
		///// <returns></returns>
		//public static NameValueCollection GenerateSelectFields(string html)
		//{
		//	var nvc = new NameValueCollection();

		//	foreach (var str in html.SplitByTag("<select", "</select>"))
		//	{
		//		var m = Match(@"name=['""]([^'""]+)['""]", str);
		//		if (!m.Success) continue;

		//		var mv = Match(@"<option[\s\w\W]+?(value=['""]([\s\w\W]+?)['""])?[\s\w\W]+?selected[\s\w\W]*?>([\s\w\W]+?)<\/option>", str);
		//		if (!mv.Success) continue;

		//		nvc[m.Groups[1].Value] = mv.Groups[2].SelectValue(s => s.Value).DefaultForEmpty(mv.Groups[3].Value);
		//	}

		//	return nvc;
		//}
	}
}
