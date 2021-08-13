using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs
{
	public interface IMainForm
	{
		/// <summary>
		/// 登录用户，此方法仅能登录已经保存密码的用户
		/// </summary>
		void Login(string username);

		/// <summary>
		/// 登录用户
		/// </summary>
		void Login();

		/// <summary>
		/// 对用户的可见性发生变化
		/// </summary>
		event EventHandler IsWindowVisibleChanged;

		/// <summary>
		/// 获得主窗口是否对用户可见
		/// </summary>
		bool IsWindowVisible { get; }

		/// <summary>
		///     服务器时间
		/// </summary>
		DateTime? ServerTime { get; set; }

		/// <summary>
		/// 在UI线程上回调
		/// </summary>
		/// <param name="action"></param>
		void UiInvoke(Action action);

		/// <summary>
		///     播放有票的声音提示
		/// </summary>
		void PlayTicketMusic();

		/// <summary>
		///     停止音乐
		/// </summary>
		void StopPlayTicketMusic(bool force = false);

		/// <summary>
		/// 显示弹出气泡通知
		/// </summary>
		/// <param name="timeout"></param>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <param name="icon"></param>
		void ShowBalloonTip(int timeout, string title, string content, ToolTipIcon icon);
	}
}
