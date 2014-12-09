using UnityEngine;
using System.Collections;

public class SoundHolder : MonoBehaviour
{
	
	public int rayTimer;
	public int monsterTimer;
	// Use this for initialization
	void Start ()
	{
		// Set Clock Timer
		rayTimer = 0;
		monsterTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		monsterTimer += 1;
		rayTimer += 1;
		
		if(monsterTimer == 250)
		{
			monsterTimer = 0;
			Debug.Log("TICK ZAMBO");
		}
		
		if(rayTimer == 25)
		{
			rayTimer = 0;
			Debug.Log("TICK RAY");
		}
	}
}
