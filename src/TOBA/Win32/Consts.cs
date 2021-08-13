namespace TOBA.Win32
{
	using System;

	class Consts
	{
		public const int WS_EX_TOPMOST = 0x00000008;
		public const int WS_EX_NOACTIVATE = 0x08000000;
		public const int WS_EX_LAYERED = 0x00080000;
		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int WS_EX_TOOLWINDOW = 0x00000080;
		public const uint DESKTOP_SWITCHDESKTOP = 0x100;
		public const uint GENERIC_ALL = 0x10000000;
		public const int WM_NCACTIVATE = 0x0086;
		public const int WM_NCHITTEST = 0x0084;
		public const int WM_GETMINMAXINFO = 0x0024;
		public const int WM_COMMAND = 0x0111;
		public const int WM_REFLECT = WM_USER + 0x1c00;
		public const int WM_USER = 0x0400;
		public const int CBN_DROPDOWN = 7;
		public const int BCM_SETSHIELD = 0x160c;

		public const int AW_ROLL = 0x0000;

		/// <summary>
		/// 在窗体卸载时若想使用本函数就得加上此常量
		/// </summary>
		public const int AW_HIDE = 0x10000;

		public const int AW_MASK = 0xfffff;
		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
	}
}
