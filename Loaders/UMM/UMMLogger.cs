#if UMM_RELEASE
using UnityModManagerNet;

namespace SandSpace.Loaders.UMM
{
	internal class UMMLogger : IModLogger
	{
		UnityModManager.ModEntry.ModLogger _source;

		public UMMLogger (UnityModManager.ModEntry.ModLogger source)
		{
			_source = source;
		}

		public void Log (object obj)
		{
			_source.Log (obj.ToString ());
		}

		public void Log (string str)
		{
			_source.Log (str);
		}

		public void Warning (string str)
		{
			_source.Warning (str);
		}

		public void Error (string str)
		{
			_source.Error (str);
		}
	}
}
#endif