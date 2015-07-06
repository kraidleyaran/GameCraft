using GameCraft.Designer;

namespace GameCraft.Designer
{
    public class GamePadThumbStickCondition : InputCondition<float, float>
    {
        public GamePadThumbStickCondition(string name) : base(name)
        {
            Name = name;
        }

        public GamePadThumbStickCondition(string name, Coords coordType, ThumbStick thumbStick) : base(name)
        {
            Name = name;
            ThumbStick = thumbStick;
            CoordType = coordType;
        }

        public Coords CoordType { get; protected set; }
        public ThumbStick ThumbStick { get; protected set; }
    }
}