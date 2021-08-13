using System;

namespace TOBA.BackupOrder
{
	using Entity;

	using Query.Entity;

	using TOBA.Entity;

	class BackupCartItem
	{
		public QueryResultItem Train { get; set; }

		public char Seat { get; set; }

		public QueryParam Query { get => Train.QueryParam; }

		public string SuccessRateInfoMessage { get; private set; }

		public int SuccessLevel
		{
			get;
			private set;
		}

		public string ToSecretData() => $"{Train.SubmitOrderInfo}#{Seat}";

		public void SetSuccessRate(int level, string message)
		{
			if (level == SuccessLevel && message == SuccessRateInfoMessage)
				return;

			SuccessLevel = level;
			SuccessRateInfoMessage = message;
			OnSuccessRateChanged();
		}

		public event EventHandler SuccessRateChanged;

		protected virtual void OnSuccessRateChanged() { SuccessRateChanged?.Invoke(this, EventArgs.Empty); }
	}
}
