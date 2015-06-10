using System;

namespace GameCraft.GameMaster
{
	public class GameObjectProperty<ObjType>
	{
		private string _propertyName;
		private ObjType _propertyValue;

		public GameObjectProperty (string propertyName)
		{
			_propertyName = propertyName;
		}

		public string Name {
			get{ return _propertyName; }
		}

		public ObjType Value {
			get{ return _propertyValue; }
			set{ _propertyValue = value; }
		}

	}
}

