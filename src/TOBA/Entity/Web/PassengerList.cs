namespace TOBA.Entity.Web
{
	using Newtonsoft.Json;

	using System;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// 乘客列表
	/// </summary>
	internal class PassengerList : EventList<Passenger>
	{
		public string Filepath
		{
			get;
			set;
		}

		public PassengerList()
		{
		}


		public PassengerList(string filepath) : this()
		{
			Filepath = filepath;

		}

		/// <summary>
		/// 查找匹配的用户
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Passenger FindMatch(Passenger p)
		{
			return FindMatch(p.Name, p.IdTypeCode, p.IdNo);
		}

		/// <summary>
		/// 查找匹配的用户
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Passenger FindMatch(PassengerInTicket p)
		{
			return FindMatch(p.Name, p.IdType, p.IdNo);
		}

		/// <summary>
		/// 查找匹配的用户
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Passenger FindMatch(string name, char idtype, string id)
		{
			return this.FirstOrDefault(s => s.Name == name && s.IdTypeCode == idtype && IsIdMatch(s.IdNo, id));
		}

		bool IsIdMatch(string id1, string id2)
		{
			if ((id1 == null ^ id2 == null) || id1.Length != id2.Length)
				return false;

			var len = id1.Length;
			for (int i = 0; i < len; i++)
			{
				if (!(id1[i] == id2[i] || id1[i] == '*' || id2[i] == '*'))
					return false;
			}

			return true;
		}

		public void Save()
		{
			if (Filepath.IsNullOrEmpty())
				return;
			lock (this)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(Filepath));
				File.WriteAllText(Filepath, JsonConvert.SerializeObject(this));
			}
		}
	}
}