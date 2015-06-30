using System.Collections.Generic;

namespace GameCraft
{
	public class Receipt<ObjValueType>
	{
	    public Receipt(string receiverName, ObjValueType response, bool status)
		{
			ReceiverName = receiverName;
			Response = response;
			Status = status;
			Failures = new List<Failure> ();
		}
		public string ReceiverName { get; private set; }

	    public ObjValueType Response { get; private set; }

	    public bool Status { get; set; }

	    public List<Failure> Failures { get; set; }
	}
}

