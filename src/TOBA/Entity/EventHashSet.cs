#if !NET35
namespace TOBA.Entity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	public class EventHashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback, ISet<T>
	{
		private HashSet<T> _inner;

		public EventHashSet()
		{
			_inner = new HashSet<T>();
		}

		public EventHashSet(IEqualityComparer<T> comparer)
		{
			_inner = new HashSet<T>(comparer);
		}

		public EventHashSet(IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			_inner = new HashSet<T>(source, comparer);
		}

		public EventHashSet(IEnumerable<T> source)
		{
			_inner = new HashSet<T>(source);
		}


		/// <summary>返回一个循环访问集合的枚举器。</summary>
		/// <returns>可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。</returns>
		public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();

		/// <summary>返回一个循环访问集合的枚举器。</summary>
		/// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 中。</summary>
		/// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 的对象。</param>
		/// <exception cref="T:System.NotSupportedException">
		/// <see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。</exception>
		public void Add(T item)
		{
			if (_inner.Add(item))
			{
				OnAdded(new ItemEventArgs<T>(-1, item));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> Added;

		/// <summary>
		/// 引发 <see cref="Added"/> 事件
		/// </summary>
		protected virtual void OnAdded(ItemEventArgs<T> e)
		{
			Added?.Invoke(this, e);
		}


		/// <summary>修改当前集，使该集包含当前集和指定集合中同时存在的所有元素。</summary>
		/// <param name="other">要与当前集进行比较的集合。</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="other" /> 为 null。</exception>
		public void UnionWith(IEnumerable<T> other)
		{
			var target = _inner.Except(other).ToArray();
			target.ForEach(s => Remove(s));
		}

		/// <summary>修改当前集，使该集仅包含指定集合中也存在的元素。</summary>
		/// <param name="other">要与当前集进行比较的集合。</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="other" /> 为 null。</exception>
		public void IntersectWith(IEnumerable<T> other)
		{
			var union = _inner.Intersect(other).ToArray();
			union.ForEach(s => Remove(s));
		}

		/// <summary>从当前集内移除指定集合中的所有元素。</summary>
		/// <param name="other">要从集内移除的项的集合。</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="other" /> 为 null。</exception>
		public void ExceptWith(IEnumerable<T> other)
		{
			other.ForEach(s => Remove(s));
		}

		/// <summary>修改当前集，使该集仅包含当前集或指定集合中存在的元素（但不可包含两者共有的元素）。</summary>
		/// <param name="other">要与当前集进行比较的集合。</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="other" /> 为 null。</exception>
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			var union = _inner.Intersect(other).ToArray();
			union.ForEach(s => Remove(s));
		}

#if !NET35

		/// <summary>向当前集内添加元素，并返回一个指示是否已成功添加元素的值。</summary>
		/// <returns>如果该元素已添加到集内，则为 true；如果该元素已在集内，则为 false。</returns>
		/// <param name="item">要添加到集内的元素。</param>
		bool ISet<T>.Add(T item)
		{
			if (_inner.Add(item))
			{
				OnAdded(new ItemEventArgs<T>(-1, item));

				return true;
			}

			return false;
		}
#endif

		/// <summary>从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除所有项。</summary>
		/// <exception cref="T:System.NotSupportedException">
		/// <see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。</exception>
		public void Clear()
		{
			foreach (var item in _inner)
			{
				OnRemoved(new ItemEventArgs<T>(-1, item));
			}

			_inner.Clear();
		}


		/// <summary>从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除特定对象的第一个匹配项。</summary>
		/// <returns>如果已从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中成功移除 <paramref name="item" />，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1" /> 中没有找到 <paramref name="item" />，该方法也会返回 false。</returns>
		/// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除的对象。</param>
		/// <exception cref="T:System.NotSupportedException">
		/// <see cref="T:System.Collections.Generic.ICollection`1" /> 是只读的。</exception>
		public bool Remove(T item)
		{
			if (_inner.Remove(item))
			{
				OnRemoved(new ItemEventArgs<T>(-1, item));

				return true;
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> Removed;

		/// <summary>
		/// 引发 <see cref="Removed"/> 事件
		/// </summary>

		protected virtual void OnRemoved(ItemEventArgs<T> e)
		{
			Removed?.Invoke(this, e);
		}

		/// <summary>获取 <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。</summary>
		/// <returns>
		/// <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。</returns>
		public int Count
		{
			get => _inner.Count;
		}

		public bool IsReadOnly => ((ICollection<T>)_inner).IsReadOnly;


		/// <summary>使用将目标对象序列化所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
		/// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
		/// <param name="context">此序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext" />）。</param>
		/// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			_inner.GetObjectData(info, context);
		}

		/// <summary>在整个对象图形已经反序列化时运行。</summary>
		/// <param name="sender">开始回调的对象。当前未实现该参数的功能。</param>
		public void OnDeserialization(object sender)
		{
			_inner.OnDeserialization(sender);
		}

		public bool Contains(T item)
		{
			return ((ICollection<T>)_inner).Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			((ICollection<T>)_inner).CopyTo(array, arrayIndex);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).IsSupersetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).IsProperSupersetOf(other);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).IsProperSubsetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).Overlaps(other);
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return ((ISet<T>)_inner).SetEquals(other);
		}
	}
}
#endif