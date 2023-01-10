using System.Reflection;

namespace SandSpace
{
	public static class PatchingExtension
	{
		public static void SetPrivateFieldValue (object obj, string fieldName, object value)
		{
			obj.GetType ().GetField (fieldName, BindingFlags.Instance | BindingFlags.NonPublic).SetValue (obj, value);
		}

		public static TResult GetPrivateFieldValue<TResult> (object obj, string fieldName)
		{
			return (TResult) obj.GetType ().GetField (fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue (obj);
		}
	}
}
