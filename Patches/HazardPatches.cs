using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace SandSpace
{
	internal class HazardPatches
	{
		[HarmonyPatch (typeof (HazardManager), "CreateAreaHazard")]
		private static class HazardManager_CreateAreaHazard_Patch
		{
			// HazardManager __instance
			private static bool Prefix (ref AreaHazard prefab, ref Vector3 position, ref float damage, ref float force, ref float radius, ref float lifetime)
			{
				if (force > 0.0f)
				{
					force = force * SandSpaceMod.Settings.hazardsForceMult;

					var preRadius = radius;
					radius = preRadius * SandSpaceMod.Settings.hazardsSizeMult;
					lifetime = lifetime * (radius / preRadius);
				}

				return true;
			}
		}
	}
}
