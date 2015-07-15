using Microsoft.Xna.Framework;

namespace GameCraft
{
    public class Drawable
    {
        public Drawable(string targetObj, string animation, Vector2 pos, PlayCount playCount)
        {
            Animation = animation;
            Position = pos;
            TargetObj = targetObj;
            PlayCount = playCount;
        }
        public string Animation { get; private set; }
        public Vector2 Position { get; private set; }
        public PlayCount PlayCount { get; private set; }
        public string TargetObj { get; private set; }

    }
}