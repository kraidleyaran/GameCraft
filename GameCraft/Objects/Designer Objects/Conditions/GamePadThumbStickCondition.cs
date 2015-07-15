namespace GameCraft.Designer
{
    public class GamePadThumbStickCondition : InputCondition<float, float>
    {
        public GamePadThumbStickCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GamePadThumbStick;
        }

        public GamePadThumbStickCondition(string name, Coords coordType, ThumbStick thumbStick) : base(name)
        {
            Name = name;
            ThumbStick = thumbStick;
            CoordType = coordType;
            ConditionType = ConditionType.GamePadThumbStick;
        }

        public Coords CoordType { get; protected set; }
        public ThumbStick ThumbStick { get; protected set; }
    }
}