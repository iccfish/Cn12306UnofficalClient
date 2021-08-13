using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Passenger
{
	internal class PassengerTab : TabPage, IOperation
	{
		public PassengerList List { get; set; }

		/// <summary>
		/// 创建 <see cref="PassengerTab" />  的新实例(PassgengerTab)
		/// </summary>
		public PassengerTab()
			: this(null)
		{
		}

		/// <summary>
		/// 创建 <see cref="PassengerTab" />  的新实例(PassgengerTab)
		/// </summary>
		public PassengerTab(Session session)
		{
			Session = session;
			if (session != null)
			{
				Text = session.UserKeyData.DisplayName.DefaultForEmpty(session.UserName) + " (" + (!Session.UserProfile.IsPassengerLoaded ? "加载中.." : Session.UserProfile.Passengers.Count.ToString()) + ")";
				Session.PassengerLoadComplete += Session_PassengerLoadComplete;
				if (Session.UserProfile.Passengers != null)
				{
					Session_PassengerLoadComplete(null, null);
				}

				List = new PassengerList(session)
						{
						};
				Controls.Add(List);
			}
		}

		void Session_PassengerLoadComplete(object sender, EventArgs e)
		{
			var action = new Action(() =>
			{
			if (sender != null)
				Session.PassengerLoadComplete -= Session_PassengerLoadComplete;
				Text = Session.UserKeyData.DisplayName.DefaultForEmpty(Session.UserName) + " (" + Session.UserProfile.Passengers.Count.ToString() + ")";
			});
			if (InvokeRequired)
				BeginInvoke(action);
			else
				action();
		}

		/// <summary>
		/// 刷新可用性
		/// </summary>
		public void RefreshAvailable()
		{
			List.RefreshAvailable();
		}

		/// <summary>
		/// 获得或设置可用性过滤
		/// </summary>
		public List<Entity.Web.Passenger> AvailableFilter { get { return List.AvailableFilter; } set { List.AvailableFilter = value; } }


		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		#endregion
	}
}
