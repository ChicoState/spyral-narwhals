using UnityEngine;
using System.Collections;

public class SoundHolder : MonoBehaviour
{
	
	public int rayTimer;
	public int monsterTimer;
	public int rayRate = 50;
	private GameObject monsterChild;
	public GameObject soundRay;

	// Use this for initialization
	void Start ()
	{
		// Set Clock Timer
		rayTimer = 0;
		monsterTimer = 0;
		monsterChild = transform.FindChild("Monster").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		monsterTimer += 1;
		rayTimer += 1;
		
		if(monsterTimer == 250)
		{
			monsterTimer = 0;
			//Debug.Log("TICK ZAMBO");
			monsterChild.GetComponent<Monster>().MoveMonster();
		}
		
		if(rayTimer == rayRate)
		{
			rayTimer = 0;
			//Debug.Log("TICK RAY");
			// Fire Ray, really instantiate a ray at the monster's currentRooms position
			GameObject monsterRoom = monsterChild.GetComponent<Monster>().get_monster_room();
			GameObject soundRayChild = Instantiate(soundRay, monsterRoom.transform.FindChild("Fill").transform.position, Quaternion.identity) as GameObject;
			//Debug.Log("Ray Position: " + monsterRoom.transform.FindChild("Fill").transform.position);
			soundRayChild.transform.parent = transform;
			soundRayChild.GetComponent<SoundRay>().audioObj = transform.FindChild("SoundSource").gameObject;

			if(!monsterRoom.GetComponent<Room>().north)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 1;
			}
			else if(!monsterRoom.GetComponent<Room>().east)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 2;
			}
			else if(!monsterRoom.GetComponent<Room>().south)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 3;
			}
			else if(!monsterRoom.GetComponent<Room>().west)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 4;
			}
			//Debug.Log("Direction Set");
		}
	}
}
