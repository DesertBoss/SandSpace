namespace SandSpace
{
	internal class MainMenuPatches
	{
		internal static class SpawnManager_Update_Patch
		{
			internal static void Prefix (SpawnManager __instance)
			{
				__instance.ResetMainMenuBattleCountdown ();
			}
		}
	}
}
