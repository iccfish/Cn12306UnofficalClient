using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Misc
{
	using System.Diagnostics;
	using System.Drawing.Drawing2D;
	using System.Threading.Tasks;

	internal partial class MsgLayer : UserControl
	{
		private Task<bool> _task;
		private TaskCompletionSource<bool> _tcs;
		private CapturedContext _context;

		public MsgLayer()
		{
			InitializeComponent();

			//遮罩颜色
			Dock = DockStyle.Fill;
			BackColor = Color.FromArgb(195, 207, 217);

			pConfirm.Visible = false;
			pInfo.Visible = false;
			Visible = false;

			pMain.ClientSizeChanged += (s, e) =>
			{
				pMain.Location = new Point((Width - pMain.Width) / 2, (Height - pMain.Height) / 2);
				pConfirm.Location = new Point((pMain.Width - pConfirm.Width) / 2, pConfirm.Location.Y);
				pInfo.Location = new Point((pMain.Width - pInfo.Width) / 2, pInfo.Location.Y);
			};
			ClientSizeChanged += MsgLayer_ClientSizeChanged;
			btnOk.Click += (s, e) =>
			{
				if (_task == null)
					return;

				_tcs.TrySetResult(true);
				_tcs = null;
				_task = null;
				Visible = false;
				Enabled = false;
			};
			btnOkOnly.Click += (s, e) =>
			{
				if (_task == null)
					return;

				_tcs.TrySetResult(true);
				_tcs = null;
				_task = null;
				Visible = false;
				Enabled = false;
			};
			btnCancel.Click += (s, e) =>
			{
				if (_task == null)
					return;

				_tcs.TrySetResult(false);
				_tcs = null;
				_task = null;
				Visible = false;
				Enabled = false;
			};
			ParentChanged += MsgLayer_ParentChanged;
			Enabled = false;
		}

		/// <param name="m">要处理的 Windows <see cref="T:System.Windows.Forms.Message"/>。</param>
		private void MsgLayer_ParentChanged(object sender, EventArgs ee)
		{
			if (Parent != null)
				BringToFront();
			var form = ParentForm;
			if (form == null)
				return;

			form.KeyPreview = true;
			form.KeyDown += (s, e) =>
			{
				if (!this.Visible || !this.Enabled)
					return;

				if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
				{
					if (pInfo.Visible)
						btnOkOnly.PerformClick();
					else
						btnOk.PerformClick();

					e.Handled = true;
				}
				else if (e.KeyCode == Keys.Escape)
				{
					btnCancel.PerformClick();
					e.Handled = true;
				}
			};
		}

		/// <summary>
		/// 内容尺寸
		/// </summary>
		public Size DlgSize
		{
			get { return pMain.Size; }
			set { pMain.Size = value; }
		}

		private void MsgLayer_ClientSizeChanged(object sender, EventArgs e)
		{
			pMain.Location = new Point((Width - pMain.Width) / 2, (Height - pMain.Height) / 2);
		}

		public void SetText(string text)
		{
			lblInfo.Text = text;
			lblInfo.Size = lblInfo.GetPreferredSize(new Size(pMain.Width, 0));
			pMain.Size = lblInfo.Size + new Size(0, pConfirm.Height + 5);
		}

		public async Task Information(string text)
		{
			var form = ParentForm;
			if (form == null)
				throw new InvalidOperationException();

			if (_task == null)
			{
				_tcs = new TaskCompletionSource<bool>();
				_task = _tcs.Task;
				_context = new CapturedContext(form, this);
				form.AcceptButton = null;
				form.CancelButton = null;
				form.ActiveControl = btnOkOnly;

				Visible = true;
				Enabled = true;

				SetText(text);
				pConfirm.Hide();
				pConfirm.Enabled = false;
				pInfo.Show();
				pInfo.Enabled = true;
				Show();

				BringToFront();
			}

			await _task.ConfigureAwait(true);
			_context?.Dispose();
		}


		public async Task<bool> Confirm(string text)
		{
			var form = ParentForm;
			if (form == null)
				return false;

			if (_task == null)
			{
				_tcs = new TaskCompletionSource<bool>();
				_task = _tcs.Task;
				_context = new CapturedContext(form, this);
				form.AcceptButton = null;
				form.CancelButton = null;
				form.ActiveControl = btnOk;

				Enabled = true;
				Visible = true;
				SetText(text);
				pConfirm.Show();
				pConfirm.Enabled = true;
				pInfo.Hide();
				pInfo.Enabled = false;
				Show();

				BringToFront();
			}

			var ret = await _task.ConfigureAwait(true);
			_context?.Dispose();
			return ret;
		}

		internal class CapturedContext : IDisposable
		{
			private readonly Control _control;
			IButtonControl _acceptedButton;
			IButtonControl _cancelButton;
			Control _activeControl;
			Dictionary<Control, bool> _state;

			public CapturedContext(Control control, Control self)
			{
				_control = control;
				if (_control is Form)
				{
					var form = _control as Form;
					_acceptedButton = form.AcceptButton;
					_cancelButton = form.CancelButton;
					_activeControl = form.ActiveControl;
				}

				_state = control.Controls.Cast<Control>().Where(s => s.TabStop).ToDictionary(s => s, s =>
				  {
					  var ret = s.Enabled;
					  if (s != self)
						  s.Enabled = false;
					  return ret;
				  });
			}

			void Restore()
			{
				if (_control == null)
					return;

				if (_control is Form)
				{
					var form = _control as Form;
					if (_acceptedButton != null)
						form.AcceptButton = _acceptedButton;
					if (_cancelButton != null)
						form.CancelButton = _cancelButton;
					if (_activeControl != null && form.Contains(_activeControl))
						form.ActiveControl = _activeControl;

					_acceptedButton = null;
					_cancelButton = null;
					_activeControl = null;
				}
				foreach (var pair in _state)
				{
					pair.Key.Enabled = pair.Value;
				}
				_state = null;
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
				if (_disposed)
					return;
				_disposed = true;

				OnDisposed();
				Restore();
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

			/// <summary>
			/// 对象销毁时触发
			/// </summary>
			public event EventHandler Disposed;

			/// <summary>
			/// 引发 <see cref="Disposed" /> 事件
			/// </summary>
			protected virtual void OnDisposed()
			{
				var handler = Disposed;
				if (handler != null)
					handler(this, EventArgs.Empty);
			}

			#endregion


		}
	}
}
