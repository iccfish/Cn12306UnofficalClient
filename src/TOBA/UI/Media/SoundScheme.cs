
namespace TOBA.UI.Media
{
	using System;

	/// <summary>Retrieves sounds associated with current Windows sound scheme. This class cannot be inherited.</summary>
	public static partial class SoundScheme
	{
		private static SystemSound _default = new SystemSound(".Default");
		/// <summary>Gets the sound associated with the Default program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound Default
		{
			get { return _default; }
		}

		private static SystemSound _appGPFault = new SystemSound("AppGPFault");
		/// <summary>Gets the sound associated with the AppGPFault program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound AppGPFault
		{
			get { return _appGPFault; }
		}

		private static SystemSound _CCSelect = new SystemSound("CCSelect");
		/// <summary>Gets the sound associated with the CCSelect program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound CCSelect
		{
			get { return _CCSelect; }
		}

		private static SystemSound _changeTheme = new SystemSound("ChangeTheme");
		/// <summary>Gets the sound associated with the ChangeTheme program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound ChangeTheme
		{
			get { return _changeTheme; }
		}

		private static SystemSound _close = new SystemSound("Close");
		/// <summary>Gets the sound associated with the Close program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound Close
		{
			get { return _close; }
		}

		private static SystemSound _criticalBatteryAlarm = new SystemSound("CriticalBatteryAlarm");
		/// <summary>Gets the sound associated with the CriticalBatteryAlarm program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound CriticalBatteryAlarm
		{
			get { return _criticalBatteryAlarm; }
		}

		private static SystemSound _deviceConnect = new SystemSound("DeviceConnect");
		/// <summary>Gets the sound associated with the DeviceConnect program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound DeviceConnect
		{
			get { return _deviceConnect; }
		}

		private static SystemSound _deviceDisconnect = new SystemSound("DeviceDisconnect");
		/// <summary>Gets the sound associated with the DeviceDisconnect program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound DeviceDisconnect
		{
			get { return _deviceDisconnect; }
		}

		private static SystemSound _deviceFail = new SystemSound("DeviceFail");
		/// <summary>Gets the sound associated with the DeviceFail program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound DeviceFail
		{
			get { return _deviceFail; }
		}

		private static SystemSound _faxBeep = new SystemSound("FaxBeep");
		/// <summary>Gets the sound associated with the FaxBeep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound FaxBeep
		{
			get { return _faxBeep; }
		}

		private static SystemSound _lowBatteryAlarm = new SystemSound("LowBatteryAlarm");
		/// <summary>Gets the sound associated with the LowBatteryAlarm program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound LowBatteryAlarm
		{
			get { return _lowBatteryAlarm; }
		}

		private static SystemSound _mailBeep = new SystemSound("MailBeep");
		/// <summary>Gets the sound associated with the MailBeep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound MailBeep
		{
			get { return _mailBeep; }
		}

		private static SystemSound _maximize = new SystemSound("Maximize");
		/// <summary>Gets the sound associated with the Maximize program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound Maximize
		{
			get { return _maximize; }
		}

		private static SystemSound _menuCommand = new SystemSound("MenuCommand");
		/// <summary>Gets the sound associated with the MenuCommand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound MenuCommand
		{
			get { return _menuCommand; }
		}

		private static SystemSound _menuPopup = new SystemSound("MenuPopup");
		/// <summary>Gets the sound associated with the MenuPopup program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound MenuPopup
		{
			get { return _menuPopup; }
		}

		private static SystemSound _minimize = new SystemSound("Minimize");
		/// <summary>Gets the sound associated with the Minimize program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound Minimize
		{
			get { return _minimize; }
		}

		private static SystemSound _open = new SystemSound("Open");
		/// <summary>Gets the sound associated with the Open program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound Open
		{
			get { return _open; }
		}

		private static SystemSound _printComplete = new SystemSound("PrintComplete");
		/// <summary>Gets the sound associated with the PrintComplete program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound PrintComplete
		{
			get { return _printComplete; }
		}

		private static SystemSound _restoreDown = new SystemSound("RestoreDown");
		/// <summary>Gets the sound associated with the RestoreDown program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound RestoreDown
		{
			get { return _restoreDown; }
		}

		private static SystemSound _restoreUp = new SystemSound("RestoreUp");
		/// <summary>Gets the sound associated with the RestoreUp program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound RestoreUp
		{
			get { return _restoreUp; }
		}

		private static SystemSound _showBand = new SystemSound("ShowBand");
		/// <summary>Gets the sound associated with the ShowBand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound ShowBand
		{
			get { return _showBand; }
		}

		private static SystemSound _systemAsterisk = new SystemSound("SystemAsterisk");
		/// <summary>Gets the sound associated with the SystemAsterisk program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemAsterisk
		{
			get { return _systemAsterisk; }
		}

		private static SystemSound _systemExclamation = new SystemSound("SystemExclamation");
		/// <summary>Gets the sound associated with the SystemExclamation program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemExclamation
		{
			get { return _systemExclamation; }
		}

		private static SystemSound _systemExit = new SystemSound("SystemExit");
		/// <summary>Gets the sound associated with the SystemExit program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemExit
		{
			get { return _systemExit; }
		}

		private static SystemSound _systemHand = new SystemSound("SystemHand");
		/// <summary>Gets the sound associated with the SystemHand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemHand
		{
			get { return _systemHand; }
		}

		private static SystemSound _systemNotification = new SystemSound("SystemNotification");
		/// <summary>Gets the sound associated with the SystemNotification program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemNotification
		{
			get { return _systemNotification; }
		}

		private static SystemSound _systemQuestion = new SystemSound("SystemQuestion");
		/// <summary>Gets the sound associated with the SystemQuestion program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemQuestion
		{
			get { return _systemQuestion; }
		}

		private static SystemSound _systemStart = new SystemSound("SystemStart");
		/// <summary>Gets the sound associated with the SystemStart program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound SystemStart
		{
			get { return _systemStart; }
		}

		private static SystemSound _windowsLogoff = new SystemSound("WindowsLogoff");
		/// <summary>Gets the sound associated with the WindowsLogoff program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound WindowsLogoff
		{
			get { return _windowsLogoff; }
		}

		private static SystemSound _windowsLogon = new SystemSound("WindowsLogon");
		/// <summary>Gets the sound associated with the WindowsLogon program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound WindowsLogon
		{
			get { return _windowsLogon; }
		}

		private static SystemSound _windowsUAC = new SystemSound("WindowsUAC");
		/// <summary>Gets the sound associated with the WindowsUAC program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:SystemSound" />.</returns>
		public static SystemSound WindowsUAC
		{
			get { return _windowsUAC; }
		}

		#region Common Sounds

		public static SystemSound Error
		{
			get { return SystemHand; }
		}

		public static SystemSound Information
		{
			get { return SystemAsterisk; }
		}

		public static SystemSound Question
		{
			get { return SystemQuestion; }
		}

		public static SystemSound Security
		{
			get
			{
				return Environment.OSVersion.Version.Major >= 6
					   ? WindowsUAC
					   : SystemHand;
			}
		}

		public static SystemSound Warning
		{
			get { return SystemExclamation; }
		}

		#endregion
	}
}
