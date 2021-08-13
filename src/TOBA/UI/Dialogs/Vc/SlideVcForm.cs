using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Vc
{
	using System.Threading.Tasks;
	using Controls.Vc;

	public partial class SlideVcForm : Form
	{
		private SlideVcControl _vcControl;
		private TaskCompletionSource<bool> _tcs;
		private Timer _timer;

		internal SlideVcForm(Session session, string token, string appId)
		{
			InitializeComponent();

			_tcs = new TaskCompletionSource<bool>();
			_vcControl = new SlideVcControl(session, token, appId)
			{
				Dock = DockStyle.Fill
			};
			Controls.Add(_vcControl);

			_vcControl.SlideOk += (sender, args) =>
			{
				DialogResult = DialogResult.OK;
				Close();
			};
			Closing += SlideVcForm_Closing;

			//60秒自动关闭
			_timer = new Timer();
			_timer.Interval = 60 * 1000;
			_timer.Tick += (sender, args) =>
			{
				_timer.Stop();
				Close();
			};
			_timer.Start();
		}

		private void SlideVcForm_Closing(object sender, CancelEventArgs e)
		{
			_tcs?.SetResult(DialogResult == DialogResult.OK);
			_tcs = null;
		}

		public Task<bool> UserOpTask => _tcs.Task;

		public string CfSessionId => _vcControl.CfSessionId;

		public string Sig => _vcControl.Sig;
	}
}
