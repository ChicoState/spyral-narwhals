using UnityEngine;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class PlayerUnitTest
	{
		[Test]
		public void playerNorthMovement()
		{
			var play = Substitute.For<Player> ();
			var room = Substitute.For<Room> ();
			play.directionFacing = 1;

			GameObject currentRoom = GameObject.Find("Room12");

			
			if(!currentRoom.GetComponent<Room>().north && currentRoom.GetComponent<Room>().east &&
			   currentRoom.GetComponent<Room>().south && currentRoom.GetComponent<Room>().west)
			{
				Assert.True(!room.north && (play.directionFacing == 1));
			}
			else
			{
				Assert.Fail();
			}
		}
	
		[Test]
		public void playerEastMovement()
		{
			var play = Substitute.For<Player> ();
			var room = Substitute.For<Room> ();
			play.directionFacing = 2;
			
			GameObject currentRoom = GameObject.Find("Room10");
			
			
			if(currentRoom.GetComponent<Room>().north && !currentRoom.GetComponent<Room>().east &&
			   currentRoom.GetComponent<Room>().south && !currentRoom.GetComponent<Room>().west)
			{
				Assert.True(!room.east && (play.directionFacing == 2));
			}
			else
			{
				Assert.Fail();
			}
		}

		[Test]
		public void playerSouthMovement()
		{
			var play = Substitute.For<Player> ();
			var room = Substitute.For<Room> ();
			play.directionFacing = 3;

			GameObject currentRoom = GameObject.Find("Room22");
			
			
			if(currentRoom.GetComponent<Room>().north && currentRoom.GetComponent<Room>().east &&
			   !currentRoom.GetComponent<Room>().south && currentRoom.GetComponent<Room>().west)
			{
				Assert.True(!room.south && (play.directionFacing == 3));
			}
			else
			{
				Assert.Fail();
			}
		}

		[Test]
		public void playerWestMovement()
		{
			var play = Substitute.For<Player> ();
			var room = Substitute.For<Room> ();
			play.directionFacing = 4;
			
			GameObject currentRoom = GameObject.Find("Room3");
			
			
			if(currentRoom.GetComponent<Room>().north && !currentRoom.GetComponent<Room>().east &&
			   currentRoom.GetComponent<Room>().south && !currentRoom.GetComponent<Room>().west)
			{
				Assert.True(!room.west && (play.directionFacing == 4));
			}
			else
			{
				Assert.Fail();
			}
		}
	}
}
