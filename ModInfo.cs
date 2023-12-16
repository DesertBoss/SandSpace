namespace SandSpace
{
	public struct ModInfo
	{
		public string ID { get; private set; }
		public string Name { get; private set; }
		public string Version { get; private set; }

		public ModInfo (string id, string name, string version)
		{
			ID = id;
			Name = name;
			Version = version;
		}
	}
}
