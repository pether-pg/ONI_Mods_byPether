Please search the project for TODO comments to see what must be changed.
Use Ctrl + Shift + F to search more than one file

After building, your mod .dll file will be located under the path:
ExampleFoodMod\bin\Release\ (.dll file here)
You can open the directory by right-clicking the project and selecting "Open Folder in File Explorer"

Put your mod files under this path:
%USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Dev\YourModName\(files here)

Make sure in that folder you have:
 - .dll file of your mod
 - mod.yaml file defining your mod data
 - mod_info.yaml file 

Additionally, you may have those in that folder:
 - anim\assets folder for each of your kanim
 - archived_versions folder for older versions of your mod
 - preview.png image or other files

If you want to add custom kanims, put the files under this path:
%USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Dev\YourModName\anim\assets\your_kanim_name\ (.png and .byte files here)
If you name your kanim folder "xyz", you will need to refer to the kanim inside your code using "xyz_kanim" name (eg. in your Food Item Config .cs file)

Make sure your mod.yaml file defines those values:

	# recommended you make this the same as your mod name in Steam
	title: "Your Mod Name" 
	# currently unused, but may be someday
	description: "Your Mod Description." 
	# a name for other mods to recognize you by, recommended to not change this once you set it
	staticID: "YourName.SomeIdForYourMod" 

Make sure your mod_info.yaml file defines those values:

	# either or both of VANILLA_ID, EXPANSION1_ID, ALL
	supportedContent: VANILLA_ID, EXPANSION1_ID
	# The build number of the last version of the game you tested your mod with
	minimumSupportedBuild: 567980
	# An arbitrary string that will be displayed to users on the mod screen. is prepended with a "v" in the UI
	version: "0.0.0.0"
	# Required if your mod includes DLLs to let the game know you've updated to Harmony2.0
	APIVersion: 2 