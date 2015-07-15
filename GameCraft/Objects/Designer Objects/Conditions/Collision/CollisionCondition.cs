namespace GameCraft
{
    public class CollisionCondition : GameCondition
    {
        public CollisionCondition(string name) : base(name)
        {
            Name = name;
        }

        public CollisionCondition(string name, string originObject, CollisionOperator inputOperator, string targetObject) : base(name)
        {
            Name = name;
            OriginObject = originObject;
            TargetObject = targetObject;
            Operator = inputOperator;
            ConditionType = ConditionType.Collision;
        }


        public string OriginObject { get; set; }

        public string TargetObject { get; set; }

        public CollisionOperator Operator { get; set; }
        
    }
}
