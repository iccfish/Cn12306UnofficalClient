namespace TOBA.Entity
{
	internal class TicketType : Dto
	{
		public int Id { get; private set; }

		/// <summary>
		/// 创建 <see cref="TicketType" />  的新实例(TicketType)
		/// </summary>
		public TicketType(int id)
		{
			Id = id;
		}

		public override string ToString()
		{
			switch (Id)
			{
				case 1:
					return "成人票";
				case 2:
					return "儿童票";
				case 3:
					return "学生票";
				case 4:
					return "残军票";
				default:
					break;
			}
			return "未知类型";
		}

		public static implicit operator string(TicketType t)
		{
			return t.ToString();
		}

		public static implicit operator int(TicketType t)
		{
			return t.Id;
		}

		public static implicit operator TicketType(int id)
		{
			return new TicketType(id);
		}

		public string DisplayName
		{
			get { return this.ToString(); }
		}
	}
}
