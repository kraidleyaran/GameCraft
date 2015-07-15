using Microsoft.Xna.Framework;

namespace GameCraft
{
    public class Circle
    {
        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        public bool Intersects(Circle other)
        {
            return ((other.Center - Center).Length() < (other.Radius - Radius));
        }
        public bool Intersects(Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();
            
            return ((distanceSquared > 0) && (distanceSquared < Radius * Radius));
        }
    }
}