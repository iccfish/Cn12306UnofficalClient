using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Interactive
{
	using System.IO;
	using System.Windows.Forms;

	using Microsoft.Win32;

	class WebBrowserManager
	{
		public static IEnumerable<WebBrowserInfo> GetWebBrowsers()
		{
			RegistryKey key;
			try
			{
				key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", false);
			}
			catch (Exception)
			{
				yield break;
			}

			if (key == null)
				yield break;

			var target = new[] { "IEXPLORE.EXE", "firefox.exe", "maxthon.exe", "chrome.exe", "liebao.exe" };
			var targetName = new[] { "IE浏览器", "Firefox", "傲游浏览器", "Google Chrome", "猎豹浏览器" };
			var flags = new bool[target.Length];
			var names = key.GetSubKeyNames().MapToHashSet(StringComparer.OrdinalIgnoreCase);

			for (int i = 0; i < target.Length; i++)
			{
				if (!names.Contains(target[i]))
					continue;

				using (var subkey = key.OpenSubKey(target[i], false))
				{
					var path = (subkey.GetValue("") ?? "").ToString();
					if (File.Exists(path))
					{
						flags[i] = true;
						yield return new WebBrowserInfo()
						{
							Name = targetName[i],
							Path = path
						};
					}
				}
			}
			key.Close();

			//firefox?
			if (!flags[1])
			{
				var firefox = DetectFirefox();

				if (firefox != null)
					yield return firefox;
			}

			if (!flags[2])
			{
				var maxthon = DetectMaxthon3();
				if (maxthon != null)
					yield return maxthon;
			}

			if (!flags[4])
			{
				var liebao = DetectLb();
				if (liebao != null)
					yield return liebao;
			}
		}

		static WebBrowserInfo DetectLb()
		{
			try
			{
				var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\liebao");
				if (key == null)
					return null;

				var path = key.GetValue("Install Path Dir") as string;
				if (string.IsNullOrEmpty(path))
					return null;

				path = Path.Combine(path, "liebao.exe");
				if (!File.Exists(path))
					return null;

				return new WebBrowserInfo()
						{
							Name = "猎豹浏览器",
							Path = path

						};
			}
			catch (Exception)
			{
				return null;
			}
		}

		static WebBrowserInfo DetectFirefox()
		{
			RegistryKey key = null;
			WebBrowserInfo firefox = null;
			try
			{
				key = Registry.CurrentUser.OpenSubKey(@"Software\Mozilla\Mozilla Firefox");
				if (key != null)
				{
					var versions = key.GetSubKeyNames();
					var firefoxPath = versions.Select(s =>
					{
						try
						{
							return key.OpenSubKey(s + "\\Main", false);
						}
						catch (Exception)
						{
							return null;
						}
					}).ExceptNull().Select(s => (s.GetValue("PathToExe") ?? "").ToString()).FirstOrDefault(s => !string.IsNullOrEmpty(s) && File.Exists(s));
					if (firefoxPath != null)
					{
						firefox = new WebBrowserInfo
						{
							Name = "Firefox",
							Path = firefoxPath
						};
					}
				}
			}
			catch (Exception)
			{

			}
			finally
			{
				if (key != null)
					key.Close();
				key = null;
			}

			return firefox;
		}

		static WebBrowserInfo DetectMaxthon3()
		{
			//maxthon3?
			WebBrowserInfo maxthon = null;
			try
			{
				var mkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Maxthon3", false);
				if (mkey != null)
				{
					var folder = mkey.GetValue("Folder") as string;
					if (!string.IsNullOrEmpty(folder))
					{
						var path = Path.Combine(folder, "bin", "maxthon.exe");
						if (File.Exists(path))
							maxthon = new WebBrowserInfo
							{
								Name = "傲游浏览器",
								Path = path
							};
					}
					mkey.Close();
				}
			}
			catch (Exception)
			{
			}
			return maxthon;
		}
	}
}
