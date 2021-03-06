﻿using System.Collections.Generic;

namespace GameCraft
{
	public class GameBox
	{
		private readonly string _name;
		private readonly List<string> _container = new List<string> ();

		public GameBox (string name)
		{
			_name = name;
		}
		public string Name
		{
			get { return _name; }
		}

	    public bool Add(string name)
	    {
	        bool objExists = _container.Contains(name);
	        if (objExists)
	        {
	            return false;
	        }
	        _container.Add(name);
	        return true;
	    }

	    public bool Remove(string name)
	    {
	        return _container.Remove(name);
	    }

	    public bool Contains(string name)
	    {
	        return _container.Contains(name);
	    }

	}
}

