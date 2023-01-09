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
		[Header ("Test"), Space (10f), Draw ("Test")]
		public bool test = false;

		public override void Save (UnityModManager.ModEntry modEntry)
		{
			UnityModManager.ModSettings.Save<Settings> (this, modEntry);
		}

		public void OnChange ()
		{
			
		}

		public override string GetPath (UnityModManager.ModEntry modEntry)
		{
			return Path.Combine (modEntry.Path, "Settings.xml");
		}
	}
}
