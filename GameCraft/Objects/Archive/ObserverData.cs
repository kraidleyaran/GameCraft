using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace GameCraft.Archive
{
    [Serializable]
    public class ObserverData
    {
        public ObserverData()
        {
            GameObjects = new List<GameObject>();
        }

        public ObserverData(List<GameObject> gameObjects)
        {
            GameObjects = gameObjects;
        }

        public List<GameObject> GameObjects { get; set; }
    }
}