using System.Collections.Generic;

namespace GameCraft
{
	public class Message<ObjType>
	{
		protected List<string> _receivers = new List<string>();
		protected CommandObject _command;


	    public List<string> Receivers { 
			get{ return _receivers; }
			set{ _receivers = value; }
		}
		public CommandObject Command { 
			get{ return _command;}
		}

	}
}

