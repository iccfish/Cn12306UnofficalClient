using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA
{
	using System.Threading;
	using System.Threading.Tasks;

	class TaskExtensions
	{
		internal static async Task<T> RunTaskVie<T>(Func<CancellationToken, int, Task<T>> factory, int count)
		{
			var tcs = new TaskCompletionSource<T>();
			var cts = new CancellationTokenSource();

			var tasks = Enumerable.Range(0, count).Select(s => factory(cts.Token, s)).ToArray();

			void CheckTaskStatus()
			{
				if (tasks.All(s => s.Status == TaskStatus.Canceled || s.Status == TaskStatus.Faulted))
				{
					tcs.SetException(new Exception("网络错误"));
				}
			}

			foreach (var task in tasks)
			{
#pragma warning disable 4014
				task.ContinueWith(t =>
#pragma warning restore 4014
					{
						if (t.IsFaulted)
						{
							var dummy = t.Exception;

							CheckTaskStatus();
						}
						else if (t.IsCanceled)
						{
							//已取消，不管
							CheckTaskStatus();
						}
						else
						{
							tcs.TrySetResult(t.Result);

							//取消其它请求
							cts.Cancel();
						}
					},
					TaskContinuationOptions.ExecuteSynchronously);
				task.Start();
			}

			return await tcs.Task.ConfigureAwait(true);
		}
	}
}
