using HarmonyLib;

namespace SandSpace
{
	internal class StrikeCraftPatches
	{
		// Патч для настройки характеристик истребителей от их навыков
		[HarmonyPatch (typeof (ItemBuildInfo), nameof (ItemBuildInfo.GetStarStrengthMultiplierForStrikeCraft))]
		private static class ItemBuildInfo_GetStarStrengthMultiplierForStrikeCraft_Patch
		{
			private static bool Prefix (ref float __result, ref int stars)
			{
				switch (stars)
				{
					case -1: __result = 1.0f;
					break;
					case 0: __result = 1.0f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 1: __result = 1.1f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 2: __result = 1.25f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 3: __result = 1.5f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 4: __result = 1.8f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 5: __result = 2.2f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					case 6: __result = 3.0f * SandSpaceMod.Settings.strikeCraftsStrengthMult;
					break;
					default: __result = 10.0f;
					break;
				}

				return false;
			}
		}
	}
}
