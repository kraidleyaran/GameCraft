using System.Security.Principal;
using GameCraft.Designer;

namespace GameCraft
{
    public class GameStateCondition : GameCondition
    {
        public GameStateCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GameState;
        }

        public GameStateCondition(string name, string targetState, Operator inputOperator) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GameState;
            TargetState = targetState;
            Operator = inputOperator;
        }


        public string TargetState { get; set; }
        public Operator Operator { get; set; }
    }
}