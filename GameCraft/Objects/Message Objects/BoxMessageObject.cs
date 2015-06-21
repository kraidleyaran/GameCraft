using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class BoxMessageObject : Message<GameBox>
	{
		protected List<GameObject> _targetObjs = new List<GameObject>();

		public BoxMessageObject (CommandObject command)
		{
			_command = command;
		}
		public BoxMessageObject(CommandObject command, List<GameObject> targetObjs)
		{
			_command = command;
			_targetObjs = targetObjs;
		}
		public BoxMessageObject(CommandObject command, GameObject gameObj)
		{
			_command = command;
			_targetObjs.Add (gameObj);
		}
		public List<GameObject> TargetObjs
		{
			get { return _targetObjs; }
			set { _targetObjs = value; }
		}
	}
}

