namespace TOBA.Query.Entity
{
	using Data;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal class LeftTickets : Dictionary<char, LeftTicketData>, IEquatable<LeftTickets>
	{
		/// <summary>
		/// 是否包含大概的数字
		/// </summary>
		public bool ContainsApproximateData => Values.Any(s => s.ContainsApproximateData);

		public LeftTicketData GetTicketData(char code, bool allowComatible = true)
		{
			var data = this.GetValue(code);
			if (data == null && allowComatible)
			{
				//switch (code)
				//{
				//	case 'M':
				//		data = this.GetValue('7');
				//		break;
				//	case 'P':
				//		data = this.GetValue('E');  //特等软座->特等座
				//		break;
				//	case 'O':
				//		data = this.GetValue('8');
				//		break;
				//	case '6':
				//		data = this.GetValue('A');
				//		break;
				//	case '4':
				//		data = this.GetValue('F');
				//		break;
				//	case '9':
				//		data = this.GetValue('H');  //一人软包->商务座
				//		break;
				//	case '3':
				//		data = this.GetValue('5');  //包厢硬卧->硬卧
				//		break;
				//}
				var map = ParamData.GetSeatCompatibleMap(code);
				if (map != null)
				{
					data = map.Select(this.GetValue).FirstOrDefault(s => s != null);
				}
			}
			//if (code == '1' && data == null)
			//	data = this.GetValue('B');

			return data;
		}

		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象。
		/// </summary>
		/// <returns>
		/// 如果当前对象等于 <paramref name="other"/> 参数，则为 true；否则为 false。
		/// </returns>
		/// <param name="other">与此对象进行比较的对象。</param>
		public bool Equals(LeftTickets other)
		{
			if (other == null)
				return false;

			if (other.Count != Count)
				return false;

			var keys = Keys.AsEnumerable().Union(other.Keys).ToArray();
			if (keys.Length != Count)
				return false;

			return keys.All(s => EqualityComparer<LeftTicketData>.Default.Equals(other[s], this[s]));
		}

		/// <summary>
		/// 确定指定的 <see cref="T:System.Object"/> 是否等于当前的 <see cref="T:System.Object"/>。
		/// </summary>
		/// <returns>
		/// 如果指定的 <see cref="T:System.Object"/> 等于当前的 <see cref="T:System.Object"/>，则为 true；否则为 false。
		/// </returns>
		/// <param name="obj">与当前的 <see cref="T:System.Object"/> 进行比较的 <see cref="T:System.Object"/>。</param>
		public override bool Equals(object obj)
		{
			return obj is LeftTickets && ((LeftTickets)obj).Equals(this);
		}

		/// <summary>
		/// 用作特定类型的哈希函数。
		/// </summary>
		/// <returns>
		/// 当前 <see cref="T:System.Object"/> 的哈希代码。
		/// </returns>
		public override int GetHashCode()
		{
			return Keys.Select(s => (int)s).Concat(Values.Select(s => s.GetHashCode())).Aggregate(0, (s, v) => s ^ v);
		}
	}
}