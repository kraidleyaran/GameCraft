using System;
using System.Collections.Generic;

namespace GameCraft
{
    public class SceneObject : GameObject
    {
        public SceneObject(string name, string type) : base(name, type)
        {
            Name = name;
            Type = "Scene";
            List<GameObjectProperty> animationList = new List<GameObjectProperty>
            {
                new GameObjectProperty("Animation", ""),
                new GameObjectProperty("Visible", false),
                new GameObjectProperty("PositionX", 50),
                new GameObjectProperty("PositionY", 50),
                new GameObjectProperty("Height", 32),
                new GameObjectProperty("Width", 32),
                new GameObjectProperty("Direction", ""),
                new GameObjectProperty("DefaultAnimation", "")
            };
            AddManyProperty(animationList);
            animationList.Clear();
            _uniqueId = Guid.NewGuid();
        }

        public SceneObject(string name, List<GameObjectProperty> propList)
            : base(name, propList, "Scene")
        {
            Name = name;
            Type = "Scene";
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