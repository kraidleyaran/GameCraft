using System;

namespace GameCraft.Designer
{
    [Serializable]
    public struct Relational
    {
        public Relational(string name, string property)
        {
            Name = name;
            Property = property;
        }

        public string Name;
        public string Property;
    }
}