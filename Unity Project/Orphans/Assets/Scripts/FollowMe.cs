using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FollowMe: MonoBehaviour {
	//the navAgent attached to the Enemies
	private NavMeshAgent navAgent;	

	public GameObject[] children;
	public GameObject player;
	// how fast the enemies walk while patrolling
	private float stopSpeed = 0f;
	public float speed = 3.5f; 

	//the layer that an object is on
	public LayerMask viewMask;	

	public GameObject following;
	public bool caughtUp;
	public bool lost = true;
	public float followDistance = 2f;
			

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
		//children = GameObject.FindGameObjectsWithTag("Children");
		player = GameObject.Find ("Player");





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
		//Followed ();

		//if the player is captured, the enter key will resart the level, escape will go to main menu



	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player" && lost) {
			lost = false;
			gameObject.tag = "Children";
			children = GameObject.FindGameObjectsWithTag("Children");
			Chasey ();
		}
	}



	void Chasey (){
			if (children.Length == 1) {
				following = player;
			} else
			{
				int a = children.Length - 2;
				following = children [a].gameObject;

			}
				Debug.Log (children.Length - 2 + " Following : " + following);



	}
		

	private void Followed (){
		// if the enemy gets close enough to the player as though they were basically touching, the game over screen shows, and the player controller is disabled
		if (Vector3.Distance (transform.position, following.transform.position) < followDistance) {
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

		if (!caughtUp && !lost) {		
			navAgent.destination = following.transform.position;
			Followed ();
		} else if (!lost){
			Followed ();
		}


			

	}

}



