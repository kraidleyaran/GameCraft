using System;
using System.Collections.Generic;
using GameCraft.GameMaster;
namespace GameCraft
{
	public class AddMessageObject<ObjType, ObjValueType> : MessageObject<ObjType>
	{
		private IList<ObjType> _receivers;
		private string _propertyName;
		private CommandObject _command;
		private ObjValueType _objValue;

		public AddMessageObject (IList<ObjType> recList, string propertyName, ObjValueType objValue)
		{
			Receivers = recList;
			PropertyName = propertyName;
			ObjValue = objValue;
			Command = CommandObject.add;
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

		public ObjValueType ObjValue {
			get { return _objValue; }
			set { _objValue = value; }
		}
	}
}

