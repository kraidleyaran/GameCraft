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
		private List<GameObject> _container = new List<GameObject> ();

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
		public Receipt<List<GameObject>> Receive(BoxMessageObject newMessage)
		{
			Receipt<List<GameObject>> returnReceipt;
			switch (newMessage.Command) {
			case CommandObject.add:
				returnReceipt = new Receipt<List<GameObject>> (_name, Add (newMessage.TargetObjs).Response, true);
				break;
			case CommandObject.get:
				List<string> getNameList = new List<string> ();
				newMessage.TargetObjs.ForEach ((GameObject obj) => getNameList.Add (obj.Name));

				returnReceipt = new Receipt<List<GameObject>> (_name, Get(getNameList).Response, true);
				break;
			case CommandObject.set:
				returnReceipt = new Receipt<List<GameObject>> (_name, Replace (newMessage.TargetObjs).Response, true);
				break;
			case CommandObject.remove:
				List<string> objNameList = new List<string> ();
				newMessage.TargetObjs.ForEach ((GameObject obj) => objNameList.Add (obj.Name));

				returnReceipt = new Receipt<List<GameObject>> (_name, Remove (objNameList).Response, true);
				break;
			default:
				returnReceipt = new Receipt<List<GameObject>> (_name, new List<GameObject>(), false);
				break;
			}
			return returnReceipt;

		}
		public Receipt<List<GameObject>> Receive(ObjectMessage newMessage)
		{
			Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>> (_name, new List<GameObject> (), true);
			foreach (string objName in newMessage.Receivers) {
				
				Receipt<GameObject> getResponse = Get (objName);
				if (getResponse.Status == true) {
					
					Receipt<List<GameObjectProperty>> objResponse = getResponse.Response.Receive (newMessage);

					if (objResponse.Status == true) {
						returnReceipt.Response.Add (getResponse.Response);

						foreach (string fail in objResponse.Failures) {
							returnReceipt.Failures.Add (fail);
						}
					} 
					else 
					{
						returnReceipt.Failures.Add ("Possible command property failure " + _name);
					}
				}
				else
				{
					returnReceipt.Failures.Add (objName + " GameObject does not exist in GameBox " + _name);
				}
			}
			return returnReceipt;
		}
		public bool Add(GameObject gameObject)
		{
			bool gameObjectInBox = Contains (gameObject.Name);
			bool returnBool;

			if (gameObjectInBox == true) {
				returnBool = false;
			} else {
				gameObject.Locations.Add (_name);
				_container.Add (gameObject);
				returnBool = true;
			}
			return returnBool;
		}
		public Receipt<List<GameObject>> Add(List<GameObject> gameObjectList)
		{
			List<GameObject> returnResult = new List<GameObject> ();
			Dictionary<string, bool> returnFailure = new Dictionary<string, bool> ();

			foreach (GameObject gameObject in gameObjectList) {
				bool returnBool = Add (gameObject);
				returnResult.Add (gameObject);
				if (returnBool == false) {
					returnFailure.Add (gameObject.Name + " " + returnBool.ToString());
				}
			}
			Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>> (_name, returnResult, true);
			returnReceipt.Failures = returnFailure;
			return returnReceipt;
		}
		public Receipt<GameObject> Remove(string objectName)
		{
			bool gameObjectInBox = Contains (objectName);
			Receipt<GameObject> returnReceipt;
			GameObject gameObject = new GameObject (objectName);
			if (gameObjectInBox == true) {
				gameObject = _container.Find (gameobject => gameobject.Name == objectName);
				returnReceipt = new Receipt<GameObject>(_name, gameObject, gameObjectInBox);
				_container.Remove (gameObject);
			} else {
				returnReceipt = new Receipt<GameObject> (_name, gameObject, false);
			}

			return returnReceipt;
		}

		public Receipt<List<GameObject>> Remove(List<string> gameObjectNameList)
		{
			List<GameObject> returnResult = new List<GameObject> ();
			Dictionary<string, bool> returnFailures = new Dictionary<string, bool> ();

			foreach (string gameObjectName in gameObjectNameList) {
				Receipt<GameObject> _returnReceipt = Remove (gameObjectName);
				if (_returnReceipt.Status == true) {
					returnResult.Add (_returnReceipt.Response);
				} else {
					returnFailures.Add (gameObjectName + " " + _returnReceipt.Status.ToString());
				}

			}
			Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>> (_name, returnResult, true);
			returnReceipt.Failures = returnFailures;
			return returnReceipt;
		}
		public bool Contains(string objectName)
		{
			bool gameObjectInBox = _container.Contains (_container.Find(gameObj => gameObj.Name == objectName));
			return gameObjectInBox;
		}
		public Receipt<GameObject> Replace(GameObject newObj)
		{
			Receipt<GameObject> replaceInBox = Remove (newObj.Name);
			if (replaceInBox.Status == true) {
				Add(newObj);
			}
			return replaceInBox;
		}
		public Receipt<List<GameObject>> Replace(List<GameObject> objList)
		{
			Receipt<List<GameObject> > returnReceipt = new Receipt<List<GameObject> > (_name, new List<GameObject> (), true);

			foreach (GameObject gameObject in objList) {
				Receipt<GameObject> replaceInBox = Remove (gameObject.Name);
				if (replaceInBox.Status == true) {
					Add (gameObject);
					returnReceipt.Response.Add (gameObject);
				} else {
					returnReceipt.Failures.Add (gameObject.Name + " " + replaceInBox.Status.ToString());
				}
			}
			return returnReceipt;
		}
		public Receipt<GameObject> Get(string objName)
		{
			GameObject returnObj = new GameObject (objName);
			bool doesObjExist = Contains (objName);
			if (doesObjExist == true) {
				returnObj = _container.Find (gameObj => gameObj.Name == objName);
				return new Receipt<GameObject> (_name, returnObj, true);
			} else {
				return new Receipt<GameObject> (_name, returnObj, false);
			}

		}
		public Receipt<List<GameObject>> Get (IList<string> objNameList)
		{
			Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>> (_name, new List<GameObject> (), true);
			foreach (string name in objNameList) {
				Receipt<GameObject> _getReturnReceipt = Get (name);
				if (_getReturnReceipt.Status == true) {
					returnReceipt.Response.Add (_getReturnReceipt.Response);
				}else{
					returnReceipt.Failures.Add (name + " " + _getReturnReceipt.Status.ToString());
				}
			}
			return returnReceipt;
		}
		public Receipt<List<GameObject>> GetAll()
		{
			return new Receipt<List<GameObject>> (_name, _container, true);
		}
		public Receipt<Dictionary<string, string>> GetIds()
		{
			Dictionary<string, string> returnIds = new Dictionary<string, string> ();
			_container.ForEach (delegate(GameObject gameObj) {
				returnIds.Add(gameObj.Name, gameObj.UniqueId);
			});

			return new Receipt<Dictionary<string, string>> (_name, returnIds, true);
		}

	}
}

