using System;
using GameCraft.Designer;

namespace GameCraft
{
    [Serializable]
    public class GameTimeCondition : GameCondition
    {
        public GameTimeCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GameTime;
        }

        public GameTimeCondition(string name, TimeSpan targetTime, Operator inputOperator) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GameTime;
            TargetTime = targetTime;
            Operator = inputOperator;
        }

        public TimeSpan TargetTime { get; set; }
        public Operator Operator { get; set; }
    }
}