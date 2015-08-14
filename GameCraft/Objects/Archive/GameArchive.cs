using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Xml.Serialization;

namespace GameCraft.Archive
{
    public class GameArchive
    {
        static readonly GameArchive instance = new GameArchive();
        public GameData GameData = new GameData();
        GameDesigner gameDesigner = GameDesigner.Instance;

        static GameArchive()
        {
            
        }

        GameArchive()
        {
            
        }

        public static GameArchive Instance
        {
            get
            {
                return instance;
            }
        }

        public void SaveData(string FilePath)
        {
            FileStream stream = File.Create(FilePath);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, GameData);
            stream.Close();
        }
        public GameData LoadData(string filePath)
        {
            FileStream stream = File.OpenRead(filePath);
            BinaryFormatter formatter = new BinaryFormatter();
            return (GameData)formatter.Deserialize(stream);
        }

    }
}