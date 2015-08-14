using System;

namespace GameCraft.Designer
{
    [Serializable]
    public class GameStateAction : GameAction
    {
        public GameStateAction(string name) : base(name)
        {
            Name = name;
            ActionType = ActionType.GameState;
        }

        public GameStateAction(string name, string setState) : base(name)
        {
            Name = name;
            ActionType = ActionType.GameState;
            SetState = setState;
        }
        public string SetState { get; set; }
    }
}