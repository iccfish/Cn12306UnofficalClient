namespace TOBA.Query.Entity
{
	using Data;

	using Newtonsoft.Json;

	using Otn.Entity;

	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	using TOBA.Entity;

	internal class QueryLeftTicketResponseSimple : OtnWebResponse<QueryLeftTicketResponseSimpleData>, IQueryLeftTicketResponse
	{

		[JsonProperty("c_url")]
		public string QueryUrl { get; set; }

		/// <summary>
		/// 将OTN实体转换为程序使用的信息
		/// </summary>
		/// <returns>
		///
		/// </returns>
		public QueryResult ToQueryResult(QueryParam query)
		{
			var data = Data?.Result;

			var r = query.CreateQueryResult(data?.Count ?? 0);
			if (data != null)
				r.AddRange(data.Select(s => TryConvertToResultItem(s, r, out var tmp) ? tmp : null).ExceptNull());
			return r;
		}

		public bool HasData => Data?.Result != null;

		static string CleanTeleCode(string code)
		{
			if (code.IsNullOrEmpty())
				return "";

			return Regex.Matches(code, "[a-zA-Z0-9]").Cast<Match>().Select(s => s.Value).JoinAsString("");
		}

		bool TryConvertToResultItem(string data, QueryResult result, out QueryResultItem item)
		{
			item = null;
			try
			{
				item = ConvertToResultItem(data, result);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}


		QueryResultItem ConvertToResultItem(string data, QueryResult result)
		{
			if (data.IsNullOrEmpty())
				return null;

			var cm = data.Split('|');
			//有时候数据有乱码
			if (cm.Length < 37)
				return null;

			var raw = new QueryLeftTicketResult();
			raw.secretStr = cm[0];
			raw.buttonTextInfo = cm[1];

			var cq = new QueryLeftTicketItem();
			raw.queryLeftNewDTO = cq;

			cq.train_no = cm[2];
			cq.station_train_code = CleanTeleCode(cm[3]);
			cq.start_station_telecode = CleanTeleCode(cm[4]);
			cq.end_station_telecode = CleanTeleCode(cm[5]);
			cq.from_station_telecode = CleanTeleCode(cm[6]);
			cq.to_station_telecode = CleanTeleCode(cm[7]);
			cq.start_time = cm[8];
			cq.arrive_time = cm[9];
			cq.lishi = cm[10];
			cq.canWebBuy = cm[11];
			cq.yp_info = cm[12];
			cq.start_train_date = cm[13];
			cq.train_seat_feature = cm[14];
			cq.location_code = cm[15];
			cq.from_station_no = cm[16];
			cq.to_station_no = cm[17];
			cq.is_support_card = cm[18];
			cq.controlled_train_flag = cm[19].ToInt32Nullable();
			cq.gg_num = cm[20].DefaultForEmpty("--");
			cq.gr_num = cm[21].DefaultForEmpty("--");
			cq.qt_num = cm[22].DefaultForEmpty("--");
			cq.rw_num = cm[23].DefaultForEmpty("--");
			cq.rz_num = cm[24].DefaultForEmpty("--");
			cq.tz_num = cm[25].DefaultForEmpty("--");
			cq.wz_num = cm[26].DefaultForEmpty("--");
			cq.yb_num = cm[27].DefaultForEmpty("--");
			cq.yw_num = cm[28].DefaultForEmpty("--");
			cq.yz_num = cm[29].DefaultForEmpty("--");
			cq.ze_num = cm[30].DefaultForEmpty("--");
			cq.zy_num = cm[31].DefaultForEmpty("--");
			cq.swz_num = cm[32].DefaultForEmpty("--");
			//新增的动卧
			cq.srrb_num = cm[33].DefaultForEmpty("--");
			cq.yp_ex = cm[34];
			cq.seat_types = cm[35];
			cq.from_station_name = Data.Map[cq.from_station_telecode];
			cq.to_station_name = Data.Map[cq.to_station_telecode];
			cq.exchange_train_flag = cm[36];
			cq.AllowBackup = cm[37] == "1";

            var flags = (cm[46] ?? "").Split('#');
            cq.IsSmartD = flags[0].StartsWith("5");
            cq.IsFuXing = flags.Length > 1 && flags[1] == "1";
            cq.IsQuiet  = flags.Length > 2 && flags[2].Contains("Q");
			//部分数据兼容修复
			cq.start_station_name = ParamData.GetStationNameByCode(cq.start_station_telecode);
			cq.end_station_name = ParamData.GetStationNameByCode(cq.end_station_telecode);

			//历时
			var elm = Regex.Match(cq.lishi ?? "", @"(\d+):(\d+)");
			if (elm.Success)
			{
				cq.lishiValue = elm.GetGroupValue(1).ToInt32() * 60 + elm.GetGroupValue(2).ToInt32();
			}

			return raw.ToQueryResultItem(result);
		}

	}
}