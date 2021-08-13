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
	using System.Drawing;

	/// <summary>
	/// Specifies constants defining which icon to display on what background.
	/// </summary>
	public enum TaskDialogIcon
	{
		/// <summary>
		/// The TaskDialog contains no icon. The background is white.
		/// </summary>
		None,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of a lowercase letter i in a circle. The background is white.
		/// </summary>
		Information,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of a question mark in a circle. The background is white.
		/// </summary>
		Question,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of an exclamation point in a yellow triangle. The background is white.
		/// </summary>
		Warning,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of white X in a red circle. The background is white.
		/// </summary>
		Error,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of white check mark in a green shield. The background is green.
		/// </summary>
		SecuritySuccess,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of a question mark in a blue shield. The background is blue.
		/// </summary>
		SecurityQuestion,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of an exclamation point in a yellow shield. The background is yellow.
		/// </summary>
		SecurityWarning,
		/// <summary>
		/// The TaskDialog contains a symbol consisting of white X in a red shield. The background is red.
		/// </summary>
		SecurityError,
		/// <summary>
		/// The TaskDialog contains a symbol of a multicolored shield. The background is white.
		/// </summary>
		SecurityShield,
		/// <summary>
		/// The TaskDialog contains a symbol of a multicolored shield. The background is blue-to-green gradient.
		/// </summary>
		SecurityShieldBlue,
		/// <summary>
		/// The TaskDialog contains a symbol of a multicolored shield. The background is gray.
		/// </summary>
		SecurityShieldGray
	}

	public class TaskDialogBigIcon
	{
		public static readonly Image Question = Properties.Resources.BigQuestion;
		public static readonly Image Information = Properties.Resources.BigInformation;
		public static readonly Image Warning = Properties.Resources.BigWarning;
		public static readonly Image Error = Properties.Resources.BigError;
		public static readonly Image Security = Properties.Resources.BigSecurity;
		public static readonly Image SecuritySuccess = Properties.Resources.BigSecuritySuccess;
		public static readonly Image SecurityQuestion = Properties.Resources.BigSecurityQuestion;
		public static readonly Image SecurityWarning = Properties.Resources.BigSecurityWarning;
		public static readonly Image SecurityError = Properties.Resources.BigSecurityError;
	}

	public class TaskDialogMediumIcon
	{
		public static readonly Image Question = Properties.Resources.Question;
		public static readonly Image Information = Properties.Resources.Information;
		public static readonly Image Warning = Properties.Resources.cou_32_warning;
		public static readonly Image Error = Properties.Resources.Error;
		public static readonly Image Security = Properties.Resources.Security;
		public static readonly Image SecuritySuccess = Properties.Resources.SecuritySuccess;
		public static readonly Image SecurityQuestion = Properties.Resources.SecurityQuestion;
		public static readonly Image SecurityWarning = Properties.Resources.SecurityWarning;
		public static readonly Image SecurityError = Properties.Resources.SecurityError;
	}

	public class TaskDialogSmallIcon
	{
		public static readonly Image Question = Properties.Resources.SmallQuestion;
		public static readonly Image Information = Properties.Resources.SmallInformation;
		public static readonly Image Warning = Properties.Resources.SmallWarning;
		public static readonly Image Error = Properties.Resources.SmallError;
		public static readonly Image Security = Properties.Resources.SmallSecurity;
		public static readonly Image SecuritySuccess = Properties.Resources.SmallSecuritySucess;
		public static readonly Image SecurityQuestion = Properties.Resources.SmallSecurityQuestion;
		public static readonly Image SecurityWarning = Properties.Resources.SmallSecurityWarning;
		public static readonly Image SecurityError = Properties.Resources.SmallSecurityError;
	}

}