using System;

namespace GameCraft
{
    [Serializable]
    public struct LevelSize
    {
        public LevelSize(float x, float y, int maxItems)
        {
            X = x;
            Y = y;
            MaxItems = maxItems;
        }
        public float X;
        public float Y;
        public int MaxItems;
    }
}