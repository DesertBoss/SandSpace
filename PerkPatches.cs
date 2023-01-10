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
			man.GetPerk (PerkType.Health_Inf).myPerkValue = SandSpaceMod.Settings.perkHealthInf / 100;
			man.GetPerk (PerkType.Armor_Inf).myPerkValue = SandSpaceMod.Settings.perkArmorInf / 100;
			man.GetPerk (PerkType.Capacitor_Inf).myPerkValue = SandSpaceMod.Settings.perkCapacitorInf / 100;
			man.GetPerk (PerkType.Reactor_Inf).myPerkValue = SandSpaceMod.Settings.perkReactorInf / 100;
			man.GetPerk (PerkType.WeaponDamage_Inf).myPerkValue = SandSpaceMod.Settings.perkWeaponDamageInf / 100;
			man.GetPerk (PerkType.Shield_Strength_Inf).myPerkValue = SandSpaceMod.Settings.perkShieldStrengthInf / 100;
			man.GetPerk (PerkType.StrikeCraftReserve_Inf).myPerkValue = SandSpaceMod.Settings.perkStrikeCraftReserveInf;

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
