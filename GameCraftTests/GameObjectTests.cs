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
		[TestCase(TestName="Adding a property to a gameObject via GameObjectProperty should not throw an exception")]
		public void AddPropertyToGameObject()
		{
			TestDelegate _addPropertyToGameObject = new TestDelegate (_AddPropertyToGameObject);

			Assert.DoesNotThrow (_addPropertyToGameObject);
		}
		public void _AddPropertyToGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			Assert.IsTrue (newGameObject.AddProperty (newGameObjProperty));
		}
		[TestCase(TestName="Adding a property to a gameObject via string should not throw an exception")]
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
		[TestCase(TestName="Adding a property via GameObjectProperty that already exists should return false")]
		public void AddPropertyThatAlreadyExists_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsFalse (newGameObject.AddProperty (newGameObjProperty));
		}
		[TestCase(TestName="Adding a property via string that already exists should return false")]
		public void AddPropertyNameThatAlreadyExists_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			newGameObject.AddProperty ("new property");

			Assert.IsFalse (newGameObject.AddProperty ("new property"));
		}
		[TestCase(TestName="Removing a property from a GameObject via GameObjectProperty should work succesfully")]
		public void RemovePropertyFromGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);
			newGameObject.RemoveProperty (newGameObjProperty);

			Assert.IsFalse(newGameObject.HasProperty(newGameObjProperty));
		}
		[TestCase(TestName="Removing a property from a GameObject via string should work succesfully")]
		public void RemovePropertyByNameFromGameObject()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.RemoveProperty ("test property"));
		}
		[TestCase(TestName="Removing a property via GameObjectProperty from a GameObject when it doesn't exist should return false")]
		public void RemovePropertyFromGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");

			Assert.IsFalse(newGameObject.RemoveProperty(newGameObjProperty));
		}
		[TestCase(TestName="Removing a property via string from a GameObject when it doesn't exist should returnfalse")]
		public void RemovePropertyFromGameObjectByName_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameObject.RemoveProperty ("doesn't exist"));
		}
		[TestCase(TestName="Setting a Property on a GameObject that exists should return true")]
		public void SetPropertyOnGameObject_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);
			newGameObjProperty.Value = "string";

			Assert.IsTrue (newGameObject.SetProperty (newGameObjProperty));
		}
		[TestCase(TestName="Setting a Property on a GameObject that doesn't exist should return false")]
		public void SetPropertyOnGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");

			Assert.IsFalse (newGameObject.SetProperty (newGameObjProperty));
		}
		[TestCase(TestName="HasProperty() via GameObjectProperty should return true when a Property matching exists")]
		public void HasPropertyOnGameObjectWithProperty_ReturnsTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.HasProperty(newGameObjProperty));
		}
		[TestCase(TestName="HasProperty() via string should return true when a Property exists")]
		public void HasPropertyOnGameObjectWithProperty_ByName_ReturnsTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			GameObjectProperty newGameObjProperty = new GameObjectProperty ("test property");
			newGameObject.AddProperty (newGameObjProperty);

			Assert.IsTrue (newGameObject.HasProperty ("test property"));
		}
		[TestCase(TestName="HasProperty should return false if a Property on a GameObject doesn't exist")]
		public void HasPropertyOnGameObject_DoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");

			Assert.IsFalse (newGameObject.HasProperty ("doesn't exist"));
		}
		[TestCase(TestName="Receiving an Add Message succesfully should return true")]
		public void ReceiveAddMessage_Success_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName);
			newProperty.Value = false;
			ObjectMessage newMessage = new ObjectMessage (CommandObject.add, new List<GameObjectProperty>());
			newMessage.PropertyList.Add (newProperty);
			Receipt<List<GameObjectProperty>> newReceipt =  newGameObject.Receive (newMessage);
			Assert.IsTrue (newReceipt.Status);
			Assert.IsTrue (newGameObject.HasProperty (propName));
		}
		[TestCase(TestName="Receiving an Add Message when a property with that name already exists")]
		public void ReceiveAddMessage_PropertyExists_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName);
			GameObjectProperty newerProperty = new GameObjectProperty (propName);
			newProperty.Value = false;
			ObjectMessage newMessage = new ObjectMessage (CommandObject.add, newProperty);
			ObjectMessage newerMessage = new ObjectMessage (CommandObject.add, newerProperty);
			newGameObject.Receive (newMessage);
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newerMessage);
			Assert.GreaterOrEqual (newReceipt.Failures.Count, 1);

		}
		[TestCase(TestName="Receiving a Set Message when a property exists should return true")]
		public void ReceiveSetMessage_PropertyExists_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty(propName, false);
			newGameObject.AddProperty (newProperty);

			GameObjectProperty newerProperty = new GameObjectProperty(propName, true);
			ObjectMessage newMessage = new ObjectMessage (CommandObject.set, newerProperty);
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newMessage);
			Assert.IsTrue (newReceipt.Status);
			Assert.GreaterOrEqual (newReceipt.Response.Count, 1);
			Assert.IsTrue (newGameObject.GetProperty (propName).Status);

		}
		[TestCase(TestName="Receiving a Set message when a property doesn't exist should return false")]
		public void ReceiveSetMessage_PropertyDoesntExist_ReturnFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty(propName, false);

			ObjectMessage newMessage = new ObjectMessage (CommandObject.set, newProperty);
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newMessage);

			Assert.GreaterOrEqual (newReceipt.Failures.Count, 1);
		}
		[TestCase(TestName="Receiving a Get message when a property exists should return true")]
		public void ReceiveGetMessage_PropertyExists_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName, true);
			newGameObject.AddProperty (newProperty);

			GameObjectProperty messageProp = new GameObjectProperty (propName);

			ObjectMessage newMessage = new ObjectMessage (CommandObject.get, messageProp);
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newMessage);

			Assert.GreaterOrEqual (newReceipt.Response.Count, 1);
		}
		[TestCase(TestName="Receive a Get message when a property exists should return the property")]
		public void ReceiveGetMessage_PropertExists_ReturnProperty()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName, true);

			newGameObject.AddProperty (newProperty);

			GameObjectProperty messageProp = new GameObjectProperty (propName);

			ObjectMessage newMessage = new ObjectMessage (CommandObject.get, messageProp);
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newMessage);

			Assert.AreEqual (newReceipt.Response.Find((GameObjectProperty obj) => obj.Name == newProperty.Name).Name, newProperty.Name);
		}
		[TestCase(TestName="Receieving a Get message when a property doesn't exist should return false")]
		public void ReceiveGetMessage_PropertyDoesntExist()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName);

			ObjectMessage newMessage = new ObjectMessage (CommandObject.get, newProperty);

			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newMessage);

			Assert.GreaterOrEqual (newReceipt.Failures.Count, 1);
		}
		[TestCase(TestName="Receving a Remove message removes a property from a GameObject, and returns true")]
		public void ReceiveRemoveMessage_RemoveProperty_ReturnTrue()
		{
			GameObject newGameObject = new GameObject ("new game object");
			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName);

			newGameObject.AddProperty (newProperty);

			ObjectMessage newmessage = new ObjectMessage (CommandObject.remove, newProperty); 
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newmessage);

			Assert.IsFalse(newGameObject.HasProperty(propName));
			Assert.GreaterOrEqual (newReceipt.Response.Count, 1);

		}
		[TestCase(TestName="Receiving a Remove message when a property doesn't exists returns false")]
		public void ReceiveRemoveMessage_PropertyDoesntExist_ReturnsFalse()
		{
			GameObject newGameObject = new GameObject ("new game object");

			string propName = "new property";
			GameObjectProperty newProperty = new GameObjectProperty (propName);

			ObjectMessage newmessage = new ObjectMessage (CommandObject.remove, newProperty); 
			Receipt<List<GameObjectProperty>> newReceipt = newGameObject.Receive (newmessage);

			Assert.GreaterOrEqual (newReceipt.Failures.Count, 1);
		}
	}
}

