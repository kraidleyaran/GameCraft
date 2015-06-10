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
		private IDictionary<string, GameObjectProperty<dynamic>> CustomProperties = new Dictionary<string, GameObjectProperty<dynamic>>();

		public GameObject (string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public void Receive(MessageObject<GameObject> newMessage)
		{

		}

		public bool AddProperty(GameObjectProperty<dynamic> gameObjProp)
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
				GameObjectProperty<dynamic> newGameObjProperty = new GameObjectProperty<dynamic> (propName);
				CustomProperties.Add (newGameObjProperty.Name, newGameObjProperty);
				response = true;
			}
			return response;
		}
		public bool RemoveProperty(GameObjectProperty<dynamic> gameObjProp)
		{
			bool doesPropExist = HasProperty (gameObjProp);

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
		public bool SetProperty(GameObjectProperty<dynamic> gameObjProp)
		{
			bool doesPropExist = RemoveProperty (gameObjProp);
			if (doesPropExist == true) {
				AddProperty (gameObjProp);
			}

			return doesPropExist;
		}
		public bool HasProperty(GameObjectProperty<dynamic> gameObjProp)
		{
			return CustomProperties.ContainsKey (gameObjProp.Name);
		}
		public bool HasProperty(string propName)
		{
			return CustomProperties.ContainsKey (propName);
		}


	}

}

