namespace TOBA.UI.Controls.Common
{
	using FSLib.Extension;

	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Forms;

	public class EnumComboBox : ComboBox
	{
		/// <summary>
		/// 创建 <see cref="EnumComboBox" /> 的新实例
		/// </summary>
		public EnumComboBox()
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		/// <summary>
		/// 覆盖下拉样式
		/// </summary>
		[Browsable(false)]
		public new ComboBoxStyle DropDownStyle
		{
			get
			{
				return ComboBoxStyle.DropDownList;
			}
			set
			{
			}
		}

		private Type _attachType;
		/// <summary>
		/// 绑定的类型
		/// </summary>
		/// <exception cref="System.ArgumentException"></exception>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type AttachType
		{
			get
			{
				return _attachType;
			}
			set
			{
				if (value == null) return;
				if (!value.IsEnum) throw new ArgumentException("Type '{0}' not an enum.".FormatWith(value.FullName));
				_attachType = value;

				base.Items.Clear();
				base.Items.AddRange(value.GetEnumDescription().Cast<object>().ToArray());
				SelectedValue = SelectedValue;
			}
		}

		/// <summary>
		/// 获得绑定的项目
		/// </summary>
		[Browsable(false)]
		public new ObjectCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		/// <summary>
		/// 获得或设置选定的值
		/// </summary>
		[Browsable(false)]
		public new object SelectedValue
		{
			get
			{
				if (AttachType == null) return null;
				if (this.SelectedIndex == -1) return null;
				else return (this.SelectedItem as Description).Value;
			}
			set
			{
				if (AttachType == null || value == null) return;
				var item = this.Items.Cast<Description>().FirstOrDefault(s => (int)s.Value == (int)value);
				if (item != null) this.SelectedItem = item;
				else if (Items.Count > 0) base.SelectedIndex = 0;
			}
		}

		#region ILocalizable 成员

		/// <summary>
		/// 切换UI语言
		/// </summary>
		/// <remarks>
		/// 大部分.net自带控件和窗体本身是由本类自动处理的，无需在这个方法中重写
		/// <para>需要额外处理的是非自带控件、无法自动处理的资源</para>
		/// </remarks>
		public void ChangeUILanguage()
		{
			AttachType = AttachType;
		}

		#endregion
	}
}
