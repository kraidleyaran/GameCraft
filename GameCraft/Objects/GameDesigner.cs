using System;
using System.Collections.Generic;
using System.Linq;
using GameCraft.Archive;
using GameCraft.Designer;
using GameCraft.SoundObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework.Constraints;
using Action = GameCraft.Designer.ObjectAction;

namespace GameCraft
{
    [Serializable]
    public class GameDesigner : IArchiveData
    {
        static readonly GameDesigner instance = new GameDesigner();
        private GameObserver Observer = GameObserver.Instance;
        private State _state;
        private Dictionary<string, State > _states = new Dictionary<string, State>();
        private GameArchive gameArchive = GameArchive.Instance;
        
        
        static GameDesigner()
        {
            
        }

        GameDesigner()
        {
            
        }

        public static GameDesigner Instance
        {
            get { return instance;}
        }

        public void SaveData()
        {
            gameArchive.GameData.StateData = new StateData(_states, TitleState);
        }

        public State CurrentState
        {
            get { return _state; }
            set
            {
                Observer.CollisionManager.CurrentTree = new QuadTree<GameObject>(new Vector2(value.Level.LevelSize.X, value.Level.LevelSize.Y), value.Level.LevelSize.MaxItems);
                _state = value;
            }
        }

        public Dictionary<string, State> StateList { get {return _states;} }

        public State TitleState { get; set; }

        public void Update(GameTime gameTime)
        {
            if (CurrentState == null)
            {
                throw new NullReferenceException("CurrentState is not set");
            }
            if (CurrentState.Started == false)
            {
                CurrentState.StartTime = gameTime.ElapsedGameTime;
                CurrentState.Started = true;
            }
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();

            foreach (KeyValuePair<string, Rule> rule in CurrentState.Rules)
            {
                System.Action ObjectAction = () =>
                {
                    foreach (KeyValuePair<string, Action> action in rule.Value.ObjectActions)
                    {
                        switch (action.Value.Operation)
                        {
                            case Operation.Add:
                                GameObject addGameObject =
                                    Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);
                                Receipt<GameObjectProperty> getReceipt =
                                    addGameObject.GetProperty(action.Value.Property);
                                double addNewValue = Convert.ToDouble(getReceipt.Response.Value);

                                if (action.Value.IsRelational)
                                {
                                    GameObject relGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.Relational.Name);
                                    if (relGameObject != null)
                                    {
                                        Receipt<GameObjectProperty> relReceipt = relGameObject.GetProperty(action.Value.Relational.Property);
                                        if (relReceipt.Status)
                                        {
                                            addNewValue += Convert.ToDouble(relReceipt.Response.Value) +
                                                          Convert.ToDouble(action.Value.SetValue);
                                        }
                                    }

                                }
                                else
                                {
                                    addNewValue += Convert.ToDouble(action.Value.SetValue); 
                                }

                                GameObjectProperty addGameObjectProperty =
                                    new GameObjectProperty(action.Value.Property, addNewValue,getReceipt.Response.DefaultValue);
                                ObjectMessage addNewMessage = new ObjectMessage(CommandObject.set,
                                    addGameObjectProperty);
                                addNewMessage.Receivers.Add(action.Value.TargetObj);

                                Observer.SendMessage(addNewMessage);
                                break;

                            case Operation.Subtract:
                                GameObject subtractGameObject =
                                    Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                Receipt<GameObjectProperty> getSubtractReceipt =
                                    subtractGameObject.GetProperty(action.Value.Property);

                                double subtractNewValue = Convert.ToDouble(getSubtractReceipt.Response.Value);

                                if (action.Value.IsRelational)
                                {
                                    GameObject relGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.Relational.Name);
                                    if (relGameObject != null)
                                    {
                                        Receipt<GameObjectProperty> relReceipt = relGameObject.GetProperty(action.Value.Relational.Property);
                                        if (relReceipt.Status)
                                        {
                                            subtractNewValue = (Convert.ToDouble(relReceipt.Response.Value) + 
                                                                Convert.ToDouble(getSubtractReceipt.Response.Value)) -
                                                                Convert.ToDouble(action.Value.SetValue);
                                        }
                                    }

                                }
                                else
                                {
                                    subtractNewValue = Convert.ToDouble(getSubtractReceipt.Response.Value) -
                                    Convert.ToDouble(action.Value.SetValue); 
                                }

                                GameObjectProperty subtractGameObjectProperty =
                                    new GameObjectProperty(action.Value.Property, subtractNewValue, getSubtractReceipt.Response.DefaultValue);
                                ObjectMessage subtractnewMessage = new ObjectMessage(CommandObject.set,
                                    subtractGameObjectProperty);
                                subtractnewMessage.Receivers.Add(action.Value.TargetObj);

                                Observer.SendMessage(subtractnewMessage);
                                break;

                            case Operation.Set:
                                Receipt<GameObjectProperty> getGameObjectProperty =
                                    Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj)
                                        .GetProperty(action.Value.Property);
                                GameObjectProperty setGameObjectProperty =
                                    new GameObjectProperty(action.Value.Property, action.Value.SetValue, getGameObjectProperty.Response.DefaultValue);

                                ObjectMessage setNewMessage;
                                if (action.Value.IsRelational)
                                {
                                    GameObject relGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.Relational.Name);
                                    if (relGameObject != null)
                                    {
                                        Receipt<GameObjectProperty> relReceipt = relGameObject.GetProperty(action.Value.Relational.Property);
                                        if (relReceipt.Status)
                                        {
                                            setGameObjectProperty.Value = Convert.ToDouble(relReceipt.Response.Value) + Convert.ToDouble(action.Value.SetValue);
                                        }
                                    }

                                }

