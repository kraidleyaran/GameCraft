using System;
using System.Collections.Generic;
using GameCraft.Designer;

namespace GameCraft.Archive
{
    [Serializable]
    public class StateData
    {
        public StateData()
        {
            States = new Dictionary<string, State>();
        }

        public StateData(Dictionary<string, State> states, State titleState)
        {
            States = states;
            TitleState = titleState;
        }

        public Dictionary<string, State> States { get; set; }
        public State TitleState { get; set; }
    }
}