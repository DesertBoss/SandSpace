namespace SandSpace
{
	internal class HazardPatches
	{
		// Патч для настройки силы и размеров всех взрывов в игре
		internal static class HazardManager_CreateAreaHazard_Patch
		{
			internal static bool Prefix (ref float damage, ref float force, ref float radius, ref float lifetime)
			{
				force = force * SandSpaceMod.Settings.hazardsAllForceMult;
				damage = damage * SandSpaceMod.Settings.hazardsAllDamageMult;

				var preRadius = radius;
				radius = preRadius * SandSpaceMod.Settings.hazardsAllSizeMult;
				lifetime = lifetime * (radius / preRadius);

				return true;
			}
		}

		// Патч для настройки силы и размеров взрывов только от кораблей и истребителей в игре
		internal static class ShockwaveGenerator_SpawnShockwave_Patch
		{
			internal static bool Prefix (ref float damage, ref float force, ref float radius, ref float lifetime)
			{
				force = force * SandSpaceMod.Settings.hazardsShockwaveForceMult;
				damage = damage * SandSpaceMod.Settings.hazardsShockwaveDamageMult;

				var preRadius = radius;
				radius = preRadius * SandSpaceMod.Settings.hazardsShockwaveSizeMult;
				lifetime = lifetime * (radius / preRadius);

				return true;
			}
		}
	}
}
