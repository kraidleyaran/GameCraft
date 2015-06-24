using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

	    public bool CreateBox(string name)
	    {
	        bool boxExists = DoesBoxExist(name);
	        if (boxExists)
	        {
	            return false;
	        }
	        else
	        {
                _boxList.Add(new GameBox(name));
	            return true;
	        }
	    }

	    public bool DoesBoxExist(string name)
	    {
	        return _boxList.Contains(_boxList.Find(gameBox => gameBox.Name == name));
	    }

	    public Receipt<GameObject> RegisterGameObject(GameObject newGameObject)
	    {
	        Receipt<GameObject> returnReceipt = new Receipt<GameObject>("GameObserver", newGameObject, false);
            bool objName = DoesGameObjNameExist(newGameObject.Name);
	        bool objId = DoesGameObjIdExist(newGameObject.UniqueId);
	        if (objName)
	        {
	            if (!objId)
	            {
                    Failure newFail = new Failure(newGameObject.Name);
                    newFail.FailList.Add("GameObject with a different Id is already being observed");
                    returnReceipt.Failures.Add(newFail);
	            }
	            else
	            {
                    returnReceipt.Status = true;
	            }
	            
	        }
	        else
	        {
	            if (!objId)
	            {
                    returnReceipt.Status = true;
                    _objList.Add(newGameObject);	                
	            }
	            else
	            {
                    Failure newFail = new Failure(newGameObject.Name);
                    newFail.FailList.Add(newGameObject.UniqueId + " Unique Id already exists on a GameObject that is being observed");
	                returnReceipt.Failures.Add(newFail);
	            }

	        }

	        return returnReceipt;
	    }

	    public Receipt<List<GameObject>> RegisterManyGameObjects(List<GameObject> gameObjList)
	    {
            Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>>("GameObserver", new List<GameObject>(), true);
	        foreach (GameObject obj in gameObjList)
	        {
                Receipt<GameObject> objReceipt = RegisterGameObject(obj);
	            if (!objReceipt.Status)
	            {
	                foreach (Failure fail in objReceipt.Failures)
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

	    public void SendMessage(ObjectMessage newMessage)
	    {
	        Receipt<List<ObjResponse>> returnrReceipt = new Receipt<List<ObjResponse>>("GameObserver", new List<ObjResponse>(), true );
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
		public bool DoesBoxNameExists(string boxName)
		{
			List<string> currentBoxNames = GetBoxNames ();
			return currentBoxNames.Contains (boxName);
		}
	}
}

