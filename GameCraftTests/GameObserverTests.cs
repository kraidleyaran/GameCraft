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

			Receipt<GameBox> response = newGameObserver.RegisterBox (newGameBox);
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
			Receipt<GameBox> response = newGameObserver.RegisterBox (newerGameBox);
			Assert.IsFalse (response.Status);
		}

	    [TestCase(TestName = "Registering a GameBox with GameObjects will also register the game Gameobjects")]
	    public void RegisterGameBox_WithGameobjects_ReturnTrue()
	    {
            GameObserver newGameObserver = GameObserver.Instance;
            newGameObserver.Clear();
            GameBox newGameBox = new GameBox("new game box");
	        GameObject newGameObject = new GameObject("new game object");
	        newGameBox.Add(newGameObject);

	        Receipt<GameBox> returnReceipt =  newGameObserver.RegisterBox(newGameBox);
            Assert.IsTrue(returnReceipt.Status);
            Assert.LessOrEqual(returnReceipt.Failures.Count, 0);
	    }

	    [TestCase(
	        TestName =
	            "Registering a GameObject with a name of another GameObject but with different Unique Ids will result in a Failure"
	        )]
	    public void RegisterGameBox_WithGameObjectNameBeingObserved_DifferentId_ReturnAsFailure()
	    {
            GameObserver newGameObserver = GameObserver.Instance;
            newGameObserver.Clear();
            GameBox newGameBox = new GameBox("new game box");
            GameObject newGameObject = new GameObject("new game object");
            newGameBox.Add(newGameObject);
	        newGameObserver.RegisterBox(newGameBox);

            GameBox newerGameBox = new GameBox("newer game box");
	        GameObject newerGameObject = new GameObject("new game object");
	        newerGameBox.Add(newerGameObject);
	        Receipt<GameBox> returnReceipt = newGameObserver.RegisterBox(newerGameBox);
            Assert.IsTrue(returnReceipt.Status);
            Assert.GreaterOrEqual(returnReceipt.Failures.Count, 1);
	    }

	    [TestCase(
	        TestName =
	            "Registering a GameObject that is a GameObject already being observed should not result in a failure")]
	    public void RegisterGameBox_WithGameObjectAlreadyBeingObserved_SameObject_ReturnTrue()
	    {
            GameObserver newGameObserver = GameObserver.Instance;
            newGameObserver.Clear();
            GameBox newGameBox = new GameBox("new game box");
            GameObject newGameObject = new GameObject("new game object");
            newGameBox.Add(newGameObject);
            newGameObserver.RegisterBox(newGameBox);

            GameBox newerGameBox = new GameBox("newer game box");
	        newerGameBox.Add(newGameObject);
            Receipt<GameBox> returnReceipt = newGameObserver.RegisterBox(newerGameBox);
            Assert.IsTrue(returnReceipt.Status);
            Assert.LessOrEqual(returnReceipt.Failures.Count, 0);
	    }

	}
}

