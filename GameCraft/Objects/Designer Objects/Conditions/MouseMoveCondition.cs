using GameCraft.Designer;

namespace GameCraft
{
    public class MouseMoveCondition : InputCondition<float, float>
    {
        public MouseMoveCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.MouseMove;
        }

        public MouseMoveCondition(string name, Coords coordType) : base(name)
        {
            Name = name;
            CoordType = coordType;
            ConditionType = ConditionType.MouseMove;
        }

        public Coords CoordType { get; protected set; }


    }
}