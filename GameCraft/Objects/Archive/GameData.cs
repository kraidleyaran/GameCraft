using System;
using System.Collections.Generic;

namespace GameCraft.Archive
{
    [Serializable]
    public class GameData
    {
        public GameData()
        {
            
        }

        public ObserverData ObserverData { get; set; }
        public StateData StateData { get; set; }
        public GraphicsData GraphicsData { get; set; }
        public SoundData SoundData { get; set; }
        public GameOptions GameOptions { get; set; }
    }
}