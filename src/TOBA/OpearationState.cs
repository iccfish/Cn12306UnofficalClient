using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA
{
	/// <summary>
	/// 操作状态
	/// </summary>
	public enum OpearationState
	{
		Running = 0,
		Success = 1,
		Fail = 2,
		Wait = 3,
		Blocked = 4
	}
}
