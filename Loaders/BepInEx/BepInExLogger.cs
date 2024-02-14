#if BEPINEX_RELEASE
using BepInEx.Logging;

namespace SandSpace.Loaders.BepInEx
{
	internal class BepInExLogger : IModLogger
	{
		ManualLogSource _source;

		public BepInExLogger (ManualLogSource source)
		{
			_source = source;
		}

		public void Log (object obj)
		{
			_source.Log (LogLevel.Info, obj.ToString ());
		}

		public void Log (string str)
		{
			_source.Log (LogLevel.Info, str);
		}

		public void Warning (string str)
		{
			_source.Log (LogLevel.Warning, str);
		}

		public void Error (string str)
		{
			_source.Log (LogLevel.Error, str);
		}
	}
}
#endif