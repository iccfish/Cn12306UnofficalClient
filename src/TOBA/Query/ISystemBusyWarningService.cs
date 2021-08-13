namespace TOBA.Query
{
	using System;

	interface ISystemBusyWarningService
	{

		event EventHandler SystemBusyWarning;

		void Enque(bool timeout);
	}
}