using GameCraft.Designer;

namespace GameCraft.Designer
{
    public class GamePadTriggerCondition : InputCondition<float, float>
    {
        public GamePadTriggerCondition(string name) : base(name)
        {
            Name = name;
        }

        public GamePadTriggerCondition(string name,Trigger trigger) : base(name)
        {
            Name = name;
            Trigger = trigger;
        }

        public Trigger Trigger { get; protected set; }
    }
}