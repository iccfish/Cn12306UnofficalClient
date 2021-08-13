namespace TOBA.Account
{
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using UI;
	using UI.Dialogs.Account;

	class SessionReloginService : ISessionReloginService
	{
		private Session _session;
		private Task<bool> _task;

		public SessionReloginService(Session session)
		{
			_session = session;
		}

		/// <summary>
		/// 重试登录
		/// </summary>
		/// <returns></returns>
		public async Task<bool> ReloginAsync()
		{
			var owner = AppContext.HostForm;

			var task = _task;
			if (_task == null)
			{
				lock (_session)
				{
					if (_task == null)
						_task = ReloginCore(owner);
				}
				task = _task;
			}

			var ret = await task.ConfigureAwait(true);
			_task = null;

			return ret;
		}

		/// <summary>
		/// 重试登录
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public Task<bool> ReloginCore(Form owner)
		{
			var tcs = new TaskCompletionSource<bool>();

			var loginDlg = new Login();
			loginDlg.PreSelectUser = _session.UserName;
			loginDlg.Session = _session;

			loginDlg.FormClosed += (s, e) =>
			{
				tcs.TrySetResult(loginDlg.DialogResult == DialogResult.OK);
			};
			UiUtility.PlaceFormAtCenter(loginDlg, owner);

			return tcs.Task;
		}

	}
}