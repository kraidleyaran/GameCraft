using Microsoft.Xna.Framework;

namespace GameCraft
{
    public class CollisionCircle
    {
        public CollisionCircle(string name, Circle circle)
        {
            Name = name;
            Circle = circle;
        }

        public string Name { get; protected set; }
        public Circle Circle { get; protected set; }
    }
}