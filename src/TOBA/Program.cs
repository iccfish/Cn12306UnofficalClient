using System.Linq;

namespace TOBA
{
	using Autofac;

	using Configuration;

	using Data;

	using DevComponents.DotNetBar;

	using Extension;

	using FSLib.Network.Http;

	using Media;

	using Platform;

	using Profile;

	using Service;

	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Net;
	using System.Reflection;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Windows.Forms;

	using UI.Controls.Option;
	using UI.Dialogs;
	using UI.Dialogs.Misc;
	using UI.Dialogs.Notification;

	using WebLib;

	using Workers;

	static class Program
	{
		#region 功能控制区

		public static bool EnableSellTip = true;

		public static bool EnableAutoVcLoading = true;

		#endregion

		internal static bool IsRunning = false;
		internal static string Version;
		internal static bool TraceEnabled;


		/// <summary>
		/// DPI-X
		/// </summary>
		internal static float DpiX { get; set; }

		/// <summary>
		/// DPI-Y
		/// </summary>
		internal static float DpiY { get; set; }

		internal static float ScaleX => DpiX / 96.0F;

		internal static float ScaleY => DpiY / 96.0F;

		internal static int GetScaledY(int current)
		{
			return (int)(current * ScaleY);
		}

		private static IntPtr _mainFormHandler;

		private static Mutex _mutex;

		private static string[] _cmd;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			IsRunning = true;
			_cmd = args;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//跟踪
			TraceEnabled = args.Any(s => s.IsIgnoreCaseEqualTo("/trace"));

			//系统调整
			Regex.CacheSize = 0x400;
			ServicePointManager.DefaultConnectionLimit = 2048;
			Version = Assembly.GetExecutingAssembly().GetVersion().ToString();

			if (TraceEnabled)
				EnableTrace();


			Trace.TraceInformation("订票助手.NET 版本 " + Assembly.GetEntryAssembly().GetFileVersionInfo().FileVersion);
			ResLoader.Init();

			//转移版本数据
			Trace.TraceInformation("正在执行版本升级任务");

			MainForm mainForm = null;

			//var dlgInit = Studio.GetStartupAboutForm(500);
			//var updateIfo = new Action<string>(_ => dlgInit.UpdateImageWithText(Color.White, _, SplashVersionLocation, SplashFont));
			var dlgInit = new Startup(Init);
			Events.SystemClosed += (s, e) => new SystemClosed().ShowDialog();
			Events.SystemSupportError += (s, e) => new SystemSupportError { Message = e.Message, Exception = e.Data as Exception }.ShowDialog();
			Trace.TraceInformation(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

			dlgInit.ShowDialog();


			if (dlgInit.Exception != null)
			{
				Trace.TraceError("未能完成初始化。错误信息如下。");
				Trace.TraceError(dlgInit.Exception.ToString());

				Events.OnSystemSupportError(null, new EventInfoArgs { Continue = false, Message = dlgInit.Exception.Message, Data = dlgInit.Exception });
				return;
			}
			else
			{
				mainForm = new MainForm();
				_mainFormHandler = mainForm.Handle;

				Operation.Instance.TicketPromptMusic = new TicketPromptMusic();
				Operation.Instance.TicketSuccessMusic = new TicketMusic4Success();
			}


			ToastNotification.ToastBackColor = Color.RoyalBlue;

			Application.Run(mainForm);

			//clear
			ProgramConfiguration.Instance.Save();
			RunTime.SessionManager.ForEach(s =>
			{
				s.UserProfile.QueryParams.PersistentQueryState();
				s.UserProfile.QueryParams.ToArray().ForEach(x =>
				{
					if (x.Resign)
					{
						x.IsLoaded = false;
						x.IsPersistent = false;
					}
				});
			});

			AppContext.ExtensionManager.Unload();
			Statistics.Current.Save();

			//unload all sessions.
			RunTime.SessionManager.UnloadAllSessions();

			//exit
			_mutex?.ReleaseMutex();
		}

