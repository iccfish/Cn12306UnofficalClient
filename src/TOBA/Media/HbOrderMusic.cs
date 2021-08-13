using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Media
{
	class HbOrderMusic : BaseMp3Player, ITicketMusic
	{
		/// <inheritdoc />
		private HbOrderMusic() : base(ResLoader.GetPath("audio\\bird.mp3"))
		{
		}

		#region 单例模式

		static HbOrderMusic _instance;
		static readonly object _lockObject = new object();

		/// <summary>
		/// 获得 <see cref="HbOrderMusic"/> 的单例对象
		/// </summary>
		public static HbOrderMusic Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new HbOrderMusic();
						}
					}
				}

				return _instance;
			}
		}

		#endregion
	}
}
