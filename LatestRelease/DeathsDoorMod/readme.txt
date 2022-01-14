Add this to make Unity Mod Manager support game Death's Door to UnityModManagerConfig.xml

<GameInfo Name="Death's Door">
		<Folder>Death's Door</Folder>
		<ModsDirectory>Mods</ModsDirectory>
		<ModInfo>Info.json</ModInfo>
		<GameExe>DeathsDoor.exe</GameExe>
		<EntryPoint>[UnityEngine.UIModule.dll]UnityEngine.Canvas.cctor:Before</EntryPoint>
		<StartingPoint>[Assembly-CSharp.dll]UIMenu.Awake:After</StartingPoint>		
</GameInfo>