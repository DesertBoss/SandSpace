using HarmonyLib;

namespace SandSpace
{
	internal class MainMenuHook
	{
		[HarmonyPatch (typeof (MainMenu), "windowFunc")]
		private static class MainMenu_windowFunc_Patch
		{
			private static void Postfix ()
			{
				SandSpaceMod.Settings.OnMainMenu ();
			}
		}
	}
}
