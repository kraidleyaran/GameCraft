using System.Reflection.Emit;

namespace GameCraft.Designer
{
    public class PropertyCondition
    {
        public PropertyCondition(string name)
        {
            Name = name;
            
        }

        public string Name { get; private set; }

        public string TargetName { get; set; }

        public string Property { get; set; }

        public Operator Operator { get; set; }

        public object CompareValue { get; set; }
        
        
    }
}