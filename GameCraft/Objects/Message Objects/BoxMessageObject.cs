using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class BoxMessageObject : Message<GameBox>
	{
		protected Dictionary<string, GameObject> _targetObjs = new Dictionary<string, GameObject>();

		public BoxMessageObject (CommandObject command)
		{
			_command = command;
		}
		public BoxMessageObject(CommandObject command, Dictionary<string, GameObject> targetObjs)
		{
			_command = command;
			_targetObjs = targetObjs;
		}
		public Dictionary<string, GameObject> TargetObjs
		{
			get { return _targetObjs; }
			set { _targetObjs = value; }
		}
	}
}

