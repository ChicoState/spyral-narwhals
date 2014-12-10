using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class fileread : MonoBehaviour
{
	public GameObject myMonster;
	private string base_path = @"C:\GitHub\spyral-narwhals\SoundMaze\Assets\My Scripts\Monster Files\";
	
	void Start()
	{
		//Pass list to myMonster

		List<int> startlist = GetMonsterPath (base_path + "monster1.txt");

		/*for (int i = 0; i < startlist.Count; i++)
		{//iterate through the entire list and print it to console
			Debug.Log(startlist[i]);
		}*/

		myMonster.GetComponent<Monster>().AssignPath(startlist);
		
		//Debug.Log ("\n");
	}
	
	//make a function that takes in a string (monster ID), it uses this to open a file
	//read the file until the EOF, while putting each element into an array
	void update()
	{
		//just to test
		/*string n;
		n = "monster1.txt";							//this will be eventually passed through when it is called from another function
		List<int> startlist = GetMonsterID (n);

		for (int i = 0; i < startlist.Count; i++)
		{//iterate through the entire list and print it to console
			Debug.Log(startlist[i]);
		}

		Debug.Log ("\n");*/
		
	}
	
	
	public List<int> GetMonsterPath(string name)
	{
		List<int> list = new List<int>();
		try
		{
			//int num;
			//string filename;
			StreamReader sRead = new StreamReader(name, Encoding.Default);
			
			using(sRead)
			{
				string line;
				do
				{
					line = sRead.ReadLine();//read the line (it sees it as a string)
					if(line != null)		//if the file is not empty, push the line onto a list
					{
						list.Add(Convert.ToInt32(line, 10));
					}
					
				}
				while (line != null);	//do this until you are at the end of the file (where the line will be null)
				sRead.Close();			//close the stream
				return list;			//return the list of integers
				
			}
		}
		catch (Exception e)
		{
			Debug.Log (e.Message);
			return list; //DO NOT DO THIS
		}
	}
}
