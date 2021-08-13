namespace TOBA.Entity
{
	using System;

	public class ItemChangeEventArgs<T> : EventArgs
	{
		public ItemChangeEventArgs(int index, T original, T current)
		{
			Index = index;
			Original = original;
			Current = current;
		}

		public T Current { get; private set; }


		public int Index { get; private set; }

		public T Original { get; private set; }
	}
}