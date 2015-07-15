using GameCraft.Designer;
using Microsoft.Xna.Framework.Input;

namespace GameCraft
{
    public class MouseButtonCondition : InputCondition<MouseButton, ButtonState>
    {
        public MouseButtonCondition(string name): base(name)
        {
            Name = name;
            ConditionType = ConditionType.MouseButton;
        }

        public MouseButtonCondition(string name, MouseButton inputValue, ButtonState compareValue,
            Operator inputOperator) : base(name, inputValue, compareValue, inputOperator)
        {
            Name = name;
            Input = inputValue;
            CompareValue = compareValue;
            Operator = inputOperator;
        }
    }
}