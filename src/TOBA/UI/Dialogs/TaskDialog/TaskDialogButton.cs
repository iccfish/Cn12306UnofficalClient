#region License
// Copyright ?2013 £ukasz Œwi¹tkowski
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
	using System;

	/// <summary>
	/// Represents a button on TaskDialog form.
	/// </summary>
	/// <remarks></remarks>
	public class TaskDialogButton
	{
		#region Fields and Properties

		private bool _useCustomText;
		/// <summary>
		/// Determines whether the button should contain a custom text.
		/// </summary>
		public bool UseCustomText
		{
			get { return _useCustomText; }
			set { _useCustomText = value; }
		}

		private string _text;
		/// <summary>
		/// The custom text shown on the button.
		/// </summary>
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		private TaskDialogResult _result;
		/// <summary>
		/// Determines the value returned to the parent form when the button is clicked.
		/// </summary>
		public TaskDialogResult Result
		{
			get { return _result; }
			set { _result = value; }
		}

		private bool _showElevationIcon;
		/// <summary>
		/// Determines whether to show the elevation icon (shield).
		/// </summary>
		public bool ShowElevationIcon
		{
			get { return _showElevationIcon; }
			set { _showElevationIcon = value; }
		}

		private bool _isEnabled;
		/// <summary>
		/// Determines whether the button is enabled.
		/// </summary>
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the button is clicked, but before the TaskDialog form is closed.
		/// </summary>
		public event EventHandler Click;
		internal void RaiseClickEvent(object sender, EventArgs e)
		{
			if (Click != null)
			{
				Click(sender, e);
			}
		}
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="result">Determines the value returned to the parent form when the button is clicked.</param>
		public TaskDialogButton(TaskDialogResult result)
		{
			_result = result;
			_isEnabled = true;
		}

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="result">Determines the value returned to the parent form when the button is clicked.</param>
		/// <param name="showElevationIcon">Determines whether to show the elevation icon (shield).</param>
		public TaskDialogButton(TaskDialogResult result, bool showElevationIcon)
		{
			_result = result;
			_showElevationIcon = showElevationIcon;
			_isEnabled = true;
		}

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="text">The custom text shown on the button.</param>
		/// <param name="click">Occurs when the button is clicked, but before the TaskDialog form is closed.</param>
		public TaskDialogButton(string text, EventHandler click)
		{
			_useCustomText = true;
			_text = text;
			_result = TaskDialogResult.None;
			this.Click += click;
			_isEnabled = true;
		}

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="text">The custom text shown on the button.</param>
		/// <param name="click">Occurs when the button is clicked, but before the TaskDialog form is closed.</param>
		/// <param name="showElevationIcon">Determines whether to show the elevation icon (shield).</param>
		public TaskDialogButton(string text, EventHandler click, bool showElevationIcon)
		{
			_useCustomText = true;
			_text = text;
			_result = TaskDialogResult.None;
			this.Click += click;
			_showElevationIcon = showElevationIcon;
			_isEnabled = true;
		}

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="taskDialogResult">Determines the value returned to the parent form when the button is clicked.</param>
		/// <param name="text">The custom text shown on the button.</param>
		public TaskDialogButton(TaskDialogResult taskDialogResult, string text)
		{
			_useCustomText = true;
			_text = text;
			_result = taskDialogResult;
			_isEnabled = true;
		}

		/// <summary>
		/// Initializes the new instance of the TaskDialogButton.
		/// </summary>
		/// <param name="tresult">Determines the value returned to the parent form when the button is clicked.</param>
		/// <param name="text">The custom text shown on the button.</param>
		/// <param name="showElevationIcon">Determines whether to show the elevation icon (shield).</param>
		public TaskDialogButton(TaskDialogResult tresult, string text, bool showElevationIcon)
		{
			_useCustomText = true;
			_text = text;
			_result = tresult;
			_showElevationIcon = showElevationIcon;
			_isEnabled = true;
		}
		#endregion
	}
}