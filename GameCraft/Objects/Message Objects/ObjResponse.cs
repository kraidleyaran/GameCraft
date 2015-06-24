using System;
using System.Collections.Generic;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class ObjResponse
	{
		private string _objName;
        private List<GameObjectProperty>  _objProps = new List<GameObjectProperty>();
		

		public ObjResponse (string objName)
		{
			_objName = objName;
		}

	    public ObjResponse(string objName, List<GameObjectProperty> objProps)
	    {
	        _objName = objName;
	        _objProps = objProps;
	    }

	    public string ObjName
		{
			get {return _objName; }
		}

	    public List<GameObjectProperty> ObjProperties
	    {
            get { return _objProps; }
            set { _objProps = value; }
	    }
	}
}

