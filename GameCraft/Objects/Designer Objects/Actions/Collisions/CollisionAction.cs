namespace GameCraft.Designer
{
    public class CollisionAction : GameAction
    {
        public CollisionAction(string name) :base(name)
        {
            Name = name;
            ActionType = ActionType.Collision;
        }

        public CollisionAction(string name, string targetObj, CollisionActionOperation inputOperation) : base(name)
        {
            Name = name;
            TargetObj = targetObj;
            Operation = inputOperation;
        }

        public string TargetObj { get; set; }

        public CollisionActionOperation Operation { get; set; }
    }

}