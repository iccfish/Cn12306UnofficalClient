using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	class UiConfiguration : ConfigurationBase
	{
		#region 单例模式

		static UiConfiguration _instance;
		static readonly object _lockObject = new object();

		/// <summary>
		/// 获得 <see cref="UiConfiguration"/> 的单例对象
		/// </summary>
		public static UiConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<UiConfiguration>("ui");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		private bool _enableAnimation = true;

		/// <summary>
		/// 是否允许动画
		/// </summary>
		public bool EnableAnimation
		{
			get { return _enableAnimation; }
			set
			{
				if (value == _enableAnimation) return;
				_enableAnimation = value;
				OnPropertyChanged(nameof(EnableAnimation));
			}
		}

		private bool _autoArrangeOrderDlg = true;

		/// <summary>
		/// 是否允许自动排列订单提交窗口
		/// </summary>
		public bool AutoArrangeOrderDlg
		{
			get { return _autoArrangeOrderDlg; }
			set
			{
				if (value == _autoArrangeOrderDlg) return;
				_autoArrangeOrderDlg = value;
				OnPropertyChanged(nameof(AutoArrangeOrderDlg));
			}
		}
	}
}
