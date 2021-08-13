namespace TOBA.UI.Controls.Common
{
	using System;
	using System.Drawing;

	/// <summary>
	/// 表示样式配置
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class StyleConfigAttribute : Attribute
	{
		/// <summary>
		/// 前景色
		/// </summary>
		public Color ForColor { get; set; }

		/// <summary>
		/// 背景色
		/// </summary>
		public Color BackColor { get; set; }

		/// <summary>
		/// 获得或设置对应样式的前景字体
		/// </summary>
		public Font Font { get; set; }

		/// <summary>
		/// 创建 <see cref="StyleConfigAttribute"/> 对象
		/// </summary>
		/// <param name="forColor">前景色</param>
		/// <param name="backColor">背景色</param>
		public StyleConfigAttribute(Color backColor, Color forColor)
			: this(backColor, forColor, null)
		{
		}

		/// <summary>
		/// 创建 <see cref="StyleConfigAttribute"/> 对象
		/// </summary>
		/// <param name="forColor">前景色</param>
		/// <param name="backColor">背景色</param>
		/// <param name="font">字体</param>
		public StyleConfigAttribute(Color backColor, Color forColor, Font font)
		{
			this.ForColor = forColor;
			this.BackColor = backColor;
			this.Font = font;
		}

		/// <summary>
		/// 创建 <see cref="StyleConfigAttribute"/> 对象
		/// </summary>
		/// <param name="forColor">前景色</param>
		/// <param name="backColor">背景色</param>
		public StyleConfigAttribute(int backColor, int forColor)
			:
			this(Color.FromArgb((backColor & 0xFF0000) >> 16, (backColor & 0xFF00) >> 8, backColor & 0xFF), Color.FromArgb((forColor & 0xFF0000) >> 16, (forColor & 0xFF00) >> 8, forColor & 0xFF))
		{
		}

		/// <summary>
		/// 创建 <see cref="StyleConfigAttribute"/> 对象
		/// </summary>
		/// <param name="forColorA">前景色A</param>
		/// <param name="forColorR">前景色R</param>
		/// <param name="forColorG">前景色G</param>
		/// <param name="forColorB">前景色B</param>
		/// <param name="backColorA">背景色A</param>
		/// <param name="backColorR">背景色R</param>
		/// <param name="backColorG">背景色G</param>
		/// <param name="backColorB">背景色B</param>
		public StyleConfigAttribute(int backColorA, int backColorR, int backColorG, int backColorB, int forColorA, int forColorR, int forColorG, int forColorB)
			: this(Color.FromArgb(backColorA, backColorR, backColorG, backColorB), Color.FromArgb(forColorA, forColorR, forColorG, forColorB))
		{
		}

		/// <summary>
		/// 创建 <see cref="StyleConfigAttribute"/> 对象
		/// </summary>
		/// <param name="forColorR">前景色R</param>
		/// <param name="forColorG">前景色G</param>
		/// <param name="forColorB">前景色B</param>
		/// <param name="backColorR">背景色R</param>
		/// <param name="backColorG">背景色G</param>
		/// <param name="backColorB">背景色B</param>
		public StyleConfigAttribute(int backColorR, int backColorG, int backColorB, int forColorR, int forColorG, int forColorB)
			: this(255, backColorR, backColorG, backColorB, 255, forColorR, forColorG, forColorB)
		{
		}
	}
}
