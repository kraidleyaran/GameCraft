using Microsoft.Xna.Framework;

namespace GameCraft
{
    public class CollisionBox
    {
        public CollisionBox(string name, Rectangle rectangle)
        {
            Name = name;
            Rectangle = rectangle;
            
        }

        public string Name { get; protected set; }
        public Rectangle Rectangle { get; protected set; }

    }

}