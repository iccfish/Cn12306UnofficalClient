using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.BackupOrder.Entity
{
	using DateTime = System.DateTime;

	class HbQueueInfoCacheItem
	{
		public string Info { get; set; }

		public int Level { get; set; }

		public DateTime UpdateTime { get; set; }

		public DateTime LastHitTime { get; set; }

		public bool Selected { get; set; }

		public void Update(int level, string msg, bool selected)
		{
			UpdateTime = DateTime.Now;
			LastHitTime = UpdateTime;
			Info = msg;
			Level = level;
			Selected = selected;
		}
	}
}
