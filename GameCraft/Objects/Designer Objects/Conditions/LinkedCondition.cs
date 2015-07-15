using System.Collections.Generic;

namespace GameCraft.Designer
{
    public class LinkedCondition : GameCondition
    {
        private Dictionary<string, MouseButtonCondition> _mouseButtonConditions = new Dictionary<string, MouseButtonCondition>();
        private Dictionary<string, MouseMoveCondition> _mouseMovementConditions = new Dictionary<string, MouseMoveCondition>();
        private Dictionary<string, KeyboardCondition> _keyInputConditions = new Dictionary<string, KeyboardCondition>();
        private Dictionary<string, GamePadButtonCondition> _gamePadButtonConditions = new Dictionary<string, GamePadButtonCondition>();
        private Dictionary<string, GamePadThumbStickCondition> _gamePadThumbStickConditions = new Dictionary<string, GamePadThumbStickCondition>();
        private Dictionary<string, GamePadTriggerCondition> _gamePadTriggerConditions = new Dictionary<string, GamePadTriggerCondition>();
        private Dictionary<string, CollisionCondition>  _collisionConditions = new Dictionary<string, CollisionCondition>();
        private Dictionary<string, AnimationCondition>  _animationConditions = new Dictionary<string, AnimationCondition>();
        
        private Dictionary<string, GameStateCondition> _gameStateConditions = new Dictionary<string, GameStateCondition>();
        private Dictionary<string, GameTimeCondition>  _gameTimeConditions = new Dictionary<string, GameTimeCondition>();

        private Dictionary<string, PropertyCondition> _propertyConditions = new Dictionary<string, PropertyCondition>();
        public LinkedCondition(string name) : base(name)
        {
            Name = name;
            ConditionType = ConditionType.Linked;
        }

        public Dictionary<string, MouseButtonCondition> MouseButtonConditions
        {
            get { return _mouseButtonConditions; }
            set { _mouseButtonConditions = value; }
        }

        public Dictionary<string, MouseMoveCondition> MouseMovementConditions
        {
            get { return _mouseMovementConditions; }
            set { _mouseMovementConditions = value; }
        }

        public Dictionary<string, KeyboardCondition> KeyInputConditions
        {
            get { return _keyInputConditions; }
            set { _keyInputConditions = value; }
        }

        public Dictionary<string, GamePadButtonCondition> GamePadButtonConditions
        {
            get { return _gamePadButtonConditions; }
            set { _gamePadButtonConditions = value; }
        }

        public Dictionary<string, GamePadThumbStickCondition> GamePadThumbStickConditions
        {
            get { return _gamePadThumbStickConditions; }
            set { _gamePadThumbStickConditions = value; }
        }

        public Dictionary<string, GamePadTriggerCondition> GamePadTriggerConditions
        {
            get { return _gamePadTriggerConditions; }
            set { _gamePadTriggerConditions = value; }
        }

        public Dictionary<string, PropertyCondition> PropertyConditions
        {
            get { return _propertyConditions; }
            set { _propertyConditions = value; }
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
            get { return _gameStateConditions; }
            set { _gameStateConditions = value; }
        }

        public Dictionary<string, GameTimeCondition> GameTimeConditions
        {
            get { return _gameTimeConditions; }
            set { _gameTimeConditions = value; }
        }
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
                {"Property", _propertyConditions.Count},
                {"Collision", _collisionConditions.Count},
                {"Animation", _animationConditions.Count},
                {"GameState", _gameStateConditions.Count},
                {"GameTime", _gameTimeConditions.Count}
            };


            return response;
        }
    }
}