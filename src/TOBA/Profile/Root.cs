using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.Profile
{
	/// <summary>
	/// 根配置文件目录
	/// </summary>
	internal class Root
	{
		static readonly object _lockObject = new object();

		/// <summary>
		/// 根配置文件的目录
		/// </summary>
		public static string Path;

		/// <summary>
		/// 用户目录
		/// </summary>
		public static string UsersPath;

		/// <summary>
		/// 获得缓存目录地址
		/// </summary>
		public static string CachePath;

		/// <summary>
		/// 获得缓存文件路径
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetCacheFile(string name)
		{
			return System.IO.Path.Combine(CachePath, name);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public static void Init()
		{
			Path = AppContext.ExtensionManager.ConfigurationProvider.ProfileRoot;
			UsersPath = System.IO.Path.Combine(Path, "Users");
			CachePath = System.IO.Path.Combine(Path, "cache");

			System.IO.Directory.CreateDirectory(UsersPath);
			System.IO.Directory.CreateDirectory(CachePath);
		}


		static HashSet<string> _users;


		/// <summary>
		/// 系统用户
		/// </summary>
		public static HashSet<string> Users
		{
			get
			{
				if (_users == null)
				{
					lock (_lockObject)
					{
						if (_users == null)
						{
							_users = System.IO.Directory.GetDirectories(UsersPath).Select(s => DecodeUserName(System.IO.Path.GetFileName(s))).MapToHashSet(StringComparer.OrdinalIgnoreCase);
						}
					}
				}

				return _users;
			}
		}

		/// <summary>
		/// 删除用户已经保存的密码
		/// </summary>
		/// <param name="username"></param>
		public static void TryDeleteUser(string username)
		{
			var filePath = System.IO.Path.Combine(UsersPath, EncodeUserName(username));
			if (!System.IO.Directory.Exists(filePath)) return;

			System.IO.Directory.Delete(filePath, true);
			UserKeyDataMap.Current[username] = null;
		}

		/// <summary>
		/// 编码用户名
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public static string EncodeUserName(string username)
		{
			return "U" + username.Select(s => ((byte)s).ToString("X2")).JoinAsString("");
		}

		/// <summary>
		/// 解码用户名
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string DecodeUserName(string text)
		{
			if (text.IsNullOrEmpty() || (text[0] != 'U' && text[0] != 'u')) return string.Empty;

			var buffer = new char[text.Length / 2];
			for (var i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (char)((text[1 + i * 2].ToHexByte() << 4) + text[2 + i * 2].ToHexByte());
			}
			return new string(buffer);
		}
	}
}
