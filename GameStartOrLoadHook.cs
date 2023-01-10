using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace SandSpace
{
	internal class GameStartOrLoadHook
	{
		[HarmonyPatch (typeof (StarmapSetup)), HarmonyPatch ("SetupNewStarmap")]
		private static class StarmapSetup_SetupNewStarmap_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnNewGame ();
				PerkPatches.PerkOverride ();
				HangarsPatch.OnGameLoad ();
			}
		}

		[HarmonyPatch (typeof (StarmapSetup)), HarmonyPatch ("OnLoadGame_2")]
		private static class StarmapSetup_OnLoadGame_2_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnLoadGame ();
				PerkPatches.PerkOverride ();
				HangarsPatch.OnGameLoad ();
			}
		}
	}
}
