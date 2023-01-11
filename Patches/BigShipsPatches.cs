using HarmonyLib;
using UnityEngine;

namespace SandSpace
{
	internal class BigShipsPatches
	{
		[HarmonyPatch (typeof (Engine), "GetDrag")]
		internal static class Engine_GetDrag_Patch
		{
			internal static void Postfix (ref float __result)
			{
				__result = __result * SandSpaceMod.Settings.engineDragMult;
			}
		}

		[HarmonyPatch (typeof (Engine), "GetRotateAngularDrag")]
		internal static class Engine_GetRotateAngularDrag_Patch
		{
			internal static void Postfix (ref float __result)
			{
				__result = __result * SandSpaceMod.Settings.engineRotateDragMult;
			}
		}
	}
}
