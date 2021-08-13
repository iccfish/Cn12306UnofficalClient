using System;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	interface IQueryTimeoutWarningService
	{

		event EventHandler TimeoutWarning;

		void Enque(bool timeout);
	}
}
