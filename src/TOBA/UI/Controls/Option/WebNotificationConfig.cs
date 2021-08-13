using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using DevComponents.DotNetBar;

	using Dialogs;

	using Entity;

	using FSLib.Network.Http;

	using WebNotification;

	internal partial class WebNotificationConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public WebNotificationConfig()
		{
			InitializeComponent();

			Load += WebNotificationConfig_Load;
			btnTest.Click += BtnTest_Click;
		}

		private async void BtnTest_Click(object sender, EventArgs e)
		{
			var notifier = new WebNotifier();
			this.ShowToast("正在发送通知...", Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Refresh, timeout: int.MaxValue);


			try
			{
				var result = await notifier.SendAsync(WebNotifyConfiguration.Instance.UrlTemplate, WebNotifyConfiguration.Instance.Body).ConfigureAwait(true);

				ToastNotification.Close(this);

				if (result.IsNullOrEmpty())
				{
					this.ShowToast("发送成功", Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Apply, Color.ForestGreen, timeout: 2000);
				}
				else
				{
					this.ShowToast($"发送失败：{result}", Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Delete, Color.DarkRed, timeout: 2000);
				}
			}
			catch (ArgumentException argumentException)
			{
				this.Error($"未能发送请求，错误：{argumentException.Message}。请检查相关设置，如果有非英文字符，请尝试转义。");
			}
			catch (Exception ex)
			{
				this.Error($"未能发送请求，错误：{ex.Message}");
			}
		}

		class Wrapper<T> : Dto
		{
			public Wrapper(T data, string text = null)
			{
				Text = text ?? data.ToString().ToUpper();
				Value = data;
			}

			public string Text { get; }

			public T Value { get; }
		}

		private void WebNotificationConfig_Load(object sender, EventArgs e)
		{
			var wnc = WebNotifyConfiguration.Instance;

			chkEnable.AddDataBinding(wnc, s => s.Checked, s => s.Enabled);
			pDetail.Visible = wnc.Enabled;
			chkEnable.CheckedChanged += (s, x) => pDetail.Visible = chkEnable.Checked;

			txtUrl.AddDataBinding(wnc, s => s.Text, s => s.UrlTemplate);
			txtRefer.AddDataBinding(wnc, s => s.Text, s => s.Refer);
			txtPostBody.AddDataBinding(wnc, s => s.Text, s => s.Body);

			ct.DataSource = new[]
			{
				new Wrapper<ContentType>(ContentType.FormUrlEncoded, "application/x-www-form-urlencoded"),
				new Wrapper<ContentType>(ContentType.FormData, "multipart/form-data"),
				new Wrapper<ContentType>(ContentType.Json, "application/json"),
				new Wrapper<ContentType>(ContentType.PlainText, "text/plain"),
				new Wrapper<ContentType>(ContentType.Xml, "text/xml")
			};
			ct.AddDataBinding(wnc, s => s.SelectedValue, s => s.RequestContentType);

			ciType.DataSource = new[]
			{
				new Wrapper<HttpMethod>(HttpMethod.Get),
				new Wrapper<HttpMethod>(HttpMethod.Post),
				new Wrapper<HttpMethod>(HttpMethod.Put)
			};
			ciType.AddDataBinding(wnc, s => s.SelectedValue, s => s.HttpMethod);

			wnc.AddPropertyChangedEventHandler(s => s.HttpMethod,
				(_s, _e) =>
				{
					txtPostBody.Enabled = ct.Enabled = wnc.HttpMethod != HttpMethod.Get;
				},
				this);
			txtPostBody.Enabled = ct.Enabled = wnc.HttpMethod != HttpMethod.Get;

			txtKeyword.AddDataBinding(wnc, s => s.Text, s => s.CheckKeyword);

		}


		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			btnTest.Focus();

			var wnc = WebNotifyConfiguration.Instance;
			if (wnc.Enabled && wnc.UrlTemplate.IsNullOrEmpty())
			{
				chkEnable.Enabled = false;
			}

			wnc.Save();

			return base.Save();
		}
	}
}
