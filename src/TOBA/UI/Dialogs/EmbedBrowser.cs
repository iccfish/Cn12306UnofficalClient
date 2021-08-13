using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs
{
	internal partial class EmbedBrowser : Form, IOperation
	{
		bool _loaded;

		public EmbedBrowser()
		{
			InitializeComponent();

			wb.DocumentCompleted += wb_DocumentCompleted;
			Load += EmbedBrowser_Load;
		}

		void EmbedBrowser_Load(object sender, EventArgs e)
		{
			wb.Navigate(new Uri("https://kyfw.12306.cn/otn/appDownload/init"));
		}

		void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (_loaded) return;
			_loaded = true;

			var document = wb.Document;

			//var info = Session.ExportSession();
			//var e1 = document.CreateElement("script");
			//var edom = (IHTMLScriptElement)e1.DomElement;
			//edom.text = "document.cookie='JSESSIONID=" + info.SessionID + ";path=/;';document.cookie='BIGipServerotn=" + info.ServerPort + ";path=/;';window.open('https://kyfw.12306.cn/otn/leftTicket/init');";
			//document.GetElementsByTagName("head")[0].AppendChild(e1);
			Close();
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }
	}
}
