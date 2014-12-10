using UnityEngine;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class RoomUnitTest
	{
		[Test]
		public void fourRoom()
		{
			var room = Substitute.For<Room> ();
			var play = Substitute.For<Player> ();
			GameObject currentRoom = GameObject.Find("Room2");

			if(!currentRoom.GetComponent<Room>().north && !currentRoom.GetComponent<Room>().east &&
			   !currentRoom.GetComponent<Room>().south && !currentRoom.GetComponent<Room>().west)
			{
				Assert.Pass ();
			}
			else
			{
				Assert.Fail ();
			}
		}

		[Test]
		public void hallway()
		{
			var room = Substitute.For<Room> ();
			var play = Substitute.For<Player> ();
			GameObject currentRoom = GameObject.Find("Room10");
			
			if(currentRoom.GetComponent<Room>().north && !currentRoom.GetComponent<Room>().east &&
			   currentRoom.GetComponent<Room>().south && !currentRoom.GetComponent<Room>().west)
			{
				Assert.Pass ();
			}
			else
			{
				Assert.Fail ();
			}
		}
		
		[Test]
		public void deadend()
		{
			var room = Substitute.For<Room> ();
			var play = Substitute.For<Player> ();
			GameObject currentRoom = GameObject.Find("Room22");
			
			if(currentRoom.GetComponent<Room>().north && currentRoom.GetComponent<Room>().east &&
			   !currentRoom.GetComponent<Room>().south && currentRoom.GetComponent<Room>().west)
			{
				Assert.Pass ();
			}
			else
			{
				Assert.Fail ();
			}
		}

		[Test]
		public void corner()
		{
			var room = Substitute.For<Room> ();
			var play = Substitute.For<Player> ();
			GameObject currentRoom = GameObject.Find("Room21");
			
			if(!currentRoom.GetComponent<Room>().north && !currentRoom.GetComponent<Room>().east &&
			   currentRoom.GetComponent<Room>().south && currentRoom.GetComponent<Room>().west)
			{
				Assert.Pass ();
			}
			else
			{
				Assert.Fail ();
			}
		}
	}
}
