namespace GameCraft.Designer
{
    public class GraphicAction : GameAction
    {
        public GraphicAction(string name) : base(name)
        {
            Name = name;
            ActionType = ActionType.Graphic;
        }

        public GraphicAction(string name, string targetObj, PlayCount playCount) : base(name)
        {
            Name = name;
            TargetObj = targetObj;
            PlayCount = playCount;
        }
        
        public string TargetObj { get; set; }

        public PlayCount PlayCount { get; set; }
    }

}