using System.Collections.Generic;

namespace TOBA.UI.Dialogs
{
	using Configuration;

	using Controls.Option;
	using Controls.PlatformUI;

	using FSLib.Extension;

	internal partial class ConfigCenter : OptionConfigForm
	{
		public ConfigCenter()
		{
			InitializeComponent();

			var isPro = RunTime.IsInProfessonalMode;

			AddOption(new GenericOption());
			AddOption(new ThemeConfig());
			AddOption(new UIConfig());
			AddOption(new MediaOption());

			if (isPro)
			{
				AddOption(new QueryConfig());
				AddOption(new QueryView());
				AddOption(new SubmitOrderConfig());
				AddOption(new SubmitAutoResumeConfig());
			}
			AddOption(new NetworkConfig());
			AddOption(new FuncOption());
			AddOption(new MailConfig());

			if (isPro)
				AddOption(new WebNotificationConfig());
			//AddOption(new PromotionOption());
			AddOption(new VcConfig());

			var ce = new GeneralEventArgs<List<AbstractOptionConfigUI>>(new List<AbstractOptionConfigUI>());
			UiEvents.OnGenerateOptionsTabs(this, ce);
			ce.Data.ForEach(AddOption);

			cbMode.SelectedIndex = (int)ProgramConfiguration.Instance.Mode;
			cbMode.SelectedIndexChanged += (_1, _2) =>
			{
				var mode = (RunningMode)cbMode.SelectedIndex;
				ConfigurationPresets.Apply(mode);

				lblModeChanged.Visible = true;
			};
		}
	}
}
