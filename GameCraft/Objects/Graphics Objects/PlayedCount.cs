namespace GameCraft
{
    public struct PlayedCount
    {
        public int TotalPlayed;
        public Drawable Drawable;
        public PlayedCount(int totalplayed, Drawable drawable)
        {
            TotalPlayed = totalplayed;
            Drawable = drawable;
        }
    }
}