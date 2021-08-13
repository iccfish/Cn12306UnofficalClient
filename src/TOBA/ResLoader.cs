using System;
using System.Linq;

namespace TOBA
{
	using Entity;

	using FSLib.Extension;

	using System.IO;
	using System.Windows.Forms;

	internal static class ResLoader
	{
		static string _root, _imagesRoot;

		static ResLoader()
		{
			_root = Path.Combine(System.Reflection.Assembly.GetEntryAssembly().GetLocation(), "data");
			_imagesRoot = Path.Combine(_root, "images");
		}

		internal static void Init()
		{
			ResourceLoader.RequireLoadFileContent += (s, e) =>
			{
				var path = GetPath(e.Data2, e.Data1);
				if (System.IO.File.Exists(path)) e.Data3 = System.IO.File.ReadAllText(path);
			};
			ResourceLoader.RequireSaveFileContent += (s, e) =>
			{
				var path = GetPath(e.Data2, e.Data1);
				IOUtility.WriteAllText(path, e.Data3);
			};
			ResourceLoader.RequireDeleteFile += (s, e) =>
			{
				var path = GetPath(e.Data2, e.Data1);
				if (File.Exists(path))
					File.Delete(path);
			};
		}

		public static ImageList LoadImagesCatalog(string name)
		{
			var files = Directory.GetFiles(Path.Combine(_imagesRoot, name), "*.png");
			var images = files.ToDictionary(Path.GetFileNameWithoutExtension, System.Drawing.Image.FromFile);

			var il = new ImageList();
			if (images.Any())
			{
				il.ColorDepth = ColorDepth.Depth32Bit;
				il.ImageSize = images.First().Value.Size;

				images.ForEach(s => il.Images.Add(s.Key, s.Value));
			}
			return il;
		}

		/// <summary>
		/// 获得指定资源的路径
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetPath(string path, ResourceLocation location = ResourceLocation.Program)
		{
			if (Path.IsPathRooted(path))
				return path;

			switch (location)
			{
				case ResourceLocation.Program:
					return Path.Combine(_root, path);
				case ResourceLocation.AppData:
					return Profile.Root.GetCacheFile(path);
				default:
					break;
			}

			return null;
		}

		public static string ToRelative(string path, ResourceLocation location = ResourceLocation.Program)
		{
			if (string.IsNullOrEmpty(path))
				return path;

			switch (location)
			{
				case ResourceLocation.Program:
					return IOUtility.GetRelativePath(_root, path);
				case ResourceLocation.AppData:
					return IOUtility.GetRelativePath(AppContext.ExtensionManager.ConfigurationProvider.ProfileRoot, path);
				default:
					break;
			}

			return path;
		}
	}
}
