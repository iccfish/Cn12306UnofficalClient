namespace TOBA.Interactive
{
	using System;
	using System.Diagnostics;
	using System.IO;

	using Newtonsoft.Json;

	/// <summary>
	/// 已检测到的浏览器信息
	/// </summary>
	internal class WebBrowserInfo
	{
		public static WebBrowserInfo SystemDefault = new WebBrowserInfo();
		public static WebBrowserInfo UserDefine = new WebBrowserInfo() { Name = "#" };

		/// <summary>
		/// 浏览器的路径
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
		/// </summary>
		/// <returns>
		/// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
		/// </returns>
		public override string ToString()
		{
			if (Name == "#")
				return "<选择浏览器程序...>";
			return Name.DefaultForEmpty("<系统默认浏览器>");
		}

		[JsonIgnore]
		public bool IsAvailable
		{
			get { return string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Path) && File.Exists(Path); }
		}

		/// <summary>
		/// 启动
		/// </summary>
		public bool Launch(string file)
		{
			try
			{
				Process.Start(Path, file);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
	}
}