                                setNewMessage = new ObjectMessage(CommandObject.set,
                                    setGameObjectProperty);
                                setNewMessage.Receivers.Add(action.Value.TargetObj);

                                Observer.SendMessage(setNewMessage);
                                break;

                            case Operation.Multiply:
                                GameObject multiplyGameObject =
                                    Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                Receipt<GameObjectProperty> getMultiplyReceipt =
                                    multiplyGameObject.GetProperty(action.Value.Property);

                                double multiplyNewValue = Convert.ToDouble(getMultiplyReceipt.Response.Value);

                                if (action.Value.IsRelational)
                                {
                                    GameObject relGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.Relational.Name);
                                    if (relGameObject != null)
                                    {
                                        Receipt<GameObjectProperty> relReceipt = relGameObject.GetProperty(action.Value.Relational.Property);
                                        if (relReceipt.Status)
                                        {
                                            multiplyNewValue = (Convert.ToDouble(relReceipt.Response.Value) +
                                                                Convert.ToDouble(getMultiplyReceipt.Response.Value)) *
                                                                Convert.ToDouble(action.Value.SetValue);
                                        }
                                    }

                                }
                                else
                                {
                                    multiplyNewValue = Convert.ToDouble(getMultiplyReceipt.Response.Value) * Convert.ToDouble(action.Value.SetValue); 
                                }

                                GameObjectProperty multiplyGameObjectProperty =
                                    new GameObjectProperty(action.Value.Property, multiplyNewValue, getMultiplyReceipt.Response.DefaultValue);
                                ObjectMessage multiplyNewMessage = new ObjectMessage(CommandObject.set,
                                    multiplyGameObjectProperty);
                                multiplyNewMessage.Receivers.Add(action.Value.TargetObj);

                                Observer.SendMessage(multiplyNewMessage);
                                break;

                            case Operation.Divide:
                                GameObject divideGameObject =
                                    Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                Receipt<GameObjectProperty> getDivideReceipt =
                                    divideGameObject.GetProperty(action.Value.Property);
                                double divideNewValue = Convert.ToDouble(getDivideReceipt.Response.Value);

