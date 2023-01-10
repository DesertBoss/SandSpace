using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityModManagerNet;
using HarmonyLib;
using System.Reflection;

namespace SandSpace
{
	public class SandSpaceMod
	{
		public const string version = "0.0.1";

		public static UnityModManager.ModEntry ModEntry { get; private set; }
		public static Settings Settings { get; private set; }
		public static Harmony Harmony { get; private set; }

		private static bool Load (UnityModManager.ModEntry modEntry)
		{
			ModEntry = modEntry;

			modEntry.OnGUI = OnGUI;
			modEntry.OnSaveGUI = OnSaveGUI;
			modEntry.OnUnload = OnUnload;

			Settings = UnityModManager.ModSettings.Load<Settings> (modEntry);
			Harmony = new Harmony (modEntry.Info.Id);

			try
			{
				Harmony.PatchAll (Assembly.GetExecutingAssembly ());
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
		}

		private static void OnGUI (UnityModManager.ModEntry modEntry)
		{
			UnityModManagerNet.Extensions.Draw (Settings, modEntry);
		}

		private static bool OnUnload (UnityModManager.ModEntry modEntry)
		{
			ModEntry = null;
			Settings = null;
			Harmony.UnpatchAll (modEntry.Info.Id);
			Harmony = null;

			return true;
		}
	}
}
