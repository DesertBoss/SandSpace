namespace SandSpace
{
	internal class HazardPatches
	{
		// Патч для настройки силы и размеров всех взрывов в игре
		internal static class HazardManager_CreateAreaHazard_Patch
		{
			internal static bool Prefix (ref float damage, ref float force, ref float radius, ref float lifetime)
			{
				force = force * SandSpaceMod.Settings.HazardsAllForceMult;
				damage = damage * SandSpaceMod.Settings.HazardsAllDamageMult;

				var preRadius = radius;
				radius = preRadius * SandSpaceMod.Settings.HazardsAllSizeMult;
				lifetime = lifetime * (radius / preRadius);

				return true;
			}
		}

		// Патч для настройки силы и размеров взрывов только от кораблей и истребителей в игре
		internal static class ShockwaveGenerator_SpawnShockwave_Patch
		{
			internal static bool Prefix (ref float damage, ref float force, ref float radius, ref float lifetime)
			{
				force = force * SandSpaceMod.Settings.HazardsShockwaveForceMult;
				damage = damage * SandSpaceMod.Settings.HazardsShockwaveDamageMult;

				var preRadius = radius;
				radius = preRadius * SandSpaceMod.Settings.HazardsShockwaveSizeMult;
				lifetime = lifetime * (radius / preRadius);

				return true;
			}
		}
	}
}
