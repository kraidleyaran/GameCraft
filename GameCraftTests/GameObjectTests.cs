using NUnit.Framework;
using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;


namespace GameCraftTests
{
	[TestFixture ()]
	public class GameObjectTests
	{
		[Test()]
		public void AddPropertyToGameObject()
		{
			TestDelegate _addPropertyToGameObject = new TestDelegate (_AddPropertyToGameObject);

			Assert.DoesNotThrow (_addPropertyToGameObject);
		}
		public void _AddPropertyToGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			Assert.IsTrue (newGameObject.AddProperty (newGameObjProperty));
		}
		[Test()]
		public void AddPropertyToGameObject_StringMethod()
		{
			TestDelegate _addPropertyToGameObject = new TestDelegate (_AddPropertyToGameObject);

			Assert.DoesNotThrow (_addPropertyToGameObject);
		}
		public void _AddPropertyToGameObject_StringMethod()
		{
			GameObject newGameObject = new GameObject ("new game object");
			Assert.IsTrue (newGameObject.AddProperty ("test property"));
		}
		[Test()]
		public void AddPropertyThatAlreadyExists_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsFalse (newGameObject.AddProperty (newGameObjProperty));
		}
		[Test()]
		public void AddPropertyNameThatAlreadyExists_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			newGameObject.AddProperty ("new property");

			Assert.IsFalse (newGameObject.AddProperty ("new property"));
		}
		[Test()]
		public void RemovePropertyFromGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);
			newGameObject.RemoveProperty (newGameObjProperty);

			Assert.IsFalse(newGameObject.HasProperty(newGameObjProperty));
		}
		[Test()]
		public void RemovePropertyByNameFromGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.RemoveProperty ("test property"));
		}
		[Test()]
		public void RemovePropertyFromGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");

			Assert.IsFalse(newGameObject.RemoveProperty(newGameObjProperty));
		}
		[Test()]
		public void RemovePropertyFromGameObjectByName_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameObject.RemoveProperty ("doesn't exist"));
		}
		[Test()]
		public void SetPropertyOnGameObject_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);
			newGameObjProperty.Value = "string";

			Assert.IsTrue (newGameObject.SetProperty (newGameObjProperty));
		}
		[Test()]
		public void SetPropertyOnGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");

			Assert.IsFalse (newGameObject.SetProperty (newGameObjProperty));
		}
		[Test()]
		public void HasPropertyOnGameObjectWithProperty_ReturnsTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.HasProperty(newGameObjProperty));
		}
		[Test()]
		public void HasPropertyOnGameObjectWithProperty_ByName_ReturnsTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.HasProperty ("test property"));
		}
		[Test()]
		public void HasPropertyOnGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameObject.HasProperty ("doesn't exist"));
		}
	}
}

