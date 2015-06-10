using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;
/*
 * 
 */
namespace GameCraft
{
	public class GetMessageObject<ObjType> : MessageObject<ObjType>
	{
		private string _propertyName;
		private IList<ObjType> _receivers;
		private CommandObject _command;

		public GetMessageObject(IList<ObjType> recList, string propertyName)
		{
			Receivers = recList;
			PropertyName = propertyName;
			Command = CommandObject.get;
		}
			
		public override IList<ObjType> Receivers {
			get { return _receivers; }
			set { _receivers = value; }
		}

		public override string PropertyName { 
			get { return _propertyName; }
			set { _propertyName = value; }
		}

		public override CommandObject Command {
			get { return _command; }
			set { _command = value; }
		}

	}
}

