---------TO DO-----------

- Rose des vents
- wheel tracks + particles
- time highscores


To create a new vehicle:

// INSTRUCTIONS NOT UP TO DATE (16/03/2019)//
1. Put mesh in scene, and give it the "Player" tag.
2. Attach RigidBody component to it.
3. Attach Vehicle script and Player Input script to it.
4. Create empty GameObject called "Wheels" (has to be exact spelling), and make all wheels child of "Wheels".
   Then, tag the front Wheels as "Front Wheel". Then, put Wheel Collider components on all wheels, and ajust radius to fit wheel.
5. Put a Mesh Collider component on the Chassis, make it Convex.
6. Put Mesh Collider component on Sail, and attach Sail script on it.
7. Add Rigidbody component on Sail, and freeze all rotations and positions of the Rigidbody.
8. Attach Camera GameObject to Vehicle and position it as wanted.
9. Attach CameraScript script to Main Camera.
10. (Optional) Save it as a prefab.


-----------------GOOD TO KNOW-----------------
(up to date as of 28/03/2019)

- When moving wheel colliders' transforms, no need to reajust the mesh's transform. At playtime, meshes should
	automatically align with collider.

- No need to put checkpoint on start position; script knows the first checkpoint will be the start position.

-----------------PARAMETERS-------------------
(up to date as of 28/03/2019)

VEHICLE SCRIPT

- Player Input
	Steer Intensity: speed at which the front wheel(s) rotate
		Default: 1

	Max Steer Angle: Max angle of front wheel in degrees
		Default: 50

- Physics
	Max Speed: max attainable speed of vehicle, regardless of wind force and physics.
		Default: 30

	Body Mass: mass of vehicle's rigidbody. Does not directly affect sail's rigidbody.
		Default: 500

	Body Air Resistance: grossly simulated aerodynamic drag. More drag = vehicle stops faster when in movement.
		0 drag = once pushed, vehicle will never stop moving on its own, like in space.
		Default: 0.1

	Center of Mass Offset: offsets the center of mass in the desired direction.
		Default: (0, 0, 0)

- Camera
	Field of View: base FOV of camera.
		Default: 60

	Max Field of View: maximum value the camera's FOV could attain.
		Default: 100

	Camera FOV Change: Ratio of how intensely the FOV will change according to the vehicle's speed. The lower the number, the more intense speed will feel.
		Default: 1.4

- Sail
	Rotation Speed: speed at which sail rotates around its axis. 
		Default: 1.0

	Max Sail Rotation: max angle of rotation. 
		Default: 90

- Wheels
	Suspension Distance: how high and how low the suspension can go. If this parameter is modified, a realignment of the wheel collider might be needed.
		Default: different for each vehicle.

	Road Adherence: How much the wheels grip the road. 0 = like ice; no grip.
		Default: 1

	Suspension Looseness: how "soft" the suspension is ie how long it takes before it comes back to its original position after a shock.
		Default: 1

WHEEL COLLIDERS

	Mass: mass of wheel (similar to Rigidbody).

	Radius: Radius of the circular Wheel Collider.

	Wheel Dampening Rate: how much resistance there is to the wheels rotating. Higher values will make the vehicle feel like its brakes are always on.



WIND SCRIPT
	"Wind Strands": wooshes of wind object (particles). Found in Prefabs folder.

	Wind Force: amount of force applied to VEHICLE ONLY (for now) when vehicle is in collider zone. Force is applied
		    in the direction of the Wind GameObject's forward vector.
	
	Wind Strands Ratio: Ajustment of number of wind strands, apart from calculations made from "Wind Force". Visual effect only, does not impact gameplay.
	
	Max Strands Distance From Center: the highest local points in collider, relative to center, where strands can spawn.
		Default: (0.45, 0.9, 0.9)
	
	Min Strands Distance From Center: the lowest local points in collider, relative to center, where strands can spawn.
		Default: (-0.45, -0.2, -1)

SOUND MANAGER SCRIPT
	Sail Intensity Min: percentage of sail intensity needed to play full sail sound.

GEYSER SCRIPT
	Strength: how hard the geyser pushes the vehicle.
	Frequency: how often the geyser activates (in seconds).
	Duration: Once activated, how long the geyser stays active (in seconds).

-----------------CONTROLS--------------------

A-D (left joystick): steer vehicle left or right.

Left-Right Arrows (right joystick): rotate sail left or right.

P: Reset vehicle to last checkpoint.
		
-----------------CHANGELOG-------------------

11/03/2019: Wind prefab: wind direction no longer an option in the script; wind direction is now Wind object's forward vector (Z axis).
	Wind prefab now contains wind strands, which adapt according to wind force and capsule size.

12/03/2019: Added HUD elements speed and direction of force applied to vehicle.

15/03/2019: Wind strands positions fixed, can now choose max and min distance from center of Wind Collider.
		Wind strands now follow Wind object if moved/rotated during runtime (they are now children of Wind).

16/03/2019: New vehicle prefab 1.6. No more need for "Wheels" empty object. Now, every wheel must have a Mesh only, and a child containing only a Wheel Collider.
		Wheels rotate practically, but not visually.

18/03/2019: New vehicle prefab 1.7 : inverted collider and mesh hierarchy of wheels (colliders now parents of meshes of wheels). Not sure if 1.7 will be better or not,
		might revert to 1.6. If in doubt, use 1.6. Suspension and wheel rotation still work only practically, not visually.

22/03/2019: Prefab for woodchar functional, but very buggy (shakes a lot). 
		Added "P" button to respawn car at same position, but a bit higher in case player gets stuck.
		Can now change center of mass of vehicle in Vehicle script.
		Char_proto 1.8 is definitely the most functional model for now.

25/03/2019: Press "P" to now revert position and rotation of vehicle back to last checkpoint. Checkpoints are
		prefabs, and once the player passes through the checkpoint collider, that checkpoint is the one
		the player will revert to if they press "P".
		****When placing checkpoints, checkpoint's transform's forward is used as direction in which player spawns.****
		Mad Max char now basically functional.

26/03/2019: New SoundManager prefab. Needed in every scene, can be placed anywhere. Full sail sound happens.

27/03/2019: Char now moves its sail using left and right arrows instead of Q and E keys. 
		Intensity of steering on controller now fixed, should be much smoother.

28/03/2019: Reorganized all scripts' parameters pertaining to vehicle and its handling. Most useful parameters are now accessible directly through
		"Vehicle" script, except for Wheel Colliders parameters, which will be made easily accessible very soon.
		Integrated WavyMap, should work fine.

01/04/2019: New Mad Char 1.2 now works with bouncy materials. Now, every collider for vehicles must have "Player" physic material attached to it.
		"Bounce" physic material created. Create new Physic material if you need different objects with different bounciness.
		Can now press "C" to reset vehicle upright (rotation z = 0). Still need to make it smooth and not sudden.
		Created Geyser prefab, should work as intended.
		Implemented AstonMartin vehicle.

