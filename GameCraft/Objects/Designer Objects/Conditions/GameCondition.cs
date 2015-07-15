using System;
using GameCraft.Designer;

namespace GameCraft
{
    [Serializable]
    public class GameCondition
    {
        public GameCondition(string name)
        {
            Name = name;
        }

        public GameCondition(string name, ConditionType conditionType)
        {
            Name = name;
            ConditionType = conditionType;
        }

        public string Name { get; protected set; }

        public ConditionType ConditionType { get; protected set; }
    }
}