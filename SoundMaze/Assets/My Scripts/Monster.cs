using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
	private List<int> monsterPath = new List<int>();
	private bool pathReverse;
	public int nextIndex = 0;
	public float maxMoveDistance = 1.5f;
	public int currentRoomID; // Set this in Unity from the Inspector Window. Don't have a way to dynamically calculate this.
	private GameObject currentRoom;

	// Use this for initialization
	void Start ()
	{
		nextIndex = 0;
		currentRoom = GameObject.Find("Room" + currentRoomID);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void AssignPath(List<int> curList)
	{
		if(curList != null)
		{
			//Debug.Log ("curList count: " + curList.Count);
			for(int i = 0; i < curList.Count; i++)
			{
				//Debug.Log (i);
				monsterPath.Add(curList[i]);
			}
		}
	}
	
	public void MoveMonster()
	{
		if(monsterPath.Count != null) // Just a sanity check
		{
			int tempRoomID;
			if(!pathReverse)
			{
				tempRoomID = monsterPath[nextIndex];
				// Move monster to this room
				GameObject tempRoom = GameObject.Find("Room" + tempRoomID);
				transform.position = Vector3.MoveTowards(transform.position, tempRoom.transform.FindChild("Fill").transform.position, maxMoveDistance);

				if(nextIndex == monsterPath.Count-1)
				{
					pathReverse = true;
				}
				nextIndex++;
			}
			else if(pathReverse)
			{
				tempRoomID = monsterPath[nextIndex];
				// Move monster to this room
				GameObject tempRoom = GameObject.Find("Room" + tempRoomID);
				transform.position = Vector3.MoveTowards(transform.position, tempRoom.transform.FindChild("Fill").transform.position, maxMoveDistance);

				if(nextIndex == 0)
				{
					pathReverse = false;
				}
				nextIndex--;
			}
		}
	}
	
	public void set_path_reverse(bool value)
	{
		pathReverse = value;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Fill")
		{
			currentRoomID = col.transform.parent.GetComponent<Room>().roomID;
			currentRoom = col.transform.parent.gameObject;
			//Debug.Log ("Monster RoomID: " + currentRoomID);
			//rigidbody.velocity = Vector3.up * 0;
		}
	}

	public GameObject get_monster_room()
	{
		return currentRoom;
	}
}
