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
		[Test()]
		public void AddGameObjectToGameBox()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			bool addGameObject = newGameBox.Add (newGameObject);
			Assert.IsTrue (addGameObject);
		}

		[Test()]
		public void AddManyGameObjectsToGameBox()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			int objCount = 5;
			IList<GameObject> gameObjectList = new List<GameObject> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				gameObjectList.Add (new GameObject ("new game object" + iiObj.ToString()));
			}

			IDictionary<GameObject, bool> response = new Dictionary<GameObject, bool> ();

			response = newGameBox.Add (gameObjectList);
			foreach (KeyValuePair<GameObject, bool> entry in response) {
				Assert.IsTrue (entry.Value);
			}
		}
		[Test()]
		public void CannotAddTheSameObject()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject);
			Assert.IsFalse(newGameBox.Add(newGameObject));
		}
		[Test()]
		public void RemoveGameObjectFromGameBox()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			GameObject newGameObject = new GameObject ("new game object");
			newGameBox.Add (newGameObject);
			Assert.IsTrue (newGameBox.Remove(newGameObject));
		}
		[Test()]
		public void RemoveManyGameObjectsFromGameBox()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			int objCount = 5;
			IList<GameObject> gameObjectList = new List<GameObject> ();

			for (var iiObj = 0; iiObj < objCount; iiObj++) {
				gameObjectList.Add (new GameObject ("new game object" + iiObj.ToString()));
			}
			newGameBox.Add (gameObjectList);

			IDictionary<GameObject, bool> response = new Dictionary<GameObject, bool> ();

			response = newGameBox.Remove (gameObjectList);
			foreach (KeyValuePair<GameObject, bool> entry in response) {
				Assert.IsTrue (entry.Value);
			}
		}
		[Test()]
		public void CannotRemoveObjectDoesntExist()
		{
			GameBox<GameObject> newGameBox = new GameBox<GameObject> ("new game box");
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameBox.Remove (newGameObject));
		}
	}
}

