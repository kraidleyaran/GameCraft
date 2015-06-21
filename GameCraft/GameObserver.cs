using System;
using System.Collections.Generic;
using GameCraft;
using GameCraft.GameMaster;

namespace GameCraft
{
	public sealed class GameObserver
	{
		static readonly GameObserver instance = new GameObserver();

		private List<GameBox> _boxList = new List<GameBox>();
        private List<GameObject> _objList = new List<GameObject>();

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
			_boxList = new List<GameBox> ();
            _objList = new List<GameObject>();
			return true;
		}

		public Receipt<GameBox> RegisterBox(GameBox gameBox)
		{
			Receipt<GameBox> returnReceipt;
			bool doesNameExist = _BoxNameExists (gameBox.Name);
			if (doesNameExist == true) {
				returnReceipt = new Receipt<GameBox> ("GameObserver", gameBox , false);
				returnReceipt.Failures.Add (gameBox.Name + " GameBox is already being observed");
			} else {
				returnReceipt = new Receipt<GameBox> ("GameObserver", gameBox, true);
			    Receipt<List<GameObject>> objReceipt = _RegisterManyGameObjects(gameBox.GetAll().Response);
			    returnReceipt.Failures = objReceipt.Failures;
				_boxList.Add (gameBox);
			}
			return returnReceipt;
		}

		public Receipt<List<GameBox>> RegisterManyBoxes(List<GameBox> boxList)
		{
			Receipt<List<GameBox>> returnReceipt = new Receipt<List<GameBox>>("GameObserver", new List<GameBox>(), true);
			foreach (GameBox box in boxList) {
				Receipt<GameBox> registerResponse = RegisterBox (box);
				if (registerResponse.Status == true) {
					returnReceipt.Response.Add (box);
				} else {
					foreach(string fail in registerResponse.Failures)
					{
						returnReceipt.Failures.Add (fail);
					}
				}
			}
			return returnReceipt;
		}

	    private Receipt<GameObject> _RegisterGameObject(GameObject newGameObject)
	    {
	        Receipt<GameObject> returnReceipt = new Receipt<GameObject>("GameObserver", newGameObject, false);
            bool objName = DoesGameObjNameExist(newGameObject.Name);
	        bool objId = DoesGameObjIdExist(newGameObject.UniqueId);
	        if (objName)
	        {
	            if (!objId)
	            {
                    returnReceipt.Failures.Add(newGameObject.Name + " GameObject with a different Id is already being observed");
	            }
	            else
	            {
                    returnReceipt.Status = true;
	            }
	            
	        }
	        if (!objName)
	        {
	            if (!objId)
	            {
                    returnReceipt.Status = true;
                    _objList.Add(newGameObject);	                
	            }
	            else
	            {
	                returnReceipt.Failures.Add(newGameObject.UniqueId + " Unique Id already exists on a GameObject that is being observed");
	            }

	        }

	        return returnReceipt;
	    }

	    private Receipt<List<GameObject>> _RegisterManyGameObjects(List<GameObject> gameObjList)
	    {
            Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>>("GameObserver", new List<GameObject>(), true);
	        foreach (GameObject obj in gameObjList)
	        {
	            Receipt<GameObject> objReceipt = _RegisterGameObject(obj);
	            if (!objReceipt.Status)
	            {
	                foreach (string fail in objReceipt.Failures)
	                {
	                    returnReceipt.Failures.Add(fail);
	                }
	            }
	            else
	            {
                    returnReceipt.Response.Add(obj);
	            }
	        }

	        return returnReceipt;
	    }



	    public bool DoesGameObjNameExist(string objName)
	    {
	        return _objList.Contains(_objList.Find(obj => obj.Name == objName));
	    }

	    public bool DoesGameObjIdExist(string objId)
	    {
	        return _objList.Contains(_objList.Find(obj => obj.UniqueId == objId));
	    }

	    public List<string> GetBoxNames()
		{
			List<string> boxNames = new List<string> ();
			_boxList.ForEach ((GameBox obj) => boxNames.Add (obj.Name));
			return boxNames;
		}
		private bool _BoxNameExists(string boxName)
		{
			List<string> currentBoxNames = GetBoxNames ();
			return currentBoxNames.Contains (boxName);
		}

			
	}
}

