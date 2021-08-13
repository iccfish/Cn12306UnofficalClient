namespace TOBA.UI.Controls.Common
{
	using System.Collections.Generic;
	using System.Drawing;

	public class ColorSchema
	{
		/// <summary>
		/// 获得或设置使用的字体
		/// </summary>
		public Font Font { get; set; }

		public Color ForColor { get; set; }

		public Color BackColor { get; set; }

		public Color BorderColor { get; set; }

		public Color TitleColor { get; set; }

		public Color TitleBackColor { get; set; }

		/// <summary>
		/// 创建 <see cref="ColorSchema" /> 的新实例
		/// </summary>
		public ColorSchema(Color forColor, Color backColor, Color borderColor, Color titleColor, Color titleBackColor)
		{
			ForColor = forColor;
			BackColor = backColor;
			BorderColor = borderColor;
			TitleColor = titleColor;
			TitleBackColor = titleBackColor;
		}

		static List<ColorSchema> _sysColorSchemas = new List<ColorSchema>()
		{
			//				ForColor							Backcolor						BorderColor					//TitleColor							TitleBackColor
			new ColorSchema(Color.FromArgb(0x55, 0x8A, 0xC4),Color.FromArgb(239, 247, 255),Color.FromArgb(150, 194, 241),Color.FromArgb(239, 247, 255), Color.FromArgb(150, 194, 241)),
			new ColorSchema(Color.FromArgb(0x68, 0x9C, 0x47),Color.FromArgb(240, 251, 235),Color.FromArgb(155, 223, 112),Color.FromArgb(240, 251, 235), Color.FromArgb(155, 223, 112)),
			new ColorSchema(Color.FromArgb(0x45, 0x7C, 0xDD),Color.FromArgb(0xFF, 0xFF, 0xFF),Color.FromArgb(146, 176, 221),Color.FromArgb(0x45, 0x7C, 0xDD), Color.FromArgb(226, 234, 248)),
			new ColorSchema(Color.FromArgb(0x3A, 0x7F, 0x9C),Color.FromArgb(239, 247, 255),Color.FromArgb(187, 225, 241),Color.FromArgb(239, 247, 255), Color.FromArgb(0x3A, 0x7F, 0x9C)),
			new ColorSchema(Color.FromArgb(0x45, 0x82, 0x8C),Color.FromArgb(250, 252, 253),Color.FromArgb(204, 239, 245),Color.FromArgb(250, 252, 253), Color.FromArgb(0x45, 0x82, 0x8C)),
			new ColorSchema(Color.FromArgb(0xCC, 0x66, 0x33),Color.FromArgb(255, 255, 247),Color.FromArgb(255, 204, 0),Color.FromArgb(255, 255, 247), Color.FromArgb(0xCC, 0x66, 0x33)),
			new ColorSchema(Color.FromArgb(0x55, 0x8A, 0xC4),Color.FromArgb(232, 245, 254),Color.FromArgb(169, 201, 226),Color.FromArgb(232, 245, 254), Color.FromArgb(0x55, 0x8A, 0xC4)),
			new ColorSchema(Color.FromArgb(0xA5, 0xA2, 0x29),Color.FromArgb(255, 255, 221),Color.FromArgb(227, 225, 151),Color.FromArgb(255, 255, 221), Color.FromArgb(0xA5, 0xA2, 0x29)),
			new ColorSchema(Color.FromArgb(0x7E, 0x95, 0x2B),Color.FromArgb(242, 253, 219),Color.FromArgb(173, 205, 60),Color.FromArgb(242, 253, 219), Color.FromArgb(0x7E, 0x95, 0x2B)),
			new ColorSchema(Color.FromArgb(0xBC, 0x3C, 0x72),Color.FromArgb(255, 245, 250),Color.FromArgb(248, 179, 208),Color.FromArgb(255, 245, 250), Color.FromArgb(0xBC, 0x3C, 0x72)),
			new ColorSchema(Color.Gray,Color.FromArgb(247, 247, 247),Color.FromArgb(211, 211, 211),Color.FromArgb(247, 247, 247), Color.Gray),
			new ColorSchema(Color.FromArgb(0x54, 0x7D, 0xB8),Color.FromArgb(243, 250, 255),Color.FromArgb(191, 209, 235),Color.FromArgb(243, 250, 255), Color.FromArgb(0x54, 0x7D, 0xB8)),
			new ColorSchema(Color.FromArgb(0xBD, 0x96, 0x49),Color.FromArgb(255, 249, 237),Color.FromArgb(255, 221, 153),Color.FromArgb(255, 249, 237), Color.FromArgb(0xBD, 0x96, 0x49)),
			new ColorSchema(Color.FromArgb(0x69, 0x69, 0xB2),Color.FromArgb(247, 247, 255),Color.FromArgb(150, 194, 241),Color.FromArgb(247, 247, 255), Color.FromArgb(0x69, 0x69, 0xB2)),
			new ColorSchema(Color.FromArgb(0x5A, 0x72, 0x8A),Color.FromArgb(238, 243, 247),Color.FromArgb(150, 194, 241),Color.FromArgb(238, 243, 247), Color.FromArgb(0x5A, 0x72, 0x8A)),
			new ColorSchema(Color.FromArgb(153, 51, 51),Color.FromArgb(247, 230, 230),Color.FromArgb(153, 51, 51),Color.FromArgb(247, 230, 230), Color.FromArgb(153, 51, 51))
		};

		public static int SysColorSchemaCount
		{
			get
			{
				return _sysColorSchemas.Count;
			}
		}

		public static ColorSchema GetSysColorSchema(int index)
		{
			return index >= 0 && index < _sysColorSchemas.Count ? _sysColorSchemas[index] : null;
		}
	}
}
