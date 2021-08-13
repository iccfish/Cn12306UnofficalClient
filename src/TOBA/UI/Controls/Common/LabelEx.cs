namespace TOBA.UI.Controls.Common
{
	using System;

	internal class LabelEx : System.Windows.Forms.Label
	{
		public LabelEx()
			: base()
		{
			//关闭AutoSize
			this.AutoSize = false;

			//捕捉事件
			this.TextChanged += new EventHandler(LabelEx_TextChanged);
			this.SizeChanged += new EventHandler(LabelEx_SizeChanged);
		}

		//记录原始的宽度
		int _oldw;

		void LabelEx_SizeChanged(object sender, EventArgs e)
		{
			//如果宽度没变，那就返回
			if (this.Width == _oldw) return;

			//重新计算高度
			LabelEx_TextChanged(null, null);

			//记录
			_oldw = this.Width;
		}

		/// <summary>
		/// 重写AutoSize属性，防止AutoSize再被打开
		/// </summary>
		public override bool AutoSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// 是否自动调整父控件
		/// </summary>
		public bool AutoResizeParent { get; set; }

		void LabelEx_TextChanged(object sender, EventArgs e)
		{
			//记录下当前的高度
			int oh = this.Height;

			//文字变化了，那就改变一下当前的大小
			System.Drawing.Size ps = GetPreferredSize(this.Size);

			//这里构造一个新的Size对象，目的是使用原始的宽度。原因嘛，见上面
			this.Size = new System.Drawing.Size(this.Width, ps.Height);

			if (this.Parent != null && AutoResizeParent)
			{
				//调整容器大小
				this.Parent.Size = new System.Drawing.Size(this.Parent.Width, ps.Height - oh + this.Parent.Height);
			}
		}
	}
}
