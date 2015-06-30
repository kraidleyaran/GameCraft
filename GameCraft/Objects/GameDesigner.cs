using System;
using System.Collections.Generic;
using System.Linq;
using GameCraft.Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Action = GameCraft.Designer.Action;

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
                    System.Action executeAction = () =>
                    {
                        foreach (KeyValuePair<string, Action> action in rule1.Value.Actions)
                        {
                            switch (action.Value.Operation)
                            {
                                case Operation.Add:
                                    GameObject addGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                    Receipt<GameObjectProperty> getReceipt =
                                        addGameObject.GetProperty(action.Value.Property);
                                    Object addNewValue = (double) getReceipt.Response.Value +
                                                         (double) action.Value.SetValue;

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
                                    Object subtractNewValue = (double) getSubtractReceipt.Response.Value -
                                                              (double) action.Value.SetValue;

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

                                    Observer.SendMessage(setNewMessage);
                                    break;

                                case Operation.Multiply:
                                    GameObject multiplyGameObject =
                                        Observer.ObjList.Find(obj => obj.Name == action.Value.TargetObj);

                                    Receipt<GameObjectProperty> getMultiplyReceipt =
                                        multiplyGameObject.GetProperty(action.Value.Property);
                                    Object multiplyNewValue = (double) getMultiplyReceipt.Response.Value*
                                                              (double) action.Value.SetValue;

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
                                    Object divideNewValue = (double) getDivideReceipt.Response.Value*
                                                            (double) action.Value.SetValue;

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
                    switch (conditionLength.Key)
                    {
                        case "MouseButton":
                            foreach (
                                KeyValuePair<string, InputCondition<MouseButton, ButtonState>> condition in rule.Value.MouseButtonConditions)
                            {
                                switch (condition.Value.CompareValue)
                                {
                                    case ButtonState.Pressed:
                                        switch (condition.Value.Operator)
                                        {
                                            case Operator.Equal:
                                                switch (condition.Value.Input)
                                                {
                                                    case MouseButton.Left:
                                                        if (mouseState.LeftButton != ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;
                                             
                                                    case MouseButton.Right:
                                                        if (mouseState.LeftButton != ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;
                                                }
                                                break;
                                            case Operator.NotEqual:
                                                switch (condition.Value.Input)
                                                {
                                                    case MouseButton.Left:
                                                        if (mouseState.LeftButton == ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;

                                                    case MouseButton.Right:
                                                        if (mouseState.LeftButton == ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                   case ButtonState.Released:
                                        switch (condition.Value.Operator)
                                        {
                                            case Operator.Equal:
                                                switch (condition.Value.Input)
                                                {
                                                    case MouseButton.Left:
                                                        if (mouseState.LeftButton == ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;

                                                    case MouseButton.Right:
                                                        if (mouseState.LeftButton == ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;
                                                }
                                                break;
                                            case Operator.NotEqual:
                                                switch (condition.Value.Input)
                                                {
                                                    case MouseButton.Left:
                                                        if (mouseState.LeftButton != ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;

                                                    case MouseButton.Right:
                                                        if (mouseState.LeftButton != ButtonState.Pressed) continue;
                                                        executeAction();
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "MouseMovement":
                            break;
                        case "KeyInput":
                            foreach (
                                KeyValuePair<string, InputCondition<Keys, KeyState>> condition in
                                    rule.Value.KeyInputConditions)
                            {
                                switch (condition.Value.Operator)
                                {
                                    case Operator.Equal:
                                        switch (condition.Value.CompareValue)
                                        {
                                            case KeyState.Down:
                                                if (!keyboardState.IsKeyDown(condition.Value.Input)) continue;
                                                executeAction();
                                                break;
                                            case KeyState.Up:
                                                if (!keyboardState.IsKeyDown(condition.Value.Input)) continue;
                                                executeAction();
                                                break;
                                        }
                                        break;
                                    case Operator.NotEqual:
                                        switch (condition.Value.CompareValue)
                                        {
                                            case KeyState.Down:
                                                if (keyboardState.IsKeyDown(condition.Value.Input)) continue;
                                                executeAction();
                                                break;
                                            case KeyState.Up:
                                                if (keyboardState.IsKeyDown(condition.Value.Input)) continue;
                                                executeAction();
                                                break;
                                        }
                                        break;
                                }


                            }

                            break;
                        case "GamePadButtons":
                            break;
                        case "GamePadThumbSticks":
                            break;
                        case "GamePadTriggers":
                            break;
                        case "Linked":
                            break;
                        case "Property":
                            foreach (KeyValuePair<string, PropertyCondition> condition in rule.Value.PropertyConditions)
                            {
                                GameObject getGameObject = Observer.ObjList.Find(o => o.Name == condition.Value.TargetName);
                                Receipt<GameObjectProperty> objProp = getGameObject.GetProperty(condition.Value.Property);
                                switch (condition.Value.Operator)
                                {
                                    case Operator.Contains:
                                        if (!objProp.Response.Value.ToString().Contains(condition.Value.CompareValue.ToString())) continue;
                                        executeAction();
                                        break;
                                    case Operator.DoesNotContain:
                                        if (objProp.Response.Value.ToString().Contains(condition.Value.CompareValue.ToString())) continue;
                                        executeAction();
                                        break;
                                    case Operator.Equal:
                                        if (objProp.Response.Value != condition.Value.CompareValue) continue;
                                        executeAction();
                                        break;
                                    case Operator.NotEqual:
                                        if (objProp.Response.Value == condition.Value.CompareValue) continue;
                                        executeAction();
                                        break;
                                    case Operator.Greater:
                                        if (!((double) objProp.Response.Value > (double) condition.Value.CompareValue)) continue;
                                        executeAction();
                                        break;
                                    case Operator.GreaterThanOrEqual:
                                        if (!((double)objProp.Response.Value >= (double)condition.Value.CompareValue)) continue;
                                        executeAction();
                                        break;
                                    case Operator.LessThan:
                                        if (!((double)objProp.Response.Value < (double)condition.Value.CompareValue)) continue;
                                        executeAction();
                                        break;
                                    case Operator.LessThanOrEqual:
                                        if (!((double)objProp.Response.Value <= (double)condition.Value.CompareValue)) continue;
                                        executeAction();
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }

    }
}