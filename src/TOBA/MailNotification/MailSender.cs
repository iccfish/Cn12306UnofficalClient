using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TOBA.MailNotification
{
	using System.Threading;
	using System.Threading.Tasks;

	internal class MailSender : IDisposable
	{
		#region 单例模式

		static MailSender _instance;
		static readonly object _lockObject = new object();

		public static MailSender Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new MailSender();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		MailConfiguration _cfg;
		Dictionary<Guid, ManualResetEvent> _eventWrapper = new Dictionary<Guid, ManualResetEvent>();
		Dictionary<Guid, string> _result = new Dictionary<Guid, string>();

		private MailSender()
		{
			_cfg = MailConfiguration.Instance;
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

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion


		/// <summary>
		/// 确认邮件设置是否正确
		/// </summary>
		/// <returns></returns>
		public string CheckConfigurationError()
		{
			if (string.IsNullOrEmpty(_cfg.LoginCredential))
			{
				return "请输入发件人（登录账号）";
			}

			if (!_cfg.LoginCredential.IsEmail())
			{
				return "发件人（登录账号）不是合法的邮件账号";
			}
			if (string.IsNullOrEmpty(_cfg.MailServer))
			{
				return "请填写邮件服务器地址";
			}

			if (_cfg.Receivers?.Any(s => !s.IsEmail()) != false)
			{
				return "请输入正确的收件人账号";
			}


			return null;
		}

		public async Task<string> SendEmailAsync(string title, string body, string[] tousers)
		{
			var err = CheckConfigurationError();
			if (!string.IsNullOrEmpty(err))
				return err;

			var smtp = new SmtpClient(_cfg.MailServer, _cfg.MailServerPort);
			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtp.EnableSsl = _cfg.SSL;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential(_cfg.LoginCredential, _cfg.LoginPassword);

			var msg = new MailMessage(new MailAddress(_cfg.LoginCredential, "12306订票助手.NET"), new MailAddress(tousers[0], tousers[0]));
			if (tousers.Length > 1)
				tousers.Skip(1).ForEach(msg.To.Add);
			msg.Subject = title;
			msg.SubjectEncoding = Encoding.UTF8;
			msg.Body = body;
			msg.BodyEncoding = Encoding.UTF8;
			msg.IsBodyHtml = true;

			return await Task<string>.Factory.StartNew(() =>
				{
					try
					{
						smtp.Send(msg);
						TOBA.Events.OnMessage(this, new EventInfoArgs("邮件 【" + title + "】发送成功。"));
						return null;
					}
					catch (Exception ex)
					{
						TOBA.Events.OnError(this, new EventInfoArgs("邮件 【" + title + "】发送失败：" + ex.Message));
						return ex.Message;
					}
				}).
				ConfigureAwait(true);
		}

		/// <summary>
		/// 尝试自动判断邮箱类型
		/// </summary>
		/// <returns></returns>
		public bool TryFigureOutMailConfig(string md = null)
		{
			md = md ?? _cfg.LoginCredential;
			if (string.IsNullOrEmpty(md) || md.IndexOf('@') == -1)
				return false;

			var domain = md.Split('@')[1].ToLower();
			_cfg.LoginCredential = md;
			if (domain == "qq.com" || domain == "foxmail.com")
			{
				_cfg.MailServer = "smtp.qq.com";
				_cfg.MailServerPort = 587;
				_cfg.SSL = true;

				return true;
			}

			if (domain == "gmail.com")
			{
				_cfg.MailServer = "smtp.gmail.com";
				_cfg.MailServerPort = 465;
				_cfg.SSL = true;

				return true;
			}
			if (domain == "163.com")
			{
				_cfg.MailServer = "smtp.163.com";
				_cfg.MailServerPort = 25;
				_cfg.SSL = false;

				return true;
			}
			if (domain == "126.com")
			{
				_cfg.MailServer = "smtp.126.com";
				_cfg.MailServerPort = 25;
				_cfg.SSL = false;

				return true;
			}
			if (domain == "yeah.net")
			{
				_cfg.MailServer = "smtp.yeah.net";
				_cfg.MailServerPort = 25;
				_cfg.SSL = false;

				return true;
			}
			if (domain == "sina.com")
			{
				_cfg.MailServer = "smtp.sina.com";
				_cfg.MailServerPort = 25;
				_cfg.SSL = false;

				return true;
			}

			return true;
		}
	}
}
