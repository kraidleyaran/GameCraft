using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public class BoxResponse
	{
		private List<string> _objNamelist = new List<string> ();
		private Dictionary<string, string> _objIdList = new Dictionary<string, string> ();
		private bool _status;
		private string _statusMessage;

		public BoxResponse (bool status)
		{
			_status = status;
		}
		public BoxResponse (bool status, string statusMessage)
		{
			_status = status;
			_statusMessage = statusMessage;
		}

		public List<string> ObjNameList
		{
			get { return _objNamelist; }
		}
		public Dictionary<string, string> ObjIdList
		{
			get { return _objIdList; }
		}
		public bool Status
		{
			get { return Status; }
		}
		public string StatusMessage
		{
			get { return _statusMessage; }
			set { _statusMessage = value; }
		}
	}
}

