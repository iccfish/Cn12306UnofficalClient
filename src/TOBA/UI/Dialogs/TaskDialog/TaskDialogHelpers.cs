#region License
// Copyright © 2013 Łukasz Świątkowski
// http://www.lukesw.net/
//
// This library is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace TOBA.UI.Dialogs.TaskDialog
{
	using System.Windows.Forms;

	/// <summary>
	/// Helper methods…
	/// </summary>
	internal class TaskDialogHelpers
	{
		public static string GetButtonName(TaskDialogResult button)
		{
			switch (button)
			{
				case TaskDialogResult.Abort:
					return "中止";

				case TaskDialogResult.Cancel:
					return "取消";

				case TaskDialogResult.Close:
					return "关闭";

				case TaskDialogResult.Continue:
					return "继续";

				case TaskDialogResult.Ignore:
					return "忽略";

				case TaskDialogResult.No:
					return "否";

				case TaskDialogResult.NoToAll:
					return "全部否";

				case TaskDialogResult.OK:
					return "确定";

				case TaskDialogResult.Retry:
					return "重试";

				case TaskDialogResult.Yes:
					return "是";

				case TaskDialogResult.YesToAll:
					return "全部是";

				default:
					return "无";
			}
		}

		public static DialogResult MakeDialogResult(TaskDialogResult Result)
		{
			switch (Result)
			{
				case TaskDialogResult.Abort:
					return DialogResult.Abort;
				case TaskDialogResult.Cancel:
				case TaskDialogResult.Close:
					return DialogResult.Cancel;
				case TaskDialogResult.Ignore:
					return DialogResult.Ignore;
				case TaskDialogResult.No:
				case TaskDialogResult.NoToAll:
					return DialogResult.No;
				case TaskDialogResult.OK:
				case TaskDialogResult.Continue:
					return DialogResult.OK;
				case TaskDialogResult.Retry:
					return DialogResult.Retry;
				case TaskDialogResult.Yes:
				case TaskDialogResult.YesToAll:
					return DialogResult.Yes;
				default:
					return DialogResult.None;
			}
		}

		public static TaskDialogDefaultButton MakeTaskDialogDefaultButton(MessageBoxDefaultButton DefaultButton)
		{
			switch (DefaultButton)
			{
				case MessageBoxDefaultButton.Button1:
					return TaskDialogDefaultButton.Button1;
				case MessageBoxDefaultButton.Button2:
					return TaskDialogDefaultButton.Button2;
				case MessageBoxDefaultButton.Button3:
					return TaskDialogDefaultButton.Button3;
				default:
					return TaskDialogDefaultButton.None;
			}
		}
	}
}
