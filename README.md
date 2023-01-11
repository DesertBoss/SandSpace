# SandSpace
This is a mod for the game "SPAZ 2" which uses the Unity Mod Manager to work.

## Features
- Changing number of active hangars
- Changing level requirement for active hangars
- Changing number of core blocks
- Changing perk bonuses after max player level
- Changing the strength and size of explosions
- Changing amount of Rez drop from asteroids

## In developing
- Block rarity bonus settings
- Add more turrets to support ships depending on their size
- Add more turrets to blocks

## Installation
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
4. Download the mod file from [releases](https://github.com/DesertBoss/SandSpace/releases), then in the "Mods" tab, drag the archive into the program window.
5. Ready.

- Use the Unity Mod Manager UI to change mod settings.

## Recommendations
- If you are running the mod for the first time and want to load a save, restart the game after loading.
- If you want to start a new game, then after entering the global map, save and restart the game.
- These actions are done once and are required to unlock all mod settings.
- All mod settings are desirable to do in the main menu.

## License
MIT
