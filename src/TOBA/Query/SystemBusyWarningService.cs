namespace TOBA.Query
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Configuration;

	class SystemBusyWarningService : ISystemBusyWarningService
	{
		public event EventHandler SystemBusyWarning;

		private Queue<bool> _queue = new Queue<bool>();

		public void Enque(bool timeout)
		{
			if (QueryConfiguration.Current.QueryTimeout >= 15)
				return;
			lock (_queue)
			{
				_queue.Enqueue(timeout);
				while (_queue.Count > ApiConfiguration.Instance.SystemBusyRecordCount)
					_queue.Dequeue();

				if (_queue.Count(s => s) >= ApiConfiguration.Instance.SystemBusyWarningLimit)
				{
					OnSystemBusyWarning();
					_queue.Clear();
				}
			}


		}

		protected virtual void OnSystemBusyWarning()
		{
			QueryConfiguration.Current.QueryTimeout += 500;
			SystemBusyWarning?.Invoke(this, EventArgs.Empty);
		}
	}
}