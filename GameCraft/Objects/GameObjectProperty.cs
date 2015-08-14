using System;

namespace GameCraft
{
    [Serializable]
	public class GameObjectProperty
	{
	    public GameObjectProperty (string propertyName)
		{
			Name = propertyName;
		}
		public GameObjectProperty (string propertyName, object propertyValue, object defaultValue)
		{
			Name = propertyName;
			Value = propertyValue;
		    DefaultValue = defaultValue;
		}

		public string Name { get; private set; }

	    public object Value { get; set; }

        public object DefaultValue { get; set; }
	}
}

