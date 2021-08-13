using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using TOBA.Configuration;

using Timer = System.Windows.Forms.Timer;

namespace TOBA.UI.Controls.Misc
{
	class MemoryMonitoringLabel : ToolStripStatusLabel
	{
		PerformanceCounter _prefCounter;
		Timer _timer;

		public MemoryMonitoringLabel()
			: base("内存监控初始化中...", Properties.Resources.HardwareChip)
		{
			_timer = new Timer()
			{
				Interval = 1000
			};
			_timer.Tick += _timer_Tick;
			Click += MemoryMonitoringLabel_Click;
			ThreadPool.QueueUserWorkItem(_ => Initialize());
		}

		void Initialize()
		{
			try
			{
				_prefCounter = new PerformanceCounter("Process", Environment.OSVersion.Version.Major >= 6 ? "Working Set - Private" : "Working Set", GetNameToUseForMemory(Process.GetCurrentProcess()));
				Parent.FindForm().BeginInvoke(new Action(() =>
				{
					_timer.Start();
				}));
			}
			catch (Exception ex)
			{
				Parent.FindForm().BeginInvoke(new Action(() =>
				{
					ForeColor = Color.Red;
					Text = "内存监控出错";
					ToolTipText = "无法检索订票助手内存使用。\n错误信息：" + ex.Message + "\nWin10或以上系统暂不支持内存监控，对订票没有影响。";

					//不支持直接隐藏
					Visible = false;
				}));
			}
		}

		void MemoryMonitoringLabel_Click(object sender, EventArgs e)
		{
			Shell.StartUrl("https://www.fishlee.net/about/");
		}

		void _timer_Tick(object sender, EventArgs e)
		{
			try
			{
				var workset = _prefCounter.RawValue;
				this.Text = workset.ToSizeDescription(2);

				ForeColor = workset < ProgramConfiguration.WraningWorksetSize ? Color.Green : Color.Red;

				if (workset >= ProgramConfiguration.WraningWorksetSize)
				{
					ToolTipText = "订票助手内存占用过大，可能有异常情况。";
				}
				else
				{
					ToolTipText = "订票助手内存占用正常。";
				}
			}
			catch (Exception ex)
			{
				if (_prefCounter != null)
					_prefCounter.Dispose();

				ThreadPool.QueueUserWorkItem(_ => Initialize());
			}
		}


		private static string GetNameToUseForMemory(Process proc)
		{
			var nameToUseForMemory = String.Empty;
			var category = new PerformanceCounterCategory("Process");
			var instanceNames = category.GetInstanceNames().Where(x => x.Contains(proc.ProcessName));
			foreach (var instanceName in instanceNames)
			{
				using (var performanceCounter = new PerformanceCounter("Process", "ID Process", instanceName, true))
				{
					if (performanceCounter.RawValue != proc.Id)
						continue;
					nameToUseForMemory = instanceName;
					break;
				}
			}
			return nameToUseForMemory;
		}
	}
}
