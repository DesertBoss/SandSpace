#if BEPINEX_RELEASE
using System.IO;
using BepInEx.Configuration;

namespace SandSpace.Loaders.BepInEx
{
	public class BepInExSettings : IModSettings
	{
		private ConfigFile _configFile;

		//don`t save
		private bool _changed = false;
		private bool _inGameLock = false;

		//Save to file all below
		public ConfigEntry<bool> _newGameNeed;

		//Hangars
		public ConfigEntry<int> maxActiveHangars;
		public ConfigEntry<int> hangar_1_unlockLevel;
		public ConfigEntry<int> hangar_2_unlockLevel;
		public ConfigEntry<int> hangar_3_unlockLevel;
		public ConfigEntry<int> hangar_4_unlockLevel;
		public ConfigEntry<int> hangar_Inf_unlockLevel;

		//Perks
		public ConfigEntry<int> perkCoreUnlockingPerLevel;

		//Infinite Perks
		public ConfigEntry<int> perkHealthInf;
		public ConfigEntry<int> perkArmorInf;
		public ConfigEntry<int> perkCapacitorInf;
		public ConfigEntry<int> perkReactorInf;
		public ConfigEntry<int> perkWeaponDamageInf;
		public ConfigEntry<int> perkShieldStrengthInf;
		public ConfigEntry<int> perkStrikeCraftReserveInf;

		//All explosions
		public ConfigEntry<bool> enableAllExplosionsPatch;
		public ConfigEntry<float> hazardsAllSizeMult;
		public ConfigEntry<float> hazardsAllDamageMult;
		public ConfigEntry<float> hazardsAllForceMult;

		//Explosions from ship destructions
		public ConfigEntry<bool> enableShockwaveExplosionsPatch;
		public ConfigEntry<float> hazardsShockwaveSizeMult;
		public ConfigEntry<float> hazardsShockwaveDamageMult;
		public ConfigEntry<float> hazardsShockwaveForceMult;

		//Resources
		public ConfigEntry<bool> enableRezDropPatch;
		public ConfigEntry<float> rezMinDropMult;
		public ConfigEntry<float> rezMaxDropMult;
		public ConfigEntry<bool> rezDropMultFromLevel;
		public ConfigEntry<float> rezGlobalDropMult;

		//Misc
		public ConfigEntry<bool> writeDefaultOnNewGame;

		//Experimental
		public ConfigEntry<bool> enableSandboxCampaign;
		public ConfigEntry<float> engineLinearDragMult;
		public ConfigEntry<float> engineAngularDragMult;
		public ConfigEntry<float> shipPartsBoosterMult;
		public ConfigEntry<float> stationPartsBoosterMult;
		public ConfigEntry<bool> enablePartCostPatch;
		public ConfigEntry<float> strikeCraftsStrengthMult;

		bool IModSettings.Changed => _changed;
		bool IModSettings.InGameLock => _inGameLock;
		bool IModSettings.NewGameNeed => _newGameNeed.Value;

