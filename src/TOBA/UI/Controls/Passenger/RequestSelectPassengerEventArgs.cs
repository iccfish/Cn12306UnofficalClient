using System;

namespace TOBA.UI.Controls.Passenger
{
	using Entity.Web;

	/// <summary>
	/// 包含了联系人选择事件的数据
	/// </summary>
	internal class RequestSelectPassengerEventArgs : EventArgs
	{
		/// <summary>
		/// 获得关联的联系人
		/// </summary>
		public TOBA.Entity.PassengerInTicket Passenger { get; private set; }

		public Passenger PassengerRaw { get; }

		/// <summary>
		/// 创建 <see cref="RequestSelectPassengerEventArgs" />  的新实例(RequestSelectPassengerEventArgs)
		/// </summary>
		public RequestSelectPassengerEventArgs(Passenger passengerRaw)
		{
			Passenger = passengerRaw;
			PassengerRaw = passengerRaw;
		}

	}
}