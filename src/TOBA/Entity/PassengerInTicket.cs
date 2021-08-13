using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using Newtonsoft.Json;

	/// <summary>
	/// 表示一个订单中的乘客数据
	/// </summary>
	internal class PassengerInTicket : ICloneable
	{
		string _idNo;
		string _name;

		[JsonProperty("passenger_name")]
		public string Name
		{
			get { return _name; }
			set { _name = (value ?? "").Trim(); }
		}

		[JsonProperty("passenger_type")]
		public int TicketType { get; set; }

		[JsonIgnore]
		public char SeatType { get; set; }

		/// <summary>
		/// 原来的席别类型
		/// </summary>
		[JsonIgnore]
		public char OriginalSeatType { get; set; }

		[JsonIgnore]
		public SubType SeatSubType { get; set; }

		[JsonProperty("passenger_id_type_code")]
		public char IdType { get; set; }

		[JsonProperty("passenger_id_no")]
		public string IdNo
		{
			get { return _idNo; }
			set { _idNo = (value ?? "").Trim(); }
		}

		[JsonProperty("mobile_no")]
		public string Mobile { get; set; }

		public bool Save { get; set; }

		[JsonIgnore]
		public string AllEncStr { get; set; }

		[JsonIgnore]
		public string DisplayTitle
		{
			get { return Name + (TicketType == 2 ? "(儿童)" : "") + (TicketType == 3 ? "(学生)" : ""); }
		}

		/// <summary>创建作为当前实例副本的新对象。</summary>
		/// <returns>作为此实例副本的新对象。</returns>
		public object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// 创建儿童票
		/// </summary>
		/// <returns></returns>
		public PassengerInTicket CreateChild()
		{
			var pit = (PassengerInTicket)this.Clone();
			pit.TicketType = 2;
			
			return pit;
		}
	}
}
