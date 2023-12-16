using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace SandSpace
{
	internal class ShipPartsPatches
	{
		// Патч для настройки характеристик деталей для кораблей и станций
		[HarmonyPatch (typeof (ItemBuildInfo), nameof (ItemBuildInfo.GetBoosterMult))]
		private static class ItemBuildInfo_GetBoosterMult_Patch
		{
			private static void Postfix (ref ItemBuildInfo __instance, ref int __result)
			{
				ItemType itemType = __instance.GetPrefabRefObject().GetItemType();
				if (GameManager.GetShipPartDatabase ().IsStationPart (itemType))
					__result = Mathf.FloorToInt (__result * SandSpaceMod.Settings.StationPartsBoosterMult);
				else
					__result = Mathf.FloorToInt (__result * SandSpaceMod.Settings.ShipPartsBoosterMult);
			}
		}

		// Фикс цены деталей при изменении множителя характеристик
		//[HarmonyPatch (typeof (ShipPartDatabase), nameof (ShipPartDatabase.GetBuildInfoScrapPrice))]
		internal static class ShipPartDatabase_GetBuildInfoScrapPrice_Patch
		{
			internal static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);

				for (var i = 0; i < codes.Count; i++)
				{
					if (codes[i].opcode == OpCodes.Ldloc_S &&
						((LocalBuilder)codes[i].operand).LocalIndex == 5 &&
						codes[i + 1].opcode == OpCodes.Add &&
						codes[i + 2].opcode == OpCodes.Stloc_S &&
						((LocalBuilder)codes[i + 2].operand).LocalIndex == 9)
					{
						codes[i].opcode = OpCodes.Nop;
						codes[i + 1].opcode = OpCodes.Nop;
						break;
					}
				}

				return codes.AsEnumerable ();
			}
		}
	}
}
