using System;

namespace GameCraft
{
    [Serializable]
    public enum ConditionType
    {
        GamePadThumbStick,GamePadTrigger,GamePadButton,MouseButton,MouseMove,KeyPress,Linked, Collision, Animation, GameState, GameTime
    }
}