		static void Init(IStartup startup)
		{

			//注册引导
			startup.InfoText = "正在初始化插件系统....";
			startup.DispatchUI(() =>
			{
				new ServiceManager().Load();
			});

			//初始化配置
			AppContext.ExtensionManager.ConfigurationProvider.Init(startup);

			//重置设置
			if (_cmd.Any(s => s == "/reset"))
			{
				AppContext.ExtensionManager.ConfigurationProvider.Reset();
			}

			Root.Init();

			if (ProgramConfiguration.Instance.CurrentVersion != Version)
			{
				startup.Update("正在升级...");
				//TODO: 版本升级初始化数据
				ProgramConfiguration.Instance.CurrentVersion = Version;
			}
			if (ProgramConfiguration.Instance.SubmitOrderBrowser != null && !ProgramConfiguration.Instance.SubmitOrderBrowser.IsAvailable)
				ProgramConfiguration.Instance.SubmitOrderBrowser = null;

			Statistics.Current.LastStartTime = DateTime.Now;
			Statistics.Current.StartupCount++;

			//初始化配置
			startup.Update("正在初始化配置信息....");
			NetworkEnvironment.Init(startup, () => startup.DispatchUI(new Action(RequireInitProxy)));

			//正在初始化相关信息
			new ResourceInitializer(startup).Run();

			//初始化服务
			startup.Update("正在初始化服务....");
			if (EnableAutoVcLoading)
				VerifyCodeRecognizeServiceLoader.Init();

			//更新HTTP配置
			var (success, message) = AppContext.ExtensionManager.GlobalKernel.Resolve<IWeb12306ConfProvider>().RefreshAsync().Result;
			if (!success)
			{
				throw new Exception("更新HTTP配置出错: " + message);
			}
			else
			{
				ParamData.Init();
			}

			//恢复会话
			startup.Update("正在恢复现场....");
			RunTime.SessionManager.TryRecoverSession(_ => startup.Update("正在尝试自动登录 " + _ + " ..."));

			//初始化资源
			startup.Update("初始化资源....");

			using (var g = startup.CreateGraphics())
			{
				DpiX = g.DpiX;
				DpiY = g.DpiY;
			}

		}
		static void RequireInitProxy()
		{
			MessageDialog.Information("阿门", "欢迎使用订票助手.NET。为了保证您能更快速更顺利地运行助手，现在需要咨询下阁下的网络设置。\n\n在这之后，您可以随时通过选项来修改设置。");

			var nc = NetworkConfiguration.Current;
			if (MessageDialog.Question("您是否可以直接访问12306？如果可以，那么将会设置成直接访问，速度最快，同时能享有服务器测速加速功能。\n\n一般家庭宽带用户均为『可以』，单位一般也『可以』，但是如果您的公司需要使用代理才能访问外网，请点击『否』。", true))
			{
				nc.ProxyType = 0;
			}
			else if (MessageDialog.Question("您是否有确定的代理服务器地址以访问12306，或您的代理服务器需要用户名和密码才可以访问？如果属于以上情况，请点击『是』。", true))
			{
				nc.ProxyType = 3;
				MessageDialog.Information("提示", "请继续在选项对话框中完成设置。");
				using (var od = new ConfigCenter())
				{
					od.SelectedConfig = od.FindConfigUI<NetworkConfig>().First();
					od.ShowDialog();
				}
			}
			else if (MessageDialog.Question("您的IE是否使用了PAC脚本？如果是的话，助手将无法为您的代理进行缓存，可能访问效果最慢。", true))
			{
				nc.ProxyType = 2;
			}
			else
			{
				nc.ProxyType = 1;
			}

			MessageDialog.Information("提示", "设置完毕，感谢您的耐心。亲，祝你回家顺利 :-)");
		}

		internal static void RestartInTraceMode()
		{
			Process.Start(new ProcessStartInfo(Assembly.GetEntryAssembly().Location, "/trace /new")
			{
				WindowStyle = ProcessWindowStyle.Normal
			});
			Environment.Exit(0);
		}

		public static bool IsTraceEnabled { get; private set; }
		public static TextWriterTraceListener LogListener { get; private set; }
		public static string LogFile { get; private set; }

		internal static void Restart()
		{
			Process.Start(Application.ExecutablePath, "/new");
			Application.Exit();
		}

		static void EnableTrace()
		{
			if (IsTraceEnabled)
				return;

			IsTraceEnabled = true;
			NetClient.EnableTrace();
			LogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "订票助手.NET 跟踪日志.txt");
			if (File.Exists(LogFile))
			{
				try
				{
					File.Delete(LogFile);
				}
				catch (Exception)
				{
					IsTraceEnabled = false;
					return;
				}
			}
			LogListener = new TextWriterTraceListener(LogFile);

			Trace.Listeners.Add(LogListener);

			if (Environment.GetCommandLineArgs().Contains("/TraceNetwork", StringComparer.OrdinalIgnoreCase))
				TraceHelper.EnableNetworkTrace(LogListener);
		}
	}
}