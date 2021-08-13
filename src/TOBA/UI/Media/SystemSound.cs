
namespace TOBA.UI.Media
{
	using Microsoft.Win32;

	using System;
	using System.IO;
	using System.Media;
	using System.Security;

	/// <summary>Represents a system sound. This class cannot be inherited.</summary>
	public sealed class SystemSound
	{
		static readonly string _mediaPath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\Media\");
		private string _name;

		/// <summary>Initializes a new instance of the <see cref="T:SystemSound" /> class.</summary>
		/// <param name="name">Name of system sound (i.e. '.Default', 'MenuCommand', 'WindowsLogo', etc...).</param>
		internal SystemSound(string name)
		{
			_name = name;
		}

		/// <summary>Plays the system sound.</summary>
		public void Play()
		{
			try
			{
				string soundPath = Registry.GetValue(@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\" + _name + @"\.Current", null, null) as string ?? string.Empty;
				if (!File.Exists(soundPath) && File.Exists(Path.Combine(_mediaPath, soundPath)))
				{
					soundPath = Path.Combine(_mediaPath, soundPath);
				}
				if (File.Exists(soundPath))
				{
					using (SoundPlayer player = new SoundPlayer(soundPath))
					{
						player.Play();
					}
				}
			}
			catch (IOException) { }
			catch (UriFormatException) { }
			catch (TimeoutException) { }
			catch (ArgumentException) { }
			catch (SecurityException) { }
			catch (InvalidOperationException) { }
		}
	}
}
