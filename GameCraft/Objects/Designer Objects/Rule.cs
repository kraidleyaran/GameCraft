using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameCraft.Designer
{
    public class Rule
    {
        private Dictionary<string, InputCondition<MouseButton, ButtonState>> _mouseButtonConditions = new Dictionary<string, InputCondition<MouseButton, ButtonState>>();
        private Dictionary<string, MouseMoveCondition> _mouseMovementConditions = new Dictionary<string, MouseMoveCondition>();
        private Dictionary<string, InputCondition<Keys, KeyState>> _keyInputConditions = new Dictionary<string, InputCondition<Keys, KeyState>>();
        private Dictionary<string, InputCondition<Buttons,ButtonState>> _gamePadButtonConditions = new Dictionary<string, InputCondition<Buttons,ButtonState>>();
        private Dictionary<string, GamePadThumbStickCondition> _gamePadThumbStickConditions = new Dictionary<string, GamePadThumbStickCondition>();
        private Dictionary<string, GamePadTriggerCondition> _gamePadTriggerConditions = new Dictionary<string, GamePadTriggerCondition>();

        private Dictionary<string, PropertyCondition> _propertyConditions = new Dictionary<string, PropertyCondition>();

        private Dictionary<string, LinkedCondition> _linkedConditions = new Dictionary<string, LinkedCondition>();

        private Dictionary<string, ObjectAction> _objectActions = new Dictionary<string, ObjectAction>();

        public Rule(string name)
        {
            Name = name;
            GraphicActions = new Dictionary<string, GraphicAction>();
        }

        public string Name { get; private set; }

        public Dictionary<string, InputCondition<MouseButton, ButtonState>> MouseButtonConditions
        {
            get { return _mouseButtonConditions;}
            set { _mouseButtonConditions = value; }
        }

        public Dictionary<string, MouseMoveCondition> MouseMovementConditions
        {
            get { return _mouseMovementConditions;}
            set { _mouseMovementConditions = value; }
        }

        public Dictionary<string, InputCondition<Keys, KeyState>> KeyInputConditions
        {
            get { return _keyInputConditions;}
            set { _keyInputConditions = value; }
        }

        public Dictionary<string, InputCondition<Buttons, ButtonState>> GamePadButtonConditions
        {
            get { return _gamePadButtonConditions;}
            set { _gamePadButtonConditions = value; }
        }

        public Dictionary<string, GamePadThumbStickCondition> GamePadThumbStickConditions
        {
            get { return _gamePadThumbStickConditions;}
            set { _gamePadThumbStickConditions = value; }
        }

        public Dictionary<string, GamePadTriggerCondition> GamePadTriggerConditions
        {
            get { return _gamePadTriggerConditions;}
            set { _gamePadTriggerConditions = value; }
        }

        public Dictionary<string, PropertyCondition> PropertyConditions
        {
            get { return _propertyConditions;}
            set { _propertyConditions = value; }
        }

        public Dictionary<string, LinkedCondition> LinkedConditions
        {
            get { return _linkedConditions; }
            set { _linkedConditions = value; }
        }

        public Dictionary<string, ObjectAction> ObjectActions { get{return _objectActions;} set { _objectActions = value; } }

        public Dictionary<string, GraphicAction> GraphicActions { get; set; }

        public Dictionary<string, int> GetConditionLengths()
        {
            Dictionary<string, int> response = new Dictionary<string, int>
            {
                {"MouseButton", _mouseButtonConditions.Count},
                {"MouseMovement", _mouseMovementConditions.Count},
                {"KeyInput", _keyInputConditions.Count},
                {"GamePadButons", _gamePadButtonConditions.Count},
                {"GamePadThumbSticks", _gamePadThumbStickConditions.Count},
                {"GamePadTriggers", _gamePadTriggerConditions.Count},
                {"Linked", _linkedConditions.Count},
                {"Property", _propertyConditions.Count}
            };


            return response;
        }
    }
}