namespace TOBA.Win32
{
	using System;

	[Flags]
	public enum SetWindowPosFlags : uint
	{
		/// <summary>
		/// [0x0001] 维持当前尺寸（忽略cx和Cy参数）
		/// </summary>
		SWP_NOSIZE = 0x0001,
		/// <summary>
		/// [0x0002] 维持当前位置（忽略X和Y参数）
		/// </summary>
		SWP_NOMOVE = 0x0002,
		/// <summary>
		/// [0x0004] 维持当前Z序（忽略hWndlnsertAfter参数）
		/// </summary>
		SWP_NOZORDER = 0x0004,
		/// <summary>
		/// [0x0008] 不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。
		/// 适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。
		/// 如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
		/// </summary>
		SWP_NOREDRAW = 0x0008,
		/// <summary>
		/// [0x0010] 不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）
		/// </summary>
		SWP_NOACTIVATE = 0x0010,
		/// <summary>
		/// [0x0020] 给窗口发送WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送WM_NCCALCSIZE
		/// </summary>
		SWP_FRAMECHANGED = 0x0020,
	}
}
