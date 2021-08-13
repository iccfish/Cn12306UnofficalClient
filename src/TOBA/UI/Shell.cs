namespace TOBA.UI
{
	using System;
	using System.Diagnostics;

	public class Shell
	{
		/// <summary>
		/// 启动指定的网址
		/// </summary>
		/// <param name="url">要启动的网址</param>
		public static void StartUrl(string url)
		{
			try
			{
				System.Diagnostics.Process.Start(url);
			}
			catch (Exception ex)
			{
				StartUrlInIE(url);
			}
		}

		/// <summary>
		/// 在IE中打开对应网址
		/// </summary>
		/// <param name="url"></param>
		public static void StartUrlInIE(string url)
		{
			var ie = Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\Internet Explorer\iexplore.exe");
			if (!System.IO.File.Exists(ie))
				ie = Environment.ExpandEnvironmentVariables(@"%programfiles%\Internet Explorer\iexplore.exe");

			if (System.IO.File.Exists(ie)) System.Diagnostics.Process.Start(ie, url);
		}

		/// <summary>
		/// 打开添加/删除程序
		/// </summary>
		public static void OpenProgramUninstallDialog()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL appwiz.cpl,,1");
			}
			else
			{
				Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL appwiz.cpl,,0");
			}
		}
	}
}
