using System;
using System.Collections.Generic;
using GameCraft.GameMaster;
namespace GameCraft
{
	public class ObjectMessage : Message<GameObject>
	{
		protected GameObjectProperty _property;


		public ObjectMessage (CommandObject command)
		{
			_command = command;
		}
		public ObjectMessage (CommandObject command, GameObjectProperty property)
		{
			_property = property;
			_command = command;
		}

		public GameObjectProperty Property{
			get {return _property;}
		}

	}
}

