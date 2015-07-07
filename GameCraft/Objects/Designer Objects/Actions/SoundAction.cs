namespace GameCraft.Designer
{
    public class SoundAction
    {
        public SoundAction(string name)
        {
            Name = name;
        }

        public SoundAction(string name, string soundName, PlayCount playCount)
        {
            Name = name;
            SoundName = soundName;
            PlayCount = playCount;
        }

        public string Name { get; protected set; }

        public string SoundName { get; set; }

        public PlayCount PlayCount { get; set; }

    }
}