using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameCraft.GameMaster;

namespace GameCraft.GameMaster
{
	public class GameBox<ObjType>
	{
		private string _name;
		private List<ObjType> _container = new List<ObjType> ();

		public GameBox (string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Add(ObjType gameObject)
		{
			bool gameObjectInBox = Contains (gameObject);
			bool returnBool;

			if (gameObjectInBox == true) {
				returnBool = false;
			} else {
				_container.Add (gameObject);
				returnBool = true;
			}
			return returnBool;
		}

		public IDictionary<ObjType, bool> Add(IList<ObjType> gameObjectList)
		{
			IDictionary<ObjType, bool> returnResult = new Dictionary<ObjType, bool> ();

			foreach (ObjType gameObject in gameObjectList) {
				bool returnBool = Add (gameObject);
				returnResult.Add (gameObject, returnBool);
			}
			return returnResult;
		}


		public bool Remove(ObjType gameObject)
		{
			bool gameObjectInBox = Contains (gameObject);
			bool returnBool;

			if (gameObjectInBox == true) {
				_container.Remove (gameObject);
				returnBool = true;
			} else {
				returnBool = false;
			}

			return returnBool;
		}

		public IDictionary<ObjType, bool> Remove(IList<ObjType> gameObjectList)
		{
			IDictionary<ObjType, bool> returnResult = new Dictionary<ObjType, bool> ();

			foreach (ObjType gameObject in gameObjectList) {
				bool returnBool = Remove (gameObject);
				returnResult.Add (gameObject, returnBool);
			}

			return returnResult;
		}
		public bool Contains(ObjType gameObject)
		{
			bool gameObjectInBox = _container.Contains (gameObject);
			return gameObjectInBox;
		}
		public bool Replace(ObjType targetObj, ObjType newObj)
		{
			bool replaceInBox = Add (newObj);
			if (replaceInBox == true) {
				Remove (targetObj);
				return replaceInBox;
			} else {
				return false;
			}

		}
		public IDictionary<ObjType, bool> Replace(IDictionary<ObjType, ObjType> objDict)
		{
			IDictionary<ObjType, bool> response = new Dictionary<ObjType, bool> ();

			foreach (KeyValuePair<ObjType, ObjType> entry in objDict) {
				bool replaceInBox = Add (entry.Value);
				if (replaceInBox == true) {
					Remove (entry.Key);
					response.Add (entry.Value, true);
				} else {
					response.Add (entry.Key, false);					
				}
			}
			return response;
		}
	}
}

