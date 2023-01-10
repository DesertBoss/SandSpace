using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace SandSpace
{
	// SymbolExtensions
	// Transpilers

	internal class HangarsPatch
	{
		[HarmonyPatch (typeof (HangarConfig)), HarmonyPatch ("DisplayAllHangars")]
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
					}
				}

				return codes.AsEnumerable ();
			}
		}

		[HarmonyPatch (typeof (GameManager)), HarmonyPatch ("IsHangarUnlocked")]
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

		[HarmonyPatch (typeof (GameManager)), HarmonyPatch ("GetHangarDisplayLevel")]
		private static class GameManager_GetHangarDisplayLevel_Patch
		{
			private static void Postfix (ref int __result, ref int hangarIndex)
			{
				Core playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();

				if (hangarIndex > 3)
				{
					int myUnlockLevelInf = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4).myUnlockLevel;
					if (myUnlockLevelInf > playerCore.GetCurrentLevel ())
					{
						__result = myUnlockLevelInf;
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
