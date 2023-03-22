using System.Collections.Generic;
using UnityEngine;

namespace SandSpace
{
	public static class Extensions
	{
		public static float Distance (this Vector3 vector1, Vector3 vector2)
		{
			return (vector1 - vector2).magnitude;
		}

		public static T RemoveFirst<T> (this List<T> list)
		{
			if (list == null || list.Count == 0)
				return default;

			var index = 0;
			var result = list[index];
			list.RemoveAt (index);

			return result;
		}

		public static T RemoveLast<T> (this List<T> list)
		{
			if (list == null || list.Count == 0)
				return default;

			var index = list.Count - 1;
			var result = list[index];
			list.RemoveAt (index);

			return result;
		}
	}
}
