using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityModManagerNet;

namespace SandSpace
{
	public class Settings : UnityModManager.ModSettings, IDrawable
	{
		//┣┏┗┃━
		//╠╔╚║═
		internal bool _changed = false;

		internal bool _inGameLock = true;

		public bool _newGameNeed = true;

		[Header ("┏ Hangars")]
		
		[Draw ("┣━ Max active hangars", Min = 1, Max = 20, InvisibleOn = "_inGameLock|true")]
		public int maxActiveHangars = 4;

		[Draw ("┣━ Hangar 1 unlock level", Min = 0, Max = 100)]
		public int hangar_1_unlockLevel = 0;

		[Draw ("┣━ Hangar 2 unlock level", Min = 0, Max = 100)]
		public int hangar_2_unlockLevel = 0;

		[Draw ("┣━ Hangar 3 unlock level", Min = 0, Max = 100)]
		public int hangar_3_unlockLevel = 10;

		[Draw ("┗━ Hangar 4 unlock level", Min = 0, Max = 100)]
		public int hangar_4_unlockLevel = 20;

		[Header ("┏ Perks"), Space (25f)]

		[Draw ("┣━ Core blocks unlocking per level", Min = 1, Max = 10)]
		public int perkCoreUnlockingPerLevel = 1;

		[Header ("┣━ Infinite Perks")]

		[Draw ("┣━━ Health perk bonus", Min = 1, Max = 1000)]
		public int perkHealthInf = 25;

		[Draw ("┣━━ Armor perk bonus", Min = 1, Max = 1000)]
		public int perkArmorInf = 20;

		[Draw ("┣━━ Capacitor perk bonus", Min = 1, Max = 1000)]
		public int perkCapacitorInf = 15;

		[Draw ("┣━━ Reactor perk bonus", Min = 1, Max = 1000)]
		public int perkReactorInf = 15;

		[Draw ("┣━━ Weapon Damage perk bonus", Min = 1, Max = 1000)]
		public int perkWeaponDamageInf = 15;

		[Draw ("┣━━ Shield Strength perk bonus", Min = 1, Max = 1000)]
		public int perkShieldStrengthInf = 20;

		[Draw ("┗━━ Strike Craft Reserve perk bonus", Min = 1, Max = 1000)]
		public int perkStrikeCraftReserveInf = 8;

		[Header ("┏ Misc"), Space (25f)]

		[Draw ("┗━ Write default values when starting a new game")]
		public bool writeDefOnNewGame = true;

		public override void Save (UnityModManager.ModEntry modEntry)
		{
			UnityModManager.ModSettings.Save<Settings> (this, modEntry);
		}

		public override string GetPath (UnityModManager.ModEntry modEntry)
		{
			return Path.Combine (modEntry.Path, "Settings.xml");
		}

		public void OnChange ()
		{
			_changed = true;
		}

		public void OnNewGame ()
		{
			_inGameLock = true;

			if (!writeDefOnNewGame)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		public void OnLoadGame ()
		{
			_inGameLock = true;

			if (!_newGameNeed)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		public void OnMainMenu ()
		{
			_inGameLock = false;
		}

		private void SetDefaults ()
		{
			PerkPatches.SetDefaults ();
		}
	}
}
