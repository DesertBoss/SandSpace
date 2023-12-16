namespace SandSpace
{
	public interface IModSettings
	{
		bool Changed { get; }
		bool InGameLock { get; }
		bool NewGameNeed { get; }

		int MaxActiveHangars { get; }
		int Hangar_1_unlockLevel { get; set; }
		int Hangar_2_unlockLevel { get; set; }
		int Hangar_3_unlockLevel { get; set; }
		int Hangar_4_unlockLevel { get; set; }
		int Hangar_Inf_unlockLevel { get; }
		int PerkCoreUnlockingPerLevel { get; }
		int PerkHealthInf { get; }
		int PerkArmorInf { get; }
		int PerkCapacitorInf { get; }
		int PerkReactorInf { get; }
		int PerkWeaponDamageInf { get; }
		int PerkShieldStrengthInf { get; }
		int PerkStrikeCraftReserveInf { get; }
		bool EnableAllExplosionsPatch { get; }
		float HazardsAllSizeMult { get; }
		float HazardsAllDamageMult { get; }
		float HazardsAllForceMult { get; }
		bool EnableShockwaveExplosionsPatch { get; }
		float HazardsShockwaveSizeMult { get; }
		float HazardsShockwaveDamageMult { get; }
		float HazardsShockwaveForceMult { get; }
		bool EnableRezDropPatch { get; }
		float RezMinDropMult { get; }
		float RezMaxDropMult { get; }
		bool RezDropMultFromLevel { get; }
		float RezGlobalDropMult { get; }
		bool WriteDefaultOnNewGame { get; }
		bool EnableSandboxCampaign { get; }
		float EngineLinearDragMult { get; }
		float EngineAngularDragMult { get; }
		float ShipPartsBoosterMult { get; }
		float StationPartsBoosterMult { get; }
		bool EnablePartCostPatch { get; }
		float StrikeCraftsStrengthMult { get; }

		void OnGameLoad ();
		void OnNewGame ();
		void OnMainMenu ();
	}
}
