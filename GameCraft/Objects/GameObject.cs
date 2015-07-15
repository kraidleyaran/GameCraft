using System;
using System.Collections.Generic;

namespace GameCraft
{
    [Serializable]
	public class GameObject
	{
	    protected Guid _uniqueId;
		private List<GameObjectProperty> _properties = new List<GameObjectProperty>();

		private List<string> _boxLocations = new List<string> ();

		public GameObject (string name, string type)
		{
		    Type = type;
			Name = name;
			_uniqueId = Guid.NewGuid ();
		}

	    public GameObject(string name, List<GameObjectProperty> propList,string type)
	    {
	        Name = name;
	        Type = type;
	        _uniqueId = Guid.NewGuid();
	        AddManyProperty(propList);
	    }


		public string Name { get; protected set; }

        public string Type { get; set; }

	    public string UniqueId
		{
			get { return _uniqueId.ToString(); }
		}
		public List<string> Locations

		{
			get { return _boxLocations; }
		}
		public Receipt<List<GameObjectProperty>> Receive(ObjectMessage newMessage)
		{
			switch (newMessage.Command) {
				case CommandObject.add:
					return AddManyProperty(newMessage.PropertyList);
				case CommandObject.get:
					return GetManyProperty(newMessage.PropertyNames);
				case CommandObject.set:
					return SetManyProperty (newMessage.PropertyList);
				case CommandObject.remove:
					return RemoveManyProperty (newMessage.PropertyNames);
				default:
					return new Receipt<List<GameObjectProperty>> ("INVALID COMMAND", new List<GameObjectProperty>() , false);
			}
		}
		public bool AddProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = HasProperty (gameObjProp);
			bool response = false;

		    if (doesPropExist != false) return response;
		    _properties.Add  (gameObjProp);
		    response = true;
		    return response;
		}
		public bool AddProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);
			bool response = false;

		    if (doesPropExist != false) return response;
		    GameObjectProperty newGameObjProperty = new GameObjectProperty (propName, null);
		    _properties.Add (newGameObjProperty);
		    response = true;
		    return response;
		}
		public Receipt<List<GameObjectProperty>> AddManyProperty(List<GameObjectProperty> propertyList)
		{
			Receipt<List<GameObjectProperty>> returnReceipt = new Receipt<List<GameObjectProperty>>(Name, new List<GameObjectProperty>(), true);
			foreach (GameObjectProperty prop in propertyList) {
				bool addPropResponse = AddProperty (prop);
				if (addPropResponse) {
					returnReceipt.Response.Add (prop);
				} else {
					Failure newFail = new Failure(prop.Name);
                    newFail.FailList.Add("Property already exists in GameObject " + Name);
                    returnReceipt.Failures.Add (newFail);
				}

			}

			return returnReceipt;

		}
		public bool RemoveProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = RemoveProperty (gameObjProp.Name);
			return doesPropExist;
		}
		public bool RemoveProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);

		    if (!doesPropExist) return doesPropExist;
		    GameObjectProperty removeProp = _properties.Find (prop => prop.Name == propName);;
		    _properties.Remove (removeProp);

		    return doesPropExist;
		}
		public Receipt<List<GameObjectProperty>> RemoveManyProperty(List<string> propNameList)
		{
			Receipt<List<GameObjectProperty>> returnReceipt = new Receipt<List<GameObjectProperty>> (Name, new List<GameObjectProperty>(), true);
			foreach (string name in propNameList) {				
				if (HasProperty (name)) {
					returnReceipt.Response.Add (GetProperty (name).Response);
					RemoveProperty (name);
				} else {
                    Failure newFail = new Failure(name);
                    newFail.FailList.Add("Property does not exist on GameObject " + Name);
					returnReceipt.Failures.Add (newFail);
				}
			}

			return returnReceipt;
		}
		public bool SetProperty(GameObjectProperty gameObjProp)
		{
			bool doesPropExist = RemoveProperty (gameObjProp.Name);
			if (doesPropExist) {
				AddProperty (gameObjProp);
			}

			return doesPropExist;
		}
		public Receipt<List<GameObjectProperty>> SetManyProperty(List<GameObjectProperty> propList)
		{
			Receipt<List<GameObjectProperty>> returnReceipt = new Receipt<List<GameObjectProperty>> (Name, new List<GameObjectProperty> (), true);
			foreach (GameObjectProperty prop in propList) {
				if (SetProperty (prop)) {
					returnReceipt.Response.Add (prop);
				} else {
                    Failure newFail = new Failure(prop.Name);
                    newFail.FailList.Add("Does not exist in GameObject " + Name);
                    returnReceipt.Failures.Add(newFail);
				}
			}
			return returnReceipt;
		}
		public bool HasProperty(GameObjectProperty gameObjProp)
		{
			return HasProperty (gameObjProp.Name);
			
		}
		public bool HasProperty(string propName)
		{
			return _properties.Contains(_properties.Find(prop => prop.Name == propName));
		}
		public Receipt<GameObjectProperty> GetProperty(string propName)
		{
			bool doesPropExist = HasProperty (propName);

			GameObjectProperty returnProperty = new GameObjectProperty (propName);

			if (doesPropExist) {
				returnProperty = _properties.Find (prop => prop.Name == propName);
			    return new Receipt<GameObjectProperty> (Name, returnProperty, doesPropExist);
			}
		    Failure newFail = new Failure(propName);
		    newFail.FailList.Add("Does not exist on " + Name + " GameObject");
            Receipt<GameObjectProperty> returnReceipt = new Receipt<GameObjectProperty>(Name, returnProperty, false);
		    returnReceipt.Failures.Add (newFail);
		    return returnReceipt;
		}
		public Receipt<List<GameObjectProperty>> GetManyProperty(List<string> propNameList)
		{
			Receipt<List<GameObjectProperty>> returnReceipt = new Receipt<List<GameObjectProperty>> (Name, new List<GameObjectProperty> (), true);
			foreach (string propName in propNameList) {
				Receipt<GameObjectProperty> getResponse = GetProperty (propName);
				if (getResponse.Status) {
					returnReceipt.Response.Add (getResponse.Response);
				} else {
                    Failure newFail = new Failure(propName);
                    newFail.FailList.Add("Does not exist in GameObject " + Name);
					returnReceipt.Failures.Add (newFail);
				}
			}

			return returnReceipt;
		}

        public Receipt<List<GameObjectProperty>> GetAllProperties()
        {
            return new Receipt<List<GameObjectProperty>>(Name, _properties, true);
        }

	    public GameObject CloneGameObject(string name)
	    {
            return new GameObject(name, _properties, Type);
	    }

	}

}

