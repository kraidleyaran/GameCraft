namespace GameCraft.Designer
{
    public class GamePadTriggerCondition : InputCondition<float, float>
    {
        public GamePadTriggerCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GamePadTrigger;
        }

        public GamePadTriggerCondition(string name,Trigger trigger) : base(name)
        {
            Name = name;
            Trigger = trigger;
            ConditionType = ConditionType.GamePadTrigger;
        }

        public Trigger Trigger { get; protected set; }
    }
}