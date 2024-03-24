# README for the master-thesis-unity repository
This readme document contains the following sections:
1. Clone or fork the repository 
2. Setting up Unity
3. Understanding the code
4. Running the scene

## 1. Fork the repository 
1. Fork this repository so you can make changes yourself
2. From your own forked repository (so not from the one you forked), copy the HTTPS link.
3. Open Git Bash
4. Browse to the location where you want to clone using: ```cd path\to\project\directory```
5. Then clone the forked repository :```git clone <<<your-repository-url>>>```
6. The repository is now located on your computer


## 2. Setting up Unity
1. In order to run the environment, the following should be downloaded
	-	UNITY version 2021.3.11f1 (it does not work with another version)
	-	SteamVR (first download steam -> setup account -> download steamVR through steam.
  Make sure that the steamVR is running on open XR (Go to settings -> developer and turn openXR on)
	- Download SR_anipal SDK-v1.3.3.0 and extract files on a location on your computer (https://developer.vive.com/resources/vive-sense/eye-and-facial-tracking-sdk/download/archive/1_3_3_0/)
	-	Install SR_anipal runtime (https://forum.htc.com/topic/5642-sranipal-getting-started-steps/)
		- First apply for sdk access
		- Then download the VIVE_SRanipalInstaller_1.3.2.0.msi, execute and follow the instructions (so do **NOT** use the SDK-v1.3.6.6)

2.	Open UNITY HUB -> Open -> Add project from disk -> Select the reposity directory that you cloned in step 1. Unity should now open the game.
	-	An error will pop-up and you need to click Continue.
	-	Another pop-up asks if you want to enter safe mode, do **NOT** do this, and click continue.

3.	Setup your VR goggles in steam VR (use HTC vive pro eye)
	- Left mouse button on the steamVR window -> room setup -> follow the instructions

4.	Open the scene
	- If the scene is already opened, skip this step.
	- In unity, go to the project folder and click Scenes.
	- In the Scenes directory, double click Test supermarket environment and the scene should open.

5. Remove the initial com.tobii.xr.sdk 
	- In the top, select Window -> Package Manager.
	- Select com.tobii.xr.sdk and remove it.

6. Import the sdk-v1.3.3.0 that you just downloaded
	- In the top menu go to Assets -> Import Package -> Custom package
	- Locate the place where you installed the SDK v1.3.3.0
	- When found: SDK-v1.3.3.0 -> 02_Unity -> Vive-SRanipal-Unity-Plugin.unitypackage

7.	Import the TobiiXR package 
	- In the top menu: Window -> Package manager
	- A window opens: click the + icon -> Add package from disk -> locate the place where you installed TobiiXR
	- When found: TobiiXR -> package.json

8. Turn VR loader of
	- In the top menu: Edit -> Project settings -> turn OpenVR loader off

9.	Put on the HTC vive and push the button on the left of the goggle. This will open the home page of the steam VR. In the left corner below, an eye icon can be seen. Select this icon and turn eye tracking on. Calibrate the eye tracking and close the home page. 

10.	The scene is set up correctly! Proceed to the next section to understand the code or jump to the last section to run the scene.

## 3. The code 
The code could be found in the Assets directory, under the scripts folder. The function of the code speaks for itself, but for convenience comments are added to the code to make it even more comprehensive. Moreover, in the thesis report an explanation of the most important scripts is provided. If there are any questions, please don't hessitate to contact me.

