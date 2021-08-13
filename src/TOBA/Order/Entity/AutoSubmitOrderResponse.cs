namespace TOBA.Order.Entity
{
	using System.Diagnostics.CodeAnalysis;
	using System.Drawing;
	using System.Linq;

	using Otn.Entity;

	using TOBA.Entity;

	[SuppressMessage("ReSharper", "InconsistentNaming")]
	internal class AutoSubmitOrderResult : Dto
	{
		public string result { get; set; }
		public bool submitStatus { get; set; }

		public bool isNoActive { get; set; }

		public string errMsg { get; set; }

		public string isRelogin { get; set; }

		public object isCheckOrderInfo { get; set; }

		public string IsNeedPassCode { get; set; }

		public string SmokeStr { get; set; }

		public string CheckSeatNum { get; set; }

		public string canChooseBeds { get; set; }
		public string canChooseSeats { get; set; }
		public string choose_Seats { get; set; }
		public string isCanChooseMid { get; set; }

		#region 2016年12月新增

		public string CanChooseBeds { get; set; }

		public string CanChooseSeats { get; set; }

		public string Choose_Seats { get; set; }

		public string IsCanChooseMid { get; set; }

		public int IfShowPassCodeTime { get; set; }

		public string IfShowPassCode { get; set; }

		#endregion
	}

	internal class AutoSubmitOrderResponse : OtnWebResponse<AutoSubmitOrderResult>
	{

	}
}
