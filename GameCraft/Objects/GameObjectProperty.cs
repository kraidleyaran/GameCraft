namespace GameCraft
{
	public class GameObjectProperty
	{
	    public GameObjectProperty (string propertyName)
		{
			Name = propertyName;
		}
		public GameObjectProperty (string propertyName, object propertyValue)
		{
			Name = propertyName;
			Value = propertyValue;
		}

		public string Name { get; private set; }

	    public object Value { get; set; }
	}
}

