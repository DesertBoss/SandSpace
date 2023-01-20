using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace SandSpace.Patches
{
	internal class ShipPartsPatches
	{
		[HarmonyPatch (typeof (ItemBuildInfo), "GetBoosterMult")]
		private static class ItemBuildInfo_GetBoosterMult_Patch
		{
			private static void Postfix (ref ItemBuildInfo __instance, ref int __result)
			{
				ItemType itemType = __instance.GetPrefabRefObject().GetItemType();
				if (GameManager.GetShipPartDatabase ().IsStationPart (itemType))
					__result = Mathf.FloorToInt (__result * SandSpaceMod.Settings.stationPartsBoosterMult);
				else
					__result = Mathf.FloorToInt (__result * SandSpaceMod.Settings.shipPartsBoosterMult);
			}
		}
	}
}
