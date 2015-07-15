using GameCraft.Designer;
using Microsoft.Xna.Framework.Input;

namespace GameCraft
{
    public class GamePadButtonCondition : InputCondition<Buttons, ButtonState>
    {
        public GamePadButtonCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.GamePadButton;
        }

        public GamePadButtonCondition(string name, Buttons input, ButtonState compareValue, Operator inputOperator)
            : base(name, input, compareValue, inputOperator)
        {
            Name = name;
            Input = input;
            CompareValue = compareValue;
            Operator = inputOperator;
        }
    }
}