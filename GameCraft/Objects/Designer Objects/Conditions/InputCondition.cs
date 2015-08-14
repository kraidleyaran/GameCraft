using System;

namespace GameCraft.Designer
{
    [Serializable]
    public class InputCondition<InputType, InputState> : GameCondition
    {
        public InputCondition(string name) : base(name)
        {
            Name = name;
            
        }

        public InputCondition(string name, InputType input, InputState compareValue, Operator inOperater) : base(name)
        {
            Name = name;
            Input = input;
            CompareValue = compareValue;
            Operator = inOperater;
        }

        public InputType Input { get; set; }
        public InputState CompareValue { get; set; }

        public Operator Operator { get; set; }
    }
}