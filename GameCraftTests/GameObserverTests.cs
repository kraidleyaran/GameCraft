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
	    [TestCase(TestName = "Registering a GameObject to be observed should return true")]
	    public void ObserveGameObject_ReturnTrue()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Observer.Clear();
            GameObject newGameObject = new GameObject("new game object");
	        Assert.IsTrue(Observer.RegisterGameObject(newGameObject).Status);
	    }

	    [TestCase(TestName = "Registering a GameObject to be observed should return the object ")]
	    public void ObserveGameObject_ReturnObject()
	    {
            GameObserver Observer = GameObserver.Instance;
            Observer.Clear();
            GameObject newGameObject = new GameObject("new game object");
	        Receipt<GameObject> returnReceipt = Observer.RegisterGameObject(newGameObject);
            Assert.AreEqual(returnReceipt.Response, newGameObject);
	    }

	    [TestCase(TestName = "Registering a GameObject should place the object on the Observed list")]
	    public void ObserveGameObject_CheckForObject_ReturnTrue()
	    {

	    }
	}
}

