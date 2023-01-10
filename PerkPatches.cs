using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace SandSpace
{
	internal class PerkPatches
	{
		[HarmonyPatch (typeof (StarmapSetup)), HarmonyPatch ("SetupNewStarmap")]
		private static class StarmapSetup_SetupNewStarmap_Patch
		{
			private static void Postfix ()
			{
				PerkOverride ();
			}
		}

		[HarmonyPatch (typeof (StarmapSetup)), HarmonyPatch ("OnLoadGame_2")]
		private static class StarmapSetup_OnLoadGame_2_Patch
		{
			private static void Postfix ()
			{
				PerkOverride ();
			}
		}

		private static void PerkOverride ()
		{
			var man = GameManager.GetPerkManager ();
			man.GetPerk (PerkType.Health_Inf).myPerkValue = 0.25f;
			man.GetPerk (PerkType.Armor_Inf).myPerkValue = 0.2f;
			man.GetPerk (PerkType.Capacitor_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.Reactor_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.WeaponDamage_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.Shield_Strength_Inf).myPerkValue = 0.2f;
			man.GetPerk (PerkType.StrikeCraftReserve_Inf).myPerkValue = 8f;
		}
	}
}
