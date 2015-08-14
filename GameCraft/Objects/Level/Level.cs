using System;

namespace GameCraft
{
    [Serializable]
    public class Level
    {
        public Level(string name)
        {
            Name = name;
        }

        public Level(string name, LevelSize levelSize )
        {
            Name = name;
            LevelSize = levelSize;
        }

        public string Name { get; protected set; }

        public LevelSize LevelSize { get; set; }
    }
}