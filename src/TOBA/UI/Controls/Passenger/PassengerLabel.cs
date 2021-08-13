using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Passenger
{
	internal class PassengerLabel : LinkLabel
	{
		public Entity.Web.Passenger Passenger { get; set; }

		/// <summary>
		/// 创建 <see cref="PassengerLabel" />  的新实例(PassengerLabel)
		/// </summary>
		public PassengerLabel(Entity.Web.Passenger passenger)
		{
			Passenger = passenger;
			AutoSize = false;
			Size = new Size(60, 20);
			Text = passenger.Name;
			TextAlign = ContentAlignment.MiddleCenter;
		}

		/// <summary>
		/// 按指定的关键字进行过滤
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Filter(string key, bool? student)
		{
			if (student != null && (student.Value ^ Passenger.Type == 3))
			{
				Hide();
				return false;
			}
			var valid = string.IsNullOrEmpty(Passenger.FirstLetter) || string.IsNullOrEmpty(Passenger.Name) || string.IsNullOrEmpty(Passenger.IdNo);

			if (!valid)
				valid = key.IsNullOrEmpty() || (Passenger.FirstLetter.Contains(key, StringComparison.OrdinalIgnoreCase) || Passenger.Name.Contains(key, StringComparison.OrdinalIgnoreCase) || Passenger.IdNo.Contains(key, StringComparison.OrdinalIgnoreCase));

			if (valid) Show();
			else Hide();

			return valid;
		}
	}
}
