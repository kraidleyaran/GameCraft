using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameCraft.Designer
{
    [Serializable]
    public class State
    {
        
        public State(string name, Level level)
        {
            Name = name;
            Rules = new Dictionary<string, Rule>();
            Level = level;
        }

        public string Name { get; private set; }

        public Level Level { get; set; }

        public Dictionary<string, Rule> Rules { get; set; }

        public TimeSpan StartTime { get; set; }

        public bool Started { get; set; }



    }
}