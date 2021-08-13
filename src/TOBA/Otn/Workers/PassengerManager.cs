namespace TOBA.Otn.Workers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using TOBA.Entity;
	using TOBA.Entity.Web;

	using UI;

	internal class PassengerManager : IOperation
	{
		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		#endregion

		public PassengerManager()
		{

		}

		public PassengerManager(Session session) => Session = session;

		public Passenger FillFullInfo(Passenger pas)
		{
			//if (pas.BornDate != null)
			//	return pas;

			return pas;
			//var task = Session.NetClient.SubmitRequest<string>(
			//												 "passengerAction.do?method=getPagePassengerAll",
			//												"passengerAction.do?method=initUsualPassenger12306",
			//												HttpMethod.POST,
			//												new
			//												{
			//													pageIndex = 0,
			//													pageSize = 7,
			//													passenger_name = pas.Name
			//												});
			//var anonymousType = new
			//{
			//	rows = new TOBA.Entity.Web.PassengerList()
			//};
			//Newtonsoft.Json.JsonConvert.PopulateObject(task.Result, anonymousType);

			////查找联系人
			//var pasfull = anonymousType.rows.FirstOrDefault(s => s.Name == pas.Name && s.IdTypeCode == pas.IdTypeCode && s.IdNo == pas.IdNo && s.Type == pas.Type);
			//return pasfull;
		}

		public async Task<string> DeletePassengerAsync(params Passenger[] passlist)
		{
			return await Task<string>.Factory.StartNew(() => DeletePassenger(passlist));
		}

		public string DeletePassenger(params Passenger[] passlist)
		{
			var data = new
			{
				passenger_name = passlist.Select(s => s.Name).JoinAsString("#") + "#",
				passenger_id_type_code = passlist.Select(s => s.IdTypeCode).JoinAsString(""),
				passenger_id_no = passlist.Select(s => s.IdNo).JoinAsString("#") + "#",
				isUserSelf = passlist.Select(s => s.IsUserSelf).JoinAsString("#"),
				allEncStr = passlist.Select(s => s.AllEncStr).JoinAsString("#")
			};
			//提交
			var task = Session.NetClient.Create(HttpMethod.Post,
					"passengers/delete",
					"passengers/show",
					data,
					new { status = false, data = new { flag = false, message = "" } }).
				Send();
			if (task == null || !task.IsSuccess)
			{
				return task.GetExceptionMessage("网络错误");
			}
			if (task.Result.status && task.Result.data.flag)
			{
				return null;
			}
			else
			{
				return task.Result.data.message.DefaultForEmpty("未知错误");
			}

		}

		public string AddPassenger(Passenger p)
		{
			var data = new Dictionary<string, string>()
			{
				{"passenger_name", p.Name},
				{"sex_code", p.Sex},
				{"passenger_id_no", p.IdNo},
				{"mobile_no", p.MobileNo},
				{"phone_no", ""},
				{"email", p.Email ?? ""},
				{"address", p.Address ?? ""},
				{"postalcode", ""},
				{"studentInfoDTO.school_code", ""},
				{"studentInfoDTO.school_name", "简码/汉字"},
				{"studentInfoDTO.department", ""},
				{"studentInfoDTO.school_class", ""},
				{"studentInfoDTO.student_no", ""},
				{"studentInfoDTO.preference_card_no", ""},
				{"GAT_valid_date_end", ""},
				{"GAT_born_date", ""},
				{"old_passenger_name", ""},
				{"country_code", p.CountryCode},
				{"_birthDate", "2017-01-05"},
				{"old_passenger_id_type_code", ""},
				{"passenger_id_type_code", p.IdTypeCode.ToString()},
				{"old_passenger_id_no", ""},

				{"passenger_type", p.Type.ToString()},
				{"studentInfoDTO.province_code", "11"},
				{"studentInfoDTO.school_system", "1"},
				{"studentInfoDTO.enter_year", "2002"},
				{"studentInfoDTO.preference_from_station_name", ""},
				{"studentInfoDTO.preference_from_station_code", ""},
				{"studentInfoDTO.preference_to_station_name", ""},
				{"studentInfoDTO.preference_to_station_code", ""}
			};
			var task = Session.NetClient.Create<string>(HttpMethod.Post,
					"passengers/add",
					"passengers/addInit",
					data).
				Send();
			if (task == null || !task.IsSuccess)
			{
				return "网络错误";
			}

			try
			{
				var obj = JsonConvert.DeserializeAnonymousType(task.Result, new { status = false, data = new { flag = false, message = "" } });
				if (obj.status && obj.data.flag)
				{
					//不延迟看不到新的信息啊。
					Thread.Sleep(2000);
					Session.LoadPassengers();
					return null;
				}
				else
				{
					return obj.data.SelectValue(s => s.message).DefaultForEmpty("未知错误");
				}
			}
			catch (Exception ex)
			{
				return "网络错误：" + ex.Message;
			}
		}
		public string EditPassenger(Passenger p, string oldName, char oldIdType, string oldId)
		{
			var data = new Dictionary<string, string>()
			{
				{"passenger_name", p.Name},
				{"sex_code", p.Sex},
				{"passenger_id_no", p.IdNo},
				{"mobile_no", p.MobileNo},
				{"phone_no", ""},
				{"email", p.Email ?? ""},
				{"address", p.Address ?? ""},
				{"postalcode", ""},
				{"studentInfoDTO.school_code", ""},
				{"studentInfoDTO.school_name", "简码/汉字"},
				{"studentInfoDTO.department", ""},
				{"studentInfoDTO.school_class", ""},
				{"studentInfoDTO.student_no", ""},
				{"studentInfoDTO.preference_card_no", ""},
				{"GAT_valid_date_end", ""},
				{"GAT_born_date", ""},
				{"old_passenger_name", oldName},
				{"country_code", p.CountryCode},
				{"_birthDate", "2017-01-05"},
				{"old_passenger_id_type_code", oldIdType.ToString()},
				{"passenger_id_type_code", p.IdTypeCode.ToString()},
				{"old_passenger_id_no", oldId},

				{"passenger_type", p.Type.ToString()},
				{"studentInfoDTO.province_code", "11"},
				{"studentInfoDTO.school_system", "1"},
				{"studentInfoDTO.enter_year", "2002"},
				{"studentInfoDTO.preference_from_station_name", ""},
				{"studentInfoDTO.preference_from_station_code", ""},
				{"studentInfoDTO.preference_to_station_name", ""},
				{"studentInfoDTO.preference_to_station_code", ""},
				{"allEncStr", p.AllEncStr}
			};
			//获得TOKEN
			var task = Session.NetClient.Create<string>(HttpMethod.Post, "passengers/edit", "passengers/show", data).Send();
			if (task == null || !task.IsSuccess)
			{
				return "网络错误";
			}

			try
			{
				var obj = JsonConvert.DeserializeAnonymousType(task.Result, new { status = false, data = new { flag = false, message = "" } });
				if (obj.status && obj.data.flag)
				{
					Session.LoadPassengers();
					return null;
				}
				else
				{
					return obj.data.SelectValue(s => s.message).DefaultForEmpty("未知错误");
				}
			}
			catch (Exception ex)
			{
				return "网络错误：" + ex.Message;
			}
		}
	}
}
