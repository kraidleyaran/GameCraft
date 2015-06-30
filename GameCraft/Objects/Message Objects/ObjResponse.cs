using System.Collections.Generic;

namespace GameCraft
{
	public class ObjResponse
	{
	    private List<GameObjectProperty>  _objProps = new List<GameObjectProperty>();
		

		public ObjResponse (string objName)
		{
			ObjName = objName;
		}

	    public ObjResponse(string objName, List<GameObjectProperty> objProps)
	    {
	        ObjName = objName;
	        _objProps = objProps;
	    }

	    public string ObjName { get; private set; }

	    public List<GameObjectProperty> ObjProperties
	    {
            get { return _objProps; }
            set { _objProps = value; }
	    }
	}
}