                                if (action.Value.IsRelational)
                                {
                                    GameObject relGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.Relational.Name);
                                    if (relGameObject != null)
                                    {
                                        Receipt<GameObjectProperty> relReceipt = relGameObject.GetProperty(action.Value.Relational.Property);
                                        if (relReceipt.Status)
                                        {
                                            divideNewValue = (Convert.ToDouble(relReceipt.Response.Value) +
                                                                Convert.ToDouble(getDivideReceipt.Response.Value)) /
                                                                Convert.ToDouble(action.Value.SetValue);
                                        }
                                    }

                                }
                                else
                                {
                                    divideNewValue = Convert.ToDouble(getDivideReceipt.Response.Value) / Convert.ToDouble(action.Value.SetValue); 
                                }

                                GameObjectProperty divideGameObjectProperty =
                                    new GameObjectProperty(action.Value.Property, divideNewValue, getDivideReceipt.Response.DefaultValue);
                                ObjectMessage divideNewMessage = new ObjectMessage(CommandObject.set,
                                    divideGameObjectProperty);
                                divideNewMessage.Receivers.Add(action.Value.TargetObj);

                                Observer.SendMessage(divideNewMessage);
                                break;
                        }
                    }
                };
                System.Action GraphicAction = () =>
                {
                    foreach (Drawable drawObject in from pair in rule.Value.GraphicActions let drawGameObject = Observer.ObjList.Find(obj => obj.Name == pair.Value.TargetObj) let drawProps = new List<string>
                    {
                            "Animation", "PositionX", "PositionY"
                    }
                    let getPositionReceipt = drawGameObject.GetManyProperty(drawProps)
                    let PositionX = Convert.ToSingle(getPositionReceipt.Response.Find(prop => prop.Name == "PositionX").Value)
                    let PositionY = Convert.ToSingle(getPositionReceipt.Response.Find(prop => prop.Name == "PositionY").Value)
                    select new Drawable(drawGameObject.Name, getPositionReceipt.Response.Find(prop => prop.Name == "Animation").Value.ToString(), new Vector2(PositionX, PositionY), pair.Value.PlayCount))
                    {
                        Observer.DrawList.Add(drawObject);
                    }
                };
                System.Action SoundAction = () =>
                {
                    foreach (Hearable newSound in rule.Value.SoundActions.Select(pair => new Hearable(pair.Value.SoundName, pair.Value.PlayCount)))
                    {
                        Observer.SoundList.Add(newSound);
                    }
                };
                System.Action CollisionAction = () =>
                {
                    foreach (KeyValuePair<string, CollisionAction> action in rule.Value.CollisionActions)
                    {
                        Receipt<GameObject> objectReceipt = Observer.GetGameObject(action.Value.TargetObj);
                        if (objectReceipt.Status != true) continue;
                        switch (action.Value.Operation)
                        {
                            case CollisionActionOperation.Add:
                                Observer.CollisionManager.Add(objectReceipt.Response);
                                break;
                            case CollisionActionOperation.Remove:
                                Observer.CollisionManager.Remove(objectReceipt.Response);
                                break;
                            case CollisionActionOperation.Move:
                                Observer.CollisionManager.Move(objectReceipt.Response);
                                break;
                        }
                    }
                };
                System.Action GameStateAction = () =>
                {
                    foreach (KeyValuePair<string, GameStateAction> action in rule.Value.GameStateActions.Where(action => StateList.ContainsKey(action.Value.SetState)))
                    {
                        State newGameState;
                        StateList.TryGetValue(action.Value.SetState, out newGameState);
                        CurrentState = newGameState;
                    }
                };
                System.Action executeActions = () =>
                {
                    ObjectAction();
                    GraphicAction();
                    CollisionAction();
                    SoundAction();
                };
                if (rule.Value.CollisionConditions.Count > 0)
                {
                    foreach (KeyValuePair<string, CollisionCondition> pair in rule.Value.CollisionConditions.Where(pair => CheckCollisionCondition(pair.Value, Observer)))
                    {
                        executeActions();
                    }
                }
                Dictionary<string, int> conditionLengths = rule.Value.GetConditionLengths();
                foreach (KeyValuePair<string, int> conditionLength in conditionLengths.Where(conditionLength => conditionLength.Value > 0))
                {
                    var rule1 = rule;
                    
                    switch (conditionLength.Key)
                    {
                        case "MouseButton":
                            foreach (bool conditionEval in rule.Value.MouseButtonConditions.Select(condition => CheckMouseButtonCondition(condition.Value, mouseState)).Where(conditionEval => conditionEval))
                            {
                                executeActions();
                            }
                            break;
                        case "MouseMovement":
                            foreach (KeyValuePair<string, MouseMoveCondition> condition in rule.Value.MouseMovementConditions.Where(condition => CheckMouseMoveCondition(condition.Value, mouseState)))
                            {
                                executeActions();
                            }
                            break;
                        case "KeyInput":
                            foreach (KeyValuePair<string, KeyboardCondition> condition in rule.Value.KeyInputConditions.Where(condition => CheckKeyInputCondition(condition.Value, keyboardState)))
                            {
                                executeActions();
                            }
                            break;
                        case "GamePadButtons":
                            foreach (
                                KeyValuePair<string, GamePadButtonCondition> condition in
                                    rule.Value.GamePadButtonConditions.Where(
                                        condition => CheckGamePadButtonCondition(condition.Value, gamePadState)))
                            {
                                executeActions();
                            }
                            break;
                        case "GamePadThumbSticks":
                            foreach (
                                KeyValuePair<string, GamePadThumbStickCondition> condition in
                                    rule.Value.GamePadThumbStickConditions.Where(
                                        condition => CheckGamePadThumbStickCondition(condition.Value, gamePadState)))
                            {
                                executeActions();
                            }
                            break;
                        case "GamePadTriggers":
                            foreach (
                                KeyValuePair<string, GamePadTriggerCondition> condition in
                                    rule.Value.GamePadTriggerConditions.Where(
                                        condition => CheckGamePadTriggerConditions(condition.Value, gamePadState)))
                            {
                                executeActions();
                            }
                            break;
                        case "Linked":
                            foreach (KeyValuePair<string, LinkedCondition> condition in rule.Value.LinkedConditions.Where(condition => CheckLinkedCondition(condition.Value,gamePadState,mouseState,keyboardState,Observer, gameTime)))
                            {
                                executeActions();
                            }
                            break;
                        case "Property":
                            foreach (KeyValuePair<string, PropertyCondition> condition in rule.Value.PropertyConditions.Where(condition => CheckPropertyCondition(condition.Value, Observer)))
                            {
                                executeActions();
                            }
                            break;
                        case "Animation":
                            foreach (KeyValuePair<string, AnimationCondition> condition in rule.Value.AnimationConditions.Where(condition => CheckAnimationCondition(condition.Value, Observer)))
                            {
                                executeActions();
                            }
                            break;
                        case "GameState":
                            foreach (KeyValuePair<string, GameStateCondition> condition in rule.Value.GameStateConditions.Where(condition => CheckGameStateCondition(condition.Value)))
                            {
                                executeActions();
                            }
                            break;
                        case "GameTime":
                            foreach (KeyValuePair<string, GameTimeCondition> condition in rule.Value.GameTimeConditions.Where(condition => CheckGameTimeCondition(condition.Value, gameTime)))
                            {
                                executeActions();
                            }
                            break;

                    }
                }
            }
        }

        private bool CheckMouseButtonCondition(InputCondition<MouseButton, ButtonState> inputCondition, MouseState mouseState)
        {
            switch (inputCondition.CompareValue)
            {
                case ButtonState.Pressed:
                    switch (inputCondition.Operator)
                    {
                        case Operator.Equal:
                            switch (inputCondition.Input)
                            {
                                case MouseButton.Left:
                                    return mouseState.LeftButton == ButtonState.Pressed;

                                case MouseButton.Right:
                                    return mouseState.RightButton == ButtonState.Pressed;
                            }
                            break;
                        case Operator.NotEqual:
                            switch (inputCondition.Input)
                            {
                                case MouseButton.Left:
                                    return mouseState.LeftButton != ButtonState.Pressed;

                                case MouseButton.Right:
                                    return mouseState.RightButton != ButtonState.Pressed;
                            }
                            break;
                    }
                    break;
                case ButtonState.Released:
                    switch (inputCondition.Operator)
                    {
                        case Operator.Equal:
                            switch (inputCondition.Input)
                            {
                                case MouseButton.Left:
                                    return mouseState.LeftButton != ButtonState.Pressed;

                                case MouseButton.Right:
                                    return mouseState.LeftButton != ButtonState.Pressed;
                            }
                            break;
                        case Operator.NotEqual:
                            switch (inputCondition.Input)
                            {
                                case MouseButton.Left:
                                    return mouseState.LeftButton == ButtonState.Pressed;

                                case MouseButton.Right:
                                    return mouseState.LeftButton == ButtonState.Pressed;
                            }
                            break;
                    }
                    break;
            }
            return false;
        }

        public bool CheckKeyInputCondition(InputCondition<Keys, KeyState> inputCondition, KeyboardState keyboardState )
        {
            switch (inputCondition.Operator)
            {
                case Operator.Equal:
                    switch (inputCondition.CompareValue)
                    {
                        case KeyState.Down:
                            return keyboardState.IsKeyDown(inputCondition.Input);
                        case KeyState.Up:
                            return keyboardState.IsKeyUp(inputCondition.Input);
                    }
                    break;
                case Operator.NotEqual:
                    switch (inputCondition.CompareValue)
                    {
                        case KeyState.Down:
                            return !keyboardState.IsKeyDown(inputCondition.Input);
                        case KeyState.Up:
                            return !keyboardState.IsKeyUp(inputCondition.Input);
                    }
                    break;
            }
            return false;
        }

        public bool CheckPropertyCondition(PropertyCondition propertyCondition, GameObserver observer)
        {
            GameObject getGameObject = observer.ObjList.Find(o => o.Name == propertyCondition.TargetName);
            if (getGameObject == null) return false;
            Receipt<GameObjectProperty> objProp = getGameObject.GetProperty(propertyCondition.Property);
            switch (propertyCondition.Operator)
            {
                case Operator.Contains:
                    return objProp.Response.Value.ToString().Contains(propertyCondition.CompareValue.ToString());
                case Operator.DoesNotContain:
                    return !objProp.Response.Value.ToString().Contains(propertyCondition.CompareValue.ToString());
                case Operator.Equal:
                    return objProp.Response.Value.ToString() == propertyCondition.CompareValue.ToString();
                case Operator.NotEqual:
                    return objProp.Response.Value.ToString() != propertyCondition.ToString();
                case Operator.Greater:
                    return Convert.ToDouble(objProp.Response.Value) > Convert.ToDouble(propertyCondition.CompareValue);
                case Operator.GreaterThanOrEqual:
                    return Convert.ToDouble(objProp.Response.Value) >= Convert.ToDouble(propertyCondition.CompareValue);
                case Operator.LessThan:
                    return Convert.ToDouble(objProp.Response.Value) < Convert.ToDouble(propertyCondition.CompareValue);
                case Operator.LessThanOrEqual:
                    return Convert.ToDouble(objProp.Response.Value) <= Convert.ToDouble(propertyCondition.CompareValue);
            }

            return false;
        }

        public bool CheckMouseMoveCondition(MouseMoveCondition condition, MouseState mouseState)
        {
            switch (condition.Operator)
            {
                case Operator.Equal:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X == condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y == condition.CompareValue;
                    }
                    break;
                case Operator.NotEqual:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X != condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y != condition.CompareValue;
                    }
                    break;
                case Operator.Greater:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X > condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y > condition.CompareValue;
                    }
                    break;
                case Operator.GreaterThanOrEqual:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X >= condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y >= condition.CompareValue;
                    }
                    break;
                case Operator.LessThan:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X < condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y < condition.CompareValue;
                    }
                    break;
                case Operator.LessThanOrEqual:
                    switch (condition.CoordType)
                    {
                        case Coords.X:
                            return mouseState.X <= condition.CompareValue;
                        case Coords.Y:
                            return mouseState.Y <= condition.CompareValue;
                    }
                    break;
            }

            return false;
        }

        public bool CheckGamePadButtonCondition(InputCondition<Buttons, ButtonState> inputCondition,
            GamePadState gamePadState)
        {
            switch (inputCondition.Operator)
            {
                case Operator.Equal:
                    switch (inputCondition.CompareValue)
                    {
                        case ButtonState.Pressed:
                            return gamePadState.IsButtonDown(inputCondition.Input);
                        case ButtonState.Released:
                            return gamePadState.IsButtonUp(inputCondition.Input);
                    }
                    break;
                case Operator.NotEqual:
                    switch (inputCondition.CompareValue)
                    {
                        case ButtonState.Pressed:
                            return gamePadState.IsButtonUp(inputCondition.Input);
                        case ButtonState.Released:
                            return gamePadState.IsButtonDown(inputCondition.Input);

                    }
                    break;
            }
            return false;
        }

        public bool CheckGamePadThumbStickCondition(GamePadThumbStickCondition inputCondition, GamePadState gamePadState)
        {
            switch (inputCondition.Operator)
            {
                case Operator.Equal:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X == inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y == inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X == inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y == inputCondition.CompareValue;
                            }
                            break;
                    }

                    break;
                case Operator.NotEqual:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X != inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y != inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X != inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y != inputCondition.CompareValue;
                            }
                            break;
                    }
                    break;
                case Operator.Greater:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X > inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y > inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X > inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y > inputCondition.CompareValue;
                            }
                            break;
                    }
                    break;
                case Operator.GreaterThanOrEqual:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X >= inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y >= inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X >= inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y >= inputCondition.CompareValue;
                            }
                            break;
                    }
                    break;
                case Operator.LessThan:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X < inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y < inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X < inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y < inputCondition.CompareValue;
                            }
                            break;
                    }
                    break;
                case Operator.LessThanOrEqual:
                    switch (inputCondition.ThumbStick)
                    {
                        case ThumbStick.Left:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Left.X <= inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Left.Y <= inputCondition.CompareValue;
                            }
                            break;
                        case ThumbStick.Right:
                            switch (inputCondition.CoordType)
                            {
                                case Coords.X:
                                    return gamePadState.ThumbSticks.Right.X <= inputCondition.CompareValue;
                                case Coords.Y:
                                    return gamePadState.ThumbSticks.Right.Y <= inputCondition.CompareValue;
                            }
                            break;
                    }
                    break;
            }

            return false;
        }

        public bool CheckGamePadTriggerConditions(GamePadTriggerCondition inputCondition, GamePadState gamePadState)
        {
            switch (inputCondition.Operator)
            {
                case Operator.Equal:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left == inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right == inputCondition.CompareValue;
                    }
                    break;
                case Operator.NotEqual:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left != inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right != inputCondition.CompareValue;
                    }
                    break;
                case Operator.Greater:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left > inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right > inputCondition.CompareValue;
                    }
                    break;
                case Operator.GreaterThanOrEqual:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left >= inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right >= inputCondition.CompareValue;
                    }
                    break;
                case Operator.LessThan:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left < inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right < inputCondition.CompareValue;
                    }
                    break;
                case Operator.LessThanOrEqual:
                    switch (inputCondition.Trigger)
                    {
                        case Trigger.Left:
                            return gamePadState.Triggers.Left <= inputCondition.CompareValue;
                        case Trigger.Right:
                            return gamePadState.Triggers.Right <= inputCondition.CompareValue;
                    }
                    break;
            }

            return false;
        }

        public bool CheckCollisionCondition(CollisionCondition condition, GameObserver observer)
        {
            Receipt<GameObject> originReceipt = observer.GetGameObject(condition.OriginObject);
            Receipt<GameObject> targetReceipt = observer.GetGameObject(condition.TargetObject);
            if (originReceipt.Status == false || targetReceipt.Status == false)
            {
                return false;
            }

            return observer.CollisionManager.CheckCollision(originReceipt.Response, targetReceipt.Response);
        }

        public bool CheckAnimationCondition(AnimationCondition condition, GameObserver observer)
        {
            if (!observer.CurrentFrameData.ContainsKey(condition.TargetObj)) return false;
            int currentFrame;
            observer.CurrentFrameData.TryGetValue(condition.TargetObj, out currentFrame);
            if (currentFrame.Equals(null)) return false;
            switch (condition.Operator)
            {
                case Operator.Equal:
                    return currentFrame == condition.Frame;
                case Operator.NotEqual:
                    return currentFrame != condition.Frame;
                case Operator.Greater:
                    return currentFrame > condition.Frame;
                case Operator.GreaterThanOrEqual:
                    return currentFrame >= condition.Frame;
                case Operator.LessThan:
                    return currentFrame < condition.Frame;
                case Operator.LessThanOrEqual:
                    return currentFrame <= condition.Frame;
            }
            return false;
        }

        public bool CheckGameStateCondition(GameStateCondition condition)
        {
            switch (condition.Operator)
            {
                case Operator.Equal:
                    return CurrentState.Name == condition.TargetState;
                case Operator.NotEqual:
                    return CurrentState.Name != condition.TargetState;
                case Operator.Contains:
                    return CurrentState.Name.Contains(condition.TargetState);
                case Operator.DoesNotContain:
                    return !(CurrentState.Name.Contains(condition.TargetState));
            }

            return false;
        }

        public bool CheckGameTimeCondition(GameTimeCondition condition, GameTime gameTime)
        {
            switch (condition.Operator)
            {
                case Operator.Equal:
                    return condition.TargetTime == gameTime.ElapsedGameTime;
                case Operator.NotEqual:
                    return condition.TargetTime != gameTime.ElapsedGameTime;
                case Operator.Greater:
                    return condition.TargetTime > gameTime.ElapsedGameTime;
                case Operator.GreaterThanOrEqual:
                    return condition.TargetTime >= gameTime.ElapsedGameTime;
                case Operator.LessThan:
                    return condition.TargetTime < gameTime.ElapsedGameTime;
                case Operator.LessThanOrEqual:
                    return condition.TargetTime <= gameTime.ElapsedGameTime;
            }

            return false;
        }
        public bool CheckLinkedCondition(LinkedCondition linkedCondition, GamePadState gamePadState,
            MouseState mouseState, KeyboardState keyboardState, GameObserver observer, GameTime gameTime)
        {
            if (linkedCondition.GamePadButtonConditions.Select(condition => CheckGamePadButtonCondition(condition.Value, gamePadState)).Any(returnValue => !returnValue))
            {
                return false;
            }

            if (linkedCondition.GamePadThumbStickConditions.Select(condition => CheckGamePadThumbStickCondition(condition.Value, gamePadState)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.GamePadTriggerConditions.Select(condition => CheckGamePadTriggerConditions(condition.Value, gamePadState)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.KeyInputConditions.Select(condition => CheckKeyInputCondition(condition.Value, keyboardState)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.MouseButtonConditions.Select(condition => CheckMouseButtonCondition(condition.Value, mouseState)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.MouseMovementConditions.Select(condition => CheckMouseMoveCondition(condition.Value, mouseState)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.PropertyConditions.Select(condition => CheckPropertyCondition(condition.Value, observer)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.CollisionConditions.Select(condition => CheckCollisionCondition(condition.Value, observer)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.AnimationConditions.Select(condition => CheckAnimationCondition(condition.Value, observer)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.GameStateConditions.Select(condition => CheckGameStateCondition(condition.Value)).Any(returnValue => !returnValue))
            {
                return false;
            }
            if (linkedCondition.GameTimeConditions.Select(condition => CheckGameTimeCondition(condition.Value, gameTime)).Any(returnValue => !returnValue))
            {
                return false;
            }
            return true;
        }


    }
}