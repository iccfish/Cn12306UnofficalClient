using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Order.Entity
{
	using System.IO;

	using Configuration;

	using Newtonsoft.Json;

	internal class OrderArchiveCollection : ConfigurationBase
	{
		string _file;
		List<OrderItem> _archive;

		/// <summary>
		/// 创建 <see cref="OrderArchiveCollection" />  的新实例(OrderArchiveCollection)
		/// </summary>
		/// <param name="file"></param>
		public OrderArchiveCollection(string file, bool shadowMode)
		{
			if (!shadowMode)
				_file = file;

			if (file.AsFileInfo().Exists)
			{
				try
				{
					var obj = JsonConvert.DeserializeAnonymousType(File.ReadAllText(file), new { orders = new List<OrderItem>() });
					_archive = obj.orders;
				}
				catch (Exception)
				{

				}
			}
			if (_archive == null)
				_archive = new List<OrderItem>();
		}

		public void Save()
		{
			if (string.IsNullOrEmpty(_file))
				return;

			Directory.CreateDirectory(Path.GetDirectoryName(_file));
			File.WriteAllText(_file, JsonConvert.SerializeObject(new { orders = _archive }));
		}

		/// <summary>
		/// 获得或设置订单存档
		/// </summary>
		public IEnumerable<OrderItem> Archive => _archive;

		public void Merge(IEnumerable<OrderItem> orders)
		{
			if (orders == null)
				return;

			var orderid = orders.Select(s => s.SequenceNo).MapToHashSet();
			_archive = orders.Concat(Archive.Where(s => !orderid.Contains(s.SequenceNo))).ToList();

			Save();
		}

		public void ClearArchive()
		{
			_archive.Clear();
			Save();
		}

		/// <summary>
		/// 合并到指定的列表中
		/// </summary>
		/// <param name="orders"></param>
		/// <returns></returns>
		public OrderItem[] MergeArchiveTo(IEnumerable<OrderItem> orders)
		{
			var orderid = orders.Select(s => s.SequenceNo).MapToHashSet();
			return orders.Concat(Archive.Where(s => !orderid.Contains(s.SequenceNo))).ToArray();
		}
	}
}
