using HarmonyLib;

namespace SandSpace
{
	internal class MainMenuHook
	{
		// Перехват во время появления главного меню
		[HarmonyPatch (typeof (MenuManager), nameof (MenuManager.ActivateMenu))]
		private static class MenuManager_ActivateMenu_Patch
		{
			private static void Postfix (ref GameMenus menu)
			{
				if (menu == GameMenus.MAINMENU)
				{
					SandSpaceMod.Settings.OnMainMenu ();
				}
			}
		}
	}
}
