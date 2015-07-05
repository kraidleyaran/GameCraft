using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameCraft
{
    public class SceneObject : GameObject
    {
        public SceneObject(string name) : base(name)
        {
            Name = name;
            List<GameObjectProperty> animationList = new List<GameObjectProperty>
            {
                new GameObjectProperty("Animation", ""),
                new GameObjectProperty("Visible", false),
                new GameObjectProperty("PositionX", 50),
                new GameObjectProperty("PositionY", 50),
                new GameObjectProperty("Length", 0),
                new GameObjectProperty("Width", 0),
                new GameObjectProperty("Direction", ""),
                new GameObjectProperty("DefaultAnimation", "")
            };
            AddManyProperty(animationList);
            animationList.Clear();
            _uniqueId = Guid.NewGuid();
        }

        public SceneObject(string name, List<GameObjectProperty> propList) : base (name, propList)
        {
            Name = name;
            List<GameObjectProperty> animationList = new List<GameObjectProperty>
            {
                new GameObjectProperty("Animation", ""),
                new GameObjectProperty("Visible", false),
                new GameObjectProperty("PositionX", 0),
                new GameObjectProperty("PositionY", 0),
                new GameObjectProperty("Length", 0),
                new GameObjectProperty("Width", 0),
                new GameObjectProperty("Direction", ""),
                new GameObjectProperty("DefaultAnimation", "")
            };
            AddManyProperty(animationList);
            animationList.Clear();
            _uniqueId = Guid.NewGuid();
            AddManyProperty(propList);
        }
         
    }
}