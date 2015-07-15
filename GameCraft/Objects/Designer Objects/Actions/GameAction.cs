using System;

namespace GameCraft.Designer
{
    [Serializable]
    public class GameAction
    {

        public GameAction(string name)
        {
            Name = name;
        }
        public GameAction(string name, ActionType actionType)
        {
            Name = name;
            ActionType = actionType;
        }

        public string Name { get; protected set; }

        public ActionType ActionType { get; protected set; }
    }
}