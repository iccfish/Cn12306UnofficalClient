using FSLib.Network.Http;

using System;
using System.Collections.Generic;
using System.Net;

using TOBA.WebLib.Sockets5;

namespace TOBA.WebLib
{
	internal class NetClientHandler : BaseHttpHandler
	{
		static IWebProxy _systemProxy;
		static Dictionary<string, WebProxy> _systemCachedProxy;
		static ICredentials _systemCredentials;

		static Configuration.NetworkConfiguration _networkConfiguration;

		/// <summary>
		/// 禁止本地代理
		/// </summary>
		internal bool ForbiddenLocalProxy { get; set; } = false;

		static NetClientHandler()
		{
		}

		internal static void Init()
		{
			_networkConfiguration = Configuration.NetworkConfiguration.Current;
			_systemProxy = WebRequest.GetSystemWebProxy();
			_systemCachedProxy = new Dictionary<string, WebProxy>();
			_systemCredentials = CredentialCache.DefaultCredentials;

			RefreshWebProxy();
		}

		public NetClient NetClient { get; set; }

		public static void RefreshWebProxy()
		{
			if (_networkConfiguration == null)
				return;

			switch (_networkConfiguration.ProxyType)
			{
				case 0:
					WebRequest.DefaultWebProxy = null;
					break;
				case 1:
					WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
					WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
					break;
				case 2:
					WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
					WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
					break;
				case 3:
					if (_networkConfiguration.ProxyClass == ProxyType.Http)
						if (_networkConfiguration.ProxyAddress.IsNullOrEmpty())
						{
							WebRequest.DefaultWebProxy = null;
						}
						else
						{
							var proxyUri = new Uri("http://" + _networkConfiguration.ProxyAddress + ":" + _networkConfiguration.ProxyPort);
							var defaultProxy = new WebProxy(proxyUri);
							WebRequest.DefaultWebProxy = defaultProxy;
							if (!_networkConfiguration.ProxyUserName.IsNullOrEmpty())
							{
								defaultProxy.UseDefaultCredentials = false;
								defaultProxy.Credentials = new NetworkCredential(_networkConfiguration.ProxyUserName, _networkConfiguration.ProxyPassword);
							}
						}
					else
					{
						WebRequest.DefaultWebProxy = null;
					}
					break;
			}

		}

		/// <summary>
		/// 获得用于发送请求的Request对象
		/// </summary>
		/// <param name="uri">当前请求的目标地址</param>
		/// <param name="method">当前请求的HTTP方法</param>
		/// <param name="context">当前的上下文 <see cref="HttpContext" /></param>
		/// <returns></returns>
		public override HttpWebRequest GetRequest(Uri uri, string method, HttpContext context)
		{
			var request = base.GetRequest(uri, method, context);

			var contextData = context.ContextData;
			if (contextData != null && contextData.ContainsKey("host"))
			{
				request.Host = contextData["host"] as string;
			}
			//设置Origin
			var origin = uri.Scheme + "://" + (request.Host) + (uri.IsDefaultPort ? "" : ":" + uri.Port);
			request.Headers.Add("Origin", origin);

			if (ForbiddenLocalProxy)
			{
				request.Proxy = null;
			}
			else
			{
				if (_networkConfiguration.ProxyType == 1)
				{
					var proxy = GetSystemProxyUri(uri.Host);
					if (proxy == null)
						request.Proxy = null;
					else
					{
						request.Proxy = proxy;
						request.Credentials = _systemCredentials;
					}
				}
				else if (_networkConfiguration.ProxyType == 3)
				{
					if (_networkConfiguration.ProxyClass == ProxyType.Socks5)
						request.Proxy = PolipoSocks5ToHttpProxyWrapper.Instance.LocalWebProxy;
				}
			}


			return request;
		}

		static WebProxy GetSystemProxyUri(string host)
		{
			if (_networkConfiguration.ProxyClass == ProxyType.Socks5)
			{
				if (PolipoSocks5ToHttpProxyWrapper.Instance.IsRunning)
					return new WebProxy(PolipoSocks5ToHttpProxyWrapper.Instance.LocalUri);
				return null;
			}

			WebProxy result;
			if (!_systemCachedProxy.TryGetValue(host, out result))
			{
				lock (_systemCachedProxy)
				{

					var uri = new Uri("https://" + host + "/");
					var proxyuri = _systemProxy.GetProxy(uri);
					if (proxyuri == uri)
						proxyuri = null;

					result = (proxyuri == null ? null : new WebProxy(proxyuri));
					if (!_systemCachedProxy.ContainsKey(host))
						_systemCachedProxy.Add(host, result);
				}
			}

			return result;
		}

		private Uri BaseUri { get; } = new Uri("https://kyfw.12306.cn/otn/");

		/// <summary>
		/// 解析URL字符串为URI
		/// </summary>
		/// <param name="header">解析后的地址使用的位置</param>
		/// <param name="url">字符串地址</param>
		/// <param name="data">获得或设置相关联的数据</param>
		/// <returns></returns>
		public override Uri ResolveUri(HttpRequestHeader? header, string url, Dictionary<string, object> data)
		{
			if (url == null)
				return base.ResolveUri(header, url, data);

			var fullurl = url;

			var uri = new Uri(BaseUri, fullurl);

			return uri;
		}
	}
}