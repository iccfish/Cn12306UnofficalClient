using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Vc
{
	using AutoVc;

	using Data;

	using DevComponents.DotNetBar;

	using Dialogs;

	using System.Threading;
	using System.Threading.Tasks;

	using TOBA.Configuration;
	using TOBA.Service;

	using WebLib;

	internal partial class TouchClickSimple : VcControlBase
	{


		List<KeyValuePair<Point, PictureBox>> _location = new List<KeyValuePair<Point, PictureBox>>();
		private CancellationTokenSource _cancellationTokenSource;

		public TouchClickSimple()
		{
			InitializeComponent();

			verifyCodeBox1.MouseClick += VerifyCodeBox1_MouseClick;
			btnOk.Click += (s, e) => OnVerifyCodeEnterComplete();
			btnRefresh.Click += (s, e) =>
			{
				_cancellationTokenSource?.Cancel(false);
				LoadVerifyCode();
			};
			DelegateVcEvents(verifyCodeBox1);

			//自动识别
			verifyCodeBox1.BeginAutoVc += VerifyCodeBox1_BeginAutoVc;
			verifyCodeBox1.AutoVcFailed += VerifyCodeBox1_AutoVcFailed;
			verifyCodeBox1.AutoVcGiveUp += VerifyCodeBox1_AutoVcGiveUp;
			verifyCodeBox1.EndAutoVc += VerifyCodeBox1_EndAutoVc;
			verifyCodeBox1.VerifyCodeLoadComplete += (s, e) =>
			{
				textBoxX1.Visible = verifyCodeBox1.IsTraditionalCode;
				if (verifyCodeBox1.IsTraditionalCode)
				{
					textBoxX1.Focus();
				}
			};
			verifyCodeBox1.VerifyCodeLoadFailed += VerifyCodeBox1_VerifyCodeLoadFailed;
			textBoxX1.KeyUp += (s, e) =>
			{
				if (textBoxX1.TextLength == 4)
				{
					OnVerifyCodeEnterComplete();
				}
			};
			Load += (s, e) =>
			{
				if (btnOk.Visible)
					btnOk.Focus();
			};
			Disposed += (_1, _2) => { _cancellationTokenSource?.Cancel(false); };
		}

		private Form Form => this.FindForm();

		private void VerifyCodeBox1_VerifyCodeLoadFailed(object sender, EventArgs e)
		{
			var ar = AutoResumeRefreshConfiguration.Instance;
			if (!ar.AutoReloadVc)
				return;

			pReload.Visible = true;
			var (isOpen, nextTime) = ParamData.GetSystemMaintenanceTime();
			nextTime = Math.Max(nextTime, ar.AutoReloadVcTime);
			var toTime = DateTime.Now.AddMilliseconds(nextTime);
			lblReloadTip.Text = $"{Utility.ShowMilliSecondInfo(nextTime)} 自动刷新{(isOpen ? "" : "(等待开放)")}";
			_cancellationTokenSource?.Cancel(false);
			_cancellationTokenSource = new CancellationTokenSource();

			async void WaitAutoReloadAsync(CancellationToken token)
			{
				try
				{
					while (DateTime.Now < toTime)
					{
						lblReloadTip.Text = $"{Utility.ShowMilliSecondInfo((int)(toTime - DateTime.Now).TotalMilliseconds)} 自动刷新";
						await Task.Delay(100, token).ConfigureAwait(true);
						if (token.IsCancellationRequested || IsDisposed || Form.IsDisposed)
							return;
					}
					if (token.IsCancellationRequested || IsDisposed || Form.IsDisposed)
						return;
					LoadVerifyCode();
				}
				catch (Exception)
				{
					return;
				}
				finally
				{
					_cancellationTokenSource?.Cancel(false);

					_cancellationTokenSource = null;
					pReload.Visible = false;
				}

			}

			WaitAutoReloadAsync(_cancellationTokenSource.Token);
		}

		public TouchClickSimple(Session session) : this()
		{
			InitSession(session);
		}

		bool AddClickPoint(Point point)
		{
			var trueLocation = point + ParamData.TouchClickPointOffset;
			if (trueLocation.X <= 0 || trueLocation.Y <= 0)
				return false;

			//添加拆图标
			var pbloc = new Point((int)((point.X) * verifyCodeBox1.CaptchaZoomX - 16), (int)((point.Y) * verifyCodeBox1.CaptchaZoomY - 16));
			pbloc.Offset(verifyCodeBox1.Location);
			var pb = new PictureBox
			{
				Image = Properties.Resources.vc_marker,
				SizeMode = PictureBoxSizeMode.AutoSize,
				Location = pbloc
			};
			Controls.Add(pb);
			pb.BringToFront();
			pb.Click += Pb_Click;

			_location.Add(new KeyValuePair<Point, PictureBox>(trueLocation, pb));

			return true;
		}

		/// <summary>
		/// 移除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Pb_Click(object sender, EventArgs e)
		{
			var target = _location.FirstOrDefault(s => s.Value == sender);
			if (target.Value != null)
			{
				Controls.Remove(target.Value);
				_location.Remove(target);
			}

			OnCodeEnterChanged();
		}

		private void VerifyCodeBox1_AutoVcFailed(object sender, EventArgs e)
		{
			lblVcTip.Text = $"第{verifyCodeBox1.AutoVcCount}次自动识别失败";
			pbVcTp.Image = Properties.Resources.block_16;
		}

		private void VerifyCodeBox1_AutoVcGiveUp(object sender, EventArgs e)
		{
			lblVcTip.ForeColor = Color.Red;
			lblVcTip.Text = "无法自动识别";
			pbVcTp.Image = Properties.Resources.block_16;
		}

		private void VerifyCodeBox1_BeginAutoVc(object sender, EventArgs e)
		{
			pVcTip.Visible = true;
			pbVcTp.Image = Properties.Resources.loading_16_3;
			lblVcTip.Text = $"正在第{verifyCodeBox1.AutoVcCount}次自动识别..";
		}


		private void VerifyCodeBox1_EndAutoVc(object sender, EventArgs e)
		{
			var result = verifyCodeBox1.AutoVcCode;
			if (result == null)
				return;
			pbVcTp.Image = Properties.Resources.tick_16;
			lblVcTip.Text = $"第{verifyCodeBox1.AutoVcCount}次识别完成";

			if (result.CodeType == VerifyCodeRecognizeType.Text)
			{
				textBoxX1.Text = result.Code;
				OnEndAutoVc();
			}
			else if (result.CodeType == VerifyCodeRecognizeType.Points)
			{
				var points = verifyCodeBox1.AutoVcCode?.Points;
				if (_location.Count > 0)
				{
					//用户已输入
					switch (AutoVcConfig.Instance.VcResultConflict)
					{
						case AutoVcConflictResult.None:
							break;
						case AutoVcConflictResult.Ignore:
							return;
						case AutoVcConflictResult.ClearUser:
							_location.ToArray().ForEach(s => Pb_Click(s.Value, EventArgs.Empty));
							break;
						default:
							break;
					}
				}

				//180,140|261,145
				foreach (var point in points)
				{
					AddClickPoint(point);
				}

				OnEndAutoVc();
			}
		}


		private void VerifyCodeBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (MediaConfiguration.Instance.StopMusicIfUserOperated)
				MainForm.Instance.StopPlayTicketMusic(true);

			//传统验证码，则重新加载
			if (verifyCodeBox1.IsTraditionalCode)
			{
				verifyCodeBox1.ResetLoadImage();
				verifyCodeBox1.LoadVerifyCode();
				return;
			}

			if (!verifyCodeBox1.Loaded)
				return;

			//转换坐标为相对于图片的
			var location = new Point((int)(e.Location.X / verifyCodeBox1.CaptchaZoomX), (int)(e.Location.Y / verifyCodeBox1.CaptchaZoomY));

			if (!AddClickPoint(location))
				return;

			OnCodeEnterChanged();
			if ((e.Button & MouseButtons.Right) > 0)
			{
				OnVerifyCodeEnterComplete();
			}
		}

		void Clear()
		{
			_location.Select(s => s.Value).ForEach(Controls.Remove);
			_location.Clear();
			ResetLoadImage();
			textBoxX1.Clear();
			textBoxX1.Visible = false;
		}

		/// <summary>
		/// 加载验证码
		/// </summary>
		public override void LoadVerifyCode()
		{
			OnVerifyCodeOnLoad();
			Clear();
			verifyCodeBox1.LoadVerifyCode();
		}


		public override void MarkError(IVerifyCodeRecognizeResult code)
		{
			verifyCodeBox1.MarkError(code);
		}

		public override void ResetLoadImage()
		{
			ValidateImage = Properties.Resources._32px_loading_1;
		}

		/// <summary>
		/// 重置自己以及父容器尺寸
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		public void ResizeParent<T>(T parent, int? zoom, int availableExtraSpaceX = 0, int availableExtraSpaceY = 0) where T : Control
		{
			if (zoom.HasValue)
				Zoom = zoom.Value;

			var clientSize = parent.ClientSize;
			var minSize = MinSize;
			parent.ClientSize = clientSize + new Size(Math.Max(0, minSize.Width - Width - availableExtraSpaceX), Math.Max(0, minSize.Height - Height - availableExtraSpaceY));
			Size = minSize;
		}

		public override void SetExpires()
		{
			//ValidateImage = Properties.Resources.tip_vcexp;
			verifyCodeBox1.SetExpires();
		}

		/// <summary>
		/// 获得或设置是否无法识别时自动重载
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoReloadIfAutoVcFailed
		{
			get { return verifyCodeBox1.AutoReloadIfAutoVcFailed; }
			set { verifyCodeBox1.AutoReloadIfAutoVcFailed = value; }
		}

		/// <summary>
		/// 获得自动识别的次数
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int AutoVcCount => verifyCodeBox1.AutoVcCount;

		public double CaptchaZoomX => verifyCodeBox1.CaptchaZoomX;
		public double CaptchaZoomY => verifyCodeBox1.CaptchaZoomY;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Code
		{
			get
			{
				if (verifyCodeBox1.IsTraditionalCode)
					return textBoxX1.Text;

				return _location.Select(s => $"{s.Key.X},{s.Key.Y}").JoinAsString(",");
			}
			protected set { }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override PictureBoxSizeMode CodeSizeMode
		{
			get { return verifyCodeBox1.CodeSizeMode; }
			set { verifyCodeBox1.CodeSizeMode = value; }
		}

		/// <summary>
		/// 获得或设置是否允许自动识别
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool EnableAutoVc
		{
			get { return verifyCodeBox1.EnableAutoVc; }
			set { verifyCodeBox1.EnableAutoVc = value; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool EnableClickReload
		{
			get { return verifyCodeBox1.EnableClickReload; }
			set { verifyCodeBox1.EnableClickReload = value; }
		}

		/// <summary>
		/// 获得是否是传统的验证码
		/// </summary>
		public override bool IsTraditionalCode => verifyCodeBox1.IsTraditionalCode;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int MaxAutoVcCount
		{
			get { return verifyCodeBox1.MaxAutoVcCount; }
			set { verifyCodeBox1.MaxAutoVcCount = value; }
		}

		/// <summary>
		/// 获得适合于验证码的最小尺寸
		/// </summary>
		public Size MinSize => new Size((int)(300 * verifyCodeBox1.CaptchaZoomX), (int)(190 * verifyCodeBox1.CaptchaZoomY + 70));

		public ButtonX OkButton => btnOk;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override RandCodeType RandType
		{
			get { return verifyCodeBox1.RandType; }
			set { verifyCodeBox1.RandType = value; }
		}

		public override bool ShowOkButton { get { return btnOk.Visible; } set { btnOk.Visible = value; } }

		/// <summary>
		/// 获得或设置当前的验证码图像
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image ValidateImage { get { return verifyCodeBox1.Image; } set { verifyCodeBox1.Image = value; } }


		/// <summary>
		/// 验证码放大缩小比例
		/// </summary>
		[DefaultValue(10), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Zoom
		{
			get { return verifyCodeBox1.Zoom; }
			set { verifyCodeBox1.Zoom = value; }
		}

		public override void SetIfNeedVc(bool? need)
		{
			base.SetIfNeedVc(need);
			Clear();
			verifyCodeBox1.SetIfNeedVc(need);

			if (need == false)
			{
				panelEx1.Text = "本次提交无需输入验证码";
				Enabled = false;
			}
			else if (need == true)
			{
				Enabled = true;
				panelEx1.Text = "请在下方输入验证码";
			}
			else
			{
				Enabled = false;
				panelEx1.Text = "未知是否需要验证码，请先提交";
			}
		}
	}
}
