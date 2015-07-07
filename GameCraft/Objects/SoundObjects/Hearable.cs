namespace GameCraft.SoundObjects
{
    public class Hearable
    {
        public Hearable(string soundName, PlayCount playCount)
        {
            SoundName = soundName;
            PlayCount = playCount;

        }

        public string SoundName { get; private set; }

        public PlayCount PlayCount { get; private set; }

        
    }
}