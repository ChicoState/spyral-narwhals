using UnityEngine;
using System.Collections.Generic;

public class SoundRay : MonoBehaviour
{

	//private int[] previousRooms = new int[25];
	public List<int> previousRooms = new List<int>();
	private int currentRoomId;
	public float speed = 1.0f;
	public float rayDistance = 1.0f;
	public bool justSpawned = true;
	public GameObject soundRay;
	public int directionTraveling = 0;


	// Use this for initialization
	void Start ()
	{
		/*if(justSpawned)
		{
			Debug.Log ("Initial Spawn");
			justSpawned = false;
			//OnTriggerEnter(col);
		}*/
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
	
			Vector3 southPos = new Vector3(curX, curY, curZ - 0.5f);
			Vector3 eastPos = new Vector3(curX + 0.5f, curY, curZ);
			Vector3 westPos = new Vector3(curX - 0.5f, curY, curZ);
			Vector3 northPos = new Vector3(curX, curY, curZ + 0.5f);

	
			/* Check adjacent rooms to see which are previously visted
			Use Physics.Overlapsphere and place the location at current location
			minus or plus whatever direction we are checking aka South, East, West. */
			
			RaycastHit southHit, eastHit, westHit, northHit;

			if(!col.gameObject.GetComponentInParent<Room>().south)
			{
				if(Physics.Raycast(transform.position, Vector3.back, out southHit, rayDistance)) //south
				{
					if(southHit.collider.name == "Fill")
					{
						southRoomId = southHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().east)
			{
				if(Physics.Raycast(transform.position, Vector3.right, out eastHit, rayDistance)) //east
				{
					if(eastHit.collider.name == "Fill")
					{
						eastRoomId = eastHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().west)
			{
				if(Physics.Raycast(transform.position, Vector3.left, out westHit, rayDistance)) //west
				{
					if(westHit.collider.name == "Fill")
					{
						westRoomId = westHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().north)
			{
				if(Physics.Raycast(transform.position, Vector3.back, out northHit, rayDistance))
				{
					if(northHit.collider.name == "Fill")
					{
						northRoomId = northHit.collider.transform.GetComponentInParent<Room>().roomID;
					}	
				}
			}
			//Checking Previous Rooms
	
			// Check all available directions and if they are previous rooms we have already visited
			
			if (!col.gameObject.GetComponentInParent<Room>().north)
			{
				if(CheckPreviousRooms(northRoomId))
				{

					Destroy(gameObject);
				}
				else 
				{
					// Move current RayObject to destination
					if(directionTraveling == 1)
					{
						rigidbody.velocity = Vector3.forward * speed;
					}
					else 
					{
	
						Debug.Log("northRay created");
						GameObject northRay = Instantiate(soundRay, northPos, Quaternion.identity) as GameObject;
						//northRay.transform.parent = transform.parent;
						northRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
						northRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						northRay.GetComponent<SoundRay>().setDirection(1);
						northRay.rigidbody.velocity = Vector3.forward * speed;
					}
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().east)
			{
				if(CheckPreviousRooms(eastRoomId))
				{
					Destroy(gameObject);
				}
				else 
				{
				//Spawn new rayObject and move it to destination
					if(directionTraveling == 2)
					{
						rigidbody.velocity = Vector3.right * speed;
					}
					else
					{
	
						Debug.Log ("eastRay created");
						GameObject eastRay = Instantiate(soundRay, eastPos, Quaternion.identity) as GameObject;
						//eastRay.transform.parent = transform.parent;
						eastRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
						eastRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						eastRay.GetComponent<SoundRay>().setDirection(2);
						eastRay.rigidbody.velocity = Vector3.right * speed;
					}
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().south)
			{
				if(CheckPreviousRooms(southRoomId))
				{
					Destroy(gameObject);
				}
				else 
				{
					//Spawn new rayObject and move it to destination
					if(directionTraveling == 3)
					{
						rigidbody.velocity = Vector3.back * speed;
					}
					else
					{
	
						Debug.Log ("southRay created");
						GameObject southRay = Instantiate(soundRay, southPos, Quaternion.identity) as GameObject;
						//southRay.transform.parent = transform.parent;
						southRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
						southRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						southRay.GetComponent<SoundRay>().setDirection(3);
						southRay.rigidbody.velocity = Vector3.back * speed;
					}
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().west)
			{
				if(CheckPreviousRooms(westRoomId))
				{
					Destroy(gameObject);
				}
				else
				{
					
					//Spawn new rayObject and move it to destination
					if(directionTraveling == 4)
					{
						rigidbody.velocity = Vector3.left * speed;
					}
					else
					{
	
						Debug.Log ("westRay created");
						GameObject westRay = Instantiate(soundRay, westPos, Quaternion.identity) as GameObject;
						//westRay.transform.parent = transform.parent;
						westRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
						westRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						westRay.GetComponent<SoundRay>().setDirection(4);
						westRay.rigidbody.velocity = Vector3.left * speed;
					}
				}
			}

			if(directionTraveling == 1 && col.gameObject.GetComponentInParent<Room>().north)
			{
				Destroy(gameObject);
			}
			else if(directionTraveling == 2 && col.gameObject.GetComponentInParent<Room>().east)
			{
				Destroy(gameObject);
			}
			else if(directionTraveling == 3 && col.gameObject.GetComponentInParent<Room>().south)
			{
				Destroy(gameObject);
			}
			else if(directionTraveling == 4 && col.gameObject.GetComponentInParent<Room>().west)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerStay(Collider col)
	{

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

	public void AssignPreviousRooms(List<int> curList)
	{
		for(int i = 0; i < curList.Count; i++)
		{
			previousRooms.Add(curList[i]);
		}
	}

	void setDirection(int value)
	{
		directionTraveling = value;
	}

}
