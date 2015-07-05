using System.Runtime.InteropServices;

namespace GameCraft.Designer
{
    public class GraphicAction
    {
        public GraphicAction(string name)
        {
            Name = name;
            ActionType = ActionType.Graphic;
        }

        public GraphicAction(string name, string targetObj, PlayCount playCount)
        {
            Name = name;
            TargetObj = targetObj;
            PlayCount = playCount;
        }

        public string Name { get; private set; }

        public ActionType ActionType { get; private set; }

        public string TargetObj { get; set; }

        public PlayCount PlayCount { get; set; }
    }

}