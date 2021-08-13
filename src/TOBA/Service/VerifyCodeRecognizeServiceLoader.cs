using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TOBA.Service
{
	using AutoVc;

	internal class VerifyCodeRecognizeServiceLoader
	{
		static IVerifyCodeRecognizeService _verifyCodeRecognizeEngine;

		/// <summary>
		/// 获得当前启用的验证码识别引擎
		/// </summary>
		public static IVerifyCodeRecognizeService VerifyCodeRecognizeEngine
		{
			get { return _verifyCodeRecognizeEngine; }
			set
			{
				if (value == _verifyCodeRecognizeEngine)
					return;

				_verifyCodeRecognizeEngine?.Unload();
				_verifyCodeRecognizeEngine = value;
				_verifyCodeRecognizeEngine?.Load();
				OnOnVerifyCodeRecognizeEngineChanged();
				OnStateChanged();
				AutoVcConfig.Instance.ActiveVcEngine = value?.Id;
			}
		}

		public static event EventHandler OnVerifyCodeRecognizeEngineChanged;

		/// <summary>
		/// 状态发生变化
		/// </summary>
		public static event EventHandler StateChanged;

		public static void Init()
		{
			var cfg = AutoVcConfig.Instance;
			if (!cfg.ActiveVcEngine.IsNullOrEmpty())
			{
				VerifyCodeRecognizeEngine = AppContext.ExtensionManager.VerifyCodeRecogniseService.FirstOrDefault(s => s.Id == cfg.ActiveVcEngine);
			}
			Array.ForEach(AppContext.ExtensionManager.VerifyCodeRecogniseService, s =>
			{
				s.StateChanged += (x, y) =>
				{
					if (x != VerifyCodeRecognizeEngine)
						return;
					OnStateChanged();
				};
			});
			cfg.PropertyChanged += (x, y) =>
			{
				if (y.PropertyName == nameof(AutoVcConfig.ActiveVcEngine))
				{
					VerifyCodeRecognizeEngine = string.IsNullOrEmpty(AutoVcConfig.Instance.ActiveVcEngine) ? null : AppContext.ExtensionManager.VerifyCodeRecogniseService.FirstOrDefault(s => s.Id == AutoVcConfig.Instance.ActiveVcEngine);
				}
			};
		}

		static void OnOnVerifyCodeRecognizeEngineChanged()
		{
			OnVerifyCodeRecognizeEngineChanged?.Invoke(null, EventArgs.Empty);
		}

		static void OnStateChanged()
		{
			StateChanged?.Invoke(null, EventArgs.Empty);
		}
	}
}
