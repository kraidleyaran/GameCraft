using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using GameCraft.GameMaster;

namespace GameCraft.GameMaster
{
	public class GameBox
	{
		private string _name;
		private Guid _uniqueId;
		private Dictionary<string, GameObject> _container = new Dictionary<string, GameObject> ();

		public GameBox (string name)
		{
			_name = name;
			_uniqueId = System.Guid.NewGuid ();
		}
		public string Name
		{
			get { return _name; }
		}
		public string UniqueId
		{
			get { return _uniqueId.ToString (); }
		}
		
		public Receipt<Dictionary<GameObject, bool>> Receive(BoxMessageObject newMessage)
		{
			Receipt<Dictionary<GameObject, bool>> returnReceipt;
			List<string> objNameList = new List<string> (newMessage.TargetObjs.Keys);
						switch (newMessage.Command) {
			case CommandObject.add:
				returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, Add (newMessage.TargetObjs).Response, true);
				break;
			case CommandObject.get:
				objNameList = new List<string> (newMessage.TargetObjs.Keys);
				returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, Get (objNameList).Response, true);
				break;
			case CommandObject.set:
				returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, Replace (newMessage.TargetObjs).Response, true);
				break;
			case CommandObject.remove:
				objNameList = new List<string> (newMessage.TargetObjs.Keys);
				returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, Remove (objNameList).Response, true);
				break;
			default:
				returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, new Dictionary<GameObject, bool>(), false);
				break;
			}
			return returnReceipt;

		}
		public bool Add(string objectName, GameObject gameObject)
		{
			bool gameObjectInBox = Contains (objectName);
			bool returnBool;

			if (gameObjectInBox == true) {
				returnBool = false;
			} else {
				_container.Add (objectName, gameObject);
				returnBool = true;
			}
			return returnBool;
		}
		public Receipt<Dictionary<GameObject, bool>> Add(Dictionary<string, GameObject> gameObjectList)
		{
			Dictionary<GameObject, bool> returnResult = new Dictionary<GameObject, bool> ();
			Dictionary<string, bool> returnFailure = new Dictionary<string, bool> ();

			foreach (KeyValuePair<string, GameObject> gameObject in gameObjectList) {
				bool returnBool = Add (gameObject.Key, gameObject.Value);
				returnResult.Add (gameObject.Value, returnBool);
				if (returnBool == false) {
					returnFailure.Add (gameObject.Key, returnBool);
				}
			}
			Receipt<Dictionary<GameObject, bool>> returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, returnResult, true);
			returnReceipt.Failures = returnFailure;
			return returnReceipt;
		}
		public Receipt<GameObject> Remove(string objectName)
		{
			bool gameObjectInBox = Contains (objectName);
			Receipt<GameObject> returnReceipt;
			GameObject outObjValue = default(GameObject);

			if (gameObjectInBox == true) {
				_container.TryGetValue (objectName, out outObjValue);
				returnReceipt = new Receipt<GameObject>(_name, outObjValue, gameObjectInBox);
				_container.Remove (objectName);
			} else {
				returnReceipt = new Receipt<GameObject> (_name, outObjValue, false);
			}

			return returnReceipt;
		}

		public Receipt<Dictionary<GameObject , bool>> Remove(IList<string> gameObjectNameList)
		{
			Dictionary<GameObject, bool> returnResult = new Dictionary<GameObject, bool> ();
			Dictionary<string, bool> returnFailures = new Dictionary<string, bool> ();

			foreach (string gameObjectName in gameObjectNameList) {
				Receipt<GameObject> _returnReceipt = Remove (gameObjectName);
				if (_returnReceipt.Status == true) {
					returnResult.Add (_returnReceipt.Response, _returnReceipt.Status);	
				} else {
					returnFailures.Add (gameObjectName, false);
				}

			}
			Receipt<Dictionary<GameObject, bool>> returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, returnResult, true);
			returnReceipt.Failures = returnFailures;
			return returnReceipt;
		}
		public bool Contains(string objectName)
		{
			bool gameObjectInBox = _container.ContainsKey (objectName);
			return gameObjectInBox;
		}
		public Receipt<GameObject> Replace(string objName, GameObject newObj)
		{
			Receipt<GameObject> replaceInBox = Remove (objName);
			if (replaceInBox.Status == true) {
				Add(objName, newObj);
			}
			return replaceInBox;
		}
		public Receipt<Dictionary<GameObject, bool>> Replace(IDictionary<string, GameObject> objDict)
		{
			Receipt<Dictionary<GameObject, bool> > returnReceipt = new Receipt<Dictionary<GameObject, bool> > (_name, new Dictionary<GameObject, bool> (), true);

			foreach (KeyValuePair<string, GameObject> entry in objDict) {
				Receipt<GameObject> replaceInBox = Remove (entry.Key);
				if (replaceInBox.Status == true) {
					Add (entry.Key, entry.Value);
					returnReceipt.Response.Add (entry.Value, true);
				} else {
					returnReceipt.Response.Add (entry.Value, false);
					returnReceipt.Failures.Add (entry.Key, replaceInBox.Status);
				}
			}
			return returnReceipt;
		}
		public Receipt<GameObject> Get(string objName)
		{
			GameObject returnObj;
			bool newStatus = _container.TryGetValue (objName, out returnObj);
			return new Receipt<GameObject> (_name, returnObj, newStatus);
		}
		public Receipt<Dictionary<GameObject, bool>> Get (IList<string> objNameList)
		{
			Receipt<Dictionary<GameObject, bool>> returnReceipt = new Receipt<Dictionary<GameObject, bool>> (_name, new Dictionary<GameObject, bool> (), true);
			foreach (string name in objNameList) {
				Receipt<GameObject> _getReturnReceipt = Get (name);
				if (_getReturnReceipt.Status == true) {
					returnReceipt.Response.Add (_getReturnReceipt.Response, _getReturnReceipt.Status);
				}else{
					returnReceipt.Failures.Add (name, _getReturnReceipt.Status);
				}
			}
			return returnReceipt;
		}
		public Receipt<Dictionary<string, GameObject>> GetAll()
		{
			return new Receipt<Dictionary<string, GameObject>> (_name, _container, true);
		}
		public Receipt<Dictionary<string, string>> GetIds()
		{
			Receipt<Dictionary<string, GameObject>> objectList = GetAll ();
			Receipt<Dictionary<string, string>> returnReceipt = new Receipt<Dictionary<string, string>> (_name, new Dictionary<string, string> (), true);
			foreach (KeyValuePair<string, GameObject> entry in objectList.Response) {
				returnReceipt.Response.Add (entry.Key, entry.Value.UniqueId);
			}

			return returnReceipt;
		}

	}
}

