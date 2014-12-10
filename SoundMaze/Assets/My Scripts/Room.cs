using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{

	/* up, down, left, right
		Z axis is the North(+z) and South(-z)
		X axis is the East(+x) and West(-x)
	*/
	public bool north, south, west, east;
	public bool exit = false;
	public int roomID;
	private bool player_presence;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
