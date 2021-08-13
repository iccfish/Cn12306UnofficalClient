using Newtonsoft.Json;

using System;
using System.ComponentModel;
using System.Linq;

namespace TOBA.Entity.Web
{
	using Data;

	using FSLib.Extension;

	using TOBA.Configuration;

	internal class Passenger : Dto, INotifyPropertyChanged, ICloneable
	{

		DateTime _addDate;
		string _address;
		string _code;
		string _countryCode;
		string _email;


		string _firstLetter;
		string _flag;
		string _idNo;
		char _idTypeCode;
		string _idTypeName;
		string _isUserSelf;
		string _mobileNo;
		string _name;

		PassengerVerificationResult _result;
		string _sex;
		int _type;
		string _typeName;

		public Passenger()
		{
			Sex = "M";
			Type = 1;
			IdTypeCode = '1';
			CountryCode = "CN";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// 创建作为当前实例副本的新对象。
		/// </summary>
		/// <returns>
		/// 作为此实例副本的新对象。
		/// </returns>
		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool IsMatch(bool onlyStudent, string key)
		{
			if (onlyStudent && Type != 3)
				return false;

			if (key.IsNullOrEmpty())
				return true;

			return (FirstLetter ?? "").Contains(key, StringComparison.OrdinalIgnoreCase)
				|| (Name ?? "").Contains(key, StringComparison.OrdinalIgnoreCase)
				|| (MobileNo ?? "").Contains(key, StringComparison.OrdinalIgnoreCase)
				|| (IdNo ?? "").Contains(key, StringComparison.OrdinalIgnoreCase);
		}

		public void SetId(char type, string id)
		{
			IdTypeCode = type;
			IdNo = id;
			IdTypeName = ParamData.PassengerIdType.GetValue(type);

			if (type != '1' || id.Length < 17)
				return;
			var m = id.RegMatch(@"\d{6}(\d{4})(\d{2})(\d{2})\d{2}(\d)");
			if (m == null)
				return;
			//var dt = (m[1] + "-" + m[2] + "-" + m[3]).ToDateTimeNullable();
			//if (dt != null && dt.Value.Year > 1900)
			//{
			//	BornDate = dt.Value.ToString("yyyy-MM-dd");
			//}
			Sex = m[4].ToInt32() % 2 == 1 ? "M" : "F";
		}

		[JsonProperty("born_date")]
		public DateTime AddDate
		{
			get { return _addDate; }
			set
			{
				if (value.Equals(_addDate)) return;
				_addDate = value;
				OnPropertyChanged(nameof(AddDate));
			}
		}

		[JsonProperty("address")]
		public string Address
		{
			get { return _address; }
			set
			{
				if (value == _address)
					return;
				_address = value;
				OnPropertyChanged("Address");
			}
		}

		public bool CanAddIntoOrder
		{
			get { return Verification.Verified == true || IdTypeCode == 'C' || IdTypeCode == 'G' || IdTypeCode == 'B'; }
		}

		[JsonProperty("code")]
		public string Code
		{
			get { return _code; }
			set
			{
				if (value == _code)
					return;
				_code = value;
				OnPropertyChanged("Code");
			}
		}

		[JsonProperty("country_code")]
		public string CountryCode
		{
			get { return _countryCode; }
			set
			{
				if (value == _countryCode)
					return;
				_countryCode = value;
				OnPropertyChanged("CountryCode");
			}
		}

		/// <summary>
		/// 获得联系人可以删除的时间
		/// </summary>
		public DateTime? DeleteTime
		{
			get
			{
				if (AddDate.Year < 1900)
					return null;
				if (AddDate.Year < 2000)
				{
					return new DateTime(2000, 1, 1);
				}

				return AddDate.Date.AddDays(ApiConfiguration.Instance.MinDeletePassengerDays);
			}
		}

		[JsonIgnore]
		public string DisplayTitle
		{
			get
			{
				return Name + (Type == 2 ? "(儿童)" : "") + (Type == 3 ? "(学生)" : "");
			}
		}

		[JsonProperty("email")]
		public string Email
		{
			get { return _email; }
			set
			{
				if (value == _email)
					return;
				_email = value;
				OnPropertyChanged("Email");
			}
		}

		[JsonProperty("first_letter")]
		public string FirstLetter
		{
			get { return _firstLetter; }
			set
			{
				if (value == _firstLetter) return;
				_firstLetter = value;
				OnPropertyChanged("FirstLetter");
			}
		}

		[JsonProperty("passenger_flag")]
		public string Flag
		{
			get { return _flag; }
			set
			{
				if (value == _flag) return;
				_flag = value;
				OnPropertyChanged("Flag");
			}
		}

		[JsonProperty("passenger_id_no")]
		public string IdNo
		{
			get { return _idNo; }
			set
			{
				if (value == _idNo) return;
				_idNo = value;
				OnPropertyChanged("IdNo");
			}
		}

		[JsonProperty("passenger_id_type_code")]
		public char IdTypeCode
		{
			get { return _idTypeCode; }
			set
			{
				if (value == _idTypeCode) return;
				_idTypeCode = value;
				OnPropertyChanged("IdTypeCode");
			}
		}

		[JsonProperty("passenger_id_type_name")]
		public string IdTypeName
		{
			get { return _idTypeName; }
			set
			{
				if (value == _idTypeName) return;
				_idTypeName = value;
				OnPropertyChanged("IdTypeName");
			}
		}

		[JsonProperty("isUserSelf")]
		public string IsUserSelf
		{
			get { return _isUserSelf; }
			set
			{
				if (value == _isUserSelf) return;
				_isUserSelf = value;
				OnPropertyChanged("IsUserSelf");
			}
		}

		[JsonProperty("mobile_no")]
		public string MobileNo
		{
			get { return _mobileNo; }
			set
			{
				if (value == _mobileNo) return;
				_mobileNo = value;
				OnPropertyChanged("MobileNo");
			}
		}

		[JsonProperty("passenger_name")]
		public string Name
		{
			get { return _name; }
			set
			{
				if (value == _name) return;
				_name = value;
				OnPropertyChanged("Name");
				OnPropertyChanged("DisplayTitle");
			}
		}

		[JsonProperty("sex_code")]
		public string Sex
		{
			get { return _sex; }
			set
			{
				if (value == _sex)
					return;
				_sex = value;
				OnPropertyChanged("Sex");
			}
		}

		[JsonProperty("total_times")]
		public int TotalTimes { get; set; }

		[JsonProperty("passenger_type")]
		public int Type
		{
			get { return _type; }
			set
			{
				if (value == _type || value < 1 || value > 4) return;
				_type = value;
				OnPropertyChanged("Type");
				OnPropertyChanged("DisplayTitle");
			}
		}

		[JsonProperty("passenger_type_name")]
		public string TypeName
		{
			get { return _typeName; }
			set
			{
				if (value == _typeName) return;
				_typeName = value;
				OnPropertyChanged("TypeName");
			}
		}

		[JsonIgnore]
		public PassengerVerificationResult Verification
		{
			get
			{
				if (_result == null)
				{
					var message = "";
					var result = this.Verify(out message);
					_result = new PassengerVerificationResult
					{
						Verified = result,
						VerifyMessage = message
					};
				}
				return _result;
			}
		}


		[JsonProperty("allEncStr")]
		public string AllEncStr { get; set; }


		public static implicit operator PassengerInTicket(Passenger p)
		{
			return new PassengerInTicket()
			{
				IdNo = p.IdNo,
				Name = p.Name,
				Mobile = p.MobileNo,
				IdType = p.IdTypeCode,
				TicketType = p.Type,
				AllEncStr = p.AllEncStr
			};
		}

		public Passenger CreateChild()
		{
			var p = (Passenger)this.Clone();
			p.Type = 2;
			p.TypeName = ParamData.PassengerType[2];

			return p;
		}
	}

