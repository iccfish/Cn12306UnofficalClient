using System;

using TOBA.Extension;

namespace TOBA
{
	using FSLib.Extension;

	using System.Diagnostics;
	using System.Windows.Forms;

	using TOBA.UI.Dialogs;

	/// <summary>
	/// 
	/// </summary>
	class AppContext
	{
		public static IExtensionManager ExtensionManager { get; internal set; }

		/// <summary>
		/// 获得或设置主窗口
		/// </summary>
		public static IMainForm MainForm { get; internal set; }

		/// <summary>
		/// 获得主窗口
		/// </summary>
		public static Form HostForm => MainForm as Form;

		public static FileVersionInfo ClientVersion { get; } = ApplicationRunTimeContext.GetProcessMainModule().FileVersionInfo;

		/// <summary>
		/// 分发事件
		/// </summary>
		/// <param name="action"></param>
		public static void DispatchEvents(Action action)
		{
			HostForm.Invoke(action);
		}

		/// <summary>
		/// 主要的程序界面已经初始化完成
		/// </summary>
		public static event EventHandler MainProgramLoaded;

		/// <summary>
		/// 引发 <see cref="MainProgramLoaded" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		internal static void OnMainProgramLoaded(object sender)
		{
			ExtensionManager.Extensions.ForEach(s => s.MainWindowInitialized());
			var handler = MainProgramLoaded;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}

	}
}
