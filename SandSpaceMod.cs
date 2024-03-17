using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace SandSpace
{
	public static class SandSpaceMod
	{
		public const string VERSION = "0.6.2";

		public static IModSettings Settings { get; internal set; }
		internal static IModLogger Logger { get; set; }
		public static ModInfo ModInfo { get; internal set; }
		internal static Harmony Harmony { get; set; }

		internal static void StaticPatches ()
		{
			Harmony.PatchAll (Assembly.GetExecutingAssembly ());
		}

		internal static void DynamicPathes ()
		{
			if (Settings.EnableAllExplosionsPatch)
				Harmony.Patch (
					AccessTools.Method (typeof (HazardManager), nameof (HazardManager.CreateAreaHazard)),
					prefix : new HarmonyMethod (typeof (HazardPatches.HazardManager_CreateAreaHazard_Patch), "Prefix")
				);

			if (Settings.EnableShockwaveExplosionsPatch)
				Harmony.Patch (
					AccessTools.Method (
						typeof (ShockwaveGenerator), nameof (ShockwaveGenerator.SpawnShockwave),
						new Type[] { typeof (Vector3), typeof (float), typeof (float), typeof (float), typeof (float), typeof (bool) }
					),
					prefix: new HarmonyMethod (typeof (HazardPatches.ShockwaveGenerator_SpawnShockwave_Patch), "Prefix")
				);

			if (Settings.EnableRezDropPatch)
				Harmony.Patch (
					AccessTools.Method (typeof (PickupRez), nameof (PickupRez.OnPickedUp)),
					prefix: new HarmonyMethod (typeof (ResourcesPatces.PickupRez_OnPickedUp_Patch), "Prefix")
				);

			if (Settings.EnableSandboxCampaign)
			{
				Harmony.Patch (
					AccessTools.Method (typeof (GameFlowManager), nameof (GameFlowManager.SetSandboxMode)),
					prefix: new HarmonyMethod (typeof (NewGamePatches.GameFlowManager_SetSandboxMode_Patch), "Prefix")
				);
				Harmony.Patch (
					AccessTools.Method (typeof (MenuManager), nameof (MenuManager.ActivateMenu)),
					prefix: new HarmonyMethod (typeof (NewGamePatches.MenuManager_ActivateMenu_Patch), "Prefix")
				);
				Harmony.Patch (
					AccessTools.Method (typeof (SinglePlayerMenu), nameof (SinglePlayerMenu.windowFunc)),
					transpiler: new HarmonyMethod (typeof (NewGamePatches.SinglePlayerMenu_windowFunc_Patch), "Transpiler")
				);
			}

			if (Settings.EnablePartCostPatch)
				Harmony.Patch (
					AccessTools.Method (typeof (ShipPartDatabase), nameof (ShipPartDatabase.GetBuildInfoScrapPrice)),
					transpiler: new HarmonyMethod (typeof (ShipPartsPatches.ShipPartDatabase_GetBuildInfoScrapPrice_Patch), "Transpiler")
				);
		}

		internal static void ApplyHarmonyPatches ()
		{
			StaticPatches ();
			DynamicPathes ();
		}

		internal static void ReloadHarmonyPatches ()
		{
			if (!Settings.Changed &&
				Settings.InGameLock)
				return;

			try
			{
				Harmony.UnpatchAll (ModInfo.ID);
				ApplyHarmonyPatches ();
			}
			catch (Exception ex)
			{
				Logger.Error ($"{ex.Message}\n{ex.StackTrace}");
			}
		}
	}
}
