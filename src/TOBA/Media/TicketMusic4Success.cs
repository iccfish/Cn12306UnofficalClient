namespace TOBA.Media
{
	using Configuration;

	using Entity;

	using System;
	using System.IO;

	class TicketMusic4Success : ITicketMusic
	{
		MciController _controller;

		public TicketMusic4Success()
		{
			MediaConfiguration.Instance.PropertyChanged += Current_PropertyChanged;
			_controller = new MciController(GetMusicPath());
		}

		void Current_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MediaConfiguration.TicketSuccessMusicFile)) return;
			_controller.Dispose();
			_controller = new MciController(GetMusicPath());
		}

		string GetMusicPath()
		{
			var path = ResLoader.GetPath(MediaConfiguration.Instance.TicketSuccessMusicFile, ResourceLocation.Program);
			if (!File.Exists(path))
			{
				path = ResLoader.GetPath(@"audio\music\终于等到你.mp3", ResourceLocation.Program);
			}
			return path;
		}

		/// <summary>
		/// 播放音乐
		/// </summary>
		public void Play()
		{
			_controller.Replay();
		}

		/// <summary>
		/// 获得或设置是否正在播放
		/// </summary>
		public bool IsPlaying
		{
			get { return _controller.IsPlaying; }
		}

		/// <summary>
		/// 暂停播放
		/// </summary>
		public void Pause()
		{
			_controller.Pause();
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
				MediaConfiguration.Instance.PropertyChanged -= Current_PropertyChanged;
				_controller.Dispose();
			}

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		#endregion


	}
}