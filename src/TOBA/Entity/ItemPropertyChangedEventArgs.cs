namespace TOBA.Entity
{
	using System.ComponentModel;

	public class ItemPropertyChangedEventArgs<T> : ItemEventArgs<T>
	{

		public ItemPropertyChangedEventArgs(int index, T item, PropertyChangedEventArgs routedEventArgs)
			: base(index, item)
		{
			RoutedEventArgs = routedEventArgs;
		}

		public PropertyChangedEventArgs RoutedEventArgs { get; private set; }
	}
}