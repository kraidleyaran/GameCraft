using Microsoft.Xna.Framework.Input;

namespace GameCraft.Designer
{
    public class InputCondition<InputType, InputState>
    {
        public InputCondition(string name)
        {
            Name = name;
            
        }

        public string Name { get; private set; }

        public InputType Input { get; set; }
        public InputState CompareValue { get; set; }

        public Operator Operator { get; set; }
    }
}