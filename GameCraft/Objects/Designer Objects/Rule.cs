using System;
using System.Collections.Generic;

namespace GameCraft.Designer
{
    [Serializable]
    public class Rule
    {
        private Dictionary<string, MouseButtonCondition> _mouseButtonConditions = new Dictionary<string, MouseButtonCondition>();
        private Dictionary<string, MouseMoveCondition> _mouseMovementConditions = new Dictionary<string, MouseMoveCondition>();
        private Dictionary<string, KeyboardCondition> _keyInputConditions = new Dictionary<string, KeyboardCondition>();
        private Dictionary<string, GamePadButtonCondition> _gamePadButtonConditions = new Dictionary<string, GamePadButtonCondition>();
        private Dictionary<string, GamePadThumbStickCondition> _gamePadThumbStickConditions = new Dictionary<string, GamePadThumbStickCondition>();
        private Dictionary<string, GamePadTriggerCondition> _gamePadTriggerConditions = new Dictionary<string, GamePadTriggerCondition>();

        private Dictionary<string, PropertyCondition> _propertyConditions = new Dictionary<string, PropertyCondition>();

        private Dictionary<string, LinkedCondition> _linkedConditions = new Dictionary<string, LinkedCondition>();

        private Dictionary<string, ObjectAction> _objectActions = new Dictionary<string, ObjectAction>();

        private Dictionary<string, CollisionCondition>  _collisionConditions = new Dictionary<string, CollisionCondition>();
        private Dictionary<string, AnimationCondition> _animationConditions = new Dictionary<string, AnimationCondition>();
        private Dictionary<string, GameStateCondition> _gameStateConditions = new Dictionary<string, GameStateCondition>();
        private Dictionary<string, GameTimeCondition> _gameTimeConditions = new Dictionary<string, GameTimeCondition>();

        public Rule(string name)
        {
            Name = name;
            GraphicActions = new Dictionary<string, GraphicAction>();
            SoundActions = new Dictionary<string, SoundAction>();
            CollisionActions = new Dictionary<string, CollisionAction>();
            GameStateActions = new Dictionary<string, GameStateAction>();
        }

        public string Name { get; private set; }

        public Dictionary<string, MouseButtonCondition> MouseButtonConditions
        {
            get { return _mouseButtonConditions;}
            set { _mouseButtonConditions = value; }
        }

        public Dictionary<string, MouseMoveCondition> MouseMovementConditions
        {
            get { return _mouseMovementConditions;}
            set { _mouseMovementConditions = value; }
        }

        public Dictionary<string, KeyboardCondition> KeyInputConditions
        {
            get { return _keyInputConditions;}
            set { _keyInputConditions = value; }
        }

        public Dictionary<string, GamePadButtonCondition> GamePadButtonConditions
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

        public Dictionary<string, CollisionCondition> CollisionConditions
        {
            get { return _collisionConditions; }
            set { _collisionConditions = value; }
        }

        public Dictionary<string, AnimationCondition> AnimationConditions
        {
            get { return _animationConditions; }
            set { _animationConditions = value; }
        }

        public Dictionary<string, GameStateCondition> GameStateConditions
        {
            get { return _gameStateConditions;}
            set { _gameStateConditions = value; }
        }

        public Dictionary<string, GameTimeCondition> GameTimeConditions
        {
            get { return _gameTimeConditions;}
            set { _gameTimeConditions = value; }
        }

        public Dictionary<string, ObjectAction> ObjectActions { get{return _objectActions;} set { _objectActions = value; } }

        public Dictionary<string, GraphicAction> GraphicActions { get; private set; }

        public Dictionary<string, SoundAction> SoundActions { get; private set; }

        public Dictionary<string, CollisionAction> CollisionActions { get; private set; }

        public Dictionary<string, GameStateAction> GameStateActions { get; private set; }

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
                {"Property", _propertyConditions.Count},
                {"Animation", _animationConditions.Count},
                {"GameState", _gameStateConditions.Count},
                {"GameTime", _gameTimeConditions.Count}
            };


            return response;
        }
    }
}