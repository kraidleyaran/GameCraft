using System.Collections.Generic;

namespace GameCraft
{
	public sealed class GameObserver
	{
		static readonly GameObserver instance = new GameObserver();

		private List<GameBox> _boxList = new List<GameBox>();
        private List<GameObject> _objList = new List<GameObject>();
	    private const string _name = "GameObserver";

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

	    public List<GameObject> ObjList
	    {
            get { return _objList;}
	    }

	    public Receipt<GameObject> RegisterGameObject(GameObject newGameObject)
	    {
	        Receipt<GameObject> returnReceipt = new Receipt<GameObject>(_name, newGameObject, false);
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
            Receipt<List<GameObject>> returnReceipt = new Receipt<List<GameObject>>(_name, new List<GameObject>(), true);
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

	    public Receipt<List<ObjResponse>> SendMessage(ObjectMessage newMessage)
	    {
            Receipt<List<ObjResponse>> returnReceipt = new Receipt<List<ObjResponse>>(_name, new List<ObjResponse>(), true);
	        foreach (string name in newMessage.Receivers)
	        {
	            if (DoesGameObjNameExist(name))
	            {
	                Receipt<List<GameObjectProperty>> messageReceipt =
	                    _objList.Find(o => o.Name == name).Receive(newMessage);
                    ObjResponse objResponse = new ObjResponse(name, messageReceipt.Response);
                    returnReceipt.Response.Add(objResponse);
                    returnReceipt.Failures.AddRange(messageReceipt.Failures);
	            }
	            else
	            {
                    Failure newFail = new Failure(name);
                    newFail.FailList.Add("GameObject is not being observed");
                    returnReceipt.Failures.Add(newFail);
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
        public bool CreateBox(string name)
        {
            bool boxExists = DoesBoxExist(name);
            if (boxExists)
            {
                return false;
            }
            _boxList.Add(new GameBox(name));
            return true;
        }

	    public Receipt<List<string>> CreateManyBoxes(List<string> nameList)
	    {
	        Receipt<List<string>> returnReceipt = new Receipt<List<string>>(_name, new List<string>(), true);
            foreach (string name in nameList)
	        {
	            if (CreateBox(name))
	            {
	                returnReceipt.Response.Add(name);
	            }
	            else
	            {
                    Failure newFail = new Failure(name);
                    newFail.FailList.Add("GameBox name already exists");
	                returnReceipt.Failures.Add(newFail);
	            }
	        }

	        return returnReceipt;
	    }

        public Receipt<GameBox> GetGameBox(string name)
        {
            var returnReceipt = DoesBoxExist(name) ? new Receipt<GameBox>(_name, _boxList.Find((GameBox box) => box.Name == name), true) : new Receipt<GameBox>(_name, new GameBox(name), false);

            return returnReceipt;
        }

	    public Receipt<List<GameBox>> GetManyGameBoxes(List<string> nameList)
	    {
	        Receipt<List<GameBox>> returnReceipt = new Receipt<List<GameBox>>(_name, new List<GameBox>(), true );
	        foreach (string name in nameList)
	        {
	            Receipt<GameBox> gameBoxReceipt = GetGameBox(name);
	            if (gameBoxReceipt.Status)
	            {
	                returnReceipt.Response.Add(gameBoxReceipt.Response);
	            }
	            else
	            {
                    Failure newFail = new Failure(name);
                    newFail.FailList.Add("GameBox does not exist");
                    returnReceipt.Failures.Add(newFail);
	            }
	        }

	        return returnReceipt;
	    }

        public bool DoesBoxExist(string name)
        {
            return _boxList.Contains(_boxList.Find(gameBox => gameBox.Name == name));
        }

        public bool RemoveBox(string name)
        {
            return _boxList.Remove(_boxList.Find(box => box.Name == name));
        }

	}
}

