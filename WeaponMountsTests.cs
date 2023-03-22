using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SandSpace
{
	internal static class WeaponMountsTests
	{
		//MountConfigManager - хранит пресеты MountPicker под конкретный тип орудий
		//MountPicker - хранит точки позиций маунтов
		//MountPrefabLinker - точка для маунта

		internal static void AddSynergyMounts ()
		{
			var shipPartDatabase = GameManager.GetShipPartDatabase ();

			//AddSynergyMountsForPrefabs (shipPartDatabase);
			AddSynergyMountsForReal (shipPartDatabase);
		}

		internal static void AddSynergyMountsForPrefabs (ShipPartDatabase shipPartDatabase)
		{
			var partTypeDictionary = PatchingExtension.GetPrivateFieldValue<Dictionary<ItemType, List<ItemInfo>>> (shipPartDatabase, "partTypeDictionary");
			var partClassDictionary = PatchingExtension.GetPrivateFieldValue<Dictionary<ItemClass, List<ItemInfo>>> (shipPartDatabase, "partClassDictionary");

			var wings = new List<ItemInfo> ();
			wings.AddRange (partTypeDictionary[ItemType.LEFT_WING]);
			wings.AddRange (partTypeDictionary[ItemType.RIGHT_WING]);

			foreach (var wing in wings)
			{
				if (wing.myAttacherCount != 3)
					continue;

				RemakeAllPickersToMaxLinkers (wing);
				//AddSynergyPicker (wing);
			}
		}

		internal static void AddSynergyMountsForReal (ShipPartDatabase shipPartDatabase)
		{
			var partTypeDictionary = PatchingExtension.GetPrivateFieldValue<List<ItemInfo>> (shipPartDatabase, "realPartPrefabs");

			var wings = new List<ItemInfo> ();

			foreach (var part in partTypeDictionary)
			{
				if ((part.myType == ItemType.LEFT_WING || part.myType == ItemType.RIGHT_WING) && part.myAttacherCount == 3)
					wings.Add (part);
			}

			foreach (var wing in wings)
			{
				RemakeAllPickersToMaxLinkers (wing);
				//AddSynergyPicker (wing);
			}
		}

		internal static void RemakeAllPickersToMaxLinkers (ItemInfo part)
		{
			var mountConfig = part.GetMountConfigManager ();

			var mountPickers = mountConfig.GetComponentsInChildren<MountPicker> ();

			var allLinkersPositions = GetAllLinkersPositions (mountPickers);

			var nextPickerIdx = mountPickers.Length;

			foreach (var mountPicker in mountPickers)
			{
				var mountLinkers = mountPicker.GetComponentsInChildren<MountPrefabLinker> ();

				var nextLinkerIdx = mountLinkers.Length;

				var linkerPrefab = mountLinkers[0];

				var newLinkerName = linkerPrefab.name;

				var newLinkerPositions = new List<Vector3> (allLinkersPositions);

				foreach (var mountLinker in mountLinkers)
				{
					mountLinker.transform.localPosition = newLinkerPositions.RemoveFirst ();
				}

				foreach (var newLinkerPosition in newLinkerPositions)
				{
					var newLinker = GameObject.Instantiate (linkerPrefab);
					newLinker.transform.SetParent (mountPicker.transform);
					newLinker.transform.localPosition = newLinkerPosition;
					newLinker.name = newLinkerName;
					newLinker.uniqueIndex = nextLinkerIdx++;
					newLinker.mountType = linkerPrefab.mountType;
				}
			}
		}

		internal static void AddSynergyPicker (ItemInfo part)
		{
			var mountConfig = part.GetMountConfigManager ();

			var mountPickers = mountConfig.GetComponentsInChildren<MountPicker> ();

			var allLinkersPositions = GetAllLinkersPositions (mountPickers);

			var linkerPrefab = mountPickers[0].transform.GetChild (0).GetComponent<MountPrefabLinker> ();

			var synergyPicker = new GameObject ().AddComponent<MountPicker> ();
			synergyPicker.transform.SetParent (mountConfig.transform);
			synergyPicker.name = "Synergy";
			synergyPicker.uniqueIndex = 0;

			var nextLinkerIdx = 0;

			foreach (var newLinkerPosition in allLinkersPositions)
			{
				var newLinker = GameObject.Instantiate (linkerPrefab);
				newLinker.transform.SetParent (synergyPicker.transform);
				newLinker.transform.localPosition = newLinkerPosition;
				newLinker.name = $"Linker-{nextLinkerIdx}";
				newLinker.uniqueIndex = nextLinkerIdx++;
				newLinker.mountType = MountDatabaseType.COUNT;
			}
		}

		internal static List<Vector3> GetAllLinkersPositions (MountPicker[] mountPickers)
		{
			var allLinkersPositions = new List<Vector3> ();

			var firstLinkers = mountPickers[0].GetComponentsInChildren<MountPrefabLinker> ();
			foreach (var firstLinker in firstLinkers)
			{
				allLinkersPositions.Add (firstLinker.transform.localPosition);
			}

			foreach (var mountPicker in mountPickers)
			{
				var mountLinkers = mountPicker.GetComponentsInChildren<MountPrefabLinker> ();

				var cacheLinkersPositions = new List<Vector3> ();

				foreach (var mountLinker in mountLinkers)
				{
					var far = true;

					foreach (var allLinkersPosition in allLinkersPositions)
					{
						if (allLinkersPosition.Distance (mountLinker.transform.localPosition) > 1f)
							continue;

						far = false;
						break;
					}

					if (far) cacheLinkersPositions.Add (mountLinker.transform.localPosition);
				}

				allLinkersPositions.AddRange (cacheLinkersPositions);
			}

			return allLinkersPositions;
		}
	}
}
