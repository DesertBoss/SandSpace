using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace SandSpace
{
	internal class PerkPatches
	{
		internal static void PerkOverride ()
		{
			var man = GameManager.GetPerkManager ();
			man.GetPerk (PerkType.Health_Inf).myPerkValue = 0.25f;
			man.GetPerk (PerkType.Armor_Inf).myPerkValue = 0.2f;
			man.GetPerk (PerkType.Capacitor_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.Reactor_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.WeaponDamage_Inf).myPerkValue = 0.15f;
			man.GetPerk (PerkType.Shield_Strength_Inf).myPerkValue = 0.2f;
			man.GetPerk (PerkType.StrikeCraftReserve_Inf).myPerkValue = 8f;

			man.GetPerk (PerkType.StrikeCraftActive_1).myUnlockLevel = SandSpaceMod.Settings.hangar_1_unlockLevel;
			man.GetPerk (PerkType.StrikeCraftActive_2).myUnlockLevel = SandSpaceMod.Settings.hangar_2_unlockLevel;
			man.GetPerk (PerkType.StrikeCraftActive_3).myUnlockLevel = SandSpaceMod.Settings.hangar_3_unlockLevel;
			man.GetPerk (PerkType.StrikeCraftActive_4).myUnlockLevel = SandSpaceMod.Settings.hangar_4_unlockLevel;
		}

		internal static void SetDefaults ()
		{
			var man = GameManager.GetPerkManager ();

			SandSpaceMod.Settings.hangar_1_unlockLevel = man.GetPerk (PerkType.StrikeCraftActive_1).myUnlockLevel;
			SandSpaceMod.Settings.hangar_2_unlockLevel = man.GetPerk (PerkType.StrikeCraftActive_2).myUnlockLevel;
			SandSpaceMod.Settings.hangar_3_unlockLevel = man.GetPerk (PerkType.StrikeCraftActive_3).myUnlockLevel;
			SandSpaceMod.Settings.hangar_4_unlockLevel = man.GetPerk (PerkType.StrikeCraftActive_4).myUnlockLevel;
		}
	}
}
