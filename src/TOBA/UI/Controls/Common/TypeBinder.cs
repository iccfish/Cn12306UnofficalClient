using System;

namespace TOBA.UI.Controls.Common
{
	using System.Collections;
	using System.ComponentModel;
	using System.Reflection;

	static class TypeBinder
	{
		/// <summary>
		/// 将原始对象的属性值复制到新对象
		/// </summary>
		/// <param name="newInstance"></param>
		/// <param name="oldInstance"></param>
		public static void BindProperty(object newInstance, object oldInstance)
		{
			var typeDesicriptor = TypeDescriptor.GetProperties(oldInstance);
			var newProperties = TypeDescriptor.GetProperties(newInstance);

			foreach (PropertyDescriptor p in newProperties)
			{
				var op = typeDesicriptor.Find(p.Name, true);
				if (op == null) continue;

				if (op.PropertyType == p.PropertyType)
				{
					p.SetValue(newInstance, op.GetValue(oldInstance));
				}
				else
				{
					var newObj = p.GetValue(newInstance);
					var oldObj = op.GetValue(oldInstance);

					if (oldObj == null) continue;
					if (newObj == null) p.SetValue(newInstance, BindObject(p.PropertyType.GetConstructor(new Type[] { }), oldInstance));

					BindProperty(newObj, oldObj);
				}
			}
		}

		/// <summary>
		/// 创建一个目标对象,并将同名的属性值复制过去
		/// </summary>
		/// <param name="objType">新对象类型</param>
		/// <param name="constructor">构造参数</param>
		/// <param name="oldValue">原始对象</param>
		/// <returns></returns>
		public static object BindObject(ConstructorInfo constructor, object oldValue)
		{
			var parameters = constructor.GetParameters();
			var paramList = new ArrayList(parameters.Length);

			var oldTypeDescriptor = TypeDescriptor.GetProperties(oldValue);

			foreach (var pitem in parameters)
			{
				var oldParamDesc = oldTypeDescriptor.Find(pitem.Name, true);

				if (oldParamDesc == null)
				{
					paramList.Add(Activator.CreateInstance(pitem.ParameterType));
				}
				else
				{
					paramList.Add(oldParamDesc.GetValue(oldValue));
					//if (pitem.ParameterType == oldParamDesc.PropertyType)
					//{
					//}
					//else
					//{
					//    var value = (int)oldParamDesc.GetValue(oldValue);
					//    paramList.Add(value);
					//}
				}
			}

			var newObj = constructor.Invoke(paramList.ToArray());
			BindProperty(newObj, oldValue);

			return newObj;
		}
	}

}
