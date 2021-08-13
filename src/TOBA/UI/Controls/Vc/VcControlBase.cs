using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Vc
{
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	using AutoVc;

	using Service;

	using WebLib;

	internal class VcControlBase : ControlBase, IVerifyCodeControl
	{
		private IVerifyCodeControl _control;

		/// <summary>
		/// 自动识别失败
		/// </summary>
		public event EventHandler AutoVcFailed;

		/// <summary>
		/// 放弃自动识别（失败次数过多）
		/// </summary>
		public event EventHandler AutoVcGiveUp;

		/// <summary>
		/// 开始自动识别时触发
		/// </summary>
		public event EventHandler BeginAutoVc;

		/// <summary>
		/// 指定当前的输入验证码状态已变
		/// </summary>
		public event EventHandler CodeEnterChanged;

		/// <summary>
		/// 完成识别
		/// </summary>
		public event EventHandler EndAutoVc;

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

		/// <summary>
		/// 开始自动识别
		/// </summary>
		protected virtual void AutoVc()
		{

		}

		/// <summary>
		/// 路由另一个控件的事件
		/// </summary>
		protected virtual void DelegateVcEvents(IVerifyCodeControl control)
		{
			_control = control;

			control.CodeEnterChanged += (s, e) => OnCodeEnterChanged();
			control.VerifyCodeEnterComplete += (s, e) => OnVerifyCodeEnterComplete();
			control.BeginAutoVc += (s, e) => OnBeginAutoVc(this, EventArgs.Empty);
			control.AutoVcFailed += (s, e) => OnAutoVcFailed();
			control.AutoVcGiveUp += (s, e) => OnAutoVcGiveUp();
			control.VerifyCodeOnLoad += (s, e) => OnVerifyCodeOnLoad();
			control.VerifyCodeLoadComplete += (s, e) => OnVerifyCodeLoadComplete();
			control.VerifyCodeLoadFailed += (s, e) => OnVerifyCodeLoadFailed();
		}

		protected virtual void OnAutoVcFailed()
		{
			AutoVcFailed?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnAutoVcGiveUp()
		{
			AutoVcGiveUp?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnBeginAutoVc(object sender, EventArgs e)
		{
			EventHandler handler = BeginAutoVc;
			if (handler != null)
				handler(sender, e);
		}

		/// <summary>
		/// 引发 <see cref="CodeEnterChanged" /> 事件
		/// </summary>
		protected virtual void OnCodeEnterChanged()
		{
			var handler = CodeEnterChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		protected virtual void OnEndAutoVc()
		{
			EndAutoVc?.Invoke(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="VerifyCodeEnterComplete" /> 事件
		/// </summary>
		protected virtual void OnVerifyCodeEnterComplete()
		{
			var handler = VerifyCodeEnterComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="VerifyCodeLoadComplete" /> 事件
		/// </summary>
		protected virtual void OnVerifyCodeLoadComplete()
		{
			if (EnableAutoVc)
				AutoVc();

			var handler = VerifyCodeLoadComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

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
		public virtual void LoadVerifyCode()
		{
		}

		public virtual void MarkError(IVerifyCodeRecognizeResult code)
		{
		}

		public virtual void ResetLoadImage()
		{
		}

		public virtual void SetExpires()
		{
		}

		/// <summary>
		/// 获得或设置是否无法识别时自动重载
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoReloadIfAutoVcFailed { get; set; }

		/// <summary>
		/// 获得自动识别的验证码
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IVerifyCodeRecognizeResult AutoVcCode { get; protected set; }

		/// <summary>
		/// 获得自动识别的次数
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int AutoVcCount { get; protected set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Code { get; protected set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual PictureBoxSizeMode CodeSizeMode { get; set; }

		/// <summary>
		/// 获得或设置是否允许自动识别
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool EnableAutoVc { get; set; }

		public virtual bool EnableClickReload
		{
			get; set;
		}

		/// <summary>
		/// 获得是否是传统的验证码
		/// </summary>
		public virtual bool IsTraditionalCode { get; protected set; }

		/// <summary>
		/// 获得验证码是否已经加载成功
		/// </summary>
		public bool Loaded => _control.Loaded;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int MaxAutoVcCount { get; set; }


		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RandCodeType RandType
		{
			get; set;
		}

		public virtual bool ShowOkButton { get; set; }


		/// <inheritdoc />
		public bool? NeedVc { get; protected set; }

		/// <inheritdoc />
		public virtual void SetIfNeedVc(bool? need)
		{
			NeedVc = need;
		}


		/// <summary>
		/// 获得或设置当前的验证码图像
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Image ValidateImage { get; set; }
	}
}
