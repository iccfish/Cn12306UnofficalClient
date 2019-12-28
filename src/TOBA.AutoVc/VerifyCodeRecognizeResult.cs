namespace TOBA.AutoVc
{
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class VerifyCodeRecognizeResult : IVerifyCodeRecognizeResult
	{
		/// <inheritdoc />
		public Image Image { get; }

		private string _code;

		/// <inheritdoc />
		public string Code
		{
			get => _code;
			private set
			{
				_code = value;

				if (!string.IsNullOrWhiteSpace(value))
					CodeType = VerifyCodeRecognizeType.Text;
			}
		}

		/// <inheritdoc />
		public string Id { get; private set; }

		/// <inheritdoc />
		public VerifyCodeRecognizeType CodeType { get; private set; }

		private List<Point> _points;

		/// <inheritdoc />
		public List<Point> Points
		{
			get => _points;
			private set
			{
				_points = value;

				if (value != null)
					CodeType = VerifyCodeRecognizeType.Points;
			}
		}

		public VerifyCodeRecognizeResult(Image image)
		{
			Image = image;
		}

		/// <summary>
		/// 设置验证码为文本型验证码。
		/// </summary>
		/// <param name="id">验证码一唯DI一唯</param>
		/// <param name="code"></param>
		public void SetTextCode(string id, string code)
		{
			Code = code;
			Id = id;
		}

		/// <summary>
		/// 将诸如“x1,y1,x2,y2,....”这种格式的坐标转换为识别结果
		/// </summary>
		/// <param name="id"></param>
		/// <param name="text"></param>
		public void SetPointsFromLocation(string id, string text)
		{
			Id = id;
			var arr = text.Split(',');
			var pts = new List<Point>(arr.Length / 2);

			for (int i = 0; i < pts.Count; i++)
			{
				pts.Add(new Point(int.Parse(arr[i * 2]), int.Parse(arr[i * 2 + 1])));
			}

			Points = pts;
		}

		/// <summary>
		/// 将诸如“3,4,....”这种格式的图片索引转换为识别结果。
		/// 若快是这种结果
		/// </summary>
		/// <param name="id"></param>
		/// <param name="text"></param>
		public void SetPointsFromIndex(string id, string text)
		{
			Id = id;
			Points = TransferCodeFromIndex(text, CheckImageIs18((Bitmap)Image));
		}

		List<Point> TransferCodeFromIndex(string code, bool is18)
		{
			var picArr = code.Split(',').Select(s => int.Parse(s)).ToArray();
			var basePoint = new Point(5, 40);
			var sizePp = is18 ? new Size(45, 45) : new Size(67, 67);
			var space = is18 ? 3 : 5;

			var arr = new List<Point>(picArr.Length);
			foreach (var p in picArr)
			{
				var x = basePoint.X + (sizePp.Width + space) * ((p - 1) % (is18 ? 6 : 4)) + sizePp.Width / 2;
				var y = basePoint.Y + (sizePp.Height + space) * ((p - 1) / (is18 ? 6 : 4)) + sizePp.Height / 2;

				arr.Add(new Point(x, y));
			}

			return arr;
		}

		bool CheckImageIs18(Bitmap bitmap)
		{
			bitmap = new Bitmap(bitmap);

			var width = bitmap.Width;
			var height = bitmap.Height;
			var countX = 0;
			var countY = 0;
			Bitmap bm = new Bitmap(width, height);

			for (var x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Color pixel = bitmap.GetPixel(x, y); //获取当前坐标的像素值
					var result = (pixel.R + pixel.G + pixel.B) / 3;
					bm.SetPixel(x, y, Color.FromArgb(result, result, result));
				}
			}

			bitmap = bm;
			foreach (var x in new[] { 146, 147, 148 })
			{
				for (int y = 0; y < height; y++)
				{
					Color color = bitmap.GetPixel(x, y);
					if (color.R >= 240 && color.G >= 240 && color.B >= 240)
					{
						countY++;
					}
				}
			}

			foreach (var yy in new[] { 110, 111 })
			{
				for (int xx = 0; xx < width; xx++)
				{
					Color color = bitmap.GetPixel(xx, yy);
					if (color.R >= 240 && color.G >= 240 && color.B >= 240)
					{
						countX++;
					}
				}
			}

			return (countX / 2) <= (width / 2) || (countY / 3) <= (height / 2);
		}

	}
}