		int IModSettings.MaxActiveHangars => maxActiveHangars.Value;
		int IModSettings.Hangar_1_unlockLevel { get => hangar_1_unlockLevel.Value; set => hangar_1_unlockLevel.Value = value; }
		int IModSettings.Hangar_2_unlockLevel { get => hangar_2_unlockLevel.Value; set => hangar_2_unlockLevel.Value = value; }
		int IModSettings.Hangar_3_unlockLevel { get => hangar_3_unlockLevel.Value; set => hangar_3_unlockLevel.Value = value; }
		int IModSettings.Hangar_4_unlockLevel { get => hangar_4_unlockLevel.Value; set => hangar_4_unlockLevel.Value = value; }
		int IModSettings.Hangar_Inf_unlockLevel => hangar_Inf_unlockLevel.Value;
		int IModSettings.PerkCoreUnlockingPerLevel => perkCoreUnlockingPerLevel.Value;
		int IModSettings.PerkHealthInf => perkHealthInf.Value;
		int IModSettings.PerkArmorInf => perkArmorInf.Value;
		int IModSettings.PerkCapacitorInf => perkCapacitorInf.Value;
		int IModSettings.PerkReactorInf => perkReactorInf.Value;
		int IModSettings.PerkWeaponDamageInf => perkWeaponDamageInf.Value;
		int IModSettings.PerkShieldStrengthInf => perkShieldStrengthInf.Value;
		int IModSettings.PerkStrikeCraftReserveInf => perkStrikeCraftReserveInf.Value;
		bool IModSettings.EnableAllExplosionsPatch => enableAllExplosionsPatch.Value;
		float IModSettings.HazardsAllSizeMult => hazardsAllSizeMult.Value;
		float IModSettings.HazardsAllDamageMult => hazardsAllDamageMult.Value;
		float IModSettings.HazardsAllForceMult => hazardsAllForceMult.Value;
		bool IModSettings.EnableShockwaveExplosionsPatch => enableShockwaveExplosionsPatch.Value;
		float IModSettings.HazardsShockwaveSizeMult => hazardsShockwaveSizeMult.Value;
		float IModSettings.HazardsShockwaveDamageMult => hazardsShockwaveDamageMult.Value;
		float IModSettings.HazardsShockwaveForceMult => hazardsShockwaveForceMult.Value;
		bool IModSettings.EnableRezDropPatch => enableRezDropPatch.Value;
		float IModSettings.RezMinDropMult => rezMinDropMult.Value;
		float IModSettings.RezMaxDropMult => rezMaxDropMult.Value;
		bool IModSettings.RezDropMultFromLevel => rezDropMultFromLevel.Value;
		float IModSettings.RezGlobalDropMult => rezGlobalDropMult.Value;
		bool IModSettings.WriteDefaultOnNewGame => writeDefaultOnNewGame.Value;
		bool IModSettings.EnableSandboxCampaign => enableSandboxCampaign.Value;
		float IModSettings.EngineLinearDragMult => engineLinearDragMult.Value;
		float IModSettings.EngineAngularDragMult => engineAngularDragMult.Value;
		float IModSettings.ShipPartsBoosterMult => shipPartsBoosterMult.Value;
		float IModSettings.StationPartsBoosterMult => stationPartsBoosterMult.Value;
		bool IModSettings.EnablePartCostPatch => enablePartCostPatch.Value;
		float IModSettings.StrikeCraftsStrengthMult => strikeCraftsStrengthMult.Value;


		public BepInExSettings (ConfigFile config)
		{
			_configFile = config;
			_configFile.SettingChanged += SettingChanged;
		}

		internal void BindAllSettings ()
		{
			_newGameNeed = _configFile.Bind<bool> ("General", "NewGameNeed", true, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { Browsable = false }));

