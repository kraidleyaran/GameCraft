﻿using System;
using System.Collections.Generic;


namespace GameCraft
{
	public class Receipt<ObjValueType>
	{
		private string _receiverName;
		private ObjValueType _response;
		private bool _status;
		private List<string> _failures;

		public Receipt(string receiverName, ObjValueType response, bool status)
		{
			_receiverName = receiverName;
			_response = response;
			_status = status;
			_failures = new List<string> ();
		}
		public string ReceiverName{
			get { return _receiverName; }
		}

		public ObjValueType Response{
			get { return _response; }
		}
		public bool Status{
			get { return _status; }
            set { _status = value; }
		}
		public List<string> Failures
		{
			get { return _failures; }
			set { _failures = value; }
		}

	}
}

