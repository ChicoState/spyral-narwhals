using UnityEngine;
using System.Collections.Generic;

public class SoundRay : MonoBehaviour
{

	//private int[] previousRooms = new int[25];
	public List<int> previousRooms = new List<int>();
	private int currentRoomId;
	public float speed = 10.0f;
	public float rayDistance = 1.0f;
	public float raySpawn = 0.25f;
	public float rayCastSpawn = 0.5f;
	public bool original = true;
	public bool triggerEnabled = false;
	public GameObject soundRay;
	public int directionTraveling = 0;
	public int southRoomId, northRoomId, eastRoomId, westRoomId;
	public int layerMask = 1 << 8;
	private float audioDenominator = 1.0f;
	public GameObject audioObj;


	// Use this for initialization
	void Start ()
	{
		//audioObj = transform.parent.FindChild("SoundSource").gameObject;
		// Being taken care of by SoundHolder parent
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Fill")
		{
			Debug.LogWarning("Fill Collision" + col.gameObject.GetComponentInParent<Room>().roomID);
			
			Vector3 col_pos = col.transform.position;
			rigidbody.velocity = Vector3.forward * 0;
			currentRoomId = col.gameObject.GetComponentInParent<Room>().roomID;

			float curX = col_pos.x;
			float curY = col_pos.y;
			float curZ = col_pos.z;
	
			southRoomId = 0;
			northRoomId = 0;
			eastRoomId = 0;
			westRoomId = 0;
	
			Vector3 southPos = new Vector3(curX, curY, curZ - raySpawn);
			Vector3 eastPos = new Vector3(curX + raySpawn, curY, curZ);
			Vector3 westPos = new Vector3(curX - raySpawn, curY, curZ);
			Vector3 northPos = new Vector3(curX, curY, curZ + raySpawn);

			Vector3 southPosRay = new Vector3(curX, curY, curZ - rayCastSpawn);
			Vector3 eastPosRay = new Vector3(curX + rayCastSpawn, curY, curZ);
			Vector3 westPosRay = new Vector3(curX - rayCastSpawn, curY, curZ);
			Vector3 northPosRay = new Vector3(curX, curY, curZ + rayCastSpawn);
	
			/* Check adjacent rooms to see which are previously visted
			Use Physics.Overlapsphere and place the location at current location
			minus or plus whatever direction we are checking aka South, East, West. */

			// GO BACK TO OVERLAPSPHERE, because that wasn't the problem. And raycasting won't work since it can't go through our player or monster objects.
			// Actually just ignoring everything that isn't on the RayCastCollider Layer

			RaycastHit southHit, eastHit, westHit, northHit;

			if(!col.gameObject.GetComponentInParent<Room>().south)
			{
				if(Physics.Raycast(southPosRay, Vector3.back, out southHit, rayDistance, layerMask)) //south
				{
					if(southHit.collider.name == "RayCastCollider")
					{
						southRoomId = southHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().east)
			{
				if(Physics.Raycast(eastPosRay, Vector3.right, out eastHit, rayDistance, layerMask)) //east
				{
					if(eastHit.collider.name == "RayCastCollider")
					{
						eastRoomId = eastHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().west)
			{
				if(Physics.Raycast(westPosRay, Vector3.left, out westHit, rayDistance, layerMask)) //west
				{
					if(westHit.collider.name == "RayCastCollider")
					{
						westRoomId = westHit.collider.transform.GetComponentInParent<Room>().roomID;
					}
				}
			}

			if(!col.gameObject.GetComponentInParent<Room>().north)
			{
				if(Physics.Raycast(northPosRay, Vector3.forward, out northHit, rayDistance, layerMask))
				{
					if(northHit.collider.name == "RayCastCollider")
					{
						northRoomId = northHit.collider.transform.GetComponentInParent<Room>().roomID;
					}	
				}
			}
			//Checking Previous Rooms

			// Check all available directions and if they are previous rooms we have already visited
			
			if (!col.gameObject.GetComponentInParent<Room>().north)
			{
				// Move current RayObject to destination
				if(directionTraveling == 1)
				{
					if(CheckPreviousRooms(northRoomId))
					{
						Destroy(gameObject);
					}
					else
					{
						AssignPreviousRoom(currentRoomId);
						transform.position = northPos;
						rigidbody.velocity = Vector3.forward * speed;
					}
				}
				else  if (!CheckPreviousRooms(northRoomId))
				{
					GameObject northRay = Instantiate(soundRay, northPos, Quaternion.identity) as GameObject;

					//Increment Direction Change for ChildRay

					northRay.transform.parent = transform.parent;
					northRay.GetComponent<SoundRay>().audioObj = transform.parent.FindChild("SoundSource").gameObject;
					northRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;

					if(previousRooms.Count <= 1)
					{
						northRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						northRay.GetComponent<SoundRay>().original = true;
					}
					else
					{
						//northRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						northRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						northRay.GetComponent<SoundRay>().original = false;
					}
					northRay.GetComponent<SoundRay>().setDirection(1);
					// Move back to current room fill
					//northRay.transform.position = col_pos;
					northRay.rigidbody.velocity = Vector3.forward * speed;
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().east)
			{
			//Spawn new rayObject and move it to destination
				if(directionTraveling == 2)
				{
					if(CheckPreviousRooms(eastRoomId))
					{
						Destroy(gameObject);
					}
					else 
					{
						AssignPreviousRoom(currentRoomId);
						transform.position = eastPos;
						rigidbody.velocity = Vector3.right * speed;
					}
				}
				else if (!CheckPreviousRooms(eastRoomId))
				{
					GameObject eastRay = Instantiate(soundRay, eastPos, Quaternion.identity) as GameObject;
					eastRay.transform.parent = transform.parent;
					eastRay.GetComponent<SoundRay>().audioObj = transform.parent.FindChild("SoundSource").gameObject;
					eastRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;

					if(previousRooms.Count <= 1)
					{
						eastRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						eastRay.GetComponent<SoundRay>().original = true;
					}
					else
					{
						//eastRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						eastRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						eastRay.GetComponent<SoundRay>().original = false;
					}

					eastRay.GetComponent<SoundRay>().setDirection(2);
					eastRay.rigidbody.velocity = Vector3.right * speed;
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().south)
			{
				//Spawn new rayObject and move it to destination
				if(directionTraveling == 3)
				{
					if(CheckPreviousRooms(southRoomId))
					{
						Destroy(gameObject);
					}
					else 
					{
						AssignPreviousRoom(currentRoomId);
						transform.position = southPos;
						rigidbody.velocity = Vector3.back * speed;
					}
				}
				else if (!CheckPreviousRooms(southRoomId))
				{
					GameObject southRay = Instantiate(soundRay, southPos, Quaternion.identity) as GameObject;
					southRay.transform.parent = transform.parent;
					southRay.GetComponent<SoundRay>().audioObj = transform.parent.FindChild("SoundSource").gameObject;
					southRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
					if(previousRooms.Count <= 1)
					{
						southRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						southRay.GetComponent<SoundRay>().original = true;
					}
					else
					{
						//southRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						southRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						southRay.GetComponent<SoundRay>().original = false;
					}
					southRay.GetComponent<SoundRay>().setDirection(3);
					southRay.rigidbody.velocity = Vector3.back * speed;
				}
			}

			if (!col.gameObject.GetComponentInParent<Room>().west)
			{
				//Spawn new rayObject and move it to destination
				if(directionTraveling == 4)
				{
					if(CheckPreviousRooms(westRoomId))
					{
						Destroy(gameObject);
					}
					else
					{
						AssignPreviousRoom(currentRoomId);
						transform.position = westPos;
						rigidbody.velocity = Vector3.left * speed;
					}
				}
				else if (!CheckPreviousRooms(westRoomId))
				{
					GameObject westRay = Instantiate(soundRay, westPos, Quaternion.identity) as GameObject;
					westRay.transform.parent = transform.parent;
					westRay.GetComponent<SoundRay>().audioObj = transform.parent.FindChild("SoundSource").gameObject;
					westRay.GetComponent<SoundRay>().currentRoomId = currentRoomId;
					if(previousRooms.Count <= 1)
					{
						westRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						westRay.GetComponent<SoundRay>().original = true;
					}
					else
					{
						//westRay.GetComponent<SoundRay>().AssignPreviousRooms(previousRooms);
						westRay.GetComponent<SoundRay>().AssignPreviousRoom(currentRoomId);
						westRay.GetComponent<SoundRay>().original = false;
					}
					westRay.GetComponent<SoundRay>().setDirection(4);
					westRay.rigidbody.velocity = Vector3.left * speed;
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

		/* This is the point in the SoundRay collision where we hit the player, which means we need to do all the calculation to place the sound object and moving it on the level */
		if(col.gameObject.name == "Player")
		{
			Debug.LogWarning("Player Collision");
			// Check last 2 Previous Rooms
			if(original)
			{
				string tempOriginal = "Room" + previousRooms[0];
				GameObject tempobj = GameObject.Find(tempOriginal);
				audioObj.transform.position = tempobj.transform.position;
				audioObj.GetComponent<AudioSource>().audio.volume = 1.0f;
				audioObj.GetComponent<AudioSource>().enabled = true;
				Destroy(gameObject);
			}
			else
			{
				string temp = "Room" + previousRooms[previousRooms.Count-2];
				GameObject tempgo = GameObject.Find(temp);
				audioObj.transform.position = tempgo.transform.position;
				audioDenominator = getDistanceTraveled();
				audioDenominator = audioDenominator * 2;
				audioObj.GetComponent<AudioSource>().audio.volume = 1.0f / audioDenominator;
				audioObj.GetComponent<AudioSource>().enabled = true;
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerStay(Collider col)
	{

	}

	void OnTriggerExit(Collider col)
	{
		//Debug.Log("Just Left Room: " + currentRoomId);
		//previousRooms.Add (currentRoomId);
		
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
			Debug.LogWarning("CurList[" + i + "] " + curList[i]);
		}
	}

	public void AssignPreviousRoom(int value)
	{
		previousRooms.Add (value);
	}

	void setDirection(int value)
	{
		directionTraveling = value;
	}

	public int getDistanceTraveled()
	{
		return previousRooms.Count - 1;
	}

}
