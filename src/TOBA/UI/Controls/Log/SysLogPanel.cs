using System;

namespace TOBA.UI.Controls.Log
{
	using Common;

	internal partial class SysLogPanel : ControlBase
	{
		public SysLogPanel()
		{
			InitializeComponent();

			if (Program.IsRunning)
			{
				log.RegisteImage("info", Properties.Resources.info_16);
				log.RegisteImage("wait", Properties.Resources.clock_16);
				log.RegisteImage("warn", Properties.Resources.warning_16);
				log.RegisteImage("ok", Properties.Resources.tick_16);
				log.RegisteImage("search", Properties.Resources.search_16);

				TOBA.Events.Warning += Events_Warning; ;
				TOBA.Events.Message += Events_Message;
				TOBA.Events.Error += Events_Error;

				Disposed += SysLogPanel_Disposed;
			}
		}

		private void Events_Error(object sender, EventInfoArgs e)
		{
			AppContext.MainForm.UiInvoke(() => log.AddLogInfo("warn", RowStyleType.DarkRed, e.Message));
		}

		private void Events_Message(object sender, EventInfoArgs e)
		{
			AppContext.MainForm.UiInvoke(() => log.AddLogInfo("info", RowStyleType.RoyalBlue, e.Message));
		}

		private void Events_Warning(object sender, EventInfoArgs e)
		{
			AppContext.MainForm.UiInvoke(() => log.AddLogInfo("warn", RowStyleType.Red, e.Message));
		}

		private void SysLogPanel_Disposed(object sender, EventArgs e)
		{
			TOBA.Events.Warning -= Events_Warning; ;
			TOBA.Events.Message -= Events_Message;
			TOBA.Events.Error -= Events_Error;
		}
	}
}
