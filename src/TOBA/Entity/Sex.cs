using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	internal class Sex : Dto
	{
		public string Code { get; set; }

		public string Name
		{
			get
			{
				return Code == "M" ? "男" : "女";
			}
		}
	}
}
