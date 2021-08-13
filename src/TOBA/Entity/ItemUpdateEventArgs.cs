namespace TOBA.Entity
{
	public class ItemUpdateEventArgs<T> : ItemChangeEventArgs<T>
	{

		public ItemUpdateEventArgs(int index, T original, T current, ItemAction action)
			: base(index, original, current)
		{
			Action = action;
		}

		public ItemAction Action { get; private set; }
	}
}