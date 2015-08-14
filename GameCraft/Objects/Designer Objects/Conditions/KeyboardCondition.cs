using System;
using GameCraft.Designer;
using Microsoft.Xna.Framework.Input;

namespace GameCraft
{
    [Serializable]
    public class KeyboardCondition : InputCondition<Keys, KeyState>
    {
        public KeyboardCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.KeyPress;
        }

        public KeyboardCondition(string name, Keys inputValue, KeyState compareValue, Operator inputOperator)
            : base(name, inputValue, compareValue, inputOperator)
        {
            Name = name;
            Input = inputValue;
            CompareValue = compareValue;
            Operator = inputOperator;
        }
    }
}