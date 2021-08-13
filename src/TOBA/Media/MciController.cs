using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Media
{
	using System.Runtime.InteropServices;

	class MciController : IDisposable
	{
		public MciController(string filepath)
		{

			Open(filepath);
		}

		//string _filepath;
		string _id;

		private string _command;

		[DllImport("winmm.dll")]
		private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

		private void Close()
		{
			_command = "close " + _id;
			mciSendString(_command, null, 0, IntPtr.Zero);
		}

		private void Open(string sFileName)
		{
			_id = DateTime.Now.Ticks.ToString();
			_command = "open \"" + sFileName + "\" type mpegvideo alias " + _id;
			mciSendString(_command, null, 0, IntPtr.Zero);
		}

		/// <summary>
		/// 播放
		/// </summary>
		/// <param name="loop"></param>
		public void Play(bool loop)
		{
			_command = "play " + _id;
			if (loop)
				_command += " REPEAT";
			mciSendString(_command, null, 0, IntPtr.Zero);
		}

		/// <summary>
		/// 定位
		/// </summary>
		/// <param name="position"></param>
		public void Seek(long position)
		{
			var command = "seek " + _id + " to " + position;
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		/// <summary>
		/// 暂停
		/// </summary>
		/// <param name="position"></param>
		public void Pause()
		{
			var command = "pause " + _id;
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		/// <summary>
		/// 重新播放
		/// </summary>
		public void Replay(bool loop = false)
		{
			Seek(0);
			Play(loop);
		}

		/// <summary>
		/// 获得是否正在播放
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				var data = new System.Text.StringBuilder(128);
				mciSendString("status " + _id + " mode", data, 128, IntPtr.Zero);

				return data.ToString() == "playing";
			}
		}


		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{

			}
			Close();

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		#endregion


	}
}
