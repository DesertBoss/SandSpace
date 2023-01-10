using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace SandSpace
{
	// SymbolExtensions
	// Transpilers

	internal class HangarsPatches
	{
		[HarmonyPatch (typeof (HangarConfig), "DisplayAllHangars")]
		private static class HangarConfig_DisplayAllHangars_Patch
		{
			private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
			{
				instructions.Manipulator (
					item => item.opcode == OpCodes.Ldc_I4_4,
					item => {
						item.opcode = OpCodes.Ldc_I4;
						item.operand = SandSpaceMod.Settings.maxActiveHangars;
					}
				);
				
				return instructions;
			}
		}

		[HarmonyPatch (typeof (GameManager), "IsHangarUnlocked")]
		private static class GameManager_IsHangarUnlocked_Patch
		{
			private static void Postfix (ref bool __result, ref int hangarIndex)
			{
				Core playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();

				if (hangarIndex > 3 && playerCore.HasPerk (PerkType.StrikeCraftActive_4))
				{
					__result = true;
				}
			}
		}

		[HarmonyPatch (typeof (GameManager), "GetHangarDisplayLevel")]
		private static class GameManager_GetHangarDisplayLevel_Patch
		{
			private static void Postfix (ref int __result, ref int hangarIndex)
			{
				Core playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();

				if (hangarIndex > 3)
				{
					int unlockLevelInf = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4).myUnlockLevel;
					if (unlockLevelInf > playerCore.GetCurrentLevel ())
					{
						__result = unlockLevelInf;
					}
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
