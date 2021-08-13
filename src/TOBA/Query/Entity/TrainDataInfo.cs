using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TOBA.Query.Entity
{
	using TOBA.Entity;
	internal class TrainDataInfo : Dto
	{
		public TrainDataInfo(string datastr)
		{
			//if (datastr.IsNullOrEmpty())
			{
				DataTime = DateTime.Now;
			}
			//else
			{
				//	var dataargs = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(datastr)));
				//	DataSegments = dataargs.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
				//
				//	DataTime = FishDateTimeExtension.JsTicksStartBase.AddMilliseconds(DataSegments[15].ToInt64());
			}
		}

		public string[] DataSegments { get; private set; }

		/// <summary>
		/// 数据时间
		/// </summary>
		public DateTime DataTime { get; private set; }
	}
}
