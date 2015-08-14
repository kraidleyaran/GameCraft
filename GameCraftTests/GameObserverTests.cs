using System.Collections.Generic;
using System.Linq;
using GameCraft;
using NUnit.Framework;

namespace GameCraftTests
{
	[TestFixture]
	public class GameObserverTests
	{
	    [TestCase(TestName = "Registering a GameObject to be observed should return true")]
	    public void ObserveGameObject_ReturnTrue()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Observer.Clear();
            GameObject newGameObject = new GameObject("new game object", "test");
	        Assert.IsTrue(Observer.RegisterGameObject(newGameObject).Status);
	    }

	    [TestCase(TestName = "Registering a GameObject to be observed should return the object ")]
	    public void ObserveGameObject_ReturnObject()
	    {
            GameObserver Observer = GameObserver.Instance;
            Observer.Clear();
            GameObject newGameObject = new GameObject("new game object", "test");
	        Receipt<GameObject> returnReceipt = Observer.RegisterGameObject(newGameObject);
            Assert.AreEqual(returnReceipt.Response, newGameObject);
	    }

	    [TestCase(TestName = "Registering a GameObject should place the object on the Observed list")]
	    public void ObserveGameObject_CheckForObject_ReturnTrue()
	    {
            GameObserver Observer = GameObserver.Instance;
            Observer.Clear();

            GameObject newGameObject = new GameObject("new game object", "test");

            Assert.IsFalse(Observer.DoesGameObjNameExist(newGameObject.Name));

	        Observer.RegisterGameObject(newGameObject);
	        Assert.IsTrue(Observer.DoesGameObjNameExist(newGameObject.Name));
	    }

	    [TestCase(TestName = "Using CreateBox() will createa a GameBox when that name does not exist")]
	    public void CreateGameBox()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Assert.IsTrue(Observer.CreateBox("new game box"));
            Assert.IsTrue(Observer.DoesBoxExist("new game box"));
	    }

	    [TestCase(
	        TestName = "Using CreateBox() when a GameBox with that name exists will not create the box and return false")]
	    public void CreateGameBox_AlreadyExists()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Observer.Clear();
	        Assert.IsTrue((Observer.CreateBox("new game box")));
	        Assert.IsFalse((Observer.CreateBox("new game box")));
	    }

	    [TestCase(TestName = "Using RemoveBox() will remove a GameBox when that name does exist")]
	    public void RemoveGameBox()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Observer.Clear();
	        Observer.CreateBox("new game box");
            Assert.IsTrue(Observer.RemoveBox("new game box"));
	    }

	    [TestCase(TestName = "Using RemoveBox() when a GameBox with that name does not exist will return false")]
	    public void RemoveGameBox_DoesntExist_ReturnFalse()
	    {
	        GameObserver Observer = GameObserver.Instance;
	        Observer.Clear();
	        Assert.IsFalse(Observer.DoesBoxExist("new game box"));
            Assert.IsFalse(Observer.RemoveBox("new game box"));
	    }

	    [TestCase(TestName = "Sending a message succesfully to a GameObject will return true")]
	    public void SendMessageToGameObject_ReturnTrue()
	    {
            GameObserver Observer = GameObserver.Instance;
            Observer.Clear();
            GameObject newGameObject = new GameObject("new game object", "test");
	        Observer.RegisterGameObject(newGameObject);

            GameObjectProperty newProperty = new GameObjectProperty("new property", true, true);
            ObjectMessage newMessage = new ObjectMessage(CommandObject.add);
            newMessage.PropertyList.Add(newProperty);
            newMessage.Receivers.Add(newGameObject.Name);
	        Receipt<List<ObjResponse>> returnReceipt = Observer.SendMessage(newMessage);
            Assert.IsTrue(returnReceipt.Status);
	    }

	    [TestCase(TestName = "Sending a message to an object gets the proper response")]
	    public void SendMessageToGameObject_AddProperty_ReturnProperResponse()
	    {
            GameObserver Observer = GameObserver.Instance;
            Observer.Clear();
            GameObject newGameObject = new GameObject("new game object", "test");
            Observer.RegisterGameObject(newGameObject);

            GameObjectProperty newProperty = new GameObjectProperty("new property", true, true);
            ObjectMessage newMessage = new ObjectMessage(CommandObject.add);
            newMessage.PropertyList.Add(newProperty);
            newMessage.Receivers.Add(newGameObject.Name);
            Observer.SendMessage(newMessage);
	        Receipt<GameObjectProperty> returnReceipt = newGameObject.GetProperty(newProperty.Name);
            Assert.AreEqual(true, returnReceipt.Response.Value);

            GameObjectProperty getProperty = new GameObjectProperty("new property");
            ObjectMessage getMessage = new ObjectMessage(CommandObject.get, getProperty);
            getMessage.Receivers.Add(newGameObject.Name);
	        Receipt<List<ObjResponse>> newReceipt = Observer.SendMessage(getMessage);
	        foreach (GameObjectProperty prop in newReceipt.Response.SelectMany(response => response.ObjProperties))
	        {
	            Assert.AreEqual(true,prop.Value);
	        }

	    }
	}
}

