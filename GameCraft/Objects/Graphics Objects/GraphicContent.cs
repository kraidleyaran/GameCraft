using System;

namespace GameCraft
{
    [Serializable]
    public class GraphicContent
    {
        public GraphicContent(string asset, int frames, int framesPerSec, float rotation, float scale, float depth)
        {
            Asset = asset;
            Frames = frames;
            FramesPerSec = framesPerSec;
            Rotation = rotation;
            Scale = scale;
            Depth = depth;
        }

        public string Asset { get; private set; }
        public int Frames { get; private set; }
        public int FramesPerSec { get; private set; }
        public float Rotation { get; private set; }
        public float Scale { get; private set; }
        public float Depth { get; private set; }
    }
}