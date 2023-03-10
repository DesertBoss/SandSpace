using HarmonyLib;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SandSpace
{
	internal class NewGamePatches
	{
		private static bool campaignMode = false;

		// Вызов меню песочницы вместо старта компании
		internal static class SinglePlayerMenu_windowFunc_Patch
		{
			internal static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);

				for (var i = 0; i < codes.Count; i++)
				{
					if (codes[i].opcode == OpCodes.Ldc_I4_S &&
						(sbyte)codes[i].operand == 55)
					{
						codes[i].operand = (sbyte)16;
						break;
					}
				}

				return codes.AsEnumerable ();
			}
		}

		// Фикс режима песочницы при старте новой игры
		internal static class GameFlowManager_SetSandboxMode_Patch
		{
			internal static bool Prefix (ref bool sandbox)
			{
				if (sandbox == false)
					campaignMode = true;
				else
				if (sandbox == true && campaignMode)
					sandbox = false;

				return true;
			}
		}

		internal static class MenuManager_ActivateMenu_Patch
		{
			internal static bool Prefix (ref GameMenus menu)
			{
				if (menu == GameMenus.SINGLEPLAYER)
					campaignMode = false;

				return true;
			}
		}
	}
}
