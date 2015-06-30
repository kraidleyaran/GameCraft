namespace GameCraft.Designer
{
    public class Action
    {
        public Action(string name)
        {
            Name = name;
            TargetObj = "";
            Property = "";
            SetValue = "";
        }

        public string Name { get; private set; }

        public string TargetObj { get; set; }

        public string Property { get; set; }

        public object SetValue { get; set; }

        public Operation Operation { get; set; }
    }


    
}