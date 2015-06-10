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
	public abstract class MessageObject<ObjType>
	{
		public MessageObject ()
		{
		}

		public abstract IList<ObjType> Receivers { get; set;}
		public abstract string PropertyName { get; set;}
		public abstract CommandObject Command { get; set; }

	}
}

