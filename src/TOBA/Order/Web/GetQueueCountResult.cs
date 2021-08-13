namespace TOBA.Order.Web
{
	using System.Linq;

	using Otn.Entity;

	using TOBA.Entity;

	internal class GetQueueCountResult : Dto
	{
		public string count { get; set; }
		public string ticket { get; set; }
		public string op_2 { get; set; }
		public string countT { get; set; }
		public string op_1 { get; set; }

		public string isRelogin { get; set; }
	}


	internal class GetQueueCountResponse : OtnWebResponse<GetQueueCountResult>
	{
	}

}
