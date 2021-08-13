using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Media
{
	using System.Media;
	using System.Threading.Tasks;

	class TipSoundPlayer : BaseMp3Player
	{
		#region 单例模式

		static TipSoundPlayer _instance;
		static readonly object _lockObject = new object();

		public static TipSoundPlayer Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new TipSoundPlayer();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		private TipSoundPlayer() : base(ResLoader.GetPath("audio\\tip.mp3"))
		{
		}
	}
}
