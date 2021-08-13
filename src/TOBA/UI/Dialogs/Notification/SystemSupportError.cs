using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Dialogs.Notification
{
	using TOBA.Entity;

	internal partial class SystemSupportError : Form
	{
		public Exception Exception { get; set; }

		public SystemSupportError()
		{
			InitializeComponent();
			Icon = Properties.Resources.icon_warning;

			Load += SystemSupportError_Load;
			lnkVisitForum.Click += (s, e) => Shell.StartUrl("http://forum.iccfish.com/forum-38-1.html");
			btnOpenLocation.Click += (s, e) => UiUtility.OpenLocationInExplorer(Program.LogFile);
		}

		void SystemSupportError_Load(object sender, EventArgs e)
		{
			btnUseDiagMode.Visible = !Program.IsTraceEnabled;
			gb.Visible = Program.IsTraceEnabled;

			if (Exception is SystemBusyException)
			{
				btnUseDiagMode.Visible = false;
			}

			if (!Exception.HelpLink.IsNullOrEmpty())
			{
				lnkHelp.Visible = true;
				lnkHelp.Click += (o, args) => Process.Start(Exception.HelpLink);
			}

			btnUseDiagMode.Click += btnUseDiagMode_Click;
			if (Program.IsTraceEnabled)
			{
				LoadDebugInfo();
			}
		}

		void btnUseDiagMode_Click(object sender, EventArgs e)
		{
			Program.RestartInTraceMode();
		}

		public string Message
		{
			get { return lblMessage.Text; }
			set { lblMessage.Text = value; }
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void btnSysConfig_Click(object sender, EventArgs e)
		{
			var dlg = new UI.Dialogs.ConfigCenter();
			dlg.Shown += (ss, ee) =>
			{
				dlg.SelectedConfig = dlg.FindConfigUI<UI.Controls.Option.NetworkConfig>().First();
			};

			dlg.ShowDialog();
			Program.Restart();
		}


		void LoadDebugInfo()
		{
			Program.LogListener.Flush();
			Program.LogListener.Close();

			lblFile.Text = "桌面\\" + Path.GetFileName(Program.LogFile);
			lblAdvice.Text = "...";
			lblInfo.Text = "分析中, 请稍等...";
			Application.DoEvents();

			var txt = System.IO.File.ReadAllText(Program.LogFile);
			if (txt.IndexOf("StatusCode=407") != -1 || txt.IndexOf("StatusCode=401") != -1 || txt.IndexOf("StatusCode=400") != -1)
			{
				lblInfo.Text = "代理服务器访问被拒绝";
				lblAdvice.Text = "进入软件设置->网络设置，选择自定义代理模式，并设置相关信息";
			}
			else if (txt.IndexOf("StatusCode=403") != -1)
			{
				lblInfo.Text = "访问12306被拒绝";
				lblAdvice.Text = "可能你的IP已被封锁，请稍后再试。如果Hosts中设置过重定向，请取消再试。";
			}
			else if (txt.IndexOf("StatusCode=302") != -1 && txt.IndexOf("http://kyfw.12306.cn/otn/") != -1)
			{
				lblInfo.Text = "未期待的转移，可能是访问通道不正确";
				lblAdvice.Text = "已自动重新设置访问通道为HTTPS，请重启软件试试。";

				NetworkConfiguration.Current.BaseUri = new Uri("https://kyfw.12306.cn/otn/");
			}
			else if (txt.IndexOf("StatusCode=502") != -1)
			{
				lblInfo.Text = "服务器网关出错";
				if (NetworkConfiguration.Current.ProxyType == 3 || txt.IndexOf("CONNECT kyfw.12306.cn") != -1)
				{
					lblAdvice.Text = "看起来您使用了代理服务器，请进入网络设置查看网络设置是否正确。";
				}
				else
				{
					lblAdvice.Text = "网关出错，可能是12306繁忙，或其他原因，稍后重试。如果始终如此，请在论坛反馈。";
				}
			}
			else if (txt.IndexOf("StatusCode=404") != -1)
			{
				lblInfo.Text = "服务器返回未找到";
				lblAdvice.Text = "请确认你的浏览器可以访问12306。必要时可以参考修复hosts或用防封锁工具处理下。";
			}
			else if (txt.IndexOf("StatusCode=503") != -1)
			{
				lblInfo.Text = "CDN服务器已经失效";
				lblAdvice.Text = "您正在访问的CDN节点已经失效，请重启客户端。如果Hosts中设置过重定向，请取消再试。";
			}
			else if (txt.IndexOf("未能解析") != -1)
			{
				lblInfo.Text = "域名无法解析";
				lblAdvice.Text = "域名无法解析，可能您的网络存在问题。请尝试直接访问网站看看是否正常。";
			}
			else if (txt.IndexOf("连接尝试失败") != -1 || txt.IndexOf("无法连接") != -1)
			{
				lblInfo.Text = "服务器或代理服务器连接失败";
				lblAdvice.Text = "服务器或代理服务器连接失败，可能您的网络或设置存在问题。请尝试修改网络设置。";
			}
			else if (txt.IndexOf("连接被意外关闭") != -1)
			{
				lblInfo.Text = "网络请求被意外强行中止";
				lblAdvice.Text = "服务器或代理服务器的连接被意外关闭，请检查网络情况。这可能是因为您的网络访问12306速度过慢，或12306负载过高导致。";
			}
			else if (txt.IndexOf("网络繁忙") != -1)
			{
				lblInfo.Text = "网络IP被封锁";
				lblAdvice.Text = "您的网络IP被封锁。公司网络等共用出口IP的情况下尤其容易被封锁，请稍后重试或使用代理服务器。如果使用拨号上网，可尝试重启猫。";
			}
			else
			{
				lblInfo.Text = "很抱歉暂时没有能确定原因，请使用浏览器订票确认没有问题";
				lblAdvice.Text = "如果浏览器没有问题并确认是订票助手问题，请加群反馈，并将日志压缩上传";
			}
		}
	}
}
