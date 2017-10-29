# DownhillSkiingGame
A 3D downhill skiing game
I used this as a way to learn Unity and C# and how to develop a game from scratch

# In order to run Project:
1. Download zip file
2. Unzip the download file
3. Run "Knutson.DownhillSkiing executable
    Note: "Knutson.DownhillSkiing_Data" file must be in the same directory as the executable
-Instructions are in-game-

I utilized free assets from the Unity asset store to add objects to my game.

#The Player:
I used the RigiBodyFPSController from Standard Assets
I edited the movement script "RigidbodyFirstPersonController" to make the player continually move forward at a constant pace
I also edited the script to not allow the player to move uphill

#The Enviornment:
I built the slope and mountains in the environment
I downloaded a white texture to apply to the ground to act as snow
I wrote the UIController and used a base DataDictionary script to handle all of the variables correctly across gameObjects and scenes

#The Obstacles and Targets:
I wrote the TargetHit and ObstacleHit to handle when objects are interacted with
I used Unity's built in tools to create white spheres to act as snowballs
I created an invisible barrier between each set of flags to notify the program that the user correctly went between them
I handle when a tree is hit and how the user is reset to the beginning of the slope
The final target was also created by me
