using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using System.ComponentModel;
	using FSLib.Extension;
	using System.Runtime.CompilerServices;

	/// <summary>
	/// 查询设置的UI设置
	/// </summary>
	internal class QueryParamUiSetting : INotifyPropertyChanged
	{
		bool _enableSellTip = true;

		/// <summary>
		/// 获得或设置是否启用购票提醒
		/// </summary>
		public bool EnableSellTip
		{
			get { return _enableSellTip; }
			set
			{
				if (value == _enableSellTip) return;
				_enableSellTip = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
