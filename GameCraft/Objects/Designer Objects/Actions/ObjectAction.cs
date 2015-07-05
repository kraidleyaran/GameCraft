namespace GameCraft.Designer
{
    public class ObjectAction
    {
        public ObjectAction(string name)
        {
            Name = name;
            TargetObj = "";
            Property = "";
            SetValue = "";
            ActionType = ActionType.GameObject;
        }

        public ObjectAction(string name, string targetObj, string property, object setValue,
            Operation operation)
        {
            Name = name;
            TargetObj = targetObj;
            Property = property;
            SetValue = setValue;
            Operation = operation;
        }

        public ActionType ActionType { get; private set; }

        public string Name { get; private set; }

        public string TargetObj { get; set; }

        public string Property { get; set; }

        public object SetValue { get; set; }

        public Operation Operation { get; set; }
    }


    
}