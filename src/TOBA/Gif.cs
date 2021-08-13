using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA
{
	using System.Drawing;
	using System.Windows.Forms;

	class Gif
	{
		private static Image[] _images_beg = new[]
		{
			Properties.Resources.gif_beg1,
			Properties.Resources.gif_beg7,
			Properties.Resources.gif_beg8,
			Properties.Resources.gif_beg10
		};

		private static Image[] _images_joke = new[]
		{
			Properties.Resources.gif_joke2,
			Properties.Resources.gif_joke3,
			Properties.Resources.gif_joke4,
			Properties.Resources.gif_joke5,
			Properties.Resources.gif_joke6,
			Properties.Resources.gif_joke9,
			Properties.Resources.gif_joke11,
			Properties.Resources.gif_joke12,
			Properties.Resources.gif_joke13,
			Properties.Resources.gif_joke16,
			Properties.Resources.gif_joke17,
			Properties.Resources.lxh_working,
			Properties.Resources.lxh_01,
			Properties.Resources.lxh_99,
			Properties.Resources.gif1
		};

		private static Image[] _images_sad = new[]
		{
			Properties.Resources.gif_sad1,
			Properties.Resources.gif_sad2,
			Properties.Resources.gif_sad4,
			Properties.Resources.gif_sad5,
			Properties.Resources.gif_sad6,
			Properties.Resources.gif_sad7,
			Properties.Resources.gif_sad8,
			Properties.Resources.gif_sad9,
			Properties.Resources.gif_sad10,
			Properties.Resources.gif_sad11,
			Properties.Resources.gif_sad12,
			Properties.Resources.gif_sad13,
			Properties.Resources.gif_sad16,
			Properties.Resources.gif_sad18,
			Properties.Resources.gif_sad19,
			Properties.Resources.gif_sad20,
			Properties.Resources.gif_sad21,
			Properties.Resources.lxh_cry
		};

		private static Image[] _images_success = new[]
		{
			Properties.Resources.gif_success1,
			Properties.Resources.gif_success2,
			Properties.Resources.gif_success3,
			Properties.Resources.gif_success4,
			Properties.Resources.gif_success5,
			Properties.Resources.gif_success6,
			Properties.Resources.gif_success9,
			Properties.Resources.gif_success10,
			Properties.Resources.lxh_happy,
			Properties.Resources.lxh_happy__2_,
			Properties.Resources.lxh_excite
		};

		static Random _random = new Random();

		static Image RandomGet(Image[] arr) => arr[_random.Next(arr.Length)];

		static void SetRandomImage(Image[] arr, PictureBox pb)
		{
			var image = RandomGet(arr);
			pb.SizeMode = image.Width > pb.Width ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
			pb.Image = image;
		}

		internal static void SetLoadingImage(PictureBox pb) => SetRandomImage((_images_beg), pb);

		internal static void SetWaitingImage(PictureBox pb) => SetRandomImage(_images_joke, pb);

		internal static void SetSuccessImage(PictureBox pb) => SetRandomImage(_images_success, pb);

		internal static void SetFailedImage(PictureBox pb) => SetRandomImage(_images_sad, pb);

	}
}
