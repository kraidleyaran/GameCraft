using System;

namespace GameCraft.GameMaster
{
	public class GameObjectProperty
	{
		private string _propertyName;
		private dynamic _propertyValue;

		public GameObjectProperty (string propertyName)
		{
			_propertyName = propertyName;
		}
		public GameObjectProperty (string propertyName, dynamic propertyValue)
		{
			_propertyName = propertyName;
			_propertyValue = propertyValue;
		}

		public string Name {
			get{ return _propertyName; }
		}

		public dynamic Value {
			get{ return _propertyValue; }
			set{ _propertyValue = value; }
		}

	}
}

