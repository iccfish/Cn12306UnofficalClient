namespace TOBA.UI.Controls.Common
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	public static class ListViewResource
	{
		static ListViewResource()
		{
			InitStyle();
		}

		#region 外观配置

		/// <summary>
		/// 样式设定
		/// </summary>
		public static readonly Dictionary<RowStyleType, ColorSchema> RowStyleCollection = new();

		/// <summary>
		/// 初始化样式信息
		/// </summary>
		static void InitStyle()
		{
			if (RowStyleCollection.Count > 0) return;

			Type t = typeof(RowStyleType);
			FieldInfo[] tlist = t.GetFields();
			foreach (var fieldInfo in tlist)
			{
				if (fieldInfo.IsSpecialName) continue;

				var tp = (RowStyleType)fieldInfo.GetRawConstantValue();
				ColorSchema style;

				object[] atts = fieldInfo.GetCustomAttributes(typeof(StyleConfigAttribute), false);
				if (atts.Length > 0)
				{
					var lr = atts[0] as StyleConfigAttribute;
					style = new ColorSchema(lr.ForColor, lr.BackColor, lr.ForColor, lr.BackColor, lr.ForColor);
				}
				else
				{
					style = new ColorSchema(SystemColors.Window, SystemColors.WindowText, SystemColors.WindowFrame, SystemColors.ActiveCaptionText, SystemColors.WindowFrame);
				}

				RowStyleCollection.Add(tp, style);
			}
		}

		/// <summary>
		/// 获得对应的样式设置
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ColorSchema Style(RowStyleType type)
		{
			return RowStyleCollection[type];
		}

		/// <summary>
		/// 切换行样式
		/// </summary>
		/// <param name="lvt">要切换的图标行</param>
		/// <param name="type">要切换为的样式</param>
		public static void SwitchListViewItemStyle(ListViewItem lvt, RowStyleType type)
		{
			SwitchListViewItemStyle(lvt, type, false);
		}

		/// <summary>
		/// 切换行样式
		/// </summary>
		/// <param name="lvt">要切换的图标行</param>
		/// <param name="type">要切换为的样式</param>
		/// <param name="inverseStyle">是否翻转显示的样式</param>
		public static void SwitchListViewItemStyle(ListViewItem lvt, RowStyleType type, bool inverseStyle)
		{
			var s = Style(type);
			lvt.ForeColor = !inverseStyle ? s.ForColor : s.BackColor;
			lvt.BackColor = inverseStyle ? s.ForColor : s.BackColor;
			if (s.Font != null) lvt.Font = s.Font;
		}


		/// <summary>
		/// 切换行样式
		/// </summary>
		/// <param name="lvt">要切换的图标行</param>
		/// <param name="style">要切换为的样式</param>
		public static void SwitchListViewItemStyle(ListViewItem lvt, ColorSchema style)
		{
			SwitchListViewItemStyle(lvt, style, false);
		}

		/// <summary>
		/// 切换行样式
		/// </summary>
		/// <param name="lvt">要切换的图标行</param>
		/// <param name="style">要切换为的样式</param>
		/// <param name="inverseStyle">是否翻转显示的样式</param>
		public static void SwitchListViewItemStyle(ListViewItem lvt, ColorSchema style, bool inverseStyle)
		{
			lvt.ForeColor = !inverseStyle ? style.ForColor : style.BackColor;
			lvt.BackColor = inverseStyle ? style.ForColor : style.BackColor;
			if (style.Font != null) lvt.Font = style.Font;
		}

		#endregion
	}
}
