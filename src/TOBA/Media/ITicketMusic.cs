namespace TOBA.Media
{
	using System;

	interface ITicketMusic : IDisposable
	{
		/// <summary>
		/// 播放音乐
		/// </summary>
		void Play();

		/// <summary>
		/// 获得或设置是否正在播放
		/// </summary>
		bool IsPlaying { get; }

		/// <summary>
		/// 暂停播放
		/// </summary>
		void Pause();
	}
}