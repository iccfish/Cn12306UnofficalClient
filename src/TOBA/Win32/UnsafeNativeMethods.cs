namespace TOBA.Win32
{
	using System;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Security.Permissions;
	using System.Windows.Forms;

	class UnsafeNativeMethods
	{
		private const string DLL_USER32 = "user32.dll";

		[DllImport(DLL_USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		/// <summary>
		/// 判断指定的窗口是否置顶
		/// </summary>
		/// <param name="hwnd"></param>
		/// <returns></returns>
		public static bool IsTopMost(IntPtr hwnd)
		{
			var result = GetWindowLong32(hwnd, (int)SetWindowLongOffsets.GWL_EXSTYLE);
			return ((int)result & (int)Consts.WS_EX_TOPMOST) != 0;
		}

		[DllImport(DLL_USER32, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
		public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

		[DllImport(DLL_USER32, SetLastError = true)]
		public static extern IntPtr GetThreadDesktop(uint dwThreadId);

		[DllImport(DLL_USER32, SetLastError = true)]
		public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="desktop"></param>
		/// <param name="device">must be null</param>
		/// <param name="devmode">must be null</param>
		/// <param name="flags">use 0</param>
		/// <param name="desiredAccess"></param>
		/// <param name="lpsa"></param>
		/// <returns></returns>
		[DllImport(DLL_USER32, EntryPoint = "CreateDesktop", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateDesktop(
			[MarshalAs(UnmanagedType.LPWStr)] string desktop,
			[MarshalAs(UnmanagedType.LPWStr)] string device, // must be null 
			[MarshalAs(UnmanagedType.LPWStr)] string devmode, // 
			uint flags,
			uint desiredAccess,
			IntPtr lpsa
		);

		[DllImport(DLL_USER32, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetThreadDesktop(IntPtr hDesktop);

		[DllImport(DLL_USER32)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SwitchDesktop(IntPtr hDesktop);

		[DllImport(DLL_USER32)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseDesktop(IntPtr hDesktop);

		[DllImport(DLL_USER32, CharSet = CharSet.Auto)]
		public extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags);

		public static void SetTopMost(Control control)
		{
			try
			{
				var sp = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				sp.Demand();
				UnsafeNativeMethods.SetWindowPos(control.Handle, new IntPtr((int)Consts.HWND_TOPMOST), 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOACTIVATE);
			}
			catch (SecurityException) { }
		}

		private static bool? _isRunningOnMono;

		public static bool IsRunningOnMono
		{
			get
			{
				if (!_isRunningOnMono.HasValue)
					_isRunningOnMono = Type.GetType("Mono.Runtime") != null;
				return _isRunningOnMono.Value;
			}
		}

		public static int HIGH_ORDER(int param)
		{
			return (param >> 16);
		}

		internal static int HIGH_ORDER(IntPtr n)
		{
			return HIGH_ORDER(unchecked((int)(long)n));
		}

		[DllImport(DLL_USER32, CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImport(DLL_USER32, CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[SuppressUnmanagedCodeSecurity]
		[DllImport(DLL_USER32, CharSet = CharSet.Auto)]
		public static extern int AnimateWindow(HandleRef windowHandle, int time, int flags);

		[DllImport(DLL_USER32)]
		public extern static bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

		internal static void AnimateWindow(Control control, int time, int flags)
		{
			try
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				AnimateWindow(new HandleRef(control, control.Handle), time, flags);
			}
			catch (SecurityException)
			{
			}
		}
	}
}
