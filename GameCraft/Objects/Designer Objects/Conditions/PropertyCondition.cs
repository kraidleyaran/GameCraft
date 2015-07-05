using System.Reflection.Emit;

namespace GameCraft.Designer
{
    public class PropertyCondition
    {
        public PropertyCondition(string name)
        {
            Name = name;
            
        }

        public PropertyCondition(string name, string targetName, string property, Operator inOperator, object compareValue)
        {
            Name = name;
            TargetName = targetName;
            Property = property;
            Operator = inOperator;
            CompareValue = compareValue;
        }

        public string Name { get; private set; }

        public string TargetName { get; set; }

        public string Property { get; set; }

        public Operator Operator { get; set; }

        public object CompareValue { get; set; }
        
        
    }
}