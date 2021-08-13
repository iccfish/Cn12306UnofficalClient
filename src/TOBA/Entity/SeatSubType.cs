namespace TOBA.Entity
{
	using Data;

	internal class SeatSubType : Dto
	{
		/// <summary>
		/// 创建 <see cref="SeatSubType" />  的新实例(SeatType)
		/// </summary>
		public SeatSubType(SubType id)
		{
			Id = id;

			switch (id)
			{
				case SubType.Random:
					DisplayName = "随机";
					break;
				default:
					DisplayName = ParamData.SeatSubTypeDisplayName[id];
					break;
			}
		}

		public SubType Id { get; set; }

		public string DisplayName { get; private set; }

		public override string ToString()
		{
			return DisplayName;
		}
	}

	enum SubType
	{
		Random,
		X,
		Z,
		S,
		A,
		B,
		C,
		D,
		F
	}
}

