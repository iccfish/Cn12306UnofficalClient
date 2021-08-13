using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	using System.Collections.Specialized;
	using System.Net;
	using System.Net.Cache;
	using System.Threading.Tasks;
	using System.Web;
	using Configuration;

	using Entity;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using TOBA.Entity;

	using WebLib;

	class LeftTicketQueryService : ILeftTicketQueryService
	{
		const string _queryInfoKey = "queryInfoOpn";
		private NetClient _client;
		TicketQueryRequestParamInfo _queryInfo;

		public LeftTicketQueryService()
		{
			_client = new NetClient();

#if !DEBUG
			(_client.HttpHandler as NetClientHandler).ForbiddenLocalProxy = true;
#endif
		}

		bool InitQueryInfo(QueryParam query)
		{
			if (_queryInfo != null)
				return true;

			if (_queryInfo == null || _queryInfo.QueryFlag != query.Resign)
			{
				lock (this)
				{
					if (_queryInfo == null || _queryInfo.QueryFlag != query.Resign)
					{
						HttpContext<string> contextHtmlContext;
						contextHtmlContext = _client.RunRequestLoop(_ => _client.Create<string>(HttpMethod.Get, "http://www.12306.cn/opn/leftTicket/init", "http://www.12306.cn/opn/login/init"));
						if (!contextHtmlContext.IsValid())
						{
							return false;
						}
						var html = contextHtmlContext.Result;
						var matches = ParseUtility.Matches(@"(CLeftTicketUrl|isSaveQueryLog)\s*=\s*['""]([^'""]+)['""]", html).ToArray();
						var saveQueryLog = matches.Where(s => s.Success && s.Groups[1].Value.IsIgnoreCaseEqualTo("isSaveQueryLog")).Select(s => s.Groups[2].Value).FirstOrDefault() == "Y";
						var queryUrl = matches.Where(s => s.Success && s.Groups[1].Value.IsIgnoreCaseEqualTo("CLeftTicketUrl")).Select(s => s.Groups[2].Value).FirstOrDefault();

						if (string.IsNullOrEmpty(queryUrl))
						{
							return false;
						}
						_queryInfo = new TicketQueryRequestParamInfo()
						{
							EnableSaveLog = saveQueryLog,
							LastSaveLogTime = DateTime.MinValue,
							QueryFlag = query.Resign,
							Url = "http://www.12306.cn/opn/" + queryUrl
						};
					}
				}
			}

			return _queryInfo != null;
		}


		public async Task<Dictionary<string, QueryResultItem>> QueryLeftTicketAsync(QueryParam query)
		{
			//页面参数
			if (!InitQueryInfo(query))
				return null;

			var fromName = query.FromStationName;
			var fromCode = query.FromStationCode;
			var toName = query.ToStationName;
			var toCode = query.ToStationCode;
			var date = query.CurrentDepartureDate;

			//组装查询参数
			var querydata = new NameValueCollection();
			querydata.Add("leftTicketDTO.train_date", date.ToString("yyyy-MM-dd"));
			querydata.Add("leftTicketDTO.from_station", fromCode);
			querydata.Add("leftTicketDTO.to_station", toCode);
			querydata.Add("purpose_codes", query.QueryStudentTicket ? "0X00" : "ADULT");

			//设置Cookies
			const string cokDomain = "www.12306.cn";
			const string cokPath = "/";
			var coc = new CookieCollection();
			coc.Add(new Cookie("_jc_save_fromStation", HttpUtility.UrlEncodeUnicode(fromName + "," + fromCode), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_toStation", HttpUtility.UrlEncodeUnicode(toName + "," + toCode), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_fromDate", date.ToString("yyyy-MM-dd"), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_toDate", DateTime.Now.ToString("yyyy-MM-dd"), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_wfdc_flag", query.Resign ? "gc" : "dc", cokPath, cokDomain));

			_client.CookieContainer.Add(coc);

			Dictionary<string, object> contextData = null;

			var task = _client.Create<string>(HttpMethod.Get, _queryInfo.Url, "http://www.12306.cn/opn/leftTicket/init", querydata, isXhr: true, contextData: contextData);
			task.Request.HttpRequestCacheLevel = HttpRequestCacheLevel.NoCacheNoStore;
			task.Request.Headers.Add(HttpRequestHeader.IfNoneMatch, "0");
			task.Request.IfModifiedSince = DateTime.MinValue;
			task.Request.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
			task.Send();

			if (!task.IsSuccess)
			{
				return null;
			}
			if (task.Result == null)
			{
				return null;
			}

			IQueryLeftTicketResponse response;
			try
			{
				if (task.Result.IndexOf("\"flag\":\"1\"", StringComparison.OrdinalIgnoreCase) != -1)
				{
					response = JsonConvert.DeserializeObject<QueryLeftTicketResponseSimple>(task.Result);
				}
				else
				{
					response = JsonConvert.DeserializeObject<QueryLeftTicketResponseComplex>(task.Result);
				}
			}
			catch (Exception e)
			{
				//Message = "无法识别的服务器响应，可能官网已升级：" + task.Result;
				return null;
			}


			if (!string.IsNullOrEmpty(response.QueryUrl))
			{
				_queryInfo.Url = response.QueryUrl;
				return await QueryLeftTicketAsync(query);
			}

			if (!response.HasData)
			{
				return null;
			}

			var result = response.ToQueryResult(query);

			return result.ToDictionary(s => s.Id);
		}
	}
}
