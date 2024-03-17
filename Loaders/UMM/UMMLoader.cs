#if UMM_RELEASE
using System;
using HarmonyLib;
using UnityModManagerNet;

namespace SandSpace.Loaders.UMM
{
	internal static class UMMLoader
	{
		internal static UnityModManager.ModEntry ModEntry { get; private set; }
		internal static UMMSettings Settings { get; private set; }

		private static bool Load (UnityModManager.ModEntry modEntry)
		{
			ModEntry = modEntry;

			modEntry.OnGUI = OnGUI;
			modEntry.OnSaveGUI = OnSaveGUI;
			modEntry.OnUnload = OnUnload;

			SandSpaceMod.Logger = new UMMLogger (modEntry.Logger);
			SandSpaceMod.ModInfo = new ModInfo ($"{modEntry.Info.Author}.{modEntry.Info.Id}", modEntry.Info.Id, modEntry.Info.Version);
			SandSpaceMod.Harmony = new Harmony (SandSpaceMod.ModInfo.ID);

			Settings = UnityModManager.ModSettings.Load<UMMSettings> (modEntry);
			Settings.SaveToFile ();
			SandSpaceMod.Settings = Settings;

			try
			{
				SandSpaceMod.ApplyHarmonyPatches ();
				ApplyUnityModManagerUIFix ();
			}
			catch (Exception ex)
			{
				modEntry.Logger.Error ($"{ex.Message}\n{ex.StackTrace}");
				return false;
			}

			modEntry.Logger.Log ($"Mod {modEntry.Info.DisplayName} Loaded");
			return true;
		}

		private static void OnSaveGUI (UnityModManager.ModEntry modEntry)
		{
			Settings.Save (modEntry);
			SandSpaceMod.ReloadHarmonyPatches ();
		}

		private static void OnGUI (UnityModManager.ModEntry modEntry)
		{
			Settings.Draw (modEntry);
		}

		private static bool OnUnload (UnityModManager.ModEntry modEntry)
		{
			SandSpaceMod.Settings = null;
			SandSpaceMod.Logger = null;
			SandSpaceMod.Harmony.UnpatchAll (modEntry.Info.Id);
			SandSpaceMod.Harmony = null;

			modEntry.OnGUI = null;
			modEntry.OnSaveGUI = null;
			modEntry.OnUnload = null;

			ModEntry = null;
			Settings = null;

			return true;
		}

		private static void ApplyUnityModManagerUIFix ()
		{
			UnityModManagerUI.Harmony = new Harmony ($"{SandSpaceMod.ModInfo.ID}.uifix");
			UnityModManagerUI.Harmony.Patch (
				AccessTools.Method (typeof (UnityModManager.UI), nameof (UnityModManager.UI.ToggleWindow), new Type[] { typeof (bool) }),
				postfix: new HarmonyMethod (typeof (UnityModManagerUI), nameof (UnityModManagerUI.UI_ToggleWindow_Patch))
			);
		}
	}

	internal static class UnityModManagerUI
	{
		internal static Harmony Harmony { get; set; }

		internal static void UI_ToggleWindow_Patch (UnityModManager.UI __instance, bool open)
		{
			if (__instance.Opened)
				EnableCursorLockPatch ();
			else
				DisableCursorLockPatch ();
		}

		internal static void EnableCursorLockPatch ()
		{
			Harmony.Patch (
				AccessTools.Method (typeof (SpawnManager), "Update"),
				prefix: new HarmonyMethod (typeof (MainMenuPatches.SpawnManager_Update_Patch), "Prefix")
			);
		}

		internal static void DisableCursorLockPatch ()
		{
			Harmony.Unpatch (AccessTools.Method (typeof (SpawnManager), "Update"), HarmonyPatchType.Prefix);
		}
	}
}
#endif