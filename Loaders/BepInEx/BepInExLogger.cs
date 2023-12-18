#if BEPINEX_RELEASE
using BepInEx.Logging;

namespace SandSpace.Loaders.BepInEx
{
	internal class BepInExLogger : ManualLogSource, IModLogger
	{
		public BepInExLogger (string sourceName) : base (sourceName)
		{
		}

		public void Log (object obj)
		{
			Log (obj.ToString ());
		}

		public void Log (string str)
		{
			Log (LogLevel.Info, str);
		}

		public void Warning (string str)
		{
			Log (LogLevel.Warning, str);
		}

		public void Error (string str)
		{
			Log (LogLevel.Error, str);
		}
	}
}
#endif