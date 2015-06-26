namespace GameCraft
{
    
    public class GameDesigner
    {
        static readonly GameDesigner instance = new GameDesigner();
        
        static GameDesigner()
        {
            
        }

        GameDesigner()
        {
            
        }

        public static GameDesigner Instance
        {
            get { return instance;}
        }

    }
}