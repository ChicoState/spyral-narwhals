using UnityEngine;
using System.Collections;

public class ExitHolder : MonoBehaviour
{

	public int rayTimer;
	public int rayRate = 250;
	public GameObject soundRay;
	public GameObject exitRoom;
	public GameObject soundSource;


	// Use this for initialization
	void Start ()
	{
		rayTimer = 0;
		soundSource = transform.FindChild("SoundSource").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		rayTimer += 1;
		
		if(rayTimer == rayRate)
		{
			rayTimer = 0;
			GameObject soundRayChild = Instantiate(soundRay, exitRoom.transform.FindChild("Fill").transform.position, Quaternion.identity) as GameObject;

			soundRayChild.transform.parent = transform;
			soundRayChild.GetComponent<SoundRay>().audioObj = transform.FindChild("SoundSource").gameObject;
			
			if(!exitRoom.GetComponent<Room>().north)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 1;
			}
			else if(!exitRoom.GetComponent<Room>().east)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 2;
			}
			else if(!exitRoom.GetComponent<Room>().south)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 3;
			}
			else if(!exitRoom.GetComponent<Room>().west)
			{
				soundRayChild.GetComponent<SoundRay>().directionTraveling = 4;
			}
			//Debug.Log("Direction Set");
		}
	}
}
