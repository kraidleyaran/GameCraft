namespace GameCraft
{
    public class Level
    {
        public Level(string name)
        {
            Name = name;
        }

        public Level(string name, QuadTree<GameObject> quadTree )
        {
            Name = name;
            QuadTree = quadTree;
        }

        public string Name { get; protected set; }

        public QuadTree<GameObject> QuadTree { get; set; }
    }
}