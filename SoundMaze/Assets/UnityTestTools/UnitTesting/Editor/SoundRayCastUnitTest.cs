using UnityEngine;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class SoundRayCastingTests
	{

		[Test]
		public void checkPreviousRoomIfVisted ()
		{
			var soundRay = Substitute.For<SoundRay> ();
			soundRay.previousRooms =  new List<int> ();
			int target = 2;
			 
			for(int i = 0; i < 10 ; i++)
			{
				soundRay.previousRooms.Add(i);
			}


			for(int i = 0; i < soundRay.previousRooms.Count; i++)
			{
				if(soundRay.previousRooms[i] == target)
				{
					Assert.Pass();
				}
			}
			Assert.Fail ();
		}

		[Test]
		public void playerSoundRayCollisionWithOriginalSoundRay()
		{
			var soundRay = Substitute.For<SoundRay> ();
			soundRay.previousRooms = new List<int> ();
			GameObject currentRoom = GameObject.Find("Room1");

			for(int i = 1; i < 2 ; i++)
			{
				soundRay.previousRooms.Add(i);
			}

			if(soundRay.previousRooms.Count == 1)
			{
				string tempOriginal = "Room" + soundRay.previousRooms[0];
				GameObject tempobj = GameObject.Find(tempOriginal);
				Assert.AreSame(tempobj,currentRoom);
			}
		}

		[Test]
		public void playerSoundRayCollisionWithNonOriginalSoundRay()
		{
			var soundRay = Substitute.For<SoundRay> ();
			soundRay.previousRooms = new List<int> ();
			GameObject currentRoom = GameObject.Find("Room4");
			
			for(int i = 0; i < 6 ; i++)
			{
				soundRay.previousRooms.Add(i);
			}
			
			if(soundRay.previousRooms.Count > 1)
			{
				int size = soundRay.previousRooms.Count-2;
				string temp = "Room" + soundRay.previousRooms[size];
				Debug.Log(temp);
				GameObject tempgo = GameObject.Find(temp);
				Assert.AreSame(tempgo,currentRoom);
			}
		}
	}
}
