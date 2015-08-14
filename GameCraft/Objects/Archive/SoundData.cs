using System;
using System.Collections.Generic;

namespace GameCraft.Archive
{
    [Serializable]
    public class SoundData
    {
        public SoundData()
        {
            Sounds = new List<string>();
        }

        public SoundData(List<string> sounds)
        {
            Sounds = sounds;
        }

        public List<string> Sounds { get; set; }
    }
}