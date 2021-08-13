using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TOBA
{
	using Entity;

	using FSLib.Extension;

	/// <summary>
	/// 资源加载类
	/// </summary>
	internal class ResourceLoader
	{
		/// <summary>
		/// 请求加载文件内容。加载返回的始终是字符串
		/// </summary>
		public static event EventHandler<GeneralEventArgs<ResourceLocation, string, string>> RequireLoadFileContent;

		/// <summary>
		/// 引发 <see cref="RequireLoadFileContent" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		internal static void OnRequireLoadFileContent([CanBeNull] object sender, [NotNull] GeneralEventArgs<ResourceLocation, string, string> ea)
		{
			if (ea == null)
				throw new ArgumentNullException("ea", "ea is null.");

			RequireLoadFileContent?.Invoke(sender, ea);
		}


		/// <summary>
		/// 请求保存文件内容，保存的始终是序列化的结果
		/// </summary>
		public static event EventHandler<GeneralEventArgs<ResourceLocation, string, string>> RequireSaveFileContent;

		/// <summary>
		/// 请求删除文件
		/// </summary>
		public static event EventHandler<GeneralEventArgs<ResourceLocation, string, string>> RequireDeleteFile;


		/// <summary>
		/// 引发 <see cref="RequireSaveFileContent" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		internal static void OnRequireSaveFileContent([CanBeNull] object sender, [NotNull] GeneralEventArgs<ResourceLocation, string, string> ea)
		{
			if (ea == null)
				throw new ArgumentNullException("ea", "ea is null.");
			var handler = RequireSaveFileContent;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 读取资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static T LoadResourceFile<T>(string filePath, ResourceLocation location = ResourceLocation.Program)
		{
			var type = typeof(T);
			var e = new GeneralEventArgs<ResourceLocation, string, string>(location, filePath, null);
			OnRequireLoadFileContent(null, e);
			if (e.Data3 == null)
				return default(T);

			if (type == typeof(string))
				return (T)(object)e.Data3;

			if (type == typeof(Image))
				return (T)(object)Convert.FromBase64String(e.Data3).ToImage();

			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(e.Data3);
		}

		/// <summary>
		/// 保存资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="file"></param>
		/// <param name="model"></param>
		public static void SaveResourceFile<T>(string file, T model, ResourceLocation location = ResourceLocation.Program)
		{
			var type = typeof(T);
			string data = null;

			if (type == typeof(string))
				data = (string)(object)model;
			else if (type == typeof(Image))
				data = Convert.ToBase64String(((Image)(object)model).ToBytes(ImageFormat.Png));
			else data = Newtonsoft.Json.JsonConvert.SerializeObject(model);

			var e = new GeneralEventArgs<ResourceLocation, string, string>(location, file, data);
			OnRequireSaveFileContent(null, e);
		}

		/// <summary>
		/// 保存资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="file"></param>
		public static void RemoveResourceFile(string file, ResourceLocation location = ResourceLocation.Program)
		{
			var e = new GeneralEventArgs<ResourceLocation, string, string>(location, file, null);
			RequireDeleteFile?.Invoke(null, e);
		}
	}
}
