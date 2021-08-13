namespace TOBA.BackupOrder
{

	using Newtonsoft.Json;

	class CheckFaceResult
	{
		[JsonProperty("face_flag")]
		public bool FaceFlag { get; set; }

		[JsonProperty("login_flag")]
		public bool LoginFlag { get; set; }

		[JsonProperty("face_check_code")]
		public int FaceCheckCode { get; set; }
	}
}
