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
	/// <summary>
	/// Specifies identifiers to indicate the return value of a TaskDialog.
	/// </summary>
	public enum TaskDialogResult
	{
		/// <summary>
		/// Nothing is returned from the TaskDialog. This means that the modal dialog continues running.
		/// </summary>
		None = 0,
		/// <summary>
		/// The TaskDialog return value is OK (usually sent from a button labeled OK).
		/// </summary>
		OK = 1,
		/// <summary>
		/// The TaskDialog return value is Cancel (usually sent from a button labeled Cancel).
		/// </summary>
		Cancel = 2,
		/// <summary>
		/// The TaskDialog return value is Close (usually sent from a button labeled Close).
		/// </summary>
		Close = 3,
		/// <summary>
		/// The TaskDialog return value is Yes (usually sent from a button labeled Yes).
		/// </summary>
		Yes = 4,
		/// <summary>
		/// The TaskDialog return value is No (usually sent from a button labeled No).
		/// </summary>
		No = 5,
		/// <summary>
		/// The TaskDialog return value is YesToAll (usually sent from a button labeled Yes to All).
		/// </summary>
		YesToAll = 6,
		/// <summary>
		/// The TaskDialog return value is NoToAll (usually sent from a button labeled No to All).
		/// </summary>
		NoToAll = 7,
		/// <summary>
		/// The TaskDialog return value is Abort (usually sent from a button labeled Abort).
		/// </summary>
		Abort = 8,
		/// <summary>
		/// The TaskDialog return value is Retry (usually sent from a button labeled Retry).
		/// </summary>
		Retry = 9,
		/// <summary>
		/// The TaskDialog return value is Ignore (usually sent from a button labeled Ignore).
		/// </summary>
		Ignore = 10,
		/// <summary>
		/// The TaskDialog return value is Continue (usually sent from a button labeled Continue).
		/// </summary>
		Continue = 11
	}
}