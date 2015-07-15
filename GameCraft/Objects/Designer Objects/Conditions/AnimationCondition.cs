using GameCraft.Designer;

namespace GameCraft
{
    public class AnimationCondition : GameCondition
    {
        public AnimationCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.Animation;
        }

        public AnimationCondition(string name, string targetObj, Operator inputOperator, int frame) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.Animation;
            TargetObj = targetObj;
            Operator = inputOperator;
            Frame = frame;
        }

        public string TargetObj { get; set; }

        public Operator Operator { get; set; }

        public int Frame { get; set; }
    }
}