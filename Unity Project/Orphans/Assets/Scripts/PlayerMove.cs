//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMove : MonoBehaviour 
{
	// Initialises public floats so Designers can adjust
	public float movementSpeed = 10f;
	public float maxSpeed = 10f;

    // Used to get the Striker's Rigidbody
    public Rigidbody rigidBody;

	// Allows access to xbox controller buttons
    public XboxController Controller;

	// Creates two private floats that record the previous x and z rotation
	private float prevRotateX;
	private float prevRotateZ;

	//--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
		// Gets the Striker's Rigidbody and stores it in variable
      //  rigidBody = GetComponent<Rigidbody>();
	
        // Sets previous rotation x and z to both be zero
        prevRotateX = 0f;
		prevRotateZ = 0f;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // If statement runs if game is not paused
       // Calls all functions from the script but Awake
            Move();      
    }

 
    //--------------------------------------------------------------------------------
    // Function allows for the Striker to move
    //--------------------------------------------------------------------------------
    private void Move()
    {
		
            // Both floats get direction of the Xbox controller's left stick
            float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
            float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

            // Creates a "new" Vector3 to allow movement
            Vector3 movement = new Vector3(axisX, 0, axisZ) * movementSpeed;

            // Applies movement physics to the player's Rigidbody
            rigidBody.MovePosition(rigidBody.position + movement * Time.deltaTime);

            // Sets the Striker's velocity to be zero
            rigidBody.velocity = Vector3.zero;
            
            // Sets axisX to equal previous Rotation's X value if axisX is zero
            if (axisX == 0f)
                axisX = prevRotateX;

            // Otherwise the previous Rotation X records the axisX
            else
                prevRotateX = axisX;

            // Sets axisZ to equal previous Rotation's Z value if axisZ is zero
            if (axisZ == 0f)
                axisZ = prevRotateZ;

            // Otherwise the previous Rotation Z records the axisZ
            else
                prevRotateZ = axisZ;

            // Runs code in braces if the left control stick is not in the centre
            if (axisX != 0 || axisZ != 0)
            {
                // Creates a "new" direction Vector3 of the left control sticks direction
                Vector3 directionVector = new Vector3(axisX, 0, axisZ);

                // Makes the player look in direction of the directionVector
                transform.rotation = Quaternion.LookRotation(directionVector);
            }

            // Angular velocity of player is set to be a zero Vector
            rigidBody.angularVelocity = Vector3.zero;
        
    }

	//------------------------------------------------------------
	// Function allows for the Striker to rotate
	//------------------------------------------------------------
    private void Rotate()
    {
            // Both floats get direction of the Xbox controller's right stick
            float rotateAxisX = XCI.GetAxisRaw(XboxAxis.RightStickX, Controller);
            float rotateAxisZ = XCI.GetAxisRaw(XboxAxis.RightStickY, Controller);

            // Checks if the right stick is at default position on the x axis
            if (rotateAxisX == 0f)
                // If so, set the rotation x to be the previous frame's rotation x
                rotateAxisX = prevRotateX;

            // Otherwise store current rotate x into the previous frame rotate x
            else
                prevRotateX = rotateAxisX;

            // Checks if the right stick is at default position on the y axis
            if (rotateAxisZ == 0f)
                // If so, set the rotation z to be the previous frame's rotation z
                rotateAxisZ = prevRotateZ;

            // Otherwise store current rotate z into the previous frame rotate z
            else
                prevRotateZ = rotateAxisZ;

            // Checks if either rotate x isn't zero or rotate z isn't zero
            if (rotateAxisX != 0 || rotateAxisZ != 0)
            {
                // If one variable isn't zero, create a "new" Vector3 refering to looking direction
                Vector3 directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);

                // Allows for the striker to look in same direction as right stick's position
                transform.rotation = Quaternion.LookRotation(directionVector);
            }

            // Sets the angular velocity of the Striker to be zero
            rigidBody.angularVelocity = Vector3.zero;
        
    }
		
}
