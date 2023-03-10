using HarmonyLib;

namespace SandSpace
{
	internal class GameStartOrLoadHook
	{
		// Перехват во время старта новой игры
		[HarmonyPatch (typeof (StarmapSetup), nameof (StarmapSetup.SetupNewStarmap))]
		private static class StarmapSetup_SetupNewStarmap_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnNewGame ();
				PerkPatches.OnNewGame ();
				HangarsPatches.OnNewGame ();
			}
		}

		// Перехват во время загрузки игры
		[HarmonyPatch (typeof (StarmapSetup), nameof (StarmapSetup.OnLoadGame_2))]
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
