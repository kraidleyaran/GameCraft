using System;
using System.Collections.Generic;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class ExecuteMessageObject<ObjType, ObjValueType> : MessageObject<ObjType>
	{
		private IList<ObjType> _receivers;
		private string _propertyName;
		private CommandObject _command;
		private IList<ObjValueType> _objValueList;

		public ExecuteMessageObject (IList<ObjType> recList, string propertyName, IList<ObjValueType> objValueList)
		{
			Receivers = recList;
			PropertyName = propertyName;
			ObjValueList = objValueList;
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

		public IList<ObjValueType> ObjValueList{
			get { return _objValueList; }
			set { _objValueList = value; }
		}
	}
}

