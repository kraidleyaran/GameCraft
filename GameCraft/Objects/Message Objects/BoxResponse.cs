using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class BoxResponse
	{
	    private readonly string _name;
        private List<GameObject> _objList = new List<GameObject> ();


		public BoxResponse (string name)
		{
		    _name = name;
		}

	    public BoxResponse(string name, List<GameObject> objList)
	    {
	        _name = name;
	        _objList = objList;
	    }

	    public string Name
	    {
            get { return _name;  }
	    }

	    public List<GameObject> ObjList
	    {
            get { return _objList;}
            set { _objList = value;  }
	    }
	}
}

