using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SoundManager 
    {
        private AudioSource SoundEffect;
        private AudioSource SoundMusic;
        private AudioSource PlayerSound;
        [Range(0f, 1f)] public float userVolume;
        private float Volume = 1f;

        private SoundType[] Soundtypes;

        public SoundManager(AudioSource SoundEffect,AudioSource SoundMusic, SoundType[] Soundtypes,float volume)
        {
            this.SoundEffect = SoundEffect;
            this.SoundMusic = SoundMusic;
            this.Soundtypes = Soundtypes;
            SetVolume(volume);
            PlayMusic(Sounds.music);
        }
        public void SetVolume(float volume)
        {
            Volume = volume;
            SoundEffect.volume = Volume;
            SoundMusic.volume = Volume;
        }

        public void PlayMusic(Sounds sound)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                SoundMusic.clip = clip;
                SoundMusic.Play();
            }
            else
            {
                Debug.LogError("Sound Clip :" + clip.name + "not found");
            }
        }
        public void Play(Sounds sound)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                SoundEffect.loop = false;
                SoundEffect.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("Sound Clip :" + clip.name + "not found");
            }
        }
        public void PlayerSoundPlay(Sounds sound)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                Debug.Log("Sound Clip :" + clip.name +PlayerSound.name);
                PlayerSound.loop = false;
                PlayerSound.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("Sound Clip :" + clip.name + "not found");
            }
        }
        public void PlayerSoundLoopPlay(Sounds sound)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                Debug.Log("Sound Clip :" + clip.name + PlayerSound.name);
                PlayerSound.loop = true;
                PlayerSound.clip = clip;
                PlayerSound.Play();
            }
            else
            {
                Debug.LogError("Sound Clip :" + clip.name + "not found");
            }
        }
        public void PlayerSoundStop(Sounds sound)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                Debug.Log("Sound Clip :" + clip.name + PlayerSound.name);
                PlayerSound.loop = true;

                PlayerSound.Stop();
            }
            else
            {
                Debug.LogError("Sound Clip :" + clip.name + "not found");
            }
        }

        private AudioClip getSoundClip(Sounds sound)
        {
            SoundType returnsound = Array.Find(Soundtypes, item => item.soundType == sound);
            if (returnsound != null)
            {
                return returnsound.soundclip;
            }
            return null;
        }

        public void StopEffect()
        {
            SoundEffect.Stop();
        }
        public void SetPlayerSound(AudioSource playerAudio)
        {
            PlayerSound=playerAudio;
        }
    }


    [Serializable]
    public class SoundType
    {
        public Sounds soundType;
        public AudioClip soundclip;
    }
    public enum Sounds
    {
        ButtonClick,
        PlayerDied,
        music,
        LevelFinished,
        LevelStarted,
        walk,
        jump,
        land,
        hover
    }
}


