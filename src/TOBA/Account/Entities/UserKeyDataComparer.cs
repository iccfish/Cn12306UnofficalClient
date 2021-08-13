using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account.Entities
{
	using Profile;
	class UserKeyDataComparer : IComparer<KeyValuePair<string, UserKeyData>>
	{
		public int Compare(KeyValuePair<string, UserKeyData> x, KeyValuePair<string, UserKeyData> y)
		{
			if (x.Value?.LastLoginTime != y.Value?.LastLoginTime)
				return x.Value?.LastLoginTime < y.Value?.LastLoginTime ? -1 : 1;
			if (x.Value?.LoginTimes != y.Value?.LoginTimes)
				return (x.Value?.LoginTimes ?? 0) < (y.Value?.LoginTimes ?? 0) ? -1 : 1;

			var title1 = (x.Value?.DisplayName).DefaultForEmpty(x.Key);
			var title2 = (y.Value?.DisplayName).DefaultForEmpty(y.Key);

			return StringComparer.OrdinalIgnoreCase.Compare(title1, title2);
		}
	}
}
