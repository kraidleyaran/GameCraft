using NUnit.Framework;
using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraftTests
{
	[TestFixture()]
	public class GameObserverTests
	{

		[TestCase(TestName="Registering a GameBox works and returns true")]
		public void RegisterGameBox_ReturnTrue()
		{
			
			GameObserver newGameObserver = GameObserver.Instance;
			newGameObserver.Clear ();
			GameBox newGameBox = new GameBox ("new game box");

			Receipt<BoxResponse> response = newGameObserver.RegisterBox (newGameBox);
			Assert.IsTrue (response.Status);
			Assert.IsTrue (newGameObserver.GetBoxNames().Contains(newGameBox.Name));

		}
		[TestCase(TestName="Registering a GameBox with the same name returns false")]
		public void RegisterGameBox_NameExists_ReturnFalse()
		{
			GameObserver newGameObserver = GameObserver.Instance;
			newGameObserver.Clear ();
			GameBox newGameBox = new GameBox ("new game box");

			newGameObserver.RegisterBox (newGameBox);

			GameBox newerGameBox = new GameBox ("new game box");
			Receipt<BoxResponse> response = newGameObserver.RegisterBox (newerGameBox);
			Assert.IsFalse (response.Status);
		}
	}
}

