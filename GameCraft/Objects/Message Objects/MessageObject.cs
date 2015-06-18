using System;
using System.Collections.Generic;
using GameCraft.GameMaster;

/*
 * MessageObject<T> structure:
 * Receivers - <T> List
 * Property - String
 * Command - Enum -> "Add, Get, Set, execute"
 * Value - For setting properties;
 * ValueList
 * 
 * 
 */


namespace GameCraft
{
	public class Message<ObjType>
	{
		protected IList<string> _receivers;
		protected CommandObject _command;

		public Message ()
		{
		}

		public IList<string> Receivers { 
			get{ return _receivers; }
			set{ _receivers = value; }
		}
		public CommandObject Command { 
			get{ return _command;}
		}

	}
}