			maxActiveHangars = _configFile.Bind<int> ("Hangars", "MaxActiveHangars", 4, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 20), new ConfigurationManagerAttributes () { DispName = "Max active hangars", Order = 34 }));
			hangar_1_unlockLevel = _configFile.Bind<int> ("Hangars", "Hangar_1_unlockLevel", 0, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (0, 100), new ConfigurationManagerAttributes () { DispName = "Hangar 1 unlock level", ShowRangeAsPercent = false, Order = 33 }));
			hangar_2_unlockLevel = _configFile.Bind<int> ("Hangars", "Hangar_2_unlockLevel", 0, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (0, 100), new ConfigurationManagerAttributes () { DispName = "Hangar 2 unlock level", ShowRangeAsPercent = false, Order = 32 }));
			hangar_3_unlockLevel = _configFile.Bind<int> ("Hangars", "Hangar_3_unlockLevel", 10, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (0, 100), new ConfigurationManagerAttributes () { DispName = "Hangar 3 unlock level", ShowRangeAsPercent = false, Order = 31 }));
			hangar_4_unlockLevel = _configFile.Bind<int> ("Hangars", "Hangar_4_unlockLevel", 20, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (0, 100), new ConfigurationManagerAttributes () { DispName = "Hangar 4 unlock level", ShowRangeAsPercent = false, Order = 30 }));
			hangar_Inf_unlockLevel = _configFile.Bind<int> ("Hangars", "Hangar_Inf_unlockLevel", 0, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (0, 100), new ConfigurationManagerAttributes () { DispName = "Hangar Inf unlock level increment", ShowRangeAsPercent = false, Order = 29 }));

			perkCoreUnlockingPerLevel = _configFile.Bind<int> ("Perks", "PerkCoreUnlockingPerLevel", 1, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 10), new ConfigurationManagerAttributes () { DispName = "Core blocks unlocking per level", Order = 28 }));

			perkHealthInf = _configFile.Bind<int> ("Infinite Perks", "PerkHealthInf", 25, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Health perk bonus", Order = 27 }));
			perkArmorInf = _configFile.Bind<int> ("Infinite Perks", "PerkArmorInf", 20, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Armor perk bonus", Order = 26 }));
			perkCapacitorInf = _configFile.Bind<int> ("Infinite Perks", "PerkCapacitorInf", 15, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Capacitor perk bonus", Order = 25 }));
			perkReactorInf = _configFile.Bind<int> ("Infinite Perks", "PerkReactorInf", 15, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Reactor perk bonus", Order = 24 }));
			perkWeaponDamageInf = _configFile.Bind<int> ("Infinite Perks", "PerkWeaponDamageInf", 15, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Weapon Damage perk bonus", Order = 24 }));
			perkShieldStrengthInf = _configFile.Bind<int> ("Infinite Perks", "PerkShieldStrengthInf", 20, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Shield Strength perk bonus", Order = 23 }));
			perkStrikeCraftReserveInf = _configFile.Bind<int> ("Infinite Perks", "PerkStrikeCraftReserveInf", 8, new ConfigDescription (string.Empty, new AcceptableValueRange<int> (1, 1000), new ConfigurationManagerAttributes () { DispName = "Strike Craft Reserve perk bonus", Order = 22 }));

			enableAllExplosionsPatch = _configFile.Bind<bool> ("All explosions", "EnableAllExplosionsPatch", false, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Enable", Order = 21 }));
			hazardsAllSizeMult = _configFile.Bind<float> ("All explosions", "HazardsAllSizeMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change size by multiplier", Order = 20 }));
			hazardsAllDamageMult = _configFile.Bind<float> ("All explosions", "HazardsAllDamageMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change damage by multiplier", Order = 19 }));
			hazardsAllForceMult = _configFile.Bind<float> ("All explosions", "HazardsAllForceMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change force by multiplier", Order = 18 }));

			enableShockwaveExplosionsPatch = _configFile.Bind<bool> ("Explosions from ship destructions", "EnableShockwaveExplosionsPatch", true, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Eneble", Order = 17 }));
			hazardsShockwaveSizeMult = _configFile.Bind<float> ("Explosions from ship destructions", "HazardsShockwaveSizeMult", 4f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change size by multiplier", Order = 16 }));
			hazardsShockwaveDamageMult = _configFile.Bind<float> ("Explosions from ship destructions", "HazardsShockwaveDamageMult", 0.2f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change damage by multiplier", Order = 15 }));
			hazardsShockwaveForceMult = _configFile.Bind<float> ("Explosions from ship destructions", "hazardsShockwaveForceMult", 0.2f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Change force by multiplier", Order = 14 }));

			enableRezDropPatch = _configFile.Bind<bool> ("Resources", "enableRezDropPatch", true, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Enable Rez overriding", Order = 13 }));
			rezMinDropMult = _configFile.Bind<float> ("Resources", "RezMinDropMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Min amount of Rez from asteroids multiplier", Order = 12 }));
			rezMaxDropMult = _configFile.Bind<float> ("Resources", "RezMaxDropMult", 2f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Max amount of Rez from asteroids multiplier", Order = 11 }));
			rezDropMultFromLevel = _configFile.Bind<bool> ("Resources", "RezDropMultFromLevel", false, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Enable player level as multiplier of Rez from asteroids", Order = 10 }));
			rezGlobalDropMult = _configFile.Bind<float> ("Resources", "RezGlobalDropMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Global multiplier of Rez drop", Order = 9 }));

			writeDefaultOnNewGame = _configFile.Bind<bool> ("Misc", "WriteDefaultOnNewGame", true, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Write default values when starting a new game", Order = 8 }));

			enableSandboxCampaign = _configFile.Bind<bool> ("Experimental", "EnableSandboxCampaign", false, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Enable sandbox settings menu when starting new campaign", Order = 7 }));
			engineLinearDragMult = _configFile.Bind<float> ("Experimental", "EngineLinearDragMult", 0.75f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Linear drag factor in outer space", Order = 6 }));
			engineAngularDragMult = _configFile.Bind<float> ("Experimental", "EngineAngularDragMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Angular drag factor in outer space", Order = 5 }));
			shipPartsBoosterMult = _configFile.Bind<float> ("Experimental", "ShipPartsBoosterMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Extra stats multiplier from rarity for ship parts", Order = 4 }));
			stationPartsBoosterMult = _configFile.Bind<float> ("Experimental", "StationPartsBoosterMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Extra stats multiplier from rarity for station parts", Order = 3 }));
			enablePartCostPatch = _configFile.Bind<bool> ("Experimental", "EnablePartCostPatch", false, new ConfigDescription (string.Empty, null, new ConfigurationManagerAttributes () { DispName = "Enable price fix for ship parts if applyed extra stats multiplier", Order = 2 }));
			strikeCraftsStrengthMult = _configFile.Bind<float> ("Experimental", "StrikeCraftsStrengthMult", 1f, new ConfigDescription (string.Empty, new AcceptableValueRange<float> (0.01f, 1000f), new ConfigurationManagerAttributes () { DispName = "Extra stats multiplier from rarity for strike crafts", Order = 1 }));

			maxActiveHangars.SettingChanged += ImportantSettingChanged;
			perkCoreUnlockingPerLevel.SettingChanged += ImportantSettingChanged;
			enableAllExplosionsPatch.SettingChanged += ImportantSettingChanged;
			enableShockwaveExplosionsPatch.SettingChanged += ImportantSettingChanged;
			enableRezDropPatch.SettingChanged += ImportantSettingChanged;
			enableSandboxCampaign.SettingChanged += ImportantSettingChanged;
			enablePartCostPatch.SettingChanged += ImportantSettingChanged;
		}

		private void SettingChanged (object sender, SettingChangedEventArgs e)
		{
			_changed = true;
		}

		private void ImportantSettingChanged (object sender, System.EventArgs e)
		{
			SandSpaceMod.ReloadHarmonyPatches ();
		}

		public void OnNewGame ()
		{
			_inGameLock = true;

			if (!writeDefaultOnNewGame.Value)
				return;

			SetDefaults ();

			_newGameNeed.Value = false;
			SaveToFile ();
		}

		public void OnGameLoad ()
		{
			_inGameLock = true;

			if (!_newGameNeed.Value)
				return;

			SetDefaults ();

			_newGameNeed.Value = false;
			SaveToFile ();
		}

		public void OnMainMenu ()
		{
			_inGameLock = false;
		}

		public void SaveToFile ()
		{
			if (_changed || !File.Exists (_configFile.ConfigFilePath))
				_configFile.Save ();
		}

		private void SetDefaults ()
		{
			foreach (var key in _configFile.Keys)
			{
				var entry = _configFile[key];
				entry.BoxedValue = entry.DefaultValue;
			}

			PerkPatches.SetDefaults ();
		}
	}
}
#endif