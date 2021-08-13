using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using TOBA.Service;

using Timer = System.Windows.Forms.Timer;

namespace TOBA.UI.Controls
{
	using AutoVc;

	using System.Net;

	using Utilities;

	using WebLib;

	internal class VerifyCodeBox : PictureBox, IRequireSessionInit, IVerifyCodeControl
	{

		Size _bakSize;
		int _reloadCount;
		Timer _timer;

		public VerifyCodeBox()
		{
			if (!Program.IsRunning)
				return;

			AutoReloadIfAutoVcFailed = AutoVcConfig.Instance.AutoReloadIfNotSuccess;
			MaxAutoVcCount = AutoVcConfig.Instance.MaxGiveupFailed;
			CodeSizeMode = SizeMode = PictureBoxSizeMode.AutoSize;
			Cursor = System.Windows.Forms.Cursors.Hand;
			RandType = RandCodeType.SjRand;
			_timer = new Timer()
			{
				Interval = 500
			};
			_timer.Tick += (s, e) =>
			{
				_timer.Enabled = false;
				LoadVerifyCode();
			};

			HandleCreated += VerifyCodeBox_HandleCreated;
			Click += VerifyCodeBox_Click;
		}

		/// <summary>
		/// 指定当前的输入验证码状态已变
		/// </summary>
		public event EventHandler CodeEnterChanged;
		/// <summary>
		/// 指定当前已经完成验证
		/// </summary>
		public event EventHandler VerifyCodeEnterComplete;

		/// <summary>
		/// 验证码加载成功
		/// </summary>
		public event EventHandler VerifyCodeLoadComplete;

		/// <summary>
		/// 验证码加载失败
		/// </summary>
		public event EventHandler VerifyCodeLoadFailed;

		public event EventHandler VerifyCodeOnLoad;

		void VerifyCodeBox_Click(object sender, EventArgs e)
		{
			if (EnableClickReload)
				LoadVerifyCode();
		}

		void VerifyCodeBox_HandleCreated(object sender, EventArgs e)
		{
			_bakSize = Size;
		}


		/// <summary>
		/// 引发 <see cref="VerifyCodeLoadComplete" /> 事件
		/// </summary>
		protected virtual void OnVerifyCodeLoadComplete()
		{
			var handler = VerifyCodeLoadComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="VerifyCodeLoadFailed"/>
		/// </summary>
		protected virtual void OnVerifyCodeLoadFailed()
		{
			VerifyCodeLoadFailed?.Invoke(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="VerifyCodeOnLoad" /> 事件
		/// </summary>
		protected virtual void OnVerifyCodeOnLoad()
		{
			var handler = VerifyCodeOnLoad;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 加载验证码
		/// </summary>
		public void LoadVerifyCode()
		{
			if (Session == null)
				return;

			Loaded = false;
			ResetLoadImage();
			WaitOnLoad = false;
			AutoVcCode = null;
			Session.NetClient.CreateVerifyCodeTask(RandType, (e1, e2) =>
			{
				if (Parent == null)
					return;

				//先保存到临时流
				Size = _bakSize;
				SizeMode = CodeSizeMode;
				IsTraditionalCode = VcUtility.IsTraditionalCode(e1);
				var size = IsTraditionalCode ? new Size(e1.Width * 3, e1.Height * 3) : new Size((int)(e1.Width * CaptchaZoomX), (int)(e1.Height * CaptchaZoomY));

				//判断大小，并设置显示类型
				SizeMode = PictureBoxSizeMode.Zoom;
				Image = e1;
				Size = size;
				Loaded = true;
				OnVerifyCodeLoadComplete();

				if (EnableAutoVc && VcUtility.IsEngineAvailable(VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine))
				{
					AutoVc();
				}
			}, (e1) =>
			{
				var ctx = e1;

				Session.NeedCheckStatus = ctx.Status == HttpStatusCode.OK;

				Image = Properties.Resources.img_failed;
				SizeMode = PictureBoxSizeMode.AutoSize;

				if (Session.NeedCheckStatus || ++_reloadCount > 10)
				{
					OnVerifyCodeLoadFailed();
				}
				else
				{
					_timer.Enabled = true;
				}
			});
			OnVerifyCodeOnLoad();
		}

		public void ResetLoadImage()
		{
			SizeMode = PictureBoxSizeMode.AutoSize;
			Image = Properties.Resources._16px_loading_1;
		}

		public void SetExpires()
		{
			Image = Properties.Resources.tip_vcexp;
			SizeMode = PictureBoxSizeMode.AutoSize;
		}

		public double CaptchaZoomX => 0.1 * Zoom * Program.DpiX / 96.0;
		public double CaptchaZoomY => 0.1 * Zoom * Program.DpiY / 96.0;

		public string Code
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsTraditionalCode { get; private set; }

		/// <summary>
		/// 获得验证码是否已经加载成功
		/// </summary>
		public bool Loaded { get; private set; }

		public RandCodeType RandType { get; set; }
		public bool ShowOkButton { get; set; }

		public virtual bool? NeedVc { get; protected set; }

		public void SetIfNeedVc(bool? need)
		{
			NeedVc = need;
			if (NeedVc == null)
			{
				Image = Properties.Resources.unknownneedpc;
				SizeMode = PictureBoxSizeMode.AutoSize;
			}
			else if (NeedVc == false)
			{
				Image = Properties.Resources.noneedpc;
				SizeMode = PictureBoxSizeMode.AutoSize;
			}
			else
			{
				LoadVerifyCode();
			}
		}

		/// <summary>
		/// 获得或设置当前的验证码图像
		/// </summary>
		public System.Drawing.Image ValidateImage
		{
			get { return Image; }
			set { Image = value; }
		}
		/// <summary>
		/// 放大比例（图片验证码）
		/// </summary>
		[DefaultValue(10), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Zoom { get; set; } = 10;


		#region 自动识别

		public bool EnableClickReload { get; set; }

		public PictureBoxSizeMode CodeSizeMode { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaxAutoVcCount { get; set; }

		/// <summary>
		/// 获得自动识别的次数
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int AutoVcCount { get; protected set; }

		/// <summary>
		/// 获得或设置是否允许自动识别
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnableAutoVc { get; set; }

		/// <summary>
		/// 获得或设置是否无法识别时自动重载
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoReloadIfAutoVcFailed { get; set; }

		/// <summary>
		/// 开始自动识别时触发
		/// </summary>
		public event EventHandler BeginAutoVc;

		/// <summary>
		/// 引发 <see cref="BeginAutoVc" /> 事件
		/// </summary>
		protected virtual void OnBeginAutoVc()
		{
			var handler = BeginAutoVc;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 完成识别
		/// </summary>
		public event EventHandler EndAutoVc;

		/// <summary>
		/// 引发 <see cref="EndAutoVc" /> 事件
		/// </summary>
		protected virtual void OnEndAutoVc()
		{
			var handler = EndAutoVc;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 自动识别失败
		/// </summary>
		public event EventHandler AutoVcFailed;

		/// <summary>
		/// 放弃自动识别（失败次数过多）
		/// </summary>
		public event EventHandler AutoVcGiveUp;

		/// <summary>
		/// 引发 <see cref="AutoVcGiveUp" /> 事件
		/// </summary>
		protected virtual void OnAutoVcGiveUp()
		{
			var handler = AutoVcGiveUp;
			handler?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 获得自动识别的验证码
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IVerifyCodeRecognizeResult AutoVcCode
		{
			get;
			private set;
		}

		/// <summary>
		/// 引发 <see cref="AutoVcFailed" /> 事件
		/// </summary>
		protected virtual void OnAutoVcFailed()
		{
			var handler = AutoVcFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		void AutoVc()
		{
			AutoVcCount++;
			if (AutoVcCount > MaxAutoVcCount)
			{
				OnAutoVcGiveUp();
				return;
			}
			OnBeginAutoVc();

			var img = (Image)Image.Clone();

			var task = new Task<IVerifyCodeRecognizeResult>(() => VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.GetCode(img));
			task.ContinueWith(_ => AppContext.MainForm.UiInvoke(() =>
			{
				if (IsDisposed)
					return;

				if (_.IsCompleted && !_.IsFaulted && _.Result != null)
				{
					AutoVcCode = _.Result;
					//report
					OnEndAutoVc();
				}
				else
				{
					OnAutoVcFailed();

					if (AutoReloadIfAutoVcFailed)
						LoadVerifyCode();
				}
			}));
			task.Start();
		}

		public void MarkError(IVerifyCodeRecognizeResult code)
		{
			if (VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null && AutoVcCode != null)
			{
				VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.MarkResult(AutoVcCode, false);
				AutoVcCode = null;
			}
		}

		#endregion


		#region IOperation 成员

		public Session Session
		{
			get;
			private set;
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			Session = session;
		}

		#endregion

	}
}
