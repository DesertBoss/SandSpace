namespace SandSpace
{
	internal interface IModLogger
	{
		void Log (string str);
		void Log (object obj);
		void Warning (string str);
		void Error (string str);
	}
}
