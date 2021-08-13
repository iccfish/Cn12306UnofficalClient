namespace TOBA.Entity
{
	using System;

	public class ItemEventArgs<T> : EventArgs
	{
		public ItemEventArgs(int index, T item)
		{
			Index = index;
			Item = item;
		}

		public int Index { get; private set; }

		public T Item { get; private set; }
	}
}