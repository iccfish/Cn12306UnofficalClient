using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA
{
	using DevComponents.DotNetBar;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;

	using TOBA.Order.Entity;
	using TOBA.Otn.Entity;

	using UI.Dialogs.Common;

	static class ExtensionMethods
	{
		static Font _defaultFont = new Font("微软雅黑", 15.0F);
		static Font _defaultFont32 = new Font("微软雅黑", 10.0F);

		public static IntPtr ShowErrorToast(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		) => parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Abort, Color.Crimson, Color.White, null, Color.Crimson, font: font ?? _defaultFont, timeout: timeout);

		public static IntPtr ShowErrorToastMini(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		) => parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Abort_32, Color.Crimson, Color.White, null, Color.Crimson, font: font ?? _defaultFont32, timeout: timeout);

		public static IntPtr ShowInfoToast(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		) => parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Info, Color.RoyalBlue, Color.White, null, Color.RoyalBlue, font: font ?? _defaultFont, timeout: timeout);

		public static IntPtr ShowInfoToastMini(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		) => parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Info_32, Color.RoyalBlue, Color.White, null, Color.RoyalBlue, font: font ?? _defaultFont32, timeout: timeout);

		public static IntPtr ShowSuccessToast(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		)
		{
			return parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_OK, Color.ForestGreen, Color.White, null, Color.ForestGreen, font: font ?? _defaultFont, timeout: timeout);
		}

		public static IntPtr ShowSuccessToastMini(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		)
		{
			return parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_OK_32, Color.ForestGreen, Color.White, null, Color.ForestGreen, font: font ?? _defaultFont32, timeout: timeout);
		}

		public static IntPtr ShowWarningToast(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		)
		{
			return parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Problem, Color.FromArgb(212, 57, 0), Color.White, null, Color.FromArgb(212, 57, 0), font: _defaultFont, timeout: timeout);
		}

		public static IntPtr ShowWarningToastMini(this Control parent,
			string text,
			int timeout = 2500,
			eToastPosition position = eToastPosition.BottomCenter,
			int? x = null,
			int? y = null,
			Font font = null
		)
		{
			return parent.ShowToast(text, Assets.FreeWp8Icons_White.FreeWp8IconsWhite_Problem_32, Color.FromArgb(212, 57, 0), Color.White, null, Color.FromArgb(212, 57, 0), font: _defaultFont32, timeout: timeout);
		}

		public static IntPtr ShowToast(this Control parent,
										string text,
										Image image = null,
										Color? backColor = null,
										Color? foreColor = null,
										eToastGlowColor? glowColor = null,
										Color? customGlowColor = null,
										int timeout = 2500,
										eToastPosition position = eToastPosition.BottomCenter,
										int? x = null,
										int? y = null,
										Font font = null
			)
		{
			if (parent == null)
				return IntPtr.Zero;
			//store parameter
			var bakForColor = ToastNotification.ToastForeColor;
			var bakBackColor = ToastNotification.ToastBackColor;
			var bakFont = ToastNotification.ToastFont;

			if (foreColor != null)
				ToastNotification.ToastForeColor = foreColor.Value;
			if (backColor.HasValue)
				ToastNotification.ToastBackColor = backColor.Value;
			ToastNotification.ToastFont = font ?? _defaultFont;
			if (customGlowColor != null) ToastNotification.CustomGlowColor = customGlowColor.Value;

			var ptr = x == null || y == null ?
						ToastNotification.Show(parent, text, image, timeout, glowColor ?? (customGlowColor == null ? ToastNotification.DefaultToastGlowColor : eToastGlowColor.Custom), position)
						: ToastNotification.Show(parent, text, image, timeout, glowColor ?? (customGlowColor == null ? ToastNotification.DefaultToastGlowColor : eToastGlowColor.Custom), x.Value, y.Value);

			ToastNotification.ToastForeColor = bakForColor;
			ToastNotification.ToastBackColor = bakBackColor;
			ToastNotification.ToastFont = bakFont;

			return ptr;
		}

		/// <summary>
		/// 关闭Toast提示
		/// </summary>
		/// <param name="control"></param>
		/// <param name="intPtr"></param>
		public static void CloseToast(this Control control, IntPtr? intPtr = null)
		{
			ToastNotification.Close(control, intPtr ?? IntPtr.Zero);
		}

		#region Toast

		#endregion

		/// <summary>
		/// 判断指定的票是否可以改签
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static bool CanResign(this IEnumerable<OrderTicket> src)
		{
			var flag = src.Select(t => t.resign_flag).Distinct().ToArray();
			if (flag.Length > 1)
				return false;

			var flagSingle = flag[0];
			return flagSingle == "1" || flagSingle == "2" || flagSingle == "3";
		}

		public static ExecutionState ToExecutionState(this TOBA.OpearationState status)
		{
			switch (status)
			{
				case OpearationState.Running:
					return ExecutionState.Running;
				case OpearationState.Success:
					return ExecutionState.Ok;
				case OpearationState.Fail:
					return ExecutionState.Warning;
				case OpearationState.Wait:
					return ExecutionState.InterActive;
				case OpearationState.Blocked:
					return ExecutionState.Block;
				default:
					break;
			}

			return ExecutionState.InterActive;
		}

		public static string FormatSpeed(this int value)
		{
			if (value >= 1000)
			{
				return (value / 1000.0).ToString("#0.0") + "秒";
			}
			if (value >= 500)
			{
				return (value / 1000.0).ToString("#0.00") + "秒";
			}
			return value.ToString("D") + "毫秒";
		}

		static List<KeyValuePair<double, string>> _digitData = new List<KeyValuePair<double, string>>()
		{
			new KeyValuePair<double, string>(1000, "K"),
			new KeyValuePair<double, string>(10, "W"),
			new KeyValuePair<double, string>(1000, "KW")
		};

		public static string FormatNumber(this int value)
		{
			var index = 0;
			var appendix = "";
			var data = value * 1.0;

			while (index < _digitData.Count && data >= _digitData[index].Key)
			{
				data /= _digitData[index].Key;
				appendix = _digitData[index].Value;
				index++;
			}

			if (appendix.IsNullOrEmpty())
				return data.ToString("N0");

			return data.ToString("N1") + appendix;
		}

		#region AutoSubmitOrderReuqest

		public static bool CanChooseBeds(this AutoSubmitOrderResponse response) => response.Data.CanChooseBeds == "Y";

		public static bool CanChooseSeats(this AutoSubmitOrderResponse response) => response.Data.CanChooseSeats == "Y";

		public static bool IsCanChooseMid(this AutoSubmitOrderResponse response) => response.Data.IsCanChooseMid == "Y";

		public static void SaveToFile<T>(this T obj, string path)
		{
			File.WriteAllText(path, JsonConvert.SerializeObject(obj));
		}


		#endregion

		#region HttpClient

		/// <summary>
		/// 返回12306的错误信息
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string Get12306ErrorMessage<T>(this HttpContext<OtnWebResponse<T>> context, string defaultMsg = null)
		{
			if (!context.IsValid())
				return context.GetExceptionMessage("网络错误，请重试");
			if (context.Result == null)
				return "服务器返回数据无效，请重试";
			return context.Result.GetErrorMessages(defaultMsg);
		}


		/// <summary>
		/// 判断响应是否正常
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="response"></param>
		/// <returns></returns>
		public static bool IsValid<T>(this OtnWebResponse<T> response)
		{
			if (response?.Status != true)
			{
				return false;
			}

			if (response.Data is BaseOtnApiResponseWithFlagAndMsg tmp1 && tmp1?.Flag != true)
			{
				return false;
			}

			return true;
		}

		#endregion
	}
}
