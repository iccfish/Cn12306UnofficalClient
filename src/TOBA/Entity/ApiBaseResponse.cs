namespace TOBA.Entity
{
	class ApiBaseResponse : Dto
	{
		public bool Success { get; set; }

		public string Message { get; set; }

		public int Code { get; set; }

	}
}