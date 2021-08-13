//#define ENABLE_LOG

using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.UI
{
	using System.Diagnostics;
	using System.Drawing;
	using System.Threading;
	using System.Windows.Forms;

	using TOBA.Configuration;

	using Timer = System.Windows.Forms.Timer;

	class FormPlacementManager
	{
		FSLib.Extension.AnimationAlgorithms.ITweenPositionAlgorithm _algorithm = new FSLib.Extension.AnimationAlgorithms.EaseFromToTweenPositionAlgorithm();

		List<Form> _formList = new List<Form>();
		private List<KeyValuePair<Form, TargetPosition>?> _formPosTarget = new List<KeyValuePair<Form, TargetPosition>?>();
		int _suspendLayout;
		private Timer _timer;

		public FormPlacementManager()
		{
			_timer = new Timer()
			{
				Interval = 10,
				Enabled = false
			};
			_timer.Tick += (s, e) =>
							{
								_timer.Stop();
								if (RunFormPositionAnimation())
									_timer.Start();
							};
		}

		[Conditional("ENABLE_LOG")]
		void Log(string message)
		{
			Events.OnMessage(this, new EventInfoArgs(message));
		}

		bool RunFormPositionAnimation()
		{
			lock (_lockObject)
			{
				if (!EnableAnimation)
				{
					foreach (var pair in _formPosTarget)
					{
						pair.Value.Key.DesktopLocation = pair.Value.Value.Point;
					}
					_formPosTarget.Clear();
					return false;
				}

				foreach (var pair in _formPosTarget.ToArray())
				{
					var form = pair.Value.Key;
					var data = pair.Value.Value;

					var position = _algorithm.GetPosition(Math.Min((DateTime.Now.Ticks - data.StartTicks) / 10000.0 / AnimationLength, 1.0));
					if (position > 0.99)
						position = 1.0;

					var newLoc = new Point(
						data.StartPoint.X + (int)((data.Point.X - data.StartPoint.X) * position),
						data.StartPoint.Y + (int)((data.Point.Y - data.StartPoint.Y) * position)
						);
					form.DesktopLocation = newLoc;
					Log($"Form[{form.Text}][{form.Handle}][{position}] {data} => {newLoc}");
					if (position > 0.99)
					{
						_formPosTarget.Remove(pair);
					}
				}

				return _formPosTarget.Count > 0;
			}
		}

		/// <summary>
		/// 加入控制
		/// </summary>
		/// <param name="form"></param>
		public void Control(Form form)
		{
			if (!UiConfiguration.Instance.AutoArrangeOrderDlg)
				return;

			lock (_lockObject)
			{
				if (_formList.Contains(form))
					return;

				Log($"[FPM] 新窗口加入；Text={form.Text}, Type={form.GetType().Name}");
				_formList.Add(form);
				form.SizeChanged += (s, e) =>
									{
										Log($"[FPM] 窗口大小变更；Text={form.Text}, Type={form.GetType().Name}");
										PerformLayout();
									};
				form.Closed += (s, e) =>
								{
									Log($"[FPM] 窗口关闭；Text={form.Text}, Type={form.GetType().Name}");
									RemoveControl((Form)s);
								};
				PerformLayout(false);
			}
		}

		public void PerformLayout(bool forceLayout = true)
		{
			if (IsLayoutSuspend)
				return;

			lock (_lockObject)
			{
				if (_formList.Count <= 0)
					return;

				Log($"[FPM] 开始布局；Force={forceLayout}, 窗体数量={_formList.Count}");


				var sizes = _formList.Select(s => s.Size).ToArray();
				//单个尺寸
				var unitSize = new Size(sizes.Max(s => s.Width), sizes.Max(s => s.Height));
				Log($"[FPM] 开始布局；尺寸单位={unitSize}");

				//计算起始位置
				var screen = Screen.FromControl(AppContext.HostForm);
				Log($"[FPM] 开始布局；Screen={screen.DeviceName}, Bounds={screen.Bounds}");
				Log($"[FPM] 开始布局；HostFormBounds={AppContext.HostForm.Bounds}, HostFormRestoreBounds={AppContext.HostForm.RestoreBounds}, HostFormDesktopBounds={AppContext.HostForm.DesktopBounds}, ");

				var startPoint = ProgramConfiguration.Instance.OrderDlgCenterMainform && AppContext.HostForm.DesktopLocation.X >= 0 && AppContext.HostForm.DesktopLocation.Y >= 0
					?
					AppContext.HostForm.DesktopLocation + new Size(AppContext.HostForm.Width / 2, AppContext.HostForm.Height / 2)
					:
					new Point(screen.WorkingArea.Width / 2, screen.WorkingArea.Height / 2)
					;
				Log($"[FPM] 开始布局；startPoint={startPoint}");
				//计算最适合的区域
				var dlgsPerLine = (int)Math.Ceiling(Math.Sqrt(_formList.Count));
				var rows = (int)Math.Ceiling(_formList.Count * 1.0 / dlgsPerLine);
				Log($"[FPM] 开始布局；dlgsPerLine={dlgsPerLine}, rows={rows}");

				//总布局区域
				var totalArea = new Size(dlgsPerLine * unitSize.Width, rows * unitSize.Height);
				Log($"[FPM] 开始布局；totalArea={totalArea}, dlgsPerLine={dlgsPerLine}, rows={rows}, startPoint={startPoint}");

				startPoint = startPoint + new Size(-totalArea.Width / 2, -totalArea.Height / 2);
				Log($"[FPM] 开始布局；layoutStartPoint={startPoint}");

				for (int j = 0; j < rows; j++)
				{
					for (int i = 0; i < dlgsPerLine; i++)
					{
						var formIndex = i + dlgsPerLine * j;
						if (formIndex >= _formList.Count)
							break;

						var form = _formList[formIndex];
						var point = startPoint + new Size(i * unitSize.Width + (unitSize.Width - form.Width) / 2, unitSize.Height * j + (unitSize.Height - form.Height) / 2);
						Log($"[FPM] 开始布局。窗体[{formIndex}]，类型 {form.GetType().Name}，目标位置 {point}");

						if (point != form.DesktopLocation)
						{
							if (EnableAnimation && (forceLayout || _formList.Count > 1))
							{
								var data = _formPosTarget.FirstOrDefault(s => s.Value.Key == form);
								if (data == null)
								{
									data = new KeyValuePair<Form, TargetPosition>(form, new TargetPosition());
									_formPosTarget.Add(data);
								}
								data.Value.Value.Point = point;
								data.Value.Value.StartPoint = form.DesktopLocation;
								data.Value.Value.StartTicks = DateTime.Now.Ticks;
							}
							else
							{
								form.DesktopLocation = point;
							}
						}
						else
						{
							//fix: 如果恰好有动画队列，则移除it
							for (int k = _formPosTarget.Count - 1; k >= 0; k--)
							{
								if (_formPosTarget[k].Value.Key == form)
								{
									_formPosTarget.RemoveAt(k);
									break;
								}
							}
						}
						form.BringToFront();
					}
				}
				if (_formPosTarget.Count > 0)
					_timer.Enabled = true;
			}
		}

		/// <summary>
		/// 移除控制
		/// </summary>
		/// <param name="form"></param>
		public void RemoveControl(Form form)
		{
			lock (_lockObject)
			{
				if (!_formList.Contains(form))
					return;

				_formList.Remove(form);

				foreach (var pair in _formPosTarget)
				{
					if (pair.Value.Key == form)
					{
						_formPosTarget.Remove(pair);
						break;
					}
				}

				PerformLayout();
			}
		}

		/// <summary>
		/// 通知恢复布局
		/// </summary>
		public void ResumeLayout()
		{
			Interlocked.Decrement(ref _suspendLayout);
			PerformLayout();
		}

		/// <summary>
		/// 通知挂起布局
		/// </summary>
		public void SuspendLayout()
		{
			Interlocked.Increment(ref _suspendLayout);
			PerformLayout();
		}

		public int AnimationLength => 200;

		/// <summary>
		/// 获得或设置是否允许动画
		/// </summary>
		public bool EnableAnimation => UiConfiguration.Instance.EnableAnimation;

		/// <summary>
		/// 获得当前是否已经挂起布局
		/// </summary>
		public bool IsLayoutSuspend => _suspendLayout > 0;
		class TargetPosition
		{
			/// <summary>
			/// 获得或设置目标尺寸
			/// </summary>
			public Point Point { get; set; }

			public Point StartPoint { get; set; }

			public long StartTicks { get; set; }

			/// <summary>
			/// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
			/// </summary>
			/// <returns>
			/// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
			/// </returns>
			public override string ToString()
			{
				return $"Point={Point}, StartPoint={StartPoint}, StartTicks={StartTicks}";
			}
		}


		#region 单例模式

		static FormPlacementManager _instance;
		static readonly object _lockObject = new object();

		/// <summary>
		/// 获得 <see cref="FormPlacementManager"/> 的单例对象
		/// </summary>
		public static FormPlacementManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new FormPlacementManager();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

	}
}
