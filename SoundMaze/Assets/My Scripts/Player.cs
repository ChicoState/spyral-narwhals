using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public Transform startMarker;
	public Transform endMarker;
	public Transform target;

	public float speed = 10.0f;
	private float startTime;
	private float travelLength;
	public float smooth = 5.0f;

	public int directionFacing;
	private int currentRoomID;
	private GameObject currentRoom;
	private bool moving = false;
	//private Room roomScript;

	// Use this for initialization
	void Start ()
	{
		//startMarker = transform.position;
		//travelLength = Vector3.Distance (transform.position, endMarker.position);
		directionFacing = 1;
		currentRoom = GameObject.Find("Room1");
		currentRoomID = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//float distance = (Time.time) * speed;
		//float fractionTravel = distance / travelLength;



		if(Input.GetKey (KeyCode.W))
		{
			switch(directionFacing)
			{
				case(1):
					if(!currentRoom.GetComponent<Room>().north)
					{
						rigidbody.velocity = Vector3.forward * speed;
						moving = true;
					}
					break;
					
				case(2):
					if(!currentRoom.GetComponent<Room>().east)
					{
						rigidbody.velocity = Vector3.right * speed;
						moving = true;
					}
					break;
					
				case(3):
					if(!currentRoom.GetComponent<Room>().south)
					{
						rigidbody.velocity = Vector3.back * speed;
						moving = true;
					}
					break;
					
				case(4):
					if(!currentRoom.GetComponent<Room>().west)
					{
						rigidbody.velocity = Vector3.left * speed;
						moving = true;
					}
					break;
			}
		}
		else if (Input.GetKeyDown(KeyCode.A) && !moving)
		{
			transform.Rotate(Vector3.up * -90.0f);

			switch(directionFacing)
			{
				case(1):
					directionFacing = 4;
					break;
					
				case(2):
					directionFacing = 1;
					break;
					
				case(3):
					directionFacing = 2;
					break;
					
				case(4):
					directionFacing = 3;
					break;
			}
		}
		else if (Input.GetKeyDown(KeyCode.D) && !moving)
		{
			transform.Rotate(Vector3.up * 90.0f);

			switch(directionFacing)
			{
				case(1):
					directionFacing = 2;
					break;
					
				case(2):
					directionFacing = 3;
					break;
					
				case(3):
					directionFacing = 4;
					break;
					
				case(4):
					directionFacing = 1;
					break;
			}
		}
		
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Fill")
		{
			//Debug.Log("Collision");
			currentRoomID = col.transform.parent.GetComponent<Room>().roomID;
			currentRoom = col.transform.parent.gameObject;
			Debug.Log ("RoomID: " + currentRoomID);
			rigidbody.velocity = Vector3.up * 0;
			moving = false;

			if(col.transform.parent.GetComponent<Room>().exit)
			{
				Debug.LogWarning("EXIT HIT");
				Application.LoadLevel("LevelComplete");
			}

		}
		/*else if(col.gameObject.name == "Exit")
		{
			Application.LoadLevel("LevelComplete");
		}*/
	}

	void OnTriggerStay(Collider col)
	{
		
	}

	void OnTriggerExist(Collider col)
	{

		//Debug.Log ("Exiting");
	}
}
