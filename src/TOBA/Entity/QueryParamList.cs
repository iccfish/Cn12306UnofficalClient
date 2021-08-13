using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TOBA.Entity
{
	using Configuration;

	using FSLib.Extension;

	using System.Threading;

	using Utility = TOBA.Utility;

	/// <summary>
	/// 查询模式列表
	/// </summary>
	internal class QueryParamList : IList<QueryParam>
	{
		private QueryParam _selectedQuery;
		List<QueryParam> _list;
		string _root;
		List<string> _keys;
		string _indexFile;
		readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

		/// <summary>
		/// 创建 <see cref="QueryParamList" />  的新实例(QueryParamList)
		/// </summary>
		public QueryParamList(string root, bool shadowMode)
		{
			_list = new List<QueryParam>();
			_keys = new List<string>();
			_root = root;

			if (!string.IsNullOrEmpty(root))
			{
				Directory.CreateDirectory(root);

				//加载
				Load();
			}
			if (shadowMode)
				_root = null;
		}

		/// <summary>
		/// 从现有文件中加载
		/// </summary>
		void Load()
		{
			_indexFile = Path.Combine(_root, "index.json");
			if (!File.Exists(_indexFile))
			{
				return;
			}

			var list = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(_indexFile)) ?? new string[0];
			list.ForEach(s => _keys.Add(s));

			var files = list.Select(s => Path.Combine(_root, s + ".json"));
			files
				.Select(s =>
				{
					try
					{
						var m = Newtonsoft.Json.JsonConvert.DeserializeObject<Entity.QueryParam>(System.IO.File.ReadAllText(s), new JsonSerializerSettings()
						{
							TypeNameHandling = TypeNameHandling.Auto
						});
						if (m != null)
						{
							m.FilePath = s;
						}
						return m;
					}
					catch (Exception)
					{
						return null;
					}
				}).ExceptNull()
				.ForEach(Add);
			if (_keys.Count != this.Count)
				_keys = this.Select(s => s.ID).ToList();
		}

		#region Implementation of IEnumerable

		public IEnumerator<QueryParam> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		#endregion

		#region Implementation of ICollection<QueryParam>

		public void Add(QueryParam item)
		{
			lock (this)
			{

				item.EnsureID();

				_list.Add(item);
				OnQueryParamAdded(new QueryParamEventArgs(item));
				AttachParamEvents(item);

				//保存索引？
				if (!_keys.Contains(item.ID, StringComparer.OrdinalIgnoreCase))
				{
					_keys.Add(item.ID);
				}
			}
		}

		/// <summary>
		/// 添加新的默认查询
		/// </summary>
		public void Add()
		{
			Add(new QueryParam()
			{
				Name = "新建查询"
			});
		}

		public void Clear()
		{
			var items = _list.ToArray();
			items.ForEach(s => Remove(s));
		}

		public bool Contains(QueryParam item)
		{
			return _list.Contains(item);
		}

		public void CopyTo(QueryParam[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		public bool Remove(QueryParam item)
		{
			//设置标记位后由绑定的事件句柄自动移除
			item.IsPersistent = false;
			item.IsLoaded = false;
			return true;
		}

		public int Count
		{
			get { return _list.Count; }
		}
		public bool IsReadOnly
		{
			get { return false; }
		}

		#endregion

		#region Implementation of IList<QueryParam>

		public int IndexOf(QueryParam item)
		{
			return _list.IndexOf(item);
		}

		public void Insert(int index, QueryParam item)
		{
			item.EnsureID();

			_list.Insert(index, item);
			OnQueryParamAdded(new QueryParamEventArgs(item));
			AttachParamEvents(item);

			//保存索引？
			if (_keys.Contains(item.ID, StringComparer.OrdinalIgnoreCase))
			{
				_keys.Add(item.ID);
			}
		}

		public void RemoveAt(int index)
		{
			lock (this)
			{

				var item = this[index];
				//设置标记位后由绑定的事件句柄自动移除
				item.IsPersistent = false;
				item.IsLoaded = false;
			}
		}

		public QueryParam this[int index]
		{
			get { return _list[index]; }
			set { _list[index] = value; }
		}

		#endregion

		#region 事件

		/// <summary>
		/// 当查询参数添加时引发此事件
		/// </summary>
		public event EventHandler<QueryParamEventArgs> QueryParamAdded;

		/// <summary>
		/// 引发 <see cref="QueryParamAdded" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnQueryParamAdded(QueryParamEventArgs ea)
		{
			var handler = QueryParamAdded;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 当查询参数移除时引发此事件
		/// </summary>
		public event EventHandler<QueryParamEventArgs> QueryParamRemoved;

		/// <summary>
		/// 引发 <see cref="QueryParamRemoved" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnQueryParamRemoved(QueryParamEventArgs ea)
		{
			var handler = QueryParamRemoved;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 当查询参数更新时引发此事件
		/// </summary>
		public event EventHandler<QueryParamEventArgs> QueryParamSaved;

		/// <summary>
		/// 引发 <see cref="QueryParamSaved" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnQueryParamSaved(QueryParamEventArgs ea)
		{
			var handler = QueryParamSaved;
			if (handler != null)
				handler(this, ea);
		}

		public void RequestStopAll()
		{
			this.ForEach(s => s.OnRequestStop());
		}

		public event EventHandler<GeneralEventArgs<QueryParam>> SelectedQueryChanged;

		/// <summary>
		/// 引发 <see cref="SelectedQueryChanged" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnSelectedQueryChanged(GeneralEventArgs<QueryParam> ea)
		{
			var handler = SelectedQueryChanged;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 获得或设置当前选中的查询
		/// </summary>
		public QueryParam SelectedQuery
		{
			get { return _selectedQuery; }
			set
			{
				if (value == _selectedQuery)
					return;

				_selectedQuery = value;
				OnSelectedQueryChanged(new GeneralEventArgs<QueryParam>(value));
			}
		}

		#endregion

		#region 事件绑定

		/// <summary>
		/// 绑定事件
		/// </summary>
		/// <param name="p"></param>
		void AttachParamEvents(QueryParam p)
		{
			p.LoadedChanged += RemoveIfNotAlive;
			p.PersistentChanged += RemoveIfNotAlive;
			p.RequireSave += SaveParam;
		}

		public void Save()
		{
			SaveIndex();
			this.ForEach(SaveParam);
		}

		void SaveParam(QueryParam param)
		{
			if (string.IsNullOrEmpty(_root) || param == null)
				return;
			if (!string.IsNullOrEmpty(_root))
			{
				param.FilePath = Path.Combine(_root, param.ID + ".json");
				param.SaveToFile(param.FilePath);
				var json = JsonConvert.SerializeObject(param,
					Formatting.None,
					new JsonSerializerSettings()
					{
						TypeNameHandling = TypeNameHandling.Auto
					});
				File.WriteAllText(param.FilePath, json, System.Text.Encoding.UTF8);
			}

			OnQueryParamSaved(new QueryParamEventArgs(param));
		}

		/// <summary>
		/// 保存
		/// </summary>
		void SaveParam(object sender, EventArgs e)
		{
			SaveParam(sender as QueryParam);
		}

		/// <summary>
		/// 检测指定的查询是否还活动，如果不活动则清除并删除
		/// </summary>
		void RemoveIfNotAlive(object sender, EventArgs e)
		{
			lock (this)
			{
				var p = sender as QueryParam;
				if (p == null || p.IsLoaded || p.IsPersistent) return;

				//解除绑定事件
				p.LoadedChanged -= RemoveIfNotAlive;
				p.PersistentChanged -= RemoveIfNotAlive;
				p.RequireSave -= SaveParam;

				_list.Remove(p);
				_keys.Remove(p.ID);
				OnQueryParamRemoved(new QueryParamEventArgs(p));
				//删除文件
				if (!string.IsNullOrEmpty(_root) && !p.FilePath.IsNullOrEmpty())
				{
					File.Delete(p.FilePath);
				}
			}
		}
		#endregion

		#region 索引

		/// <summary>
		/// 保存索引
		/// </summary>
		void SaveIndex()
		{

			if (!string.IsNullOrEmpty(_root))
			{
				_keys.SaveToFile(_indexFile);
			}
		}

		#endregion

		#region 查找

		/// <summary>
		/// 查找符合要求的车次
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public QueryParam Find(string fromName, string toName, DateTime date)
		{
			var dt = date.Date;

			return this.FirstOrDefault(s => Utility.IsStationInclude(fromName, toName) && dt == s.DepartureDate.Date);
		}

		/// <summary>
		/// 获得当前的账户是否查出来票
		/// </summary>
		public bool HasTicket
		{
			get
			{
				return this.Any(s => s.IsLoaded && s.HasTicket);
			}
		}

		/// <summary>
		/// 返回当前的车票是否有任何改签的查询
		/// </summary>
		public bool HasResignQuery
		{
			get { return this.Any(s => s.Resign); }
		}

		#endregion

		#region 查询相关

		/// <summary>
		/// 获得安全的查询间隔时间
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public int CheckForSafty(int time)
		{
			if (!QueryConfiguration.Current.EnableSpeedProtect)
				return time;

			var baseInterval = 0.1;
			switch (ProgramConfiguration.Instance.Mode)
			{
				case RunningMode.PreSell:
					baseInterval = 0.5;
					break;
				case RunningMode.CatchLeak:
					baseInterval = 1;
					break;
				default:
					baseInterval = 0.5;
					break;
			}

			_lock.EnterReadLock();
			var queriesInBusy = this.Count(s => s.QueryState == QueryState.Query || s.QueryState == QueryState.Wait);
			_lock.ExitReadLock();

			var minTime = queriesInBusy * baseInterval * 1000;

			return (int)Math.Max(minTime, time);
		}

		/// <summary>
		/// 持久化当前查询状态
		/// </summary>
		/// <param name="clear"></param>
		public void PersistentQueryState(bool clear = false)
		{
			_lock.EnterReadLock();
			this.ForEach(s => s.PersistentQueryState(clear));
			_lock.ExitReadLock();
		}

		#endregion
	}
}
