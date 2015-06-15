using System;

namespace GameCraft
{
	public class ObjResponse
	{
		private bool _objName;
		private bool _objId;

		public ObjResponse (bool objName, bool objId)
		{
			_objName = objName;
			_objId = objId;
		}

		public bool ObjName
		{
			get {return _objName; }
		}
		public bool ObjId
		{
			get {return ObjId; }
		}
	}
}

