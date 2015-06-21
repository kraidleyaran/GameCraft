using NUnit.Framework;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraftTests
{
	[TestFixture()]
	public class GameBoxTests
	{
		[TestCase(TestName="Adding a GameObject to a GameBox works and returns true")]
		public void AddGameObjectToGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			bool addGameObject = newGameBox.Add (newGameObject);
			Assert.IsTrue (addGameObject);
		}

		[TestCase(TestName="Adding many GameObjects to a GameBox works and returns true")]
		public void AddManyGameObjectsToGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			int objCount = 5;
			List<GameObject> gameObjectList = new List<GameObject> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				string gameObjName = "new game object" + iiObj.ToString ();
				gameObjectList.Add (new GameObject (gameObjName));
			}

			Receipt<List<GameObject>> newReceipt = newGameBox.Add (gameObjectList);
			Assert.GreaterOrEqual (newReceipt.Response.Count, 4);
		}
		[TestCase(TestName="Adding the same object to a GameBox that already contains that object will return false")]
		public void CannotAddTheSameObject()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject);
			Assert.IsFalse(newGameBox.Add (newGameObject));
		}
		[TestCase(TestName="Removing a GameObject from a GameBox that contains that GameObject does succesfully and returns true")]
		public void RemoveGameObjectFromGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject);
			Assert.IsTrue (newGameBox.Remove(newGameObject.Name).Status);
		}
		[TestCase(TestName="Removing many GameObjects from a GameBox that contains those GameObjects does so succesfully and returns true")]
		public void RemoveManyGameObjectsFromGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			int objCount = 5;
			List<GameObject> gameObjectList = new List<GameObject> ();
			List<string> objNameList = new List<string> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				string objName = "new game object" + iiObj.ToString ();
				objNameList.Add (objName);
				gameObjectList.Add (new GameObject (objName));
			}
			newGameBox.Add (gameObjectList);



			Receipt<List<GameObject>> response = newGameBox.Remove (objNameList);
			Assert.GreaterOrEqual (response.Response.Count, 5);
		}
		[TestCase(TestName="Removing objects that do not exist in a GameBox returns false")]
		public void CannotRemoveObjectDoesntExist()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameBox.Remove (newGameObject.Name).Status);
		}
		[TestCase(TestName="Removing many objects from a GameBox that does not contain those items will return false")]
		public void CannotRemoveManyObjectsThatDoNotExist()
		{
			int objCount = 10;
			List<string> objNameList = new List<string> ();
			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				objNameList.Add ("new obj" + iiObj.ToString ());
			}

			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("this object exists in the box");

			newGameBox.Add (newGameObject);
			Receipt<List<GameObject>> response = newGameBox.Remove (objNameList);
			Assert.AreEqual (response.Failures.Count, objCount);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of add will add the objects to the collection")]
		public void ReceiveAddObjectBoxMessage_AddObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject);
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.GreaterOrEqual (response.Response.Count, 1);
			Assert.IsTrue(newGameBox.Contains(newGameObject.Name));

		}
		[TestCase(TestName="Receiving a BoxMessage with the command of Add but with an object that already exists in the gameBox will return as a failure")]
		public void ReceiveAddObjectBoxMessage_ObjectAlreadyExists_ReturnFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
		    newGameBox.Add(newGameObject);
            
			GameObject newerGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newerGameObject);
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.GreaterOrEqual (response.Failures.Count, 1);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of get will return the object if it exists")]
		public void ReceiveGetBoxMessage_GetObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject);
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);

			BoxMessageObject getMessage = new BoxMessageObject (CommandObject.get);
			GameObject getGameObject = new GameObject ("new game object");
			getMessage.TargetObjs.Add (getGameObject);
			Receipt<List<GameObject>> getResponse = newGameBox.Receive(getMessage);
			Assert.GreaterOrEqual (getResponse.Response.Count, 1);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of get when that object doesn't exist in the game box will return as a failure")]
		public void ReceiveGetBoxMessage_GetObjectDoesntExist_ReturnAsFailureFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.get);
			newMessage.TargetObjs.Add (newGameobject);

			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.GreaterOrEqual (response.Failures.Count, 1);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of set will set the game object and return true if the object exists")]
		public void ReceiveSetBoxMessage_SetObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject);
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);

			GameObject setGameObject = new GameObject ("new game object");
			GameObjectProperty newProp = new GameObjectProperty ("new obj property");
			setGameObject.AddProperty (newProp);
			BoxMessageObject setMessage = new BoxMessageObject(CommandObject.set);
			setMessage.TargetObjs.Add (setGameObject);
			Receipt<List<GameObject>> setResponse = newGameBox.Receive(setMessage);
			Assert.GreaterOrEqual (setResponse.Response.Count, 1);
			Assert.IsTrue (newGameBox.Contains (setGameObject.Name));
		}
		[TestCase(TestName="Receieiving a BoxMessage with the command of set when an object with that name doesn't exist inside the GameBox will return as a failure")]
		public void ReceiveSetBoxMessage_SetObjectDoesntExist_ReturnAsFailture_returnFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.set);
			newMessage.TargetObjs.Add (newGameobject);

			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.GreaterOrEqual (response.Failures.Count, 1);

		}
		[TestCase(TestName="Receieve a BoxMessage with the command of remove will remove the object from the GameBox and return trueif the object exists in the Gamebox")]
		public void ReceiveRemoveBoxMessage_RemoveObject_Returntrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject);
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);

			GameObject removeGameObject = new GameObject ("new game object");
			BoxMessageObject removeMessage = new BoxMessageObject (CommandObject.remove);
			removeMessage.TargetObjs.Add (removeGameObject);
			Receipt<List<GameObject>> removeResponse = newGameBox.Receive(removeMessage);
			Assert.GreaterOrEqual (removeResponse.Response.Count, 1);
			Assert.IsFalse(newGameBox.Contains(newGameObject.Name));
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of Remove when the object doesn't exist within the GameBox will return as a failure")]
		public void ReceiveBoxMessage_RemoveCommandObjectDoesntExist_ReturnAsFailure()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.remove);
			newMessage.TargetObjs.Add (newGameobject);

			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.GreaterOrEqual (response.Failures.Count, 1);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of none will return a receipt with a false status")]
		public void ReceiveBoxMessage_NoCommand_ReturnFalse()
		{
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.none);
			GameBox newGameBox = new GameBox ("new game box");
			Receipt<List<GameObject>> response = newGameBox.Receive (newMessage);
			Assert.IsFalse (response.Status);
			Assert.AreEqual (response.Response.Count, 0);
		}

	    [TestCase(
	         TestName =
	            "Receiving an Object Message when the object exists in the gameBox will succesfully deliver the mssage to the object"
	        )]
	    public void ReceiveObjectMessage_ObjectExists_ReturnTrue()
	    {
	        GameBox newGameBox = new GameBox("new game box");
            GameObject newGameObject = new GameObject("new game boject");
	        newGameBox.Add(newGameObject);

            GameObjectProperty newProperty = new GameObjectProperty("new property", true);
            ObjectMessage newMessage = new ObjectMessage(CommandObject.add, newProperty);
            newMessage.Receivers.Add(newGameObject.Name);

	        Receipt<List<GameObject>> returnReceipt = newGameBox.Receive(newMessage);
            Assert.LessOrEqual(returnReceipt.Failures.Count, 0);
            Assert.GreaterOrEqual(returnReceipt.Response.Count, 1);
            Assert.IsTrue(newGameObject.HasProperty(newProperty.Name));

	    }

	    [TestCase(
	        TestName = "Receiving an Object message when an object doesn't exist in the gameBox will return as a failure")]
	    public void ReceiveObjectMessage_ObjectDoesntExist_ReturnAsFailure()
	    {
            GameBox newGameBox = new GameBox("new game box");

            GameObjectProperty newProperty = new GameObjectProperty("new property", true);
            ObjectMessage newMessage = new ObjectMessage(CommandObject.add, newProperty);
            newMessage.Receivers.Add("game object that doesnt exist");

            Receipt<List<GameObject>> returnReceipt = newGameBox.Receive(newMessage);
            Assert.LessOrEqual(returnReceipt.Response.Count, 0);
            Assert.GreaterOrEqual(returnReceipt.Failures.Count, 1);

	    }

	}
}

