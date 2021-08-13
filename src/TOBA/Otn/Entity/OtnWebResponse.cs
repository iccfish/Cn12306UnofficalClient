using System;

namespace TOBA.Otn.Entity
{
	using System.Collections.Generic;
	using System.Linq;

	using Newtonsoft.Json;

	using TOBA.Entity;

	internal class OtnWebResponse : Dto
	{
		[JsonProperty("validateMessagesShowId")]
		public string ValidateMessagesShowId { get; set; }

		[JsonProperty("Status")]
		public bool Status { get; set; }

		[JsonProperty("httpstatus")]
		public int HttpStatus { get; set; }

		[JsonProperty("messages")]
		public string[] Messages { get; set; }

		[JsonProperty("validateMessages")]
		public Dictionary<string, string> ValidateMessages { get; set; }

		[JsonProperty("attributes")]
		public string Attributes { get; set; }

		public static OtnWebResponse<T> Create<T>(T data)
		{
			return new OtnWebResponse<T>();
		}

		/// <summary>
		/// 获得错误信息
		/// </summary>
		/// <returns></returns>
		public virtual string GetErrorMessages(string defaultMsg = null)
		{
			return Messages == null || Messages.Length == 0 ? defaultMsg : "服务器信息：" + string.Join(";", Messages);
		}
	}

	/// <summary>
	/// 包装了OTN的响应
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class OtnWebResponse<T> : OtnWebResponse
	{
		/// <summary>
		/// 创建 <see cref="OtnWebResponse" />  的新实例(OtnWebResponse)
		/// </summary>
		public OtnWebResponse(T data)
		{
			Data = data;
		}

		public OtnWebResponse()
		{

		}


		[JsonProperty("Data")]
		public T Data { get; set; }

		/// <inheritdoc />
		public override string GetErrorMessages(string defaultMsg = null)
		{
			if (Data != null)
			{
				if (Data is BaseOtnApiResponseWithFlagAndMsg tmp1 && tmp1?.Msg.IsNullOrEmpty() == false)
				{
					return tmp1.Msg;
				}
			}
			return base.GetErrorMessages(defaultMsg);
		}
	}
}
