using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.BackupOrder
{
	[Flags]
	enum HbOrderLoadScope
	{
		None = 0,
		NotComplete = 1,
		NotProcessed = 2,
		Processed = 4,
		All = NotComplete | NotProcessed | Processed
	}
}
