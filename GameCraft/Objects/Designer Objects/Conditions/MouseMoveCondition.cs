using System;
using GameCraft.Designer;

namespace GameCraft
{
    public class MouseMoveCondition: InputCondition<float, float>
    {
        public MouseMoveCondition(string name) : base(name)
        {
            Name = name;
        }

        public MouseMoveCondition(string name, Coords coordType) : base(name)
        {
            Name = name;
            CoordType = coordType;
        }

        public Coords CoordType { get; protected set; }


    }
}