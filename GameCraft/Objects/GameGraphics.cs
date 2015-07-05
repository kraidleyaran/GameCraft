﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameCraft;

namespace GameCraft
{
    public class GameGraphics
    {
        
        private Dictionary<string, Animation>  _animations = new Dictionary<string, Animation>();
        private Dictionary<string, GameBox> _boxedLists = new Dictionary<string, GameBox>();
        
        private GameObserver observer = GameObserver.Instance;
        
       
        public GameGraphics(Game game)
        {
            DeviceManager = new GraphicsDeviceManager(game);
            Content = game.Content;
            DrawList = new Dictionary<string, Animation>();
            PositionList = new Dictionary<string, PlayedCount>();

        }
        public GraphicsDeviceManager DeviceManager { get; private set; }
        public ContentManager Content { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public Dictionary<string, Animation> DrawList { get; private set; }
        private Dictionary<string, PlayedCount> PositionList { get; set; } 

        public void Update(GameTime gameTime)
        {
            float elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Drawable drawable in observer.DrawList.Where(drawable => _animations.ContainsKey(drawable.Animation)))
            {
                Animation animation;
                DrawList.TryGetValue(drawable.TargetObj, out animation);
                if (animation != null && animation.Name == drawable.Animation)
                {
                    PositionList.Remove(drawable.TargetObj);
                    PositionList.Add(drawable.TargetObj, new PlayedCount(drawable.PlayCount.Count, drawable));
                    continue;
                }

                DrawList.Remove(drawable.TargetObj);
                PositionList.Remove(drawable.TargetObj);

                DrawList.Add(drawable.TargetObj, _animations.First(objPair => objPair.Key == drawable.Animation).Value);
                PositionList.Add(drawable.TargetObj, new PlayedCount(0, drawable));
            }

            foreach (KeyValuePair<string, Animation> pair in DrawList)
            {
                pair.Value.UpdateFrame(elapsed);
            }
            
        }

        public void Draw()
        {
            foreach (KeyValuePair<string, Animation> pair in DrawList)
            {
                KeyValuePair<string, PlayedCount> drawPos = PositionList.First(posPair => posPair.Key == pair.Key);

                pair.Value.DrawFrame(SpriteBatch, drawPos.Value.Drawable.Position);
                

                if (pair.Value.GetCurrentFrame() != (pair.Value.GetFrameCount() - 1) ||
                    drawPos.Value.Drawable.PlayCount.Loop ||
                    drawPos.Value.TotalPlayed >= drawPos.Value.Drawable.PlayCount.Count) continue;

                DrawList.Remove(pair.Key);
                PositionList.Remove(pair.Key);
            }
            observer.DrawList.Clear();
        }

        public void LoadContent(List<GraphicContent> contentList )
        {
            foreach (GraphicContent content in contentList)
            {
                LoadContent(content);
            }
        }

        public void LoadContent(GraphicContent content)
        {
            Animation newAnimation = new Animation(content.Asset, Vector2.Zero, content.Rotation, content.Scale, content.Depth);
            newAnimation.Load(Content, content.Asset, content.Frames, content.FramesPerSec);
            AddAnimation(newAnimation);
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
           
        }
        public bool AddAnimation(Animation animation)
        {
            if (_animations.ContainsValue(animation)) return false;
            _animations.Add(animation.Name, animation);
            return true;
        }

        public bool RemoveAnimation(Animation animation)
        {
            if (!_animations.ContainsKey(animation.Name)) return false;
            _animations.Remove(animation.Name);
            return true;
        }

        public bool AddBox(string boxName)
        {
            if (_boxedLists.ContainsKey(boxName)) return false;
            _boxedLists.Add(boxName, new GameBox(boxName));
            return true;
        }

        public bool RemoveBox(string boxName)
        {
            if (!_boxedLists.ContainsKey(boxName)) return false;
            _boxedLists.Remove(boxName);
            return true;
        }
        
        public bool AddAnimationToBox(Animation animation, string boxName)
        {
            return AddAnimationToBox(animation.Name, boxName);
        }

        public bool AddAnimationToBox(string animationName, string boxName)
        {
            if (!_boxedLists.ContainsKey(boxName)) return false;
            GameBox outList;
            _boxedLists.TryGetValue(boxName, out outList);
            if (outList == null) return false;
            if (outList.Contains(animationName)) return false;

            _boxedLists.Remove(boxName);
            outList.Add(animationName);
            _boxedLists.Add(boxName, outList);

            return true; 
        }

        public bool RemoveAnimationFromBox(string animationName, string boxName)
        {
            if (!_boxedLists.ContainsKey(boxName)) return false;
            GameBox outBox;
            _boxedLists.TryGetValue(boxName, out outBox);
            if (outBox == null) return false;
            if (!outBox.Contains(animationName)) return false;

            outBox.Remove(animationName);
            _boxedLists.Remove(boxName);
            _boxedLists.Add(boxName, outBox);
            return true;
        }

               
    }
}