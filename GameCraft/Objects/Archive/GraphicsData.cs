using System;
using System.Collections.Generic;

namespace GameCraft.Archive
{
    [Serializable]
    public class GraphicsData
    {
        public GraphicsData()
        {
            GraphicContents = new Dictionary<string, GraphicContent>();
        }

        public GraphicsData(Dictionary<string, GraphicContent> graphicContents)
        {
            GraphicContents = graphicContents;
        }

        public Dictionary<string, GraphicContent> GraphicContents { get; set; }
    }
}