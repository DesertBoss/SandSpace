using System.Collections.Generic;
using HarmonyLib;

namespace SandSpace
{
	internal class PerkPatches
	{
		internal static void OnGameLoad ()
		{
			PerkOverride ();
		}

		internal static void OnNewGame ()
		{
			PerkOverride ();
		}

		[HarmonyPatch (typeof (PerkCategoryManager), "SetCoreUnlocking")]
		private static class PerkCategoryManager_SetCoreUnlocking_Patch
		{
			private static void Postfix ()
			{
				PerkPatches.CoreUnlockingPatch ();
			}
		}

		private static void CoreUnlockingPatch ()
		{
			var man = GameManager.GetPerkManager ();
			var startIdx = 5;
			var totalCorePerks = PatchingExtension
				.GetPrivateFieldValue<Dictionary<PerkClass, List<PerkType>>> (man, "perkClassLookup")
				[PerkClass.Core]
				.Count;

			for (var i = 1; i < totalCorePerks; i++) // First CorePerk skip
			{
				var cur = startIdx + i;
				var perkType = (PerkType)cur;
				man.GetPerk (perkType).myPerkValue = SandSpaceMod.Settings.perkCoreUnlockingPerLevel;
			}
		}

		internal static void PerkOverride ()
		{
			var man = GameManager.GetPerkManager ();
			man.GetPerk (PerkType.Health_Inf).myPerkValue = SandSpaceMod.Settings.perkHealthInf / 100f;
			man.GetPerk (PerkType.Armor_Inf).myPerkValue = SandSpaceMod.Settings.perkArmorInf / 100f;
			man.GetPerk (PerkType.Capacitor_Inf).myPerkValue = SandSpaceMod.Settings.perkCapacitorInf / 100f;
			man.GetPerk (PerkType.Reactor_Inf).myPerkValue = SandSpaceMod.Settings.perkReactorInf / 100f;
			man.GetPerk (PerkType.WeaponDamage_Inf).myPerkValue = SandSpaceMod.Settings.perkWeaponDamageInf / 100f;
			man.GetPerk (PerkType.Shield_Strength_Inf).myPerkValue = SandSpaceMod.Settings.perkShieldStrengthInf / 100f;
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
