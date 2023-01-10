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
		public bool _newGameNeed = true;

		[Header ("Hangars"), Space (25f)]

		[Draw ("Max active hangars", Min = 1, Max = 20), Space (10f)]
		public int maxActiveHangars = 4;

		[Draw ("Hangar 1 unlock level", Min = 0, Max = 100, InvisibleOn = "_newGameNeed|true"), Space (10f)]
		public int hangar_1_unlockLevel = 0;

		[Draw ("Hangar 2 unlock level", Min = 0, Max = 100, InvisibleOn = "_newGameNeed|true"), Space (10f)]
		public int hangar_2_unlockLevel = 0;

		[Draw ("Hangar 3 unlock level", Min = 0, Max = 100, InvisibleOn = "_newGameNeed|true"), Space (10f)]
		public int hangar_3_unlockLevel = 0;

		[Draw ("Hangar 4 unlock level", Min = 0, Max = 100, InvisibleOn = "_newGameNeed|true"), Space (10f)]
		public int hangar_4_unlockLevel = 0;

		[Space (15f), Header ("Misc"), Space (25f)]

		[Draw ("Write default values when starting a new game"), Space (10f)]
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
		}

		public void OnNewGame ()
		{
			if (!writeDefOnNewGame)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		public void OnLoadGame ()
		{
			if (!_newGameNeed)
				return;

			SetDefaults ();

			_newGameNeed = false;
			Save (SandSpaceMod.ModEntry);
		}

		private void SetDefaults ()
		{
			PerkPatches.SetDefaults ();
		}
	}
}
