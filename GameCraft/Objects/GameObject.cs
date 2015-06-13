using System;
using System.Collections.Generic;
using System.Reflection;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft.GameMaster
{
	public class GameObject
	{
		private string _name;
		private IDictionary<string, GameObjectProperty> CustomProperties = new Dictionary<string, GameObjectProperty>();

		public GameObject (string name)
		{
			_name = name;
		}


		public string Name
		{
			get { return _name; }
		}

		public Receipt<GameObjectProperty> Receive(ObjectMessage newMessage)
		{
			switch (newMessage.Command) {
				case CommandObject.add:
					return new Receipt<GameObjectProperty>(_name, newMessage.Property, AddProperty(newMessage.Property));
				case CommandObject.get:
					return GetProperty(newMessage.Property.Name);
				case CommandObject.set:
					return new Receipt<GameObjectProperty> (_name, newMessage.Property, SetProperty (newMessage.Property));
				case CommandObject.remove:
					return new Receipt<GameObjectProperty> (_name, newMessage.Property, RemoveProperty(newMessage.Property));
				default:
					return new Receipt<GameObjectProperty> ("INVALID COMMAND", newMessage.Property, false);
			}
		}
		public bool AddProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = HasProperty (gameObjProp);
			bool response = false;

			if (doesPropExist == false) {
				CustomProperties.Add (gameObjProp.Name, gameObjProp);
				response = true;
			}
			return response;
		}
		public bool AddProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);
			bool response = false;

			if (doesPropExist == false) {
				GameObjectProperty newGameObjProperty = new GameObjectProperty (propName, null);
				CustomProperties.Add (newGameObjProperty.Name, newGameObjProperty);
				response = true;
			}
			return response;
		}
		public bool RemoveProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = HasProperty (gameObjProp.Name);

			if (doesPropExist == true) {
				CustomProperties.Remove (gameObjProp.Name);
			}

			return doesPropExist;
		}
		public bool RemoveProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);

			if (doesPropExist == true) {
				CustomProperties.Remove (propName);
			}

			return doesPropExist;
		}
		public bool SetProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = RemoveProperty (gameObjProp.Name);
			if (doesPropExist == true) {
				AddProperty (gameObjProp);
			}

			return doesPropExist;
		}
		public bool HasProperty(GameObjectProperty gameObjProp)
		{
			return CustomProperties.ContainsKey (gameObjProp.Name);
		}
		public bool HasProperty(string propName)
		{
			return CustomProperties.ContainsKey (propName);
		}
		public Receipt<GameObjectProperty> GetProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);
			GameObjectProperty returnProperty = new GameObjectProperty(propName);	
			if (doesPropExist == true) {
				CustomProperties.TryGetValue (propName, out returnProperty);
			} else {
				returnProperty.Value = false;
			}
			Receipt<GameObjectProperty> returnReceipt = new Receipt<GameObjectProperty> (_name, returnProperty, doesPropExist);
			return returnReceipt;

		}

	}

}

