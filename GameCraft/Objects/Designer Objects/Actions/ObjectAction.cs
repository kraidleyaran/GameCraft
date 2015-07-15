namespace GameCraft.Designer
{
    public class ObjectAction : GameAction
    {
        public ObjectAction(string name) : base(name)
        {
            Name = name;
            TargetObj = "";
            Property = "";
            SetValue = "";
            ActionType = ActionType.GameObject;
        }

        public ObjectAction(string name, string targetObj, string property, object setValue,
            Operation operation) : base(name)
        {
            Name = name;
            TargetObj = targetObj;
            Property = property;
            SetValue = setValue;
            Operation = operation;
        }

        public ObjectAction(string name, string targetObj, string property, object setValue,
            Operation operation, Relational relational)
            : base(name)
        {
            Name = name;
            TargetObj = targetObj;
            Property = property;
            SetValue = setValue;
            Operation = operation;
            IsRelational = true;
            Relational = relational;
        }

        public string TargetObj { get; set; }

        public string Property { get; set; }

        public object SetValue { get; set; }

        public Operation Operation { get; set; }

        public bool IsRelational { get; set; }
        public Relational Relational { get; set; }
    }


    
}