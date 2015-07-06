using System;
using System.Collections.Generic;
using System.Linq;
using GameCraft.Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using Action = GameCraft.Designer.ObjectAction;

namespace GameCraft
{
    
    public class GameDesigner
    {
        static readonly GameDesigner instance = new GameDesigner();
        
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

        public State CurrentState { get; set; }

        public List<State> StateList { get; set; }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();
            GameObserver Observer = GameObserver.Instance;

            foreach (KeyValuePair<string, Rule> rule in CurrentState.Rules)
            {
                Dictionary<string, int> conditionLengths = rule.Value.GetConditionLengths();
                foreach (
                    KeyValuePair<string, int> conditionLength in
                        conditionLengths.Where(conditionLength => conditionLength.Value > 0))
                {
                    var rule1 = rule;
                    
                    System.Action ObjectAction = () =>
                    {
                        foreach (KeyValuePair<string, Action> action in rule1.Value.ObjectActions)
                        {
                            switch (action.Value.Operation)
                            {
                                case Operation.Add:
                                    GameObject addGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                    Receipt<GameObjectProperty> getReceipt =
                                        addGameObject.GetProperty(action.Value.Property);
                                    Object addNewValue = Convert.ToDouble(getReceipt.Response.Value) +
                                                         Convert.ToDouble(action.Value.SetValue);

                                    GameObjectProperty addGameObjectProperty =
                                        new GameObjectProperty(action.Value.Property, addNewValue);
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
                                    Object subtractNewValue = Convert.ToDouble(getSubtractReceipt.Response.Value) -
                                                              Convert.ToDouble(action.Value.SetValue);

                                    GameObjectProperty subtractGameObjectProperty =
                                        new GameObjectProperty(action.Value.Property, subtractNewValue);
                                    ObjectMessage subtractnewMessage = new ObjectMessage(CommandObject.set,
                                        subtractGameObjectProperty);
                                    subtractnewMessage.Receivers.Add(action.Value.TargetObj);

                                    Observer.SendMessage(subtractnewMessage);
                                    break;

                                case Operation.Set:
                                    GameObjectProperty setGameObjectProperty =
                                        new GameObjectProperty(action.Value.Property, action.Value.SetValue);
                                    ObjectMessage setNewMessage = new ObjectMessage(CommandObject.set,
                                        setGameObjectProperty);
                                    setNewMessage.Receivers.Add(action.Value.TargetObj);

                                    Observer.SendMessage(setNewMessage);
                                    break;

                                case Operation.Multiply:
                                    GameObject multiplyGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                    Receipt<GameObjectProperty> getMultiplyReceipt =
                                        multiplyGameObject.GetProperty(action.Value.Property);
                                    Object multiplyNewValue = Convert.ToDouble(getMultiplyReceipt.Response.Value) *
                                                              Convert.ToDouble(action.Value.SetValue);

                                    GameObjectProperty multiplyGameObjectProperty =
                                        new GameObjectProperty(action.Value.Property, multiplyNewValue);
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
                                    Object divideNewValue = Convert.ToDouble(getDivideReceipt.Response.Value) /
                                                            Convert.ToDouble(action.Value.SetValue);

                                    GameObjectProperty divideGameObjectProperty =
                                        new GameObjectProperty(action.Value.Property, divideNewValue);
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
                        foreach (Drawable drawObject in from pair in rule1.Value.GraphicActions let drawGameObject = Observer.ObjList.Find(obj => obj.Name == pair.Value.TargetObj) let drawProps = new List<string>
                        {
                            "Animation", "PositionX", "PositionY"
                        } let getPositionReceipt = drawGameObject.GetManyProperty(drawProps) let PositionX = Convert.ToSingle(getPositionReceipt.Response.Find(prop => prop.Name == "PositionX").Value) let PositionY = Convert.ToSingle(getPositionReceipt.Response.Find(prop => prop.Name == "PositionY").Value) select new Drawable(drawGameObject.Name, getPositionReceipt.Response.Find(prop => prop.Name == "Animation").Value.ToString(),new Vector2(PositionX, PositionY), pair.Value.PlayCount))
                        {
                            Observer.DrawList.Add(drawObject);
                        }
                    };
                    System.Action executeActions = () =>
                    {
                        ObjectAction();
                        GraphicAction();
                    };
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
                            foreach (KeyValuePair<string, InputCondition<Keys, KeyState>> condition in rule.Value.KeyInputConditions.Where(condition => CheckKeyInputCondition(condition.Value, keyboardState)))
                            {
                                executeActions();
                            }
                            break;
                        case "GamePadButtons":
                            foreach (
                                KeyValuePair<string, InputCondition<Buttons, ButtonState>> condition in
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
                            break;
                        case "Property":
                            foreach (KeyValuePair<string, PropertyCondition> condition in rule.Value.PropertyConditions.Where(condition => CheckPropertyCondition(condition.Value, Observer)))
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

        public bool CheckLinkedCondition(LinkedCondition linkedCondition, GamePadState gamePadState,
            MouseState mouseState, KeyboardState keyboardState, GameObserver observer)
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

            return true;
        }


    }
}