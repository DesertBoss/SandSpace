#if BEPINEX_RELEASE
using System;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace SandSpace.Loaders.BepInEx
{
	internal static class PluginInfo
	{
		public const string PLUGIN_GUID = "DesertBoss.SandSpace";
		public const string PLUGIN_NAME = "SandSpace";
		public const string PLUGIN_VERSION = SandSpaceMod.VERSION;
	}

	[BepInPlugin (PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	internal class BepInExLoader : BaseUnityPlugin
	{
		internal static BepInExLoader Instance { get; private set; }
		internal static BepInExSettings Settings { get; private set; }

		private void Awake ()
		{
			Instance = this;

			SandSpaceMod.Logger = Logger as BepInExLogger;
			SandSpaceMod.ModInfo = new ModInfo (PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION);
			SandSpaceMod.Harmony = new Harmony (SandSpaceMod.ModInfo.ID);

			Settings = new BepInExSettings (Config);
			Settings.BindAllSettings ();
			Settings.SaveToFile ();

			SandSpaceMod.Settings = Settings;

			try
			{
				SandSpaceMod.ApplyHarmonyPatches ();
				CheckForConfigurator ();
			}
			catch (Exception ex)
			{
				Logger.LogError ($"{ex.Message}\n{ex.StackTrace}");
			}

			Logger.LogInfo ($"Mod {PluginInfo.PLUGIN_NAME} Loaded");
		}

		private void CheckForConfigurator ()
		{
			var guid = "com.bepis.bepinex.configurationmanager";
			var configurator = Chainloader.PluginInfos.Any (kv => kv.Value.Metadata.GUID.Equals (guid));

			if (!configurator)
				return;

			var harmony = new Harmony ($"{PluginInfo.PLUGIN_GUID}.configurationmanager");
			harmony.Patch (
				AccessTools.Method (typeof (SpawnManager), "Update"),
				prefix: new HarmonyMethod (typeof (MainMenuPatches.SpawnManager_Update_Patch), "Prefix")
			);
		}
	}
}
#endif