	internal class PassengerVerificationResult
	{
		public bool? Verified { get; set; }

		public string VerifyMessage { get; set; }
	}

	internal static class PassengerExtensionMethods
	{
		/// <summary>
		/// 验证乘客状态
		/// </summary>
		/// <param name="p"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public static bool? Verify(this Passenger p, out string message)
		{
			message = null;

			if (p.IdTypeCode == '2')
			{
				message = "一代身份证不再可用，请更新为二代身份证。";
				return false;
			}

			if (p.TotalTimes == 95 || p.TotalTimes == 97)
			{
				return true;
			}
			if (p.TotalTimes == 93 || p.TotalTimes == 99)
			{
				if (p.IdTypeCode != '1')
				{
					message = "已预通过。";
				}
				return true;
			}
			if (p.TotalTimes == 94 || p.TotalTimes == 96)
			{
				if (p.IdTypeCode == '1')
				{
					message = "已通过校验，但未通过审查";
				}
				else
				{
					message = "未通过校验";
				}
				return false;
			}
			if (p.TotalTimes == 92 || p.TotalTimes == 98)
			{
				if (p.IdTypeCode == 'B' || p.IdTypeCode == 'C' || p.IdTypeCode == 'D' || p.IdTypeCode == 'G')
				{
					message = "未审核，请到窗口办理预校验";
				}
				else
				{
					message = "未经校验，请到窗口办理校验";
				}
				return null;
			}
			if (p.TotalTimes == 91)
			{
				if (p.IdTypeCode == 'B' || p.IdTypeCode == 'C' || p.IdTypeCode == 'D' || p.IdTypeCode == 'G')
				{
					message = "未经校验，请到窗口办理校验";
				}
				else
				{
					message = "未经校验，请到窗口办理校验";
				}
				return null;
			}

			message = "状态未知，请刷新联系人列表";
			return null;
		}
	}
}
