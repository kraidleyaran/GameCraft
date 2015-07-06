using Microsoft.Xna.Framework.Input;

namespace GameCraft.Designer
{
    public class InputCondition<InputType, InputState>
    {
        public InputCondition(string name)
        {
            Name = name;
            
        }

        public InputCondition(string name, InputType input, InputState compareValue, Operator inOperater)
        {
            Name = name;
            Input = input;
            CompareValue = compareValue;
            Operator = inOperater;
        }

        public string Name { get; protected set; }

        public InputType Input { get; set; }
        public InputState CompareValue { get; set; }

        public Operator Operator { get; set; }
    }
}