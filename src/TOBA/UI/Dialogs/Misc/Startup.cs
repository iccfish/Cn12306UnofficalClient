using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Misc
{

	using System.Drawing.Drawing2D;
	using System.Threading.Tasks;

	partial class Startup : Form, IStartup
	{
		private readonly Action<IStartup> _initFun;
		private bool _allowClose = false;

		public Startup(Action<IStartup> initFun)
		{
			_initFun = initFun;
			InitializeComponent();

			FormClosing += (_1, _2) => _2.Cancel = !_allowClose;
			Shown += (sender, args) => Init();

			var img = CreateTexture();
			var pb = new PictureBox();
			pb.Size = img.Size;
			pb.Image = img;
			pb.Dock = DockStyle.Bottom;
			Controls.Add(pb);
		}

		Image CreateTexture()
		{
			var msg = $"COPYRIGHT 2012-{DateTime.Now.Year}, iFish, ALL RIGHTS RESERVED";

			using (var og = this.CreateGraphics())
			{
				var size = og.MeasureString(msg, DefaultFont);

				var img = new Bitmap((int)size.Width + 4, (int)size.Height + 4);
				using (var ng = Graphics.FromImage(img))
				{
					ng.SmoothingMode = SmoothingMode.AntiAlias;
					ng.DrawString(msg, DefaultFont, Brushes.White, 2, 2);
				}

				return img;
			}
		}

		public Exception Exception { get; private set; }


		async void Init()
		{
			try
			{
				await Task.Delay(1000);
				await Task.Factory.StartNew(() =>
				{
					_initFun(this);
				});
			}
			catch (Exception e)
			{
				Exception = e;
			}

			_allowClose = true;
			Close();
		}

		public string InfoText
		{
			get => label3.Text;
			set
			{
				this.Invoke(() => label3.Text = value);
			}
		}

		/// <inheritdoc />
		public void DispatchUI(Action action)
		{
			this.Invoke(action);
		}

		public void Update(string text) => InfoText = text;

	}

	interface IStartup
	{
		string InfoText { get; set; }

		void DispatchUI(Action action);

		void Update(string text);

		Graphics CreateGraphics();
	}
}
