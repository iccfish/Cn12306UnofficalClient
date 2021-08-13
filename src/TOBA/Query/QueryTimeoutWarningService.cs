namespace TOBA.Query
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Configuration;

	class QueryTimeoutWarningService : IQueryTimeoutWarningService
	{
		public event EventHandler TimeoutWarning;

		private Queue<bool> _queue = new Queue<bool>();

		public void Enque(bool timeout)
		{
			if (QueryConfiguration.Current.QueryTimeout >= 60)
				return;
			lock (_queue)
			{
				_queue.Enqueue(timeout);
				while (_queue.Count > ApiConfiguration.Instance.TimeoutRecordCount)
					_queue.Dequeue();

				if (_queue.Count(s => s) >= ApiConfiguration.Instance.TimeoutWarningLimit)
				{
					OnTimeoutWarning();
					_queue.Clear();
				}
			}


		}

		protected virtual void OnTimeoutWarning()
		{
			if (QueryConfiguration.Current.TimeoutAutoIncreaseSetting)
				QueryConfiguration.Current.QueryTimeout += 1000;

			TimeoutWarning?.Invoke(this, EventArgs.Empty);
		}
	}
}