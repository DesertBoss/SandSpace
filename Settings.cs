using System.IO;
using UnityEngine;
using UnityModManagerNet;

namespace SandSpace
{
	public class Settings : UnityModManager.ModSettings, IDrawable
	{
		//┣┏┗┃━
		//╠╔╚║═
		internal bool _changed = false;

		internal bool _inGameLock = false;

		public bool _newGameNeed = true;

		[Header ("┏ Hangars")]
		
		[Draw ("┣━ Max active hangars", Min = 1, Max = 20, InvisibleOn = "_inGameLock|true")]
		public int maxActiveHangars = 4;

		[Draw ("┣━ Hangar 1 unlock level", Min = 0, Max = 100)]
		public int hangar_1_unlockLevel = 0;

		[Draw ("┣━ Hangar 2 unlock level", Min = 0, Max = 100)]
		public int hangar_2_unlockLevel = 0;

		[Draw ("┣━ Hangar 3 unlock level", Min = 0, Max = 100)]
		public int hangar_3_unlockLevel = 10;

		[Draw ("┣━ Hangar 4 unlock level", Min = 0, Max = 100)]
		public int hangar_4_unlockLevel = 20;

		[Draw ("┗━ Hangar Inf unlock level increment", Min = 0, Max = 100)]
		public int hangar_Inf_unlockLevel = 0;

		[Header ("┏ Perks"), Space (25f)]

		[Draw ("┣━ Core blocks unlocking per level", Min = 1, Max = 10)]
		public int perkCoreUnlockingPerLevel = 1;

		[Header ("┣━ Infinite Perks")]

		[Draw ("┣━━ Health perk bonus", Min = 1, Max = 1000)]
		public int perkHealthInf = 25;

		[Draw ("┣━━ Armor perk bonus", Min = 1, Max = 1000)]
		public int perkArmorInf = 20;

		[Draw ("┣━━ Capacitor perk bonus", Min = 1, Max = 1000)]
		public int perkCapacitorInf = 15;

		[Draw ("┣━━ Reactor perk bonus", Min = 1, Max = 1000)]
		public int perkReactorInf = 15;

		[Draw ("┣━━ Weapon Damage perk bonus", Min = 1, Max = 1000)]
		public int perkWeaponDamageInf = 15;

		[Draw ("┣━━ Shield Strength perk bonus", Min = 1, Max = 1000)]
		public int perkShieldStrengthInf = 20;

		[Draw ("┗━━ Strike Craft Reserve perk bonus", Min = 1, Max = 1000)]
		public int perkStrikeCraftReserveInf = 8;

		[Header ("┏ All explosions"), Space (25f)]

		[Draw ("┣━ Enable")]
		public bool enableAllExplosionsPatch = false;

		[Draw ("┣━ Change size by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableAllExplosionsPatch|true")]
		public float hazardsAllSizeMult = 1.0f;

		[Draw ("┣━ Change damage by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableAllExplosionsPatch|true")]
		public float hazardsAllDamageMult = 1.0f;

		[Draw ("┗━ Change force by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableAllExplosionsPatch|true")]
		public float hazardsAllForceMult = 1.0f;

		[Header ("┏ Explosions from ship destructions"), Space (25f)]

		[Draw ("┣━ Eneble")]
		public bool enableShockwaveExplosionsPatch = true;

		[Draw ("┣━ Change size by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableShockwaveExplosionsPatch|true")]
		public float hazardsShockwaveSizeMult = 4.0f;

		[Draw ("┣━ Change damage by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableShockwaveExplosionsPatch|true")]
		public float hazardsShockwaveDamageMult = 0.2f;

		[Draw ("┗━ Change force by multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableShockwaveExplosionsPatch|true")]
		public float hazardsShockwaveForceMult = 0.2f;

		[Header ("┏ Resources"), Space (25f)]

		[Draw ("┣━ Enable Rez overriding")]
		public bool enableRezDropPatch = true;

		[Draw ("┣━━ Min amount of Rez from asteroids multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableRezDropPatch|true")]
		public float rezMinDropMult = 1.0f;

		[Draw ("┣━━ Max amount of Rez from asteroids multiplier", Min = 0.01, Max = 1000, VisibleOn = "enableRezDropPatch|true")]
		public float rezMaxDropMult = 2.0f;

		[Draw ("┣━━ Enable player level as multiplier of Rez from asteroids")]
		public bool rezDropMultFromLevel = false;

		[Draw ("┗━━ Global multiplier of Rez drop", Min = 0.01, Max = 1000, VisibleOn = "enableRezDropPatch|true")]
		public float rezGlobalDropMult = 1.0f;

		[Header ("┏ Misc"), Space (25f)]

		[Draw ("┗━ Write default values when starting a new game")]
		public bool writeDefaultOnNewGame = true;

		[Header ("┏ Experimental"), Space (25f)]

		[Draw ("┣━━ Enable sandbox settings menu when starting new campaign")]
		public bool enableSandboxCampaign = false;

		[Draw ("┣━━ Linear drag factor in outer space", Min = 0.01, Max = 1000)]
		public float engineLinearDragMult = 0.75f;

		[Draw ("┣━━ Angular drag factor in outer space", Min = 0.01, Max = 1000)]
		public float engineAngularDragMult = 1.0f;

		[Draw ("┣━━ Extra stats multiplier from rarity for ship parts", Min = 0.01, Max = 1000)]
		public float shipPartsBoosterMult = 1.0f;

		[Draw ("┣━━ Extra stats multiplier from rarity for station parts", Min = 0.01, Max = 1000)]
		public float stationPartsBoosterMult = 1.0f;

		[Draw ("┣━━ Enable price fix for ship parts if applyed extra stats multiplier")]
		public bool enablePartCostPatch = false;

		[Draw ("┗━━ Extra stats multiplier from rarity for strike crafts", Min = 0.01, Max = 1000)]
		public float strikeCraftsStrengthMult = 1.0f;

		public override void Save (UnityModManager.ModEntry modEntry)
		{
			UnityModManager.ModSettings.Save<Settings> (this, modEntry);
		}

		public override string GetPath (UnityModManager.ModEntry modEntry)
		{
			return Path.Combine (modEntry.Path, "Settings.xml");
		}

		public void OnChange ()
		{
			_changed = true;
		}

		public void OnNewGame ()
		{
			_inGameLock = true;

			if (!writeDefaultOnNewGame)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		public void OnLoadGame ()
		{
			_inGameLock = true;

			if (!_newGameNeed)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		public void OnMainMenu ()
		{
			_inGameLock = false;
		}

		private void SetDefaults ()
		{
			var def = new Settings ();

			maxActiveHangars = def.maxActiveHangars;
			hangar_Inf_unlockLevel = def.hangar_Inf_unlockLevel;
			perkCoreUnlockingPerLevel = def.perkCoreUnlockingPerLevel;
			perkHealthInf = def.perkHealthInf;
			perkArmorInf = def.perkArmorInf;
			perkCapacitorInf = def.perkCapacitorInf;
			perkReactorInf = def.perkReactorInf;
			perkWeaponDamageInf = def.perkWeaponDamageInf;
			perkShieldStrengthInf = def.perkShieldStrengthInf;
			perkStrikeCraftReserveInf = def.perkStrikeCraftReserveInf;
			enableAllExplosionsPatch = def.enableAllExplosionsPatch;
			hazardsAllSizeMult = def.hazardsAllSizeMult;
			hazardsAllDamageMult = def.hazardsAllDamageMult;
			hazardsAllForceMult = def.hazardsAllForceMult;
			enableShockwaveExplosionsPatch = def.enableShockwaveExplosionsPatch;
			hazardsShockwaveSizeMult = def.hazardsShockwaveSizeMult;
			hazardsShockwaveDamageMult = def.hazardsShockwaveDamageMult;
			hazardsShockwaveForceMult = def.hazardsShockwaveForceMult;
			enableRezDropPatch = def.enableRezDropPatch;
			rezMinDropMult = def.rezMinDropMult;
			rezMaxDropMult = def.rezMaxDropMult;
			rezGlobalDropMult = def.rezGlobalDropMult;

			enableSandboxCampaign = def.enableSandboxCampaign;
			engineLinearDragMult = def.engineLinearDragMult;
			engineAngularDragMult = def.engineAngularDragMult;
			shipPartsBoosterMult = def.shipPartsBoosterMult;
			stationPartsBoosterMult = def.stationPartsBoosterMult;
			enablePartCostPatch = def.enablePartCostPatch;
			strikeCraftsStrengthMult = def.strikeCraftsStrengthMult;

			PerkPatches.SetDefaults ();
		}
	}
}
