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
	}
}
#endif