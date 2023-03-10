using HarmonyLib;

namespace SandSpace
{
	internal class BigShipsPatches
	{
		// Патч сопротивления линейному ускорению для кораблей
		[HarmonyPatch (typeof (Engine), nameof (Engine.GetDrag))]
		internal static class Engine_GetDrag_Patch
		{
			internal static void Postfix (ref float __result)
			{
				__result = __result * SandSpaceMod.Settings.engineDragMult;
			}
		}

		// Патч сопротивления угловому ускорению для кораблей
		[HarmonyPatch (typeof (Engine), nameof (Engine.GetRotateAngularDrag))]
		internal static class Engine_GetRotateAngularDrag_Patch
		{
			internal static void Postfix (ref float __result)
			{
				__result = __result * SandSpaceMod.Settings.engineRotateDragMult;
			}
		}
	}
}
