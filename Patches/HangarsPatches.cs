using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace SandSpace
{
	internal class HangarsPatches
	{
		internal static void OnGameLoad ()
		{

		}

		internal static void OnNewGame ()
		{
			
		}

		internal static void OnSetupPerks ()
		{
			FixActiveStrikeCraftInBattle ();
		}

		// Фикс отображения всех ангаров в меню
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
						codes[i + 1].operand = SandSpaceMod.Settings.MaxActiveHangars;
						break;
					}
				}

				return codes.AsEnumerable ();
			}
		}

		// Фикс для возможности разблокировки ангаров
		[HarmonyPatch (typeof (GameManager), nameof (GameManager.IsHangarUnlocked))]
		private static class GameManager_IsHangarUnlocked_Patch
		{
			private static void Postfix (ref bool __result, ref int hangarIndex)
			{
				var playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();

				if (hangarIndex > 3 && playerCore.HasPerk (PerkType.StrikeCraftActive_4))
				{
					var perk = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4);
					var unlockLevel_4 = perk.myUnlockLevel;
					var unlockLevel_Inf = unlockLevel_4 + ((hangarIndex - 3) * SandSpaceMod.Settings.Hangar_Inf_unlockLevel);
					__result = playerCore.GetCurrentLevel () >= unlockLevel_Inf;
				}
			}
		}

		// Фикс для отображения необходимого уровня для разблокировки ангаров
		[HarmonyPatch (typeof (GameManager), nameof (GameManager.GetHangarDisplayLevel))]
		private static class GameManager_GetHangarDisplayLevel_Patch
		{
			private static void Postfix (ref int __result, ref int hangarIndex)
			{
				if (hangarIndex > 3)
				{
					var playerCore = StarmapManager.GetLevelSetup().GetPlayerCore();
					var unlockLevel_4 = GameManager.GetPerkManager().GetPerk(PerkType.StrikeCraftActive_4).myUnlockLevel;
					var unlockLevel_Inf = unlockLevel_4 + ((hangarIndex - 3) * SandSpaceMod.Settings.Hangar_Inf_unlockLevel);
					var unlock = playerCore.GetCurrentLevel () >= unlockLevel_Inf;
					__result = unlock ? -1 : unlockLevel_Inf;
				}
			}
		}

		// Фикс активных истребителей во время боя
		private static void FixActiveStrikeCraftInBattle ()
		{
			if (SandSpaceMod.Settings.MaxActiveHangars > 4)
			{
				var perk = GameManager.GetPerkManager ().GetPerk (PerkType.StrikeCraftActive_4);
				perk.myPerkValue = SandSpaceMod.Settings.MaxActiveHangars - 3;
			}
		}
	}
}
