using UnityModManagerNet;

namespace SandSpace
{
	internal class UMMLogger : UnityModManager.ModEntry.ModLogger, IModLogger
	{
		public UMMLogger (string Id) : base (Id)
		{
		}

		public void Log (object obj)
		{
			Log (obj.ToString ());
		}
	}
}
