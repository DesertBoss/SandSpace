using HarmonyLib;

namespace SandSpace
{
	internal class GameStartOrLoadHook
	{
		[HarmonyPatch (typeof (StarmapSetup), "SetupNewStarmap")]
		private static class StarmapSetup_SetupNewStarmap_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnNewGame ();
				PerkPatches.OnNewGame ();
				HangarsPatches.OnNewGame ();
			}
		}

		[HarmonyPatch (typeof (StarmapSetup), "OnLoadGame_2")]
		private static class StarmapSetup_OnLoadGame_2_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnLoadGame ();
				PerkPatches.OnGameLoad ();
				HangarsPatches.OnGameLoad ();
			}
		}
	}
}
