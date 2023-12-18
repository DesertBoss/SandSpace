# SandSpace

This is a mod for the game "Space Pirates and Zombies 2" which uses the Unity Mod Manager or BepInEx to work. (final version)

## Features

- Changing number of active hangars
- Changing level requirement for active hangars
- Changing number of core blocks
- Changing perk bonuses after max player level
- Changing the strength and size of explosions
- Changing amount of Rez drop from asteroids
- Changing block bonus stats from rarity
- Sandbox settings menu when starting new campaign

## Installation

### For Unity Mod Manager

- Mod requires [Unity Mod Manager](https://www.nexusmods.com/site/mods/21) to work.

1. Add the following lines to the end of the file before ```</Config>``` in the UnityModManagerConfig.xml which is located in the Unity Mod Manager folder:
```
	<GameInfo Name="SPAZ 2">
		<Folder>SPAZ2</Folder>
		<ModsDirectory>Mods</ModsDirectory>
		<ModInfo>Info.json</ModInfo>
		<GameExe>SPAZ2_64.exe</GameExe>
		<StartingPoint>[Assembly-CSharp.dll]GameFlowManager.StartMainMenu:After</StartingPoint>
		<EntryPoint>[Assembly-CSharp.dll]GameFlowManager.StartMainMenu:After</EntryPoint>
		<MinimalManagerVersion>0.24.0</MinimalManagerVersion>
	</GameInfo>
```
2. Open Unity Mod Manager and select game from the Install tab.
3. Then change the installation method to DoorstopProxy and click Install.
4. Download the mod file from [releases](https://github.com/DesertBoss/SandSpace/releases) named SandSpace-UMM.
5. In the "Mods" tab, drag the archive into the program window.
5. Ready.

- Use the Unity Mod Manager UI to change mod settings.

### For BepInEx

1. Download the mod loader [BepInEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.22/BepInEx_x64_5.4.22.0.zip) and extract it to the game folder.
2. Download the mod file from [releases](https://github.com/DesertBoss/SandSpace/releases) named SandSpace-BepInEx.
3. Extract the mod files to the game folder.
4. Ready.

- To change mod settings, install [Configuration Manager](https://github.com/BepInEx/BepInEx.ConfigurationManager).

## Recommendations

- If you are running the mod for the first time and want to load a save, restart the game after loading.
- If you want to start a new game, then after entering the global map, save and restart the game.
- These actions are done once and are required to unlock all mod settings.
- All mod settings are desirable to do in the main menu.

## License
MIT
