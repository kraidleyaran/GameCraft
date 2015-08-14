using System;

namespace GameCraft.Designer
{
    [Serializable]
    public class SoundAction : GameAction
    {
        public SoundAction(string name) : base(name)
        {
            Name = name;
            ActionType = ActionType.Sound;
        }

        public SoundAction(string name, string soundName, PlayCount playCount) : base(name)
        {
            Name = name;
            SoundName = soundName;
            PlayCount = playCount;
            ActionType = ActionType.Sound;
        }

        public string SoundName { get; set; }

        public PlayCount PlayCount { get; set; }

    }
}