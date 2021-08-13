namespace TOBA.Otn
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	internal class Base32
	{
		static uint _delta = 0x9E3779B8;
		static string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

		static string LongArrayToString(uint[] data, bool includeLength)
		{
			var length = (uint)data.Length;
			var n = (length - 1) << 2;
			if (includeLength)
			{
				var m = data[length - 1];
				if ((m < n - 3) || (m > n)) return null;
				n = m;
			}
			var result = new string[length];
			for (var i = 0; i < length; i++)
			{
				result[i] = new uint[] { data[i] & 0xff, data[i] >> 8 & 0xff, data[i] >> 16 & 0xff, data[i] >> 24 & 0xff }.Select(s => (char)s).JoinAsString("");
			}
			if (includeLength)
			{
				return result.JoinAsString("").Substring(0, (int)n);
			}
			else
			{
				return result.JoinAsString("");
			}
		}

		static uint[] StringToLongArray(string str, bool includeLength)
		{
			var length = str.Length;
			var result = new List<uint>(length / 4);

			for (var i = 0; i < length; i += 4)
			{
				result.Add((uint)(str[i]) | (uint)(str[i + 1]) << 8 | (uint)(str[i + 2]) << 16 | (uint)(str[i + 3]) << 24);
			}
			if (includeLength)
			{
				result.Add((uint)length);
			}
			return result.ToArray();
		}

		static string Encrypt(string str, string key)
		{
			if (str.IsNullOrEmpty())
			{
				return "";
			}
			var v = StringToLongArray(str, true);
			var k = StringToLongArray(key, false);
			if (k.Length < 4)
			{
				var nk = new uint[4];
				Array.Copy(k, nk, k.Length);
				k = nk;
			}
			var n = v.Length - 1;
			var z = v[n];
			var y = v[0];
			var q = (int)Math.Floor(6 + 52.0 / (n + 1));
			var sum = 0U;
			while (0 < q--)
			{
				sum = sum + _delta & 0xffffffff;
				uint e = (sum >> 2 & 3);
				uint mx;
				uint p;
				for (p = 0; p < n; p++)
				{
					y = v[p + 1];
					mx = (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);
					z = v[p] = v[p] + mx & 0xffffffff;
				}
				y = v[0];
				mx = (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);
				z = v[n] = v[n] + mx & 0xffffffff;
			}
			return LongArrayToString(v, false);
		}

		static string Encode32(string input)
		{
			var output = "";
			var i = 0;
			do
			{
				var chr1 = (uint)input[i++];
				var chr2 = i >= input.Length ? 0 : (uint)input[i++];
				var chr3 = i >= input.Length ? 0 : (uint)input[i++];
				var enc1 = chr1 >> 2;
				var enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
				var enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
				var enc4 = chr3 & 63;
				if (chr2 == 0)
				{
					enc3 = enc4 = 64;
				}
				else if (chr3 == 0)
				{
					enc4 = 64;
				}
				output = output + _keyStr[(int)enc1] + _keyStr[(int)enc2] + _keyStr[(int)enc3] + _keyStr[(int)enc4];
			} while (i < input.Length);
			return output;
		}

		static string Bin216(string s)
		{
			return s.Select(c => ((int)c).ToString("x2")).JoinAsString("");
		}

		public static string Encode(string key, string value = "1111")
		{
			var data = Encrypt(value, key);
			var data2 = Bin216(data);
			return Encode32(data2);
		}

		public static string CreateRandomKey()
		{
			return Convert.ToBase64String(Encoding.ASCII.GetBytes(new Random().Next(1000, 10000).ToString()));
		}
	}
}
