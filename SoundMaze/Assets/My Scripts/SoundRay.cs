using UnityEngine;
using System.Collections.Generic;

public class SoundRay : MonoBehaviour
{

	//private int[] previousRooms = new int[25];
	private List<int> previousRooms = new List<int>();
	private int currentRoomId = -1;
	public float speed = 1.0f;
	public float overlapSize = 0.000025f;
	public bool justSpawned = true;
	public GameObject soundRay;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Fill")
		{
			rigidbody.velocity = Vector3.forward * 0;
			currentRoomId = col.gameObject.GetComponentInParent<Room>().roomID;
			float curX = transform.position.x;
			float curY = transform.position.y;
			float curZ = transform.position.z;
	
			int southRoomId = 0;
			int northRoomId = 0;
			int eastRoomId = 0;
			int westRoomId = 0;
	
			Vector3 southPos = new Vector3(curX, curY, curZ - 1);
			Vector3 eastPos = new Vector3(curX + 1, curY, curZ);
			Vector3 westPos = new Vector3(curX - 1, curY, curZ);
			Vector3 northPos = new Vector3(curX, curY, curZ + 1);
	
			/* Check adjacent rooms to see which are previously visted
			Use Physics.Overlapsphere and place the location at current location
			minus or plus whatever direction we are checking aka South, East, West. */
			Collider[] southCol = Physics.OverlapSphere(southPos,overlapSize);
			Collider[] eastCol = Physics.OverlapSphere(eastPos,overlapSize);
			Collider[] westCol = Physics.OverlapSphere(westPos,overlapSize);
			Collider[] northCol = Physics.OverlapSphere(northPos,overlapSize);
	
	
			foreach(Collider curCol in southCol)
			{
				if(curCol.gameObject.name == "Fill")
				{
					southRoomId = curCol.gameObject.GetComponentInParent<Room>().roomID;
				}
			}
	
			foreach(Collider curCol in eastCol)
			{
				if(curCol.gameObject.name == "Fill")
				{
					eastRoomId = curCol.gameObject.GetComponentInParent<Room>().roomID;
				}
			}
	
			foreach(Collider curCol in westCol)
			{
				if(curCol.gameObject.name == "Fill")
				{
					westRoomId = curCol.gameObject.GetComponentInParent<Room>().roomID;
				}
			}
	
			foreach(Collider curCol in northCol)
			{
				if(curCol.gameObject.name == "Fill")
				{
					northRoomId = curCol.gameObject.GetComponentInParent<Room>().roomID;
				}
			}
	
			//Checking Previous Rooms
	
			// Check all available directions and if they are previous rooms we have already visited
			if (!col.gameObject.GetComponentInParent<Room>().east && !CheckPreviousRooms(eastRoomId))
			{
				//Spawn new rayObject and move it to destination
				Debug.Log ("eastRay created");
				GameObject eastRay = Instantiate(soundRay, transform.position, Quaternion.identity) as GameObject;
				eastRay.transform.parent = transform.parent;
				
				eastRay.rigidbody.velocity = Vector3.right * speed;
			}
			if (!col.gameObject.GetComponentInParent<Room>().west && !CheckPreviousRooms(westRoomId))
			{
				//Spawn new rayObject and move it to destination
				Debug.Log ("westRay created");
				GameObject westRay = Instantiate(soundRay, transform.position, Quaternion.identity) as GameObject;
				westRay.transform.parent = transform.parent;
				westRay.rigidbody.velocity = Vector3.left * speed;
			}
			if (!col.gameObject.GetComponentInParent<Room>().south && !CheckPreviousRooms(southRoomId))
			{
				//Spawn new rayObject and move it to destination
				Debug.Log ("southRay created");
				GameObject southRay = Instantiate(soundRay, transform.position, Quaternion.identity) as GameObject;
				southRay.transform.parent = transform.parent;
				southRay.rigidbody.velocity = Vector3.back * speed;
			}
			if (!col.gameObject.GetComponentInParent<Room>().north && !CheckPreviousRooms(northRoomId))
			{
				// Move current RayObject to destination
				rigidbody.velocity = Vector3.forward * speed;
			}
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(justSpawned)
		{
			Debug.Log ("Initial Spawn");
			justSpawned = false;
			//currentRoomId = col.gameObject.GetComponentInParent<Room>().roomID;
			OnTriggerEnter(col);
		}
	}

	void OnTriggerExit(Collider col)
	{
		previousRooms.Add (currentRoomId);
		
	}

	//Check if id exists in array
	bool CheckPreviousRooms(int target)
	{
		bool alreadyVisited = false;

		for(int i = 0; i < previousRooms.Count; i++)
		{
			if(previousRooms[i] == target)
			{
				alreadyVisited = true;
			}
		}
		return alreadyVisited;
	}
}
