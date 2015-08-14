using System;

namespace GameCraft.Designer
{
    [Serializable]
    public class PropertyCondition : GameCondition
    {

        public PropertyCondition(string name) : base(name)
        {
            Name = name;
            
        }

        public PropertyCondition(string name, string targetName, string property, Operator inOperator, object compareValue) : base(name)
        {
            Name = name;
            TargetName = targetName;
            Property = property;
            Operator = inOperator;
            CompareValue = compareValue;
        }

        public string TargetName { get; set; }

        public string Property { get; set; }

        public Operator Operator { get; set; }

        public object CompareValue { get; set; }
        
        
    }
}