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

        public void SaveData()
        {

        }
        public void LoadData(string filePath)
        {

        }

    }
}