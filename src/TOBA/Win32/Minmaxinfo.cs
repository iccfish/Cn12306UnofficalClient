namespace TOBA.Win32
{
	using System.Drawing;
	using System.Runtime.InteropServices;

	[StructLayout(LayoutKind.Sequential)]
	internal struct Minmaxinfo
	{
		public Point reserved;
		public Size maxSize;
		public Point maxPosition;
		public Size minTrackSize;
		public Size maxTrackSize;
	}
}
