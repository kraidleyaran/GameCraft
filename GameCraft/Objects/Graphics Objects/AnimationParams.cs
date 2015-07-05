using Microsoft.Xna.Framework;

namespace GameCraft
{
    public struct AnimationParams
    {
        public string Asset;
        public float Rotation;
        public float Scale;
        public float Depth;
        public AnimationParams(string asset, float rotation, float scale, float depth)
        {
            Asset = asset;
            Rotation = rotation;
            Scale = scale;
            Depth = depth;
        }

        
    }
}