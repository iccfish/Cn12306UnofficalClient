namespace TOBA.Entity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class EventList<T> : IList<T>, IDisposable
	{
		List<T> _innerList;

		/// <summary>
		/// 新建个默认值对象
		/// </summary>
		public EventList()
		{
			_innerList = new List<T>();
		}

		/// <summary>
		/// 新建个默认值对象
		/// </summary>
		public EventList(int capacity)
		{
			_innerList = new List<T>(capacity);
		}

		public EventList(IEnumerable<T> source)
		{
			_innerList = source.ToList();
		}

		/// <summary>
		/// 当添加对象时触发
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> Added;

		/// <summary>
		/// 当列表被清空时触发
		/// </summary>
		public event EventHandler Cleared;

		/// <summary>
		/// 项已经变化时触发
		/// </summary>
		public event EventHandler<ItemUpdateEventArgs<T>> ItemChanged;

		/// <summary>
		/// 当对象更新时触发
		/// </summary>
		public event EventHandler<ItemPropertyChangedEventArgs<T>> ItemPropertyChanged;
		/// <summary>
		/// 当对象更新时触发
		/// </summary>
		public event EventHandler<ItemPropertyChangingEventArgs<T>> ItemPropertyChanging;

		/// <summary>
		/// 当移除对象时触发
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> Removed;

		/// <summary>
		/// 项已发生变化
		/// </summary>
		public event EventHandler<ItemChangeEventArgs<T>> Replaced;

		void EventList_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var index = _innerList.IndexOf((T)sender);
			OnItemPropertyChanged(new ItemPropertyChangedEventArgs<T>(index, (T)sender, e));
		}

		private void EventList_PropertyChanging(object sender, PropertyChangingEventArgs e)
		{
			var index = _innerList.IndexOf((T)sender);
			OnItemPropertyChanging(new ItemPropertyChangingEventArgs<T>(index, (T)sender, e));
		}

		/// <summary>
		/// 引发 <see cref="Added" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnAdded(ItemEventArgs<T> ea)
		{
			var item = ea.Item;
			if (item is INotifyPropertyChanged changed)
			{
				changed.PropertyChanged += EventList_PropertyChanged;
			}
			if (item is INotifyPropertyChanging changing)
			{
				changing.PropertyChanging += EventList_PropertyChanging;
			}

			var handler = Added;
			handler?.Invoke(this, ea);

			ItemChanged?.Invoke(this, new ItemUpdateEventArgs<T>(ea.Index, default(T), ea.Item, ItemAction.Add));
		}

		/// <summary>
		/// 引发 <see cref="Cleared" /> 事件
		/// </summary>
		protected virtual void OnCleared()
		{
			var handler = Cleared;
			handler?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnItemChanged(ItemUpdateEventArgs<T> e)
		{
			ItemChanged?.Invoke(this, e);
		}

		/// <summary>
		/// 引发 <see cref="ItemPropertyChanged" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnItemPropertyChanged(ItemPropertyChangedEventArgs<T> ea)
		{
			ItemPropertyChanged?.Invoke(this, ea);
		}

		protected virtual void OnItemPropertyChanging(ItemPropertyChangingEventArgs<T> e)
		{
			ItemPropertyChanging?.Invoke(this, e);
		}

		/// <summary>
		/// 引发 <see cref="Removed" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRemoved(ItemEventArgs<T> ea)
		{
			var item = ea.Item;
			if (item is INotifyPropertyChanged changed)
			{
				changed.PropertyChanged -= EventList_PropertyChanged;
			}
			if (item is INotifyPropertyChanging changing)
			{
				changing.PropertyChanging -= EventList_PropertyChanging;
			}

			var handler = Removed;
			handler?.Invoke(this, ea);
			ItemChanged?.Invoke(this, new ItemUpdateEventArgs<T>(ea.Index, ea.Item, default(T), ItemAction.Remove));
		}

		protected virtual void OnReplaced(ItemChangeEventArgs<T> e)
		{
			Replaced?.Invoke(this, e);
			ItemChanged?.Invoke(this, new ItemUpdateEventArgs<T>(e.Index, e.Original, e.Current, ItemAction.Replace));
		}


		#region Implementation of IEnumerable

		/// <summary>
		/// 返回一个循环访问集合的枚举器。
		/// </summary>
		/// <returns>
		/// 可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1"/>。
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return _innerList.GetEnumerator();
		}

		/// <summary>
		/// 返回一个循环访问集合的枚举器。
		/// </summary>
		/// <returns>
		/// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of ICollection<T>

		/// <summary>
		/// 将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 中。
		/// </summary>
		/// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
		public void Add(T item)
		{
			if (item == null)
				return;

			_innerList.Add(item);
			OnAdded(new ItemEventArgs<T>(_innerList.Count - 1, item));
		}

		/// <summary>
		/// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除所有项。
		/// </summary>
		/// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
		public void Clear()
		{
			var array = _innerList.ToArray();
			_innerList.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				array.ForEach(s => OnRemoved(new ItemEventArgs<T>(i, array[i])));
			}

			OnCleared();
		}

		/// <summary>
		/// 确定 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否包含特定值。
		/// </summary>
		/// <returns>
		/// 如果在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中找到 <paramref name="item"/>，则为 true；否则为 false。
		/// </returns>
		/// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中定位的对象。</param>
		public bool Contains(T item)
		{
			return _innerList.Contains(item);
		}

		/// <summary>
		/// 从特定的 <see cref="T:System.Array"/> 索引处开始，将 <see cref="T:System.Collections.Generic.ICollection`1"/> 的元素复制到一个 <see cref="T:System.Array"/> 中。
		/// </summary>
		/// <param name="array">作为从 <see cref="T:System.Collections.Generic.ICollection`1"/> 复制的元素的目标位置的一维 <see cref="T:System.Array"/>。<see cref="T:System.Array"/> 必须具有从零开始的索引。</param><param name="arrayIndex"><paramref name="array"/> 中从零开始的索引，将在此处开始复制。</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> 为 null。</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> 小于 0。</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> 是多维数组。- 或 -源 <see cref="T:System.Collections.Generic.ICollection`1"/> 中的元素数大于从 <paramref name="arrayIndex"/> 到目标 <paramref name="array"/> 结尾处之间的可用空间。- 或 -无法自动将类型 <paramref name="T"/> 强制转换为目标 <paramref name="array"/> 的类型。</exception>
		public void CopyTo(T[] array, int arrayIndex)
		{
			_innerList.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除特定对象的第一个匹配项。
		/// </summary>
		/// <returns>
		/// 如果已从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中成功移除 <paramref name="item"/>，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1"/> 中没有找到 <paramref name="item"/>，该方法也会返回 false。
		/// </returns>
		/// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
		public bool Remove(T item)
		{
			var index = _innerList.IndexOf(item);
			if (index > -1)
			{
				_innerList.RemoveAt(index);
				OnRemoved(new ItemEventArgs<T>(index, item));
				return true;
			}
			return false;
		}

		/// <summary>
		/// 获取 <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
		/// </summary>
		/// <returns>
		/// <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
		/// </returns>
		public int Count { get { return _innerList.Count; } }

		/// <summary>
		/// 获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否为只读。
		/// </summary>
		/// <returns>
		/// 如果 <see cref="T:System.Collections.Generic.ICollection`1"/> 为只读，则为 true；否则为 false。
		/// </returns>
		public bool IsReadOnly
		{
			get { return false; }
		}

		#endregion

		#region Implementation of IList<T>

		/// <summary>
		/// 确定 <see cref="T:System.Collections.Generic.IList`1"/> 中特定项的索引。
		/// </summary>
		/// <returns>
		/// 如果在列表中找到，则为 <paramref name="item"/> 的索引；否则为 -1。
		/// </returns>
		/// <param name="item">要在 <see cref="T:System.Collections.Generic.IList`1"/> 中定位的对象。</param>
		public int IndexOf(T item)
		{
			return _innerList.IndexOf(item);
		}

		/// <summary>
		/// 将一个项插入指定索引处的 <see cref="T:System.Collections.Generic.IList`1"/>。
		/// </summary>
		/// <param name="index">从零开始的索引，应在该位置插入 <paramref name="item"/>。</param><param name="item">要插入到 <see cref="T:System.Collections.Generic.IList`1"/> 中的对象。</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> 不是 <see cref="T:System.Collections.Generic.IList`1"/> 中的有效索引。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IList`1"/> 为只读。</exception>
		public void Insert(int index, T item)
		{
			_innerList.Insert(index, item);
			OnAdded(new ItemEventArgs<T>(index, item));
		}

		/// <summary>
		/// 移除指定索引处的 <see cref="T:System.Collections.Generic.IList`1"/> 项。
		/// </summary>
		/// <param name="index">从零开始的索引（属于要移除的项）。</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> 不是 <see cref="T:System.Collections.Generic.IList`1"/> 中的有效索引。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IList`1"/> 为只读。</exception>
		public void RemoveAt(int index)
		{
			var item = _innerList[index];
			_innerList.RemoveAt(index);
			OnRemoved(new ItemEventArgs<T>(index, item));
		}

		/// <summary>
		/// 获取或设置指定索引处的元素。
		/// </summary>
		/// <returns>
		/// 指定索引处的元素。
		/// </returns>
		/// <param name="index">要获得或设置的元素从零开始的索引。</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> 不是 <see cref="T:System.Collections.Generic.IList`1"/> 中的有效索引。</exception><exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Generic.IList`1"/> 为只读。</exception>
		public T this[int index]
		{
			get { return _innerList[index]; }
			set
			{
				var v = _innerList[index];
				_innerList[index] = value;
				OnReplaced(new ItemChangeEventArgs<T>(index, v, value));
			}
		}

		#endregion



		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				//反注册事件
				if (typeof(T).GetInterface(typeof(INotifyPropertyChanged).FullName) != null)
				{
					this.Cast<INotifyPropertyChanged>().ForEach(s => s.PropertyChanged -= EventList_PropertyChanged);
				}
			}

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		#endregion

	}
}