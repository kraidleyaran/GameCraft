using System;
using System.Collections.Generic;
using GameCraft.GameMaster;
namespace GameCraft
{
	public class ObjectMessage : Message<GameObject>
	{
		protected List<GameObjectProperty> _propertyList = new List<GameObjectProperty> ();


		public ObjectMessage (CommandObject command)
		{
			_command = command;
		}
		public ObjectMessage (CommandObject command, List<GameObjectProperty> propertyList)
		{
			_propertyList = propertyList;
			_command = command;
		}
		public ObjectMessage (CommandObject command, GameObjectProperty prop)
		{
			_command = command;
			_propertyList.Add (prop);
		}

		public List<GameObjectProperty> PropertyList{
			get {return _propertyList;}
		}

		public List<string> PropertyNames{
			get {
				List<string> names = new List<string> ();
				_propertyList.ForEach ((GameObjectProperty obj) => names.Add (obj.Name));
				return names;
			}
				
		}
	}
}

