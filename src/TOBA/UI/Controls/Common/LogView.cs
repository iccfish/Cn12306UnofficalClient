namespace TOBA.UI.Controls.Common
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public class LogView : ListView
	{


		public LogView()
		{
			InitializeComponent();
			base.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			base.FullRowSelect = true;
			base.View = System.Windows.Forms.View.Details;
			base.Dock = DockStyle.Fill;

			base.Columns.Add(new ColumnHeader()
			{
				Text = "时间",
				TextAlign = HorizontalAlignment.Center,
				Width = 150,
				DisplayIndex = 0
			});

			base.Columns.Add(new ColumnHeader()
			{
				Text = "信息",
				TextAlign = HorizontalAlignment.Left,
				Width = this.ClientSize.Width - base.Columns[0].Width,
				DisplayIndex = 1
			});

			//尺寸变化
			this.SizeChanged += (s, o) =>
			{
				base.Columns[1].Width = this.ClientSize.Width - base.Columns[0].Width;
			};

			base.SmallImageList = new ImageList()
			{
				ImageSize = new Size(20, 20),
				ColorDepth = ColorDepth.Depth32Bit
			};
			ItemCountLimit = 5000;
		}

		/// <summary>
		/// 向图像列表中注册图像
		/// </summary>
		/// <param name="key">键值</param>
		/// <param name="image">图像</param>
		public void RegisteImage(string key, Image image)
		{
			if (image.Size.Width > 20)
			{
				//过大，自动缩放
				image = image.GetThumbnailImage(20, 20, null, IntPtr.Zero);
			}
			else if (image.Size.Width < 20)
			{
				//过小，自动放大
				var img = new Bitmap(20, 20, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				using (var g = Graphics.FromImage(img))
				{
					g.DrawImage(image, (20 - image.Width) / 2, (20 - image.Height) / 2, 16, 16);
				}
				image = img;
			}

			SmallImageList.Images.Add(key, image);
		}

		/// <summary>
		/// 向图像列表中注册图像
		/// </summary>
		/// <param name="key">键值</param>
		/// <param name="icon">图像</param>
		public void RegisteImage(string key, Icon icon)
		{
			SmallImageList.Images.Add(key, icon);
		}

		#region 重载属性，不让更改

		/// <summary>
		/// 禁止标题样式
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public new ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return base.HeaderStyle;
			}
		}

		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public new ColumnHeaderCollection Columns
		{
			get
			{
				return base.Columns;
			}
		}

		/// <summary>
		/// 禁止更改全行选定
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public new bool FullRowSelect
		{
			get
			{
				return base.FullRowSelect;
			}
			set
			{
			}
		}

		/// <summary>
		/// 禁止更改视图样式
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public new View View
		{
			get
			{
				return base.View;
			}
			set
			{
			}
		}

		[System.ComponentModel.Browsable(false)]
		public new ImageList SmallImageList
		{
			get
			{
				return base.SmallImageList;
			}
		}

		#endregion

		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);
		}

		/// <summary>
		/// 添加日志记录
		/// </summary>
		/// <param name="type"></param>
		/// <param name="message"></param>
		public void AddLogInfo(string imageKey, ColorSchema imageType, string message)
		{
			AddLogInfo(imageKey, imageType, false, message);
		}

		/// <summary>
		/// 添加日志记录
		/// </summary>
		public void AddLogInfo(RowStyleType style, string message)
		{
			AddLogInfo(style.ToString(), style, message);
		}

		/// <summary>
		/// 添加日志记录
		/// </summary>
		public void AddLogInfo(string imageKey, RowStyleType style, string message)
		{
			AddLogInfo(imageKey, style, false, message);
		}

		/// <summary>
		/// 添加日志记录
		/// </summary>
		public void AddLogInfo(string imageKey, RowStyleType style, bool inverseStyle, string message)
		{
			if (this.InvokeRequired)
			{
				Invoke(new Action<string, RowStyleType, bool, string>(AddLogInfo), imageKey, style, inverseStyle, message);
				return;
			}

			var lit = CreateItemStatic(style, inverseStyle, DateTime.Now.ToString());
			lit.SubItems.Add(message);
			lit.ImageKey = imageKey;

			this.Items.Add(lit);
			if (ItemCountLimit > 0 && ItemCountLimit <= Items.Count)
			{
				Items.RemoveAt(0);
			}
			lit.EnsureVisible();
		}

		/// <summary>
		/// 添加日志记录
		/// </summary>
		public void AddLogInfo(string imageKey, ColorSchema style, bool inverseStyle, string message)
		{
			if (this.InvokeRequired)
			{
				Invoke(new Action<string, ColorSchema, bool, string>(AddLogInfo), imageKey, style, inverseStyle, message);
				return;
			}

			ListViewItem lit = CreateItemStatic(style, inverseStyle, DateTime.Now.ToString());
			lit.SubItems.Add(message);
			lit.ImageKey = imageKey;

			this.Items.Add(lit);
			if (ItemCountLimit > 0 && ItemCountLimit <= Items.Count)
			{
				Items.RemoveAt(0);
			}
			lit.EnsureVisible();
		}

		/// <summary>
		/// 创建一个列表项
		/// </summary>
		/// <param name="style">行的类型</param>
		/// <param name="text">文本内容</param>
		/// <returns>创建的列表项</returns>
		public static ListViewItem CreateItemStatic(ColorSchema style, string text)
		{
			return CreateItemStatic(style, false, text);
		}

		/// <summary>
		/// 创建一个列表项
		/// </summary>
		/// <param name="style">行的类型</param>
		/// <param name="inverveStyle">是否交换前景色和背景色配置</param>
		/// <param name="text">文本内容</param>
		/// <returns>创建的列表项</returns>
		public static ListViewItem CreateItemStatic(ColorSchema style, bool inverveStyle, string text)
		{
			ListViewItem lit = new ListViewItem()
			{
				Text = text,
				UseItemStyleForSubItems = true
			};
			ListViewResource.SwitchListViewItemStyle(lit, style, inverveStyle);

			return lit;
		}

		/// <summary>
		/// 创建一个列表项
		/// </summary>
		/// <param name="style">行的类型</param>
		/// <param name="text">文本内容</param>
		/// <returns>创建的列表项</returns>
		public static ListViewItem CreateItemStatic(RowStyleType style, string text)
		{
			return CreateItemStatic(style, false, text);
		}

		/// <summary>
		/// 创建一个列表项
		/// </summary>
		/// <param name="style">行的类型</param>
		/// <param name="reserveStyle">是否交换前景色和背景色配置</param>
		/// <param name="text">文本内容</param>
		/// <returns>创建的列表项</returns>
		public static ListViewItem CreateItemStatic(RowStyleType style, bool reserveStyle, string text)
		{
			ListViewItem lit = new ListViewItem()
			{
				Text = text,
				UseItemStyleForSubItems = true
			};
			ListViewResource.SwitchListViewItemStyle(lit, style, reserveStyle);

			return lit;
		}

		/// <summary>
		/// 清空日志记录
		/// </summary>
		public void Clear()
		{
			this.Items.Clear();
		}

		/// <summary>
		/// 获得或设置日志条数限制
		/// </summary>
		public int ItemCountLimit { get; set; }
	}
}
