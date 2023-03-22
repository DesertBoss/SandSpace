using UnityEngine;

namespace SandSpace
{
	internal class ResourcesPatces
	{
		// Перехват в момент сбора игроком ресурса Рез
		// Патч с настройкой колличества Реза добываемого с астероидов
		internal static class PickupRez_OnPickedUp_Patch
		{
			internal static bool Prefix (ref PickupRez __instance, ref PickupGrabber grabber)
			{
				var newValue = __instance.pickupOverrideValue;
				if (newValue > 0)
				{
					var min = Mathf.FloorToInt (1 * SandSpaceMod.Settings.rezMinDropMult);
					var max = Mathf.FloorToInt (10 * SandSpaceMod.Settings.rezMaxDropMult);
					var rand = Random.Range (0, max + 2);
					newValue = Random.Range (min, rand);
				}
				else
				{
					newValue = Mathf.FloorToInt (newValue * SandSpaceMod.Settings.rezGlobalDropMult);
				}
				if (SandSpaceMod.Settings.rezDropMultFromLevel)
				{
					var playerLevel = grabber.GetShipControls ().GetBattleEntity ().GetCurrentLevel ();
					newValue *= playerLevel;
				}

				__instance.pickupOverrideValue = newValue;

				return true;
			}
		}
	}
}
