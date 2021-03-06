﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameCraft
{
    public class Animation
    {
        private int framecount;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int Frame;
        private float TotalElapsed;
        private bool Paused;
        private string Asset;
        private int FramesPerSec;


        public float Rotation, Scale, Depth;
        public Vector2 Origin;

        public float Height { get; private set; }
        public float WidthPerFrame { get; private set; }
        public Animation(string name, Vector2 origin, float rotation,
            float scale, float depth)
        {
            Origin = origin;
            Rotation = rotation;
            Scale = scale;
            Depth = depth;
            Name = name;
            
        }
        
        public void Load(ContentManager content, string asset,
            int frameCount, int framesPerSec)
        {
            framecount = frameCount;
            myTexture = content.Load<Texture2D>(asset);
            Asset = asset;
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
            Height = myTexture.Height;
            WidthPerFrame = myTexture.Width/ (float)frameCount;
            FramesPerSec = framesPerSec;
        }

        public string Name { get; private set; }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;
            TotalElapsed += elapsed;
            if (!(TotalElapsed > TimePerFrame)) return;
            Frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            Frame = Frame % framecount;
            TotalElapsed -= TimePerFrame;
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
        {
            DrawFrame(batch, Frame, screenPos);
        }
        public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
        {
            int frameWidth = (int) WidthPerFrame;
            Rectangle sourcerect = new Rectangle(frameWidth * frame, 0,
                frameWidth, myTexture.Height);
            batch.Draw(myTexture, screenPos, sourcerect, Color.White,
                Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

        public int GetCurrentFrame()
        {
            return Frame;
        }

        public int GetFrameCount()
        {
            return framecount;
        }

        public AnimationParams GetParams()
        {
            return new AnimationParams(Asset, Rotation, Scale, Depth);
        }

        public Animation CloneAnimation(ContentManager content)
        {
            Animation returnAnimation = new Animation(Name, Origin, Rotation, Scale, Depth);
            returnAnimation.Load(content, Asset, framecount, FramesPerSec);
            return returnAnimation;
        }

    }
}
