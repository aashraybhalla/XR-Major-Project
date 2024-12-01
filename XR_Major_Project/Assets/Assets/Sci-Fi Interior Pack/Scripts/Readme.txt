These scripts must be placed on the character. 
Scripts must be located on the character together with the Character Controller component.

- Triggers_Doors.cs -
---------------------
This script is designed to control the animation. Open and close the doors.
Next to each door there is one trigger.
Each door has its own animator and animation. You can control the animation of each door.
When an FPSController enters the trigger, then there is playing the animation (Open the door).
When the FPSController leaves the trigger, then there is playing the animation (Close the door).
---------------------

- Triggers_Lift.cs -
--------------------
This script is designed to control the animation. Open and close the lift doors. Also, control the horizontal position of the lift.
This script is an example of how you can control the animation.

Lift operates automatically and does not require pressing buttons.
However, you can easily add buttons.
It is important that the door of the the lift and the door of another room contain different animation.

Next to the elevator located triggers.
Two trigger (Trigger_Lift_Door_Room) designed for playing animations (Open the lift doors).
Two trigger (Trigger_Lift) designed for playing animations (Change the horizontal position of the lift).

In the game there is a special object of the lift platform. The platform allows you to move your character, when the lift is moving.
--------------------

- Windows.cs -
--------------
This script is designed to control the animation. Open and close the windows.
This script is an example of how you can control the animation.
Each window has its own animator and animation. You can control the animation of each window.
--------------
