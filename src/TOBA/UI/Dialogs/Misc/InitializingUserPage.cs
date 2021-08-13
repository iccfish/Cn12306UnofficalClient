using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Misc
{
	using System.Threading;

	public partial class InitializingUserPage : Form
	{
		private InitializingUserPage(Form parentForm)
		{
			InitializeComponent();

			TopMost = true;
			StartPosition = FormStartPosition.Manual;
			Location = new Point(
				parentForm.Location.X + parentForm.Width / 2 - Width / 2,
				parentForm.Location.Y + parentForm.Height / 2 - Height / 2
				);
		}

		public static IDisposable Show(Form parentForm)
		{
			var f = new InitializingUserPage(parentForm);
			var ret = new DisposableScope(f);

			new Thread(() => f.ShowDialog()).Start();

			return ret;
		}

		class DisposableScope : IDisposable
		{
			private InitializingUserPage _container;

			#region Dispose方法实现

			bool _disposed;

			public DisposableScope(InitializingUserPage container) { _container = container; }

			/// <summary>
			/// 释放资源
			/// </summary>
			public void Dispose()
			{
				Dispose(true);

				_container.Invoke(new Action(() => _container.Close()));
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



		}
	}
}
