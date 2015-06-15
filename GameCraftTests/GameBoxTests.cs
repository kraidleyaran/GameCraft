using NUnit.Framework;
using System;
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
			bool addGameObject = newGameBox.Add (newGameBox.Name, newGameObject);
			Assert.IsTrue (addGameObject);
		}

		[TestCase(TestName="Adding many GameObjects to a GameBox works and returns true")]
		public void AddManyGameObjectsToGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			int objCount = 5;
			Dictionary<string, GameObject> gameObjectList = new Dictionary<string, GameObject> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				string gameObjName = "new game object" + iiObj.ToString ();
				gameObjectList.Add (gameObjName, new GameObject (gameObjName));
			}

			Receipt<Dictionary<GameObject, bool>> response;

			response = newGameBox.Add (gameObjectList);
			foreach (KeyValuePair<GameObject, bool> entry in response.Response) {
				Assert.IsTrue (entry.Value);
			}
		}
		[TestCase(TestName="Adding the same object to a GameBox that already contains that object will return false")]
		public void CannotAddTheSameObject()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject.Name, newGameObject);
			Assert.IsFalse(newGameBox.Add (newGameObject.Name, newGameObject));
		}
		[TestCase(TestName="Removing a GameObject from a GameBox that contains that GameObject does succesfully and returns true")]
		public void RemoveGameObjectFromGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject.Name, newGameObject);
			Assert.IsTrue (newGameBox.Remove(newGameObject.Name).Status);
		}
		[TestCase(TestName="Removing many GameObjects from a GameBox that contains those GameObjects does so succesfully and returns true")]
		public void RemoveManyGameObjectsFromGameBox()
		{
			GameBox newGameBox = new GameBox ("new game box");
			int objCount = 5;
			Dictionary<string, GameObject> gameObjectList = new Dictionary<string, GameObject> ();
			IList<string> objNameList = new List<string> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				string objName = "new game object" + iiObj.ToString ();
				objNameList.Add (objName);
				gameObjectList.Add (objName, new GameObject (objName));
			}
			newGameBox.Add (gameObjectList);

			Receipt<Dictionary<GameObject, bool>> response;

			response = newGameBox.Remove (objNameList);
			foreach (KeyValuePair<GameObject, bool> entry in response.Response) {
				Assert.IsTrue (entry.Value);
			}
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
			IList<string> objNameList = new List<string> ();
			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				objNameList.Add ("new obj" + iiObj.ToString ());
			}

			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("this object exists in the box");

			newGameBox.Add (newGameObject.Name, newGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Remove (objNameList);
			foreach (KeyValuePair<string, bool> entry in response.Failures) {
				Assert.IsTrue(objNameList.Contains(entry.Key));
				Assert.IsFalse (entry.Value);
			}
			Assert.AreEqual (response.Failures.Count, objCount);

		}
		[TestCase(TestName="Receiving a BoxMessage with the command of add will add the objects to the collection")]
		public void ReceiveAddObjectBoxMessage_AddObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject.Name, newGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			foreach (KeyValuePair<GameObject, bool> entry in response.Response) {
				Assert.IsTrue (entry.Value);
			}
			Assert.IsTrue(newGameBox.Contains(newGameObject.Name));

		}
		[TestCase(TestName="Receiving a BoxMessage with the command of Add but with an object that already exists in the gameBox will return as a failure")]
		public void ReceiveAddObjectBoxMessage_ObjectAlreadyExists_ReturnFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameObject = new GameObject ("new game object");

			GameObject newerGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newerGameObject.Name, newerGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			bool objResponse;
			response.Failures.TryGetValue (newerGameObject.Name,out objResponse);
			Assert.IsFalse (objResponse);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of get will return the object if it exists")]
		public void ReceiveGetBoxMessage_GetObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject.Name, newGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);

			BoxMessageObject getMessage = new BoxMessageObject (CommandObject.get);
			GameObject getGameObject = new GameObject ("new game object");
			getMessage.TargetObjs.Add (getGameObject.Name, getGameObject);
			Receipt<Dictionary<GameObject, bool>> getResponse = newGameBox.Receive(getMessage);
			Assert.IsTrue(getResponse.Response.ContainsKey(newGameObject));
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of get when that object doesn't exist in the game box will return as a failure")]
		public void ReceiveGetBoxMessage_GetObjectDoesntExist_ReturnAsFailureFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.get);
			newMessage.TargetObjs.Add (newGameBox.Name, newGameobject);

			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			bool objResponse;
			response.Failures.TryGetValue (newGameBox.Name, out objResponse);
			Assert.IsFalse (objResponse);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of set will set the game object and return true if the object exists")]
		public void ReceiveSetBoxMessage_SetObject_ReturnTrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject.Name, newGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);

			GameObject setGameObject = new GameObject ("new game object");
			GameObjectProperty newProp = new GameObjectProperty ("new obj property");
			setGameObject.AddProperty (newProp);
			BoxMessageObject setMessage = new BoxMessageObject(CommandObject.set);
			setMessage.TargetObjs.Add (setGameObject.Name, setGameObject);
			Receipt<Dictionary<GameObject, bool>> setResponse = newGameBox.Receive(setMessage);
			Assert.IsTrue(setResponse.Response.ContainsKey(setGameObject));
			bool objectResponse;
			setResponse.Response.TryGetValue (setGameObject, out objectResponse);
			Assert.IsTrue(objectResponse);
			Assert.IsTrue (newGameBox.Contains (setGameObject.Name));
		}
		[TestCase(TestName="Receieiving a BoxMessage with the command of set when an object with that name doesn't exist inside the GameBox will return as a failure")]
		public void ReceiveSetBoxMessage_SetObjectDoesntExist_ReturnAsFailture_returnFalse()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.set);
			newMessage.TargetObjs.Add (newGameBox.Name, newGameobject);

			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			bool objResponse;
			response.Failures.TryGetValue (newGameBox.Name, out objResponse);
			Assert.IsFalse (objResponse);

		}
		[TestCase(TestName="Receieve a BoxMessage with the command of remove will remove the object from the GameBox and return trueif the object exists in the Gamebox")]
		public void ReceiveRemoveBoxMessage_RemoveObject_Returntrue()
		{
			GameBox newGameBox = new GameBox ("new game box");

			GameObject newGameObject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.add);
			newMessage.TargetObjs.Add (newGameObject.Name, newGameObject);
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);

			GameObject removeGameObject = new GameObject ("new game object");
			BoxMessageObject removeMessage = new BoxMessageObject (CommandObject.remove);
			removeMessage.TargetObjs.Add (removeGameObject.Name, removeGameObject);
			Receipt<Dictionary<GameObject, bool>> removeResponse = newGameBox.Receive(removeMessage);

			Assert.IsTrue (removeResponse.Status);
			bool objectResponse;
			removeResponse.Response.TryGetValue (newGameObject, out objectResponse);
			Assert.IsTrue (objectResponse);
			Assert.IsFalse(newGameBox.Contains(newGameObject.Name));
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of Remove when the object doesn't exist within the GameBox will return as a failure")]
		public void ReceiveBoxMessage_RemoveCommandObjectDoesntExist_ReturnAsFailure()
		{
			GameBox newGameBox = new GameBox ("new game box");
			GameObject newGameobject = new GameObject ("new game object");
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.remove);
			newMessage.TargetObjs.Add (newGameBox.Name, newGameobject);

			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			bool objResponse;
			response.Failures.TryGetValue (newGameBox.Name, out objResponse);
			Assert.IsFalse (objResponse);
		}
		[TestCase(TestName="Receiving a BoxMessage with the command of none will return a receipt with a false status")]
		public void ReceiveBoxMessage_NoCommand_ReturnFalse()
		{
			BoxMessageObject newMessage = new BoxMessageObject (CommandObject.none);
			GameBox newGameBox = new GameBox ("new game box");
			Receipt<Dictionary<GameObject, bool>> response = newGameBox.Receive (newMessage);
			Assert.IsFalse (response.Status);
			Assert.AreEqual (response.Response.Count, 0);
		}

	}
}

