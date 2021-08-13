using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebNotification
{
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Net;

	using Configuration;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	/// <summary>
	/// 通知
	/// </summary>
	internal class WebNotifyConfiguration : ConfigurationBase, INotifyPropertyChanged
	{

		private string _checkKeyword;
		private bool _enabled;

		private string _refer;

		private string _urlTemplate;

		private bool _usePost;

		public string CheckKeyword
		{
			get { return _checkKeyword; }
			set
			{
				if (value == _checkKeyword)
					return;
				_checkKeyword = value;
				OnPropertyChanged(nameof(CheckKeyword));
			}
		}

		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				if (value == _enabled)
					return;
				_enabled = value;
				OnPropertyChanged(nameof(Enabled));
			}
		}

		public string Body { get; set; }

		[Obsolete("已过时的配置项，等待移除")]
		public Dictionary<string, string> PostBody
		{
			get { return null; }
			set
			{
				if (value == null)
					return;
				Body = value.Select(s => $"{s.Key}={s.Value}").JoinAsString("&");
			}
		}

		[Obsolete("已过时的配置项，等待移除")]
		public bool UsePost
		{
			get { return false; }
			set
			{
				HttpMethod = value ? HttpMethod.Post : HttpMethod.Get;
				RequestContentType = ContentType.FormUrlEncoded;
			}
		}


		public string Refer
		{
			get { return _refer; }
			set
			{
				if (value == _refer)
					return;
				_refer = value;
				OnPropertyChanged(nameof(Refer));
			}
		}

		public string UrlTemplate
		{
			get { return _urlTemplate; }
			set
			{
				if (value == _urlTemplate)
					return;
				_urlTemplate = value;
				OnPropertyChanged(nameof(UrlTemplate));
			}
		}

		private ContentType _requestContentType = ContentType.FormUrlEncoded;

		[JsonProperty("rct")]
		public ContentType RequestContentType
		{
			get => _requestContentType;
			set
			{
				if (_requestContentType == value)
				{
					return;
				}

				_requestContentType = value;
				OnPropertyChanged(nameof(RequestContentType));
			}
		}

		HttpMethod _httpMethod;

		[JsonProperty("hm")]
		public HttpMethod HttpMethod
		{
			get => _httpMethod;
			set
			{
				if (_httpMethod == value)
				{
					return;
				}

				_httpMethod = value;
				OnPropertyChanged(nameof(HttpMethod));
			}
		}

		#region 单例模式

		static WebNotifyConfiguration _instance;
		static readonly object LockObject = new object();

		public static WebNotifyConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (LockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<WebNotifyConfiguration>("web_notification", "order");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

	}
}
