using System;

using TOBA.Entity;

namespace TOBA.Profile
{
	using Configuration;

	using Entity.Web;

	using Order.Entity;

	using System.IO;

	/// <summary>
	/// 用户的配置文件
	/// </summary>
	internal class UserProfile : IDisposable
	{
		public string PassengerListFile
		{
			get;
			private set;
		}

		/// <summary>
		/// 获得是否在临时模式中
		/// </summary>
		public bool TemporaryMode { get; private set; }

		/// <summary>
		/// 获得当前的用户名
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// 配置文件路径
		/// </summary>
		public string ProfilePath { get; private set; }

		/// <summary>
		/// 密钥文件路径
		/// </summary>
		public string PwdFilePath { get; set; }


		/// <summary>
		/// 是否处于影子模式
		/// </summary>
		public bool ShadowMode { get; private set; }


		/// <summary>
		/// 获得或设置订单存档
		/// </summary>
		public OrderArchiveCollection OrderArchive
		{
			get
			{
				if (!OrderConfiguration.Instance.EnableOrderArchive || TemporaryMode)
					return null;

				return _orderArchive;
			}
			set { _orderArchive = value; }
		}

		/// <summary>
		/// 构造新的 <see cref="UserProfile"/> 对象
		/// </summary>
		/// <param name="userName">绑定的用户名</param>
		public UserProfile(string userName, bool tempMode, bool shadowMode = false)
		{
			UserName = userName;

			TemporaryMode = tempMode;
			ShadowMode = shadowMode;
			if (!tempMode)
			{
				ProfilePath = System.IO.Path.Combine(Root.UsersPath, Root.EncodeUserName(userName));

				if (!ShadowMode || Directory.Exists(ProfilePath))
				{
					System.IO.Directory.CreateDirectory(ProfilePath);

					PwdFilePath = System.IO.Path.Combine(ProfilePath, "pwd.dat");
					Configuration = UserConfiguration.Create(Path.Combine(ProfilePath, "userconfig.json"), shadowMode);
					PassengerListFile = Path.Combine(ProfilePath, "passengers.json");

					if (PassengerListFile.AsFileInfo().Exists)
					{
						try
						{
							_passengers = Newtonsoft.Json.JsonConvert.DeserializeObject<PassengerList>(File.ReadAllText(PassengerListFile));
							_passengers.Filepath = PassengerListFile;
						}
						catch (Exception)
						{
							PassengerListFile.AsFileInfo().Delete();
						}
					}
					_queryParams = new QueryParamList(TemporaryMode ? null : Path.Combine(ProfilePath, "queryParams"), ShadowMode);
					_orderArchive = new OrderArchiveCollection(Path.Combine(ProfilePath, "order", "archive"), shadowMode);
				}
				else
				{
					_queryParams = new QueryParamList(TemporaryMode ? null : Path.Combine(ProfilePath, "queryParams"), ShadowMode);
					_orderArchive = new OrderArchiveCollection(Path.Combine(ProfilePath, "order", "archive"), shadowMode);
				}
				//影子模式，重置路径以防止覆盖
				if (shadowMode)
				{
					ProfilePath = null;
					_passengers.Filepath = null;
				}
			}
			else
			{
				Configuration = UserConfiguration.Create(null);
				_queryParams = new QueryParamList(null, ShadowMode);
			}
		}

		///// <summary>
		///// 保存密码
		///// </summary>
		//[Obsolete]
		//public void SavePassword(string pwd)
		//{
		//	if (TemporaryMode)
		//		return;

		//	System.IO.File.WriteAllText(PwdFilePath, OldSecurity.EncodeString(pwd));
		//}

		/// <summary>
		/// 获得配置
		/// </summary>
		public UserConfiguration Configuration { get; private set; }

		#region 查询列表

		QueryParamList _queryParams;
		OrderArchiveCollection _orderArchive;
		PassengerList _passengers;

		/// <summary>
		/// 获得或设置用户的查询参数设置
		/// </summary>
		public QueryParamList QueryParams
		{
			get { return _queryParams; }
		}

		#endregion

		#region 联系人列表

		/// <summary>
		/// 获得当前的联系人是否已经加载
		/// </summary>
		public bool IsPassengerLoaded => (Passengers?.Count ?? 0) > 0;

		/// <summary>
		/// 获得用户的联系人列表
		/// </summary>
		public Entity.Web.PassengerList Passengers
		{
			get => _passengers;
			set
			{
				if (value == null)
					return;

				value.Filepath = PassengerListFile;
				_passengers = value;
			}
		}

		#endregion

		public void Save()
		{
			Passengers?.Save();
			Configuration.Save();
			QueryParams.Save();
		}

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
				//TODO 释放托管资源

			}
			//TODO 释放非托管资源

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion


	}
}
