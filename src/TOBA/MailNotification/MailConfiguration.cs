using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TOBA.Configuration;

namespace TOBA.MailNotification
{
	internal class MailConfiguration : ConfigurationBase
	{
		public MailConfiguration()
		{
			_mailServerPort = 25;
			_title = _defaultTitle;
			_body = _defaultBody;
		}

		string _mailServer;
		int _mailServerPort;
		string _title;
		string _body;
		bool _enabled;
		string[] _receivers;
		string _loginPassword;
		string _loginCredential;
		bool _ssl;

		static string _defaultTitle = "[订票助手通知] $ordertime$ $acc$ 已成功订到 $date$ $from$ 至 $to$ 的 $code$ 次车票";
		static string _defaultBody = "[订票助手通知] $ordertime$ $acc$ 已成功订到 $date$ $from$ 至 $to$ 的 $code$ 次车票，订单号为 $order$，预计付款截止时间为 $paytime$，请及时登录付款。";

		private bool _useSystemMail;

		public bool UseSystemMail
		{
			get => _useSystemMail;
			set
			{
				if (value == _useSystemMail) return;
				_useSystemMail = value;
				OnPropertyChanged(nameof(UseSystemMail));
			}
		}

		public string LoginCredential
		{
			get => _loginCredential;
			set
			{
				if (value == _loginCredential) return;
				_loginCredential = value;
				OnPropertyChanged(nameof(LoginCredential));
			}
		}

		public bool SSL
		{
			get => _ssl;
			set
			{
				if (value.Equals(_ssl)) return;
				_ssl = value;
				OnPropertyChanged(nameof(SSL));
			}
		}

		public string LoginPassword
		{
			get => _loginPassword;
			set
			{
				if (value == _loginPassword) return;
				_loginPassword = value;
				OnPropertyChanged(nameof(LoginPassword));
			}
		}

		public string[] Receivers
		{
			get => _receivers;
			set
			{
				if (Equals(value, _receivers)) return;
				_receivers = value;
				OnPropertyChanged(nameof(Receivers));
			}
		}

		public string MailServer
		{
			get => _mailServer;
			set
			{
				if (value == _mailServer) return;
				_mailServer = value;
				OnPropertyChanged(nameof(MailServer));
			}
		}

		public int MailServerPort
		{
			get => _mailServerPort;
			set
			{
				if (value == _mailServerPort) return;
				_mailServerPort = value >= 1 && value <= 65535 ? value : (SSL ? 465 : 25);
				OnPropertyChanged(nameof(MailServerPort));
			}
		}

		public string Title
		{
			get => _title;
			set
			{
				if (value == _title) return;
				_title = value.DefaultForEmpty(_defaultTitle);
				OnPropertyChanged(nameof(Title));
			}
		}

		public string Body
		{
			get => _body;
			set
			{
				if (value == _body) return;
				_body = value.DefaultForEmpty(_defaultBody);
				OnPropertyChanged(nameof(Body));
			}
		}

		public bool Enabled
		{
			get => _enabled;
			set
			{
				if (value.Equals(_enabled)) return;
				_enabled = value;
				OnPropertyChanged(nameof(Enabled));
			}
		}

		#region 单例模式

		static MailConfiguration _instance;
		static readonly object _lockObject = new object();

		public static MailConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<MailConfiguration>("mail", "network");
						}
					}
				}

				return _instance;
			}
		}

		#endregion
	}
}
