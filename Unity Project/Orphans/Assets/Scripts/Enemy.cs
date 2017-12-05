using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
	//the navAgent attached to the Enemies
	private NavMeshAgent navAgent;	

	public GameObject[] children;
	// how fast the enemies walk while patrolling
	private float stopSpeed = 0f;
	public float speed = 3.5f; 

	//the layer that an object is on
	public LayerMask viewMask;	

	public GameObject following;
	public bool caughtUp;
			

	//--------------------------------------------------------------------------------------
	//	Start()
	// Runs during initialisation
	// Sets the view of the Enemies to the angle of their vision spotlight, their patrol speed, and the origional spotlight color.
	//
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Start () {

		navAgent = GetComponent<NavMeshAgent> ();				
		navAgent.speed = speed;
		caughtUp = false;
		children = GameObject.FindGameObjectsWithTag("Children");
		Chasey ();

	}

	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	// Lets you reset game if you get caught.Or go back to main menu
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update () {
		CheckDistance ();
		Followed ();
		children = GameObject.FindGameObjectsWithTag("Children");
		//if the player is captured, the enter key will resart the level, escape will go to main menu
		Chasey ();


	}
	void Chasey (){
		for (int c = children.Length-1; c >= 0; c--) {
			
			following = children[c].gameObject;


		}
	}
		

	private void Followed (){
		// if the enemy gets close enough to the player as though they were basically touching, the game over screen shows, and the player controller is disabled
		if (Vector3.Distance (transform.position, following.transform.position) < 2f) {
			caughtUp = true;
			navAgent.speed = stopSpeed;
		} else {
			caughtUp = false;
			navAgent.speed = speed;
		}
	}



	//--------------------------------------------------------------------------------------
	//	CheckDistance()
	// if player is within the sound detection range of an enemy, the enemy will go to the players location, the enemies speed will increase to its chase speed
	// The Alarm UI shows, the enemies are alerted to the player.
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void CheckDistance(){
		//if player is not sneaking

		if (!caughtUp) {		
			navAgent.destination = following.transform.position;		


			}

	}

}



