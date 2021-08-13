using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Configuration;

	using DevComponents.DotNetBar;

	using Dialogs;

	using Entity;

	using System.IO;
	using System.Threading.Tasks;

	using TOBA.BackupOrder;
	using TOBA.Media;

	internal partial class MediaOption : OptionConfigForm.AbstractOptionConfigUI
	{
		OpenFileDialog _musicFileDialog;
		private ITicketMusic _music;
		private ITicketMusic _ticketMusic;

		public MediaOption()
		{
			InitializeComponent();

			BigImage = Properties.Resources.sound;
			Image = Properties.Resources.sound_16;
			DisplayText = "音乐设置";

			Load += MediaOption_Load;
		}

		private void MediaOption_Load(object sender, EventArgs eex)
		{
			var c = ProgramConfiguration.Instance;
			var qc = Configuration.QueryConfiguration.Current;
			var mc = MediaConfiguration.Instance;

			_musicFileDialog = new OpenFileDialog()
			{
				Multiselect = false,
				Filter = "音乐文件(*.mp3;*.wma;*.wav;*.mid;*.midi)|*.mp3;*.wma;*.wav;*.mid;*.midi"
			};
			//刷到票
			txtMusicPath.Text = qc.MusicPath;
			txtMusicPath.TextChanged += (ss, ee) =>
			{
				var txt = txtMusicPath.Text;
				if (txt.IndexOf(Path.DirectorySeparatorChar.ToString()) == -1)
					txt = @"audio\music\" + txt + ".mp3";
				QueryConfiguration.Current.MusicPath = txt;
			};
			//订票成功
			cbSuccessMusicPath.Text = mc.TicketSuccessMusicFile;
			cbSuccessMusicPath.TextChanged += (ss, ee) =>
			{
				var txt = cbSuccessMusicPath.Text;
				if (txt.IndexOf(Path.DirectorySeparatorChar.ToString()) == -1)
					txt = @"audio\music\" + txt + ".mp3";
				mc.TicketSuccessMusicFile = txt;
			};


			var directory = ResLoader.GetPath(@"audio\music");
			txtMusicPath.Items.AddRange(Directory.GetFiles(directory, "*.mp3", SearchOption.AllDirectories).Select(s => (object)Path.GetFileNameWithoutExtension(s)).ToArray());
			cbSuccessMusicPath.Items.AddRange(Directory.GetFiles(directory, "*.mp3", SearchOption.AllDirectories).Select(s => (object)Path.GetFileNameWithoutExtension(s)).ToArray());

			qc.PropertyChanged += (ss, ee) =>
			{
				if (ee.PropertyName == "MusicPath")
				{
					txtMusicPath.Text = QueryConfiguration.Current.MusicPath;
					_music.Pause();
				}
			};
			mc.PropertyChanged += (ss, ee) =>
			{
				if (ee.PropertyName == nameof(MediaConfiguration.TicketSuccessMusicFile))
				{
					cbSuccessMusicPath.Text = mc.TicketSuccessMusicFile;
				}
			};
			chkAutoStop.AddDataBinding(mc, s => s.Checked, s => s.StopMusicIfUserOperated);

			btnResetDefault.Click += (ss, ee) =>
			{
				qc.MusicPath = "";
				_music.Pause();
			};
			btnBrowser.Click += (ss, ee) =>
			{
				if (_musicFileDialog.ShowDialog() != DialogResult.OK) return;
				qc.MusicPath = ResLoader.ToRelative(_musicFileDialog.FileName, ResourceLocation.Program);
			};
			btnBrowser4Success.Click += (ss, ee) =>
			{
				if (_musicFileDialog.ShowDialog() != DialogResult.OK) return;
				mc.TicketSuccessMusicFile = ResLoader.ToRelative(_musicFileDialog.FileName, ResourceLocation.Program);
			};
			btnBrowser.AddDataBinding(ProgramConfiguration.Instance, s => s.Enabled, s => s.EnableMusicPrompt);
			txtMusicPath.AddDataBinding(ProgramConfiguration.Instance, s => s.Enabled, s => s.EnableMusicPrompt);
			btnResetDefault.AddDataBinding(ProgramConfiguration.Instance, s => s.Enabled, s => s.EnableMusicPrompt);

			btnResetDefault4Succ.AddDataBinding(mc, s => s.Enabled, s => s.MusicOnSuccess);
			btnResetDefault4Succ.Click += (s, e) =>
			{
				mc.TicketSuccessMusicFile = "终于等到你";
				_ticketMusic.Pause();
			};

			chkEnableMusic.AddDataBinding(c, s => s.Checked, s => s.EnableMusicPrompt);
			chkMusicOnSuccess.AddDataBinding(mc, s => s.Checked, s => s.MusicOnSuccess);

			chkEnableSuggestTrain.AddDataBinding(mc, s => s.Checked, s => s.EnableSuggestTicketFoundPrompt);
			chkEnableForceLogout.AddDataBinding(mc, s => s.Checked, s => s.EnableForceLogoutAudioPrompt);

			_music = new TicketPromptMusic();
			_ticketMusic = new TicketMusic4Success();
			btnPlayLogoutSound.Click += async (s, x) =>
			{
				btnPlayLogoutSound.Enabled = false;
				await LosingSoundPlayer.Instance.PlayAsync().ConfigureAwait(true);
				btnPlayLogoutSound.Enabled = true;
			};
			btnPlayTicketMusic.Click += (s, x) => PlayTicketMusic(btnPlayTicketMusic, _music);
			btnPlayTicketSuccess.Click += (s, x) => PlayTicketMusic(btnPlayTicketSuccess, _ticketMusic);
			btnPlayOnHbSuccess.Click += (s, x) => PlayTicketMusic(btnPlayOnHbSuccess, HbOrderMusic.Instance);
			chkPlayOnHbSuccess.AddDataBinding(BackupOrderConfiguration.Instance, s => s.Checked, s => s.PlayMusicOnAutoSubmitSuccess);
			btnSuggestTrain.Click += async (s, x) =>
			{
				btnSuggestTrain.Enabled = false;
				await TipSoundPlayer.Instance.PlayAsync().ConfigureAwait(true);
				btnSuggestTrain.Enabled = true;
			};
		}

		async void PlayTicketMusic(ButtonX btn, ITicketMusic media)
		{
			if (media.IsPlaying)
			{
				btn.Image = Properties.Resources.stock_media_play;
				media.Pause();
				return;
			}

			btn.Image = Properties.Resources.stock_media_pause;
			media.Play();
			while (media.IsPlaying)
			{
				await Task.Delay(200).ConfigureAwait(true);
			}
			btn.Image = Properties.Resources.stock_media_play;
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			_music?.Pause();
			_ticketMusic?.Pause();
			return base.Save();
		}
	}
}
