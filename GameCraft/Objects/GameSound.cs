using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameCraft
{
    public class GameSound
    {           
        Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();

        public GameSound(Game game)
        {
            Content = game.Content;
        }


        public Dictionary<string, bool> LoadContent(List<string> soundNames)
        {
            return soundNames.ToDictionary(name => name, LoadContent);
        }

        public bool LoadContent(string soundName)
        {
            if (_soundEffects.ContainsKey(soundName)) return false;
            SoundEffect soundEffect = Content.Load<SoundEffect>(soundName);
            if (soundEffect == null) return false;
            _soundEffects.Add(soundName, Content.Load<SoundEffect>(soundName));
            return true;
        }

        public ContentManager Content { get; private set; }

        public void PlaySound(string soundName, bool loop)
        {
            if (!_soundEffects.ContainsKey(soundName)) return;
            SoundEffect soundEffect;
            _soundEffects.TryGetValue(soundName, out soundEffect);
            if (soundEffect == null) return;
            SoundEffectInstance playSound = soundEffect.CreateInstance();
            if (loop)
            {
                playSound.IsLooped = true;
            }
            if (playSound.State == SoundState.Stopped || playSound.State == SoundState.Paused)
            {
                playSound.Play();
            }
        }

        public void PlaySounds(List<string> soundList)
        {
            
        }

        public void PauseSound(string soundName)
        {
            if (!_soundEffects.ContainsKey(soundName)) return;
            SoundEffect soundEffect;
            _soundEffects.TryGetValue(soundName, out soundEffect);
            if (soundEffect == null) return;
            SoundEffectInstance playSound = soundEffect.CreateInstance();
            if (playSound.State != SoundState.Paused || playSound.State != SoundState.Stopped)
            {
                playSound.Pause();
            }
                
        }

        public void StopSound(string soundName)
        {
            if (!_soundEffects.ContainsKey(soundName)) return;
            SoundEffect soundEffect;
            _soundEffects.TryGetValue(soundName, out soundEffect);
            if (soundEffect == null) return;
            SoundEffectInstance playSound = soundEffect.CreateInstance();
            playSound.Stop();
        }

    }
}