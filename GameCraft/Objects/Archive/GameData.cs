using System;
using System.Collections.Generic;

namespace GameCraft.Archive
{
    [Serializable]
    public class GameData<T>
    {
        public GameData()
        {
            
        }

        public GameData(string name, T data)
        {
            Name = name;
            Data = data;
            DataType = typeof (T).ToString();
        }
        public string Name { get; set; }
        public T Data { get; set; }
        public string DataType { get; set; }
    }
}