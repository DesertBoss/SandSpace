using System.Reflection;
using HarmonyLib;

namespace SandSpace
{
	public static class PatchingExtension
	{
		public static void SetPrivateFieldValue (object obj, string fieldName, object value)
		{
			AccessTools.Field (obj.GetType (), fieldName).SetValue (obj, value);
		}

		public static TResult GetPrivateFieldValue<TResult> (object obj, string fieldName)
		{
			return (TResult) AccessTools.Field (obj.GetType (), fieldName).GetValue (obj);
		}

		public static void SetPrivatePropertyValue (object obj, string propName, object value)
		{
			var property = AccessTools.Property (obj.GetType (), propName);
			if (!property.CanWrite)
				return;

			property.SetValue (obj, value, null);
		}

		public static TResult GetPrivatePropertyValue<TResult> (object obj, string propName)
		{
			var property = AccessTools.Property (obj.GetType (), propName);
			if (!property.CanRead)
				return default;

			return (TResult) property.GetValue (obj, null);
		}
	}
}
