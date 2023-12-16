using System;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;

namespace SandSpace
{
	internal static class UMMLoader
	{
		internal static UnityModManager.ModEntry ModEntry { get; private set; }
		internal static UMMSettings UMMSettings { get; private set; }

		private static bool Load (UnityModManager.ModEntry modEntry)
		{
			modEntry.OnGUI = OnGUI;
			modEntry.OnSaveGUI = OnSaveGUI;
			modEntry.OnUnload = OnUnload;

			UMMSettings = UnityModManager.ModSettings.Load<UMMSettings> (modEntry);
			SandSpaceMod.Settings = UMMSettings;
			SandSpaceMod.Logger = modEntry.Logger as UMMLogger;
			SandSpaceMod.Harmony = new Harmony (modEntry.Info.Id);
			SandSpaceMod.ModInfo = new ModInfo (modEntry.Info.Id, modEntry.Info.DisplayName, modEntry.Info.Version);

			try
			{
				SandSpaceMod.Harmony.PatchAll (Assembly.GetExecutingAssembly ());
				SandSpaceMod.DynamicPathes ();
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
			UMMSettings.Save (modEntry);
			SandSpaceMod.ReloadHarmonyPatches ();
		}

		private static void OnGUI (UnityModManager.ModEntry modEntry)
		{
			UMMSettings.Draw (modEntry);
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
			UMMSettings = null;

			return true;
		}
	}
}
