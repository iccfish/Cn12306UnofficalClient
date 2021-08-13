using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Media
{
	using System.Media;
	using System.Threading.Tasks;
	class LosingSoundPlayer : BaseMp3Player
	{
		#region 单例模式

		static LosingSoundPlayer _instance;
		static readonly object _lockObject = new object();

		public static LosingSoundPlayer Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new LosingSoundPlayer();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		private LosingSoundPlayer() : base(ResLoader.GetPath("audio\\losing.mp3"))
		{
		}
	}
}
