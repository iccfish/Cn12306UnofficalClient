namespace TOBA.Entity
{
	using System.ComponentModel;

	public class ItemPropertyChangingEventArgs<T> : ItemEventArgs<T>
	{

		public ItemPropertyChangingEventArgs(int index, T item, PropertyChangingEventArgs routedEventArgs)
			: base(index, item)
		{
			RoutedEventArgs = routedEventArgs;
		}

		public PropertyChangingEventArgs RoutedEventArgs { get; private set; }
	}
}