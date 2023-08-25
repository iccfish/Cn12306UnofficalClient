using System;
using System.Linq;
using System.Text;

namespace TOBA.Profile
{
	using System.ComponentModel;
	using FSLib.Extension;
	using System.Runtime.CompilerServices;

	internal class UserKeyData : INotifyPropertyChanged
	{
		string _password;
		string _displayName;

		private string _email;

		private string _mobileNumber;

		/// <summary>
		/// 手机号码
		/// </summary>
		public string MobileNumber
		{
			get { return _mobileNumber; }
			set
			{
				if (value == _mobileNumber) return;
				_mobileNumber = value;
				OnPropertyChanged(nameof(MobileNumber));
			}
		}

		/// <summary>
		/// 电子邮件
		/// </summary>
		public string Email
		{
			get { return _email; }
			set
			{
				if (value == _email) return;
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		/// <summary>
		/// 获得或设置密码
		/// </summary>
		public string Password
		{
			get { return _password; }
			set
			{
				if (value == _password)
					return;
				_password = value;
				OnPropertyChanged("Password");
			}
		}

		/// <summary>
		/// 真实名称
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
			set
			{
				if (value == _displayName) return;
				_displayName = value;
				OnPropertyChanged("DisplayName");
				OnDisplayNameChanged();
			}
		}

		private string _idLast4;
		public string IdLast4
		{
			get => _idLast4;
			set
			{
				if (value == _idLast4) return;
				_idLast4 = value;
				OnPropertyChanged();
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		public event EventHandler DisplayNameChanged;

		/// <summary>
		/// 引发 <see cref="DisplayNameChanged" /> 事件
		/// </summary>
		protected virtual void OnDisplayNameChanged()
		{
			var handler = DisplayNameChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private int _loginTimes;

		/// <summary>
		/// 登录次数
		/// </summary>
		public int LoginTimes
		{
			get { return _loginTimes; }
			set
			{
				if (value == _loginTimes)
					return;
				_loginTimes = value;
				OnPropertyChanged(nameof(LoginTimes));
			}
		}

		private DateTime? _lastLoginTime;

		/// <summary>
		/// 最后登录时间
		/// </summary>
		public DateTime? LastLoginTime
		{
			get { return _lastLoginTime; }
			set
			{
				if (value.Equals(_lastLoginTime))
					return;
				_lastLoginTime = value;
				OnPropertyChanged();
			}
		}
	}
}
