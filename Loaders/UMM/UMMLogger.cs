#if UMM_RELEASE
using UnityModManagerNet;

namespace SandSpace.Loaders.UMM
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
#endif