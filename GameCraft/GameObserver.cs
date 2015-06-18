using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public sealed class GameObserver
	{
		static readonly GameObserver instance = new GameObserver();

		private List<GameObject> 

		private Dictionary<string, List<string>> _objLocationList = new Dictionary<string, List<string>>();

		private List<GameObject> _testList = new List<GameObject>();

		static GameObserver ()
		{
		}
		GameObserver()
		{
		}
		public static GameObserver Instance
		{
			get{
				return instance;
			}
		}

		public bool Clear()
		{

			return true;
		}

		public Receipt<BoxResponse> RegisterBox(GameBox gameBox)
		{
			bool doesNameExist = _BoxNameExists (gameBox.Name);
			if (doesNameExist == true) {
				return new Receipt<BoxResponse> ("GameObserver", new BoxResponse (false, gameBox.Name + " GameBox name has already been reigster"), false);
			}
			bool doesIdExist = _BoxIdExists (gameBox.UniqueId);
			if (doesIdExist == true) {
				return new Receipt<BoxResponse> ("GameObserver", new BoxResponse (false, gameBox.UniqueId + " GameBox Id has already been reigster"), false);
			}
			Receipt<BoxResponse> returnReceipt = new Receipt<BoxResponse> ("GameObserver", new BoxResponse (true), true);
			Receipt<Dictionary<GameObject>> gameObjs = gameBox.GetAll ();
			foreach (KeyValuePair<string, GameObject> entry in gameObjs.Response) {
				Receipt<ObjResponse> _registerObj = _RegisterGameObject (entry.Value);
				if (_registerObj.Response.ObjName == true) {
					returnReceipt.Response.ObjNameList.Add (entry.Value.Name);
				}
				if (_registerObj.Response.ObjId == true) {
					returnReceipt.Response.ObjIdList.Add (entry.Value.UniqueId, entry.Value.Name);
				} 
			}
			_boxList.Add (gameBox.Name, gameBox);
			_boxIdList.Add (gameBox.UniqueId, gameBox.Name);
			return returnReceipt;
		
		}

		public List<string> GetBoxNames()
		{
			return new List<string> (_boxList.Keys);
		}
		private Receipt<ObjResponse> _RegisterGameObject(GameObject gameObject)
		{
			Receipt<ObjResponse> returnReciept = new Receipt<ObjResponse> (gameObject.Name,
				new ObjResponse (_ObjNameExists (gameObject.Name), _ObjIdExists (gameObject.UniqueId)), true); 
			
			if (returnReciept.Response.ObjId == false && returnReciept.Response.ObjName == false) {
				_objList.Add (gameObject.Name, gameObject);
			}
			return returnReciept;
		}
		private bool _BoxIdExists(string boxId)
		{
			List<string> currentBoxIds = new List<string> (_boxIdList.Values);
			return currentBoxIds.Contains (boxId);
		}
		private bool _BoxNameExists(string boxName)
		{
			List<string> currentBoxNames = new List<string> (_boxList.Keys);
			return currentBoxNames.Contains (boxName);
		}

		private bool _ObjIdExists(string objId)
		{
			List<string> currentObjIds = new List<string> (_objIdList.Values);
			return currentObjIds.Contains (objId);
		}

		private bool _ObjNameExists(string objName)
		{
			List<string> currentObjNames = new List<string> (_objList.Keys);
			return currentObjNames.Contains (objName);			
		}
	}
}

