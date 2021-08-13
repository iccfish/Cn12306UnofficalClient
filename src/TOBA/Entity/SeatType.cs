namespace TOBA.Entity
{
	using Data;

	internal class SeatType : Dto
	{
		/// <summary>
		/// 创建 <see cref="SeatType" />  的新实例(SeatType)
		/// </summary>
		public SeatType(char id)
		{
			Id = id;
			DisplayName = ParamData.SeatTypeFull[id];
		}

		public char Id { get; set; }

		public string DisplayName { get; private set; }

		public override string ToString()
		{
			return DisplayName;
		}

		public static implicit operator SeatType(char code)
		{
			return new SeatType(code);
		}
	}
}