namespace TOBA.UI.Dialogs
{
	using System.Windows.Forms;

	public static class MessageDialog
	{
		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Information(string content)
		{
			Information("提示", content);
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Information(string content, params string[] arguments)
		{
			Information("提示", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Information(string title, string content, params string[] arguments)
		{
			Information(title, string.Format(content, arguments));
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Information(string title, string content)
		{
			TaskDialog.TaskDialog.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Information(this IWin32Window owner, string content)
		{
			Information(owner, "提示", content);
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Information(this IWin32Window owner, string content, params string[] arguments)
		{
			Information(owner, "提示", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Information(this IWin32Window owner, string title, string content, params string[] arguments)
		{
			Information(owner, title, string.Format(content, arguments));
		}

		/// <summary>
		/// 显示信息对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Information(this IWin32Window owner, string title, string content)
		{
			TaskDialog.TaskDialog.Show(owner, content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Error(string content)
		{
			Error("错误", content);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Error(string content, params string[] arguments)
		{
			Error("错误", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Error(string title, string content)
		{
			TaskDialog.TaskDialog.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Error(string title, string content, params string[] arguments)
		{
			Error(title, string.Format(content, arguments));
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Error(this IWin32Window owner, string content)
		{
			Error(owner, "错误", content);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Error(this IWin32Window owner, string content, params string[] arguments)
		{
			Error(owner, "错误", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Error(this IWin32Window owner, string title, string content)
		{
			TaskDialog.TaskDialog.Show(owner, content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// 显示错误对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Error(IWin32Window owner, string title, string content, params string[] arguments)
		{
			Error(owner, title, string.Format(content, arguments));
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string content)
		{
			Stop("错误", content);
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string content, params string[] arguments)
		{
			Stop("错误", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string title, string content, params string[] arguments)
		{
			Stop(title, string.Format(content, arguments));
		}


		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Stop(string title, string content)
		{
			TaskDialog.TaskDialog.Show(title, content, MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Stop(this IWin32Window owner, string content)
		{
			Stop(owner, "错误", content);
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="content">要显示的内容</param>
		public static void Stop(this IWin32Window owner, string content, params string[] arguments)
		{
			Stop(owner, "错误", string.Format(content, arguments));
		}

		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Stop(this IWin32Window owner, string title, string content, params string[] arguments)
		{
			Stop(owner, title, string.Format(content, arguments));
		}


		/// <summary>
		/// 显示停止对话框
		/// </summary>
		/// <param name="title">要显示的标题</param>
		/// <param name="content">要显示的内容</param>
		public static void Stop(this IWin32Window owner, string title, string content)
		{
			TaskDialog.TaskDialog.Show(owner, title, content, MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static bool Question(string content)
		{
			return Question(content, "确定", false);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static bool Question(string content, params string[] arguments)
		{
			return Question(string.Format(content, arguments), "确定", false);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(string content, bool isYesNo)
		{
			return Question(content, "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(string content, bool isYesNo, params string[] arguments)
		{
			return Question(string.Format(content, arguments), "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(string title, string content, bool isYesNo)
		{
			return TaskDialog.TaskDialog.Show(title, content, isYesNo ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == (isYesNo ? DialogResult.Yes : DialogResult.OK);
		}



		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string content)
		{
			return QuestionWithCancel("确定", content);
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string content, params object[] param)
		{
			return QuestionWithCancel("确定", string.Format(content, param));
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string title, string content)
		{
			return TaskDialog.TaskDialog.Show(content, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(string title, string content, params object[] param)
		{
			return QuestionWithCancel(title, string.Format(content, param));
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static bool Question(this IWin32Window owner, string content)
		{
			return Question(owner, content, "确定", false);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static bool Question(this IWin32Window owner, string content, params string[] arguments)
		{
			return Question(owner, string.Format(content, arguments), "确定", false);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(this IWin32Window owner, string content, bool isYesNo)
		{
			return Question(owner, content, "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(this IWin32Window owner, string content, bool isYesNo, params string[] arguments)
		{
			return Question(owner, string.Format(content, arguments), "确定", isYesNo);
		}

		/// <summary>
		/// 提示对话框
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <param name="isYesNo">提示内容，true是 “是/否”，false为“确定”、“取消”</param>
		/// <returns></returns>
		public static bool Question(this IWin32Window owner, string title, string content, bool isYesNo)
		{
			return TaskDialog.TaskDialog.Show(owner, title, content, isYesNo ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == (isYesNo ? DialogResult.Yes : DialogResult.OK);
		}



		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(this IWin32Window owner, string content)
		{
			return QuestionWithCancel(owner, "确定", content);
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(this IWin32Window owner, string content, params object[] param)
		{
			return QuestionWithCancel(owner, "确定", string.Format(content, param));
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(this IWin32Window owner, string title, string content)
		{
			return TaskDialog.TaskDialog.Show(owner, content, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		/// <summary>
		/// 提示对话框（带有取消）
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">提示内容</param>
		/// <returns></returns>
		public static DialogResult QuestionWithCancel(this IWin32Window owner, string title, string content, params object[] param)
		{
			return QuestionWithCancel(owner, title, string.Format(content, param));
		}


		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(string content)
		{
			return TaskDialog.TaskDialog.Show("重试", content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show("重试", string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(string title, string content)
		{
			return TaskDialog.TaskDialog.Show(title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title">显示的标题</param>
		/// <param name="content">提示的内容</param>
		/// <param name="param">如果提示的内容中有占位符,则使用此参数进行格式化</param>
		/// <returns></returns>
		public static bool RetryError(string title, string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show(title, string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(string content)
		{
			return TaskDialog.TaskDialog.Show("重试", content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}


		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(string title, string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show(title, string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(string title, string content)
		{
			return TaskDialog.TaskDialog.Show(title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(IWin32Window owner, string content)
		{
			return TaskDialog.TaskDialog.Show(owner, "重试", content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(this IWin32Window owner, string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show(owner, "重试", string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryError(IWin32Window owner, string title, string content)
		{
			return TaskDialog.TaskDialog.Show(owner, title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title">显示的标题</param>
		/// <param name="content">提示的内容</param>
		/// <param name="param">如果提示的内容中有占位符,则使用此参数进行格式化</param>
		/// <returns></returns>
		public static bool RetryError(IWin32Window owner, string title, string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show(owner, title, string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(IWin32Window owner, string content)
		{
			return TaskDialog.TaskDialog.Show(owner, "重试", content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}


		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(IWin32Window owner, string title, string content, params object[] param)
		{
			return TaskDialog.TaskDialog.Show(owner, title, string.Format(content, param), MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}

		/// <summary>
		/// 重试
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static bool RetryCommon(IWin32Window owner, string title, string content)
		{
			return TaskDialog.TaskDialog.Show(owner, title, content, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry;
		}
	}
}
