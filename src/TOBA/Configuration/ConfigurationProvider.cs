using System;
using System.IO;
using System.Reflection;

namespace TOBA.Configuration
{
	using Newtonsoft.Json;

	using System.Threading;

	using TOBA.UI.Dialogs.Misc;

	internal class ConfigurationProvider : IConfigurationProvider
	{
		private string _assemblyRoot;
		bool _inPortableMode;
		private int _inited;

		public ConfigurationProvider()
		{
			_assemblyRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			ProfileRoot = Path.Combine(_assemblyRoot, "Profile");
		}

		/// <summary>
		/// 重置所有设置
		/// </summary>
		public void Reset()
		{
			if (Directory.Exists(ProfileRoot))
				Directory.Delete(ProfileRoot, true);
		}

		/// <summary>
		/// 获得配置的根目录
		/// </summary>
		public string ProfileRoot { get; }

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="category"></param>
		/// <returns></returns>
		public T LoadConfiguration<T>(string name = "main", params string[] category) where T : ConfigurationBase, new()
		{
			if (_inited == 0)
				throw new InvalidOperationException("配置类尚未初始化");

			var filepath = Path.Combine(ProfileRoot, string.Join(Path.DirectorySeparatorChar.ToString(), category), name + ".cfg");

			Directory.CreateDirectory(Path.GetDirectoryName(filepath));
			if (!File.Exists(filepath))
				return new T() { FilePath = filepath };

			try
			{
				var bytes = File.ReadAllBytes(filepath);
				var obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));

				obj.FilePath = filepath;

				return obj;
			}
			catch (Exception)
			{
				return new T() { FilePath = filepath };
			}
		}

		public void Init(IStartup startup)
		{
			if (Interlocked.CompareExchange(ref _inited, 1, 0) != 0)
				throw new InvalidOperationException("配置类已初始化，不可重复调用");
		}
	}
}
