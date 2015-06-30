using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameCraft;

namespace GameCraft.Designer
{
    public class State
    {
        
        public State(string name)
        {
            Name = name;
            Rules = new Dictionary<string, Rule>();
        }

        public string Name { get; private set; }

        public Dictionary<string, Rule> Rules { get; set; }

    }
}