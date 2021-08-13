#define IE

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Vc
{
	using System.Reflection;
	using System.Runtime.InteropServices;

	[ComVisible(true)]
	public partial class SlideVcControl : UserControl
	{
		private Session _session;
		private readonly string _token;

#if IE
		private WebBrowser _webView;
#else
		private WebView _webView;
#endif
		public string AppId { get; }

		public string Token
		{
			get => _token;
			set
			{
				if (value == _token)
					return;

				Reload();
			}
		}

		internal SlideVcControl(Session session, string token, string appId)
		{
			InitializeComponent();

			AppId = appId;
			ClientSize = new Size(320, 70);

			_session = session;
			_token = token;

#if IE
			_webView = new WebBrowser()
			{
				Dock = DockStyle.Fill,
				ScriptErrorsSuppressed = true,
				ObjectForScripting = this
			};
			viewArea.Controls.Add(_webView);
#else
			_webView = new WebView(viewArea);
#endif

			Load += SlideVcForm_Load;
		}

		public void Reload()
		{
			Visible = true;
			Sig = null;
			CfSessionId = null;

			var html = $@"<!DOCTYPE html><html lang=""en""><head><meta charset=""UTF-8""><meta name=""viewport"" content=""width=device-width, initial-scale=1.0""><meta http-equiv=""X-UA-Compatible"" content=""ie=edge""><title>滑动验证</title></head><body style=""overflow:hidden;"">
	<div id=""J-slide-passcode""></div><script src=""https://g.alicdn.com/sd/ncpc/nc.js?t=2015052012""></script><script>
		var x = {{
			renderTo: ""#J-slide-passcode"",
			appkey: ""{AppId}"",
			scene: ""nc_login"",
			token: '{Token}',
			customWidth: 300,
			trans: {{
				key1: ""code0""
			}},
			elementID: [""usernameID""],
			is_Opt: 0,
			language: ""cn"",
			isEnabled: true,
			timeout: 3000,
			times: 5,
			apimap: {{}},
			callback: function(z) {{
				 window.external.callback(z.csessionid, z.sig)
			}}
		}};
		var y = new noCaptcha(x);
		y.upLang(""cn"", {{
			_startTEXT: ""请按住滑块，拖动到最右边，提交订单"",
			_yesTEXT: ""验证通过"",
			_error300: '哎呀，出错了，点击<a href=""javascript:__nc.reset()"">刷新</a>再来一次',
			_errorNetwork: '网络不给力，请<a href=""javascript:__nc.reset()"">点击刷新</a>',
		}})
	</script>
</body></html>";

#if IE
			_webView.LoadHtml(html);
#else
			_webView.LoadHTML(html);
#endif

		}

		private void SlideVcForm_Load(object sender, EventArgs e)
		{
			Reload();
		}

		[SmartAssembly.Attributes.DoNotObfuscate]
		[Obfuscation(Exclude = false, Feature = "-rename")]
		public void Callback(string sessid, string sig)
		{
			CfSessionId = sessid;
			Sig = sig;

			OnSlideOk();
		}

		public event EventHandler SlideOk;

		public string CfSessionId { get; private set; }

		public string Sig { get; private set; }

		protected virtual void OnSlideOk() { SlideOk?.Invoke(this, EventArgs.Empty); }
	}
}
