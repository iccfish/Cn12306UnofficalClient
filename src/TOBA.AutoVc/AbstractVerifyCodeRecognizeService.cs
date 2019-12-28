using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.AutoVc
{

	/// <summary>
	/// <see cref="IVerifyCodeRecognizeService"/> 基类
	/// </summary>
	public abstract class AbstractVerifyCodeRecognizeService : IVerifyCodeRecognizeService
	{
		/// <summary>
		/// 状态发生变化
		/// </summary>
		public event EventHandler StateChanged;

		/// <summary>
		/// 引发 <see cref="StateChanged"/> 事件
		/// </summary>
		protected virtual void OnStateChanged()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				//TODO 释放托管资源

			}
			//TODO 释放非托管资源

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion

		/// <summary>
		/// 登录
		/// </summary>
		/// <returns></returns>
		public abstract bool DoLogin();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public abstract IVerifyCodeRecognizeResult GetCode(System.Drawing.Image image);

		/// <summary>
		/// 加载
		/// </summary>
		public virtual void Load()
		{
		}

		/// <summary>
		/// 注销
		/// </summary>
		/// <returns></returns>
		public abstract bool Logout();

		/// <summary>
		/// 标记识别结果
		/// </summary>
		/// <param name="result"></param>
		/// <param name="correct"></param>
		public abstract void MarkResult(IVerifyCodeRecognizeResult result, bool correct);

		/// <summary>
		/// 刷新题分
		/// </summary>
		/// <returns></returns>
		public abstract bool RefreshScore();

		/// <summary>
		/// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
		/// </summary>
		/// <returns>
		/// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
		/// </returns>
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// 卸载
		/// </summary>
		public virtual void Unload()
		{
			Dispose();
		}

		public string Author { get; protected set; }

		/// <summary>
		/// 获得错误代码
		/// </summary>
		public int ErrorCode { get; protected set; }

		/// <summary>
		/// 错误代码查询地址
		/// </summary>
		public string ErrorCodeQueryUrl { get; protected set; }


		/// <summary>
		/// 获得或设置是否可用
		/// </summary>
		public bool IsEnabled { get; protected set; }


		bool _logined;

		/// <summary>
		/// 是否已经登录
		/// </summary>
		public bool IsLoggedIn
		{
			get => _logined;
			protected set
			{
				if (_logined == value)
					return;

				_logined = value;
				OnStateChanged();
			}
		}

		string _name;

		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get => _name;
			protected set
			{
				if (_name == value)
					return;

				_name = value;
				IsLoggedIn = false;
			}
		}

		/// <summary>
		/// 引发 <see cref="NameChanged" /> 事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OnNameChanged(object sender, EventArgs e)
		{
			NameChanged?.Invoke(sender, e);
		}

		/// <summary>
		/// <see cref="Name"/> 发生变化
		/// </summary>
		public event EventHandler NameChanged;

		string _password;

		/// <summary>
		/// 登录密码
		/// </summary>
		public string Password
		{
			get { return _password; }
			set
			{
				if (_password == value)
					return;

				_password = value;
				IsLoggedIn = false;
			}
		}

		/// <summary>
		/// 获得提供者的名字
		/// </summary>
		public virtual string ProviderName
		{
			get; protected set;
		}

		/// <summary>
		/// 获得剩余题分
		/// </summary>
		public int Score { get; protected set; }

		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }

		bool _verified;

		/// <summary>
		/// 是否已经通过验证
		/// </summary>
		public bool Verified
		{
			get => _verified;
			protected set
			{
				if (value == _verified)
					return;

				_verified = value;
				OnStateChanged();
			}
		}

		/// <summary>
		/// 获得官网地址
		/// </summary>
		public string WebUrl { get; protected set; }

		/// <inheritdoc />
		public abstract string Id { get; }
	}
}
