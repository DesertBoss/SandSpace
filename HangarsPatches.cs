using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace SandSpace
{
	// SymbolExtensions
	// Transpilers
	// AccessTools

	internal class HangarsPatches
	{
		[HarmonyPatch (typeof (HangarConfig), "DisplayAllHangars")]
		private static class HangarConfig_DisplayAllHangars_Patch
		{
			private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);

				for (var i = 0; i < codes.Count; i++)
				{
					if (codes[i].opcode == OpCodes.Ldloc_0 &&
						codes[i + 1].opcode == OpCodes.Ldc_I4_4 &&
						codes[i + 2].opcode == OpCodes.Blt)
					{
						codes[i + 1].opcode = OpCodes.Ldc_I4;
						codes[i + 1].operand = SandSpaceMod.Settings.maxActiveHangars;
						return codes.AsEnumerable ();
					}
				}

				return codes.AsEnumerable ();
			}
		}

		[HarmonyPatch (typeof (GameManager), "IsHangarUnlocked")]
		private static class GameManager_IsHangarUnlocked_Patch
		{
			private static void Postfix (ref bool __result, ref int hangarIndex)
			{
				var playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();

				if (hangarIndex > 3 && playerCore.HasPerk (PerkType.StrikeCraftActive_4))
				{
					var perk = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4);
					var unlockLevel_4 = perk.myUnlockLevel;
					var unlockLevel_Inf = unlockLevel_4 + ((hangarIndex - 3) * SandSpaceMod.Settings.hangar_Inf_unlockLevel);
					__result = playerCore.GetCurrentLevel () >= unlockLevel_Inf;
				}
			}
		}

		[HarmonyPatch (typeof (GameManager), "GetHangarDisplayLevel")]
		private static class GameManager_GetHangarDisplayLevel_Patch
		{
			private static void Postfix (ref int __result, ref int hangarIndex)
			{
				if (hangarIndex > 3)
				{
					var playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();
					var unlockLevel_4 = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4).myUnlockLevel;
					var unlockLevel_Inf = unlockLevel_4 + ((hangarIndex - 3) * SandSpaceMod.Settings.hangar_Inf_unlockLevel);
					var unlock = playerCore.GetCurrentLevel () >= unlockLevel_Inf;
					__result = unlock ? -1 : unlockLevel_Inf;
				}
			}
		}

		private static void FixActiveInBattle ()
		{
			if (SandSpaceMod.Settings.maxActiveHangars > 4)
			{
				var perk = GameManager.GetPerkManager ().GetPerk (PerkType.StrikeCraftActive_4);
				perk.myPerkValue = SandSpaceMod.Settings.maxActiveHangars - 3;
			}
		}

		internal static void OnGameLoad ()
		{
			FixActiveInBattle ();
		}

		internal static void OnNewGame ()
		{
			FixActiveInBattle ();
		}
	}
}
