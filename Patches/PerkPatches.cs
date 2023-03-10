using System.Collections.Generic;
using HarmonyLib;

namespace SandSpace
{
	internal class PerkPatches
	{
		internal static void OnGameLoad ()
		{
			
		}

		internal static void OnNewGame ()
		{
			
		}

		// Перехват во время инициализации всех перков в игре
		[HarmonyPatch (typeof (PerkCategoryManager), nameof (PerkCategoryManager.SetupPerks))]
		private static class PerkCategoryManager_SetupPerks_Patch
		{
			private static void Postfix (ref PerkCategoryManager __instance)
			{
				PerkOverride (ref __instance);
				HangarsPatches.OnSetupPerks ();
				FixMaxLevelFromPerks (ref __instance);
				FixInfPerksUnlockLevel (ref __instance);
			}
		}

		[HarmonyPatch (typeof (PerkCategoryManager), "SetCoreUnlocking")]
		private static class PerkCategoryManager_SetCoreUnlocking_Patch
		{
			private static void Postfix ()
			{
				CoreUnlockingPatch ();
			}
		}

		// Патч для изменения лимита блоков за уровень игрока
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

		// Фикс от софтлока из-за неправильного уровня раблокировки бесконечных навыков
		private static void FixMaxLevelFromPerks (ref PerkCategoryManager manager)
		{
			var perkLookup = PatchingExtension.GetPrivateFieldValue<Perk[]> (manager, "perkLookup");
			var maxLvl = PatchingExtension.GetPrivateFieldValue<int> (manager, "maxLevel");
			var newMaxLvl = 0;

			foreach (var perk in perkLookup)
			{
				if (perk != null && perk.myUnlockLevel > 0 && !perk.isInfinite &&
					!(perk.myClass == PerkClass.StrikeCraftSize || perk.myClass == PerkClass.Core 
					|| perk.myClass == PerkClass.PartSize || perk.myClass == PerkClass.StarBase 
					|| perk.myClass == PerkClass.Bounty || perk.myClass == PerkClass.Cloaking))
					newMaxLvl++;
			}

			if (maxLvl != newMaxLvl)
				SandSpaceMod.ModEntry.Logger.Log ($"FixMaxLevelFromPerks: old {maxLvl}, new {newMaxLvl}");

			PatchingExtension.SetPrivateFieldValue (manager, "maxLevel", newMaxLvl);
		}

		// Фикс от софтлока из-за неправильного уровня раблокировки бесконечных навыков
		private static void FixInfPerksUnlockLevel (ref PerkCategoryManager manager)
		{
			var maxLvl = PatchingExtension.GetPrivateFieldValue<int> (manager, "maxLevel");

			manager.GetPerk (PerkType.Armor_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.Capacitor_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.Health_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.Reactor_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.Shield_Strength_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.StrikeCraftReserve_Inf).myUnlockLevel = maxLvl + 1;
			manager.GetPerk (PerkType.WeaponDamage_Inf).myUnlockLevel = maxLvl + 1;
		}

		// Применение настроек мода для перков
		internal static void PerkOverride (ref PerkCategoryManager manager)
		{
			manager.GetPerk (PerkType.Health_Inf).myPerkValue = SandSpaceMod.Settings.perkHealthInf / 100f;
			manager.GetPerk (PerkType.Armor_Inf).myPerkValue = SandSpaceMod.Settings.perkArmorInf / 100f;
			manager.GetPerk (PerkType.Capacitor_Inf).myPerkValue = SandSpaceMod.Settings.perkCapacitorInf / 100f;
			manager.GetPerk (PerkType.Reactor_Inf).myPerkValue = SandSpaceMod.Settings.perkReactorInf / 100f;
			manager.GetPerk (PerkType.WeaponDamage_Inf).myPerkValue = SandSpaceMod.Settings.perkWeaponDamageInf / 100f;
			manager.GetPerk (PerkType.Shield_Strength_Inf).myPerkValue = SandSpaceMod.Settings.perkShieldStrengthInf / 100f;
			manager.GetPerk (PerkType.StrikeCraftReserve_Inf).myPerkValue = SandSpaceMod.Settings.perkStrikeCraftReserveInf;

			manager.GetPerk (PerkType.StrikeCraftActive_1).myUnlockLevel = SandSpaceMod.Settings.hangar_1_unlockLevel;
			manager.GetPerk (PerkType.StrikeCraftActive_2).myUnlockLevel = SandSpaceMod.Settings.hangar_2_unlockLevel;
			manager.GetPerk (PerkType.StrikeCraftActive_3).myUnlockLevel = SandSpaceMod.Settings.hangar_3_unlockLevel;
			manager.GetPerk (PerkType.StrikeCraftActive_4).myUnlockLevel = SandSpaceMod.Settings.hangar_4_unlockLevel;
		}

		// Возврат к стандартным параметрам
		internal static void SetDefaults ()
		{
			var manager = GameManager.GetPerkManager ();

			SandSpaceMod.Settings.hangar_1_unlockLevel = manager.GetPerk (PerkType.StrikeCraftActive_1).myUnlockLevel;
			SandSpaceMod.Settings.hangar_2_unlockLevel = manager.GetPerk (PerkType.StrikeCraftActive_2).myUnlockLevel;
			SandSpaceMod.Settings.hangar_3_unlockLevel = manager.GetPerk (PerkType.StrikeCraftActive_3).myUnlockLevel;
			SandSpaceMod.Settings.hangar_4_unlockLevel = manager.GetPerk (PerkType.StrikeCraftActive_4).myUnlockLevel;
		}
	}
}
