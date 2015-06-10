using System;
using System.Collections.Generic;

namespace GameCraft
{
	public class ReceiptObject<ObjType, ObjValueType>
	{
		private string _propertyName;
		private ObjType _recObject;
		private ObjValueType _propertyValue;

		public ReceiptObject ()
		{
		}
		public ReceiptObject(string propertyName, ObjValueType propertyValue)
		{
			PropertyName = propertyName;
			PropertyValue = propertyValue;
		}
		public ReceiptObject(ObjType recObject)
		{
			RecObject = recObject;
		}
		public ReceiptObject(string propertyName, ObjValueType propertyValue, ObjType recObject)
		{
			PropertyName = propertyName;
			PropertyValue = propertyValue;
			RecObject = recObject;
		}
			
		public string PropertyName{
			get { return _propertyName ; }
			set { _propertyName = value; }
		}

		public ObjValueType PropertyValue{
			get { return _propertyValue; }
			set { _propertyValue = value; }
		}

		public ObjType RecObject{
			get { return _recObject; }
			set { _recObject = value; }
		}




	}
}

