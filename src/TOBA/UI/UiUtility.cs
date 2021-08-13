using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI
{
	using FSLib.Extension;

	using Microsoft.Win32;

	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Runtime.InteropServices;

	using Win32;

	using Point = System.Drawing.Point;

	internal static class UiUtility
	{
		private static float _dpiX;
		private static float _dpiY;
		private static float _scaleX;
		public static int ScaleInt { get; }

		private static float _scaleY;
		private static int _scaleIntY;

		static UiUtility()
		{
			using (var bmp = new Bitmap(1, 1))
			using (var g = Graphics.FromImage(bmp))
			{
				_dpiX = g.DpiX;
				_dpiY = g.DpiY;
				_scaleX = _dpiX / 96F;
				_scaleY = _dpiY / 96F;
				ScaleInt = _scaleX > 1.9F ? 2 : 1;
				_scaleIntY = _scaleY > 1.9F ? 2 : 1;
			}
		}

		internal static void ApplyDpiAware()
		{

		}


		/// <summary>
		/// 附加环境到控件上
		/// </summary>
		/// <param name="control"></param>
		/// <param name="context"></param>
		internal static void AttachContext([NotNull] Control control, [NotNull] Session context)
		{
			if (control == null)
				return;

			foreach (Control ctl in control.Controls)
			{
				if (ctl is IRequireSessionInit)
					(ctl as IRequireSessionInit).InitSession(context);

				AttachContext(ctl, context);
			}
			if (control is TabControl)
			{
				var tp = control as TabControl;
				tp.TabPages.Cast<TabPage>().ForEach(s =>
				{
					if (s is IRequireSessionInit)
						(s as IRequireSessionInit).InitSession(context);
					else
					{
						AttachContext(s, context);
					}
				});
			}
		}

		internal static void ApplySubStyle(ListViewItem lvi)
		{
			var foreColor = lvi.ForeColor;
			var backColor = lvi.BackColor;
			var font = lvi.Font;

			//fix bug in dnb2
			for (int i = 1; i < lvi.SubItems.Count; i++)
			{
				var subItem = lvi.SubItems[i];

				subItem.BackColor = backColor;
				subItem.ForeColor = foreColor;
				subItem.Font = font;
			}
		}

		internal static string FormatTimeSpan(this TimeSpan ts, bool includeSecs = false, bool includeMillSecs = false)
		{
			var sb = new StringBuilder(30);
			if (ts.Days > 0)
				sb.Append(ts.Days + "天");
			if (ts.Hours > 0 || ts.Minutes > 0 || ts.Seconds > 0)
			{
				sb.Append(ts.Hours + "时");
			}
			if (ts.Minutes > 0 || ts.Hours > 0)
			{
				sb.Append(ts.Minutes + "分");
			}
			if (ts.Seconds > 0 && includeSecs)
			{
				sb.Append(ts.Seconds + "秒");
			}
			if (ts.Milliseconds > 0 && includeMillSecs)
			{
				sb.Append(ts.Milliseconds + "毫秒");
			}

			return sb.ToString();
		}

		internal static void PlaceFormAtCenter(Form self, Form host = null, bool show = true)
		{
			self.StartPosition = FormStartPosition.Manual;

			host = host ?? AppContext.HostForm;
			if (host.WindowState == FormWindowState.Minimized)
			{
				self.StartPosition = FormStartPosition.CenterScreen;

				if (show && !self.Visible)
					self.Show();

				return;
			}

			var location = host.DesktopLocation;
			var sizeHost = host.Size;
			var targetSize = self.Size;

			//offset
			var targetLocation = new Point(
				Math.Max((sizeHost.Width - targetSize.Width) / 2, 0),
				Math.Max((sizeHost.Height - targetSize.Height) / 2, 0)
				);

			location.Offset(targetLocation);
			self.DesktopLocation = location;

			if (show && !self.Visible)
				self.Show();

		}

		/// <summary>
		/// 加载HTML到浏览器中
		/// </summary>
		/// <param name="browser"></param>
		/// <param name="html"></param>
		public static void LoadHtml(this WebBrowser browser, string html)
		{
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(html));
			browser.DocumentStream = ms;
		}

		public static void SetIeBrowserCompatibleFlag()
		{
			var procName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
			var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION").CreateSubKey("FEATURE_BROWSER_EMULATION");
			}
			using (key)
			{
				key.SetValue(procName, 99999, RegistryValueKind.DWord);
			}
		}

		public static void ClearIeBrowserCompatibleFlag()
		{
			var procName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
			using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true))
			{
				if (key?.GetValueNames().Contains(procName, StringComparer.OrdinalIgnoreCase) == true)
					key.DeleteValue(procName);
			}
		}

		public static int? GetIeVersion()
		{
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer"))
			{
				var ver = key.GetValue("svcVersion") as string;
				if (ver == null)
					ver = key.GetValue("Version") as string;

				if (ver.IsNullOrEmpty())
					return null;

				return Version.Parse(ver).Major;
			}
		}

		/// <summary>
		/// 将16px的图像转换为20px的图像
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static Image Get20PxImageFrom16PxImg(Image img)
		{
			var image = new Bitmap(20, 20, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(image))
			{
				g.DrawImage(img, 2, 2, 16, 16);
			}

			return image;
		}

		/// <summary>
		/// 将16px的图像转换为24px的图像
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static Image Get24PxImageFrom16PxImg(Image img)
		{
			var image = new Bitmap(24, 24, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(image))
			{
				g.DrawImage(img, 4, 4, 16, 16);
			}

			return image;
		}

		/// <summary>
		/// 获得灰度图像
		/// </summary>
		/// <param name="source">源图像</param>
		/// <param name="opacity">透明度</param>
		/// <returns></returns>
		public static System.Drawing.Image MakeGrayImage(System.Drawing.Image source)
		{
			var nbmp = new Bitmap(source);

			var bmpdata = nbmp.LockBits(new Rectangle(0, 0, nbmp.Width, nbmp.Height), ImageLockMode.ReadWrite, nbmp.PixelFormat);
			var startAddress = bmpdata.Scan0;

			var pixelWidth = Math.Abs(bmpdata.Stride) / bmpdata.Width;
			var bytesLength = pixelWidth * bmpdata.Width * bmpdata.Height;
			var buffer = new byte[bytesLength];

			Marshal.Copy(startAddress, buffer, 0, buffer.Length);
			for (var i = 0; i < buffer.Length; i += pixelWidth)
			{
				var value = (buffer[i + 2] * 0.299 + buffer[i + 1] * 0.587 + buffer[i] * 0.114);
				buffer[i] = buffer[i + 1] = buffer[i + 2] = (byte)value;
			}

			Marshal.Copy(buffer, 0, startAddress, buffer.Length);
			nbmp.UnlockBits(bmpdata);

			return nbmp;
		}

		/// <summary>
		/// 向按钮上添加一个小图标，标志需要管理员权限
		/// </summary>
		/// <param name="b">要添加图标的按钮</param>
		public static void AddShieldToButton(global::System.Windows.Forms.Button b)
		{
			b.FlatStyle = FlatStyle.System;
			UnsafeNativeMethods.SendMessage(b.Handle, Consts.BCM_SETSHIELD, 0, int.MaxValue);
		}

		/// <summary>
		/// 为一个空容器绑定一个内容为空的标记符
		/// </summary>
		/// <param name="parent">父容器</param>
		/// <param name="target">如果为空，要显示的目标</param>
		public static void BindEmptyIndicator(this Control parent, Control target = null)
		{
			if (target == null)
				target = parent.Controls[0];

			void Refresh(object sender, EventArgs e)
			{
				target.Visible = parent.Controls.Count <= (target.Parent == parent ? 1 : 0);
			}

			Refresh(null, null);
			parent.ControlAdded += Refresh;
			parent.ControlRemoved += Refresh;
		}

		/// <summary>
		/// 在资源管理器中打开指定位置
		/// </summary>
		/// <param name="path">路径</param>
		public static void OpenLocationInExplorer(string path)
		{
			System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + path + "\"");
		}
	}
}
