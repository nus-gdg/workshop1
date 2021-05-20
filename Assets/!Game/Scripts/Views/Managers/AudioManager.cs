using UnityEngine;
using UnityEngine.Audio;

namespace Project.Views.Managers
{
    public class AudioManager : MonoBehaviour
    {
        // Master mixer parameters names
        private static readonly string MasterVolumeId = "masterVolume";
        private static readonly string MusicVolumeId = "musicVolume";
        private static readonly string SoundVolumeId = "soundVolume";
        
        // Volume range in decibels
        private static readonly int minVolume = -80;

        // Mixers
        [SerializeField]
        private AudioMixer masterMixer;
        [SerializeField]
        private AudioMixer soundMixer;

        // Music Source
        [SerializeField]
        private AudioSource audioSource;

        // Volume controls
        [SerializeField, Range(0, 100)]
        private int masterVolume;
        [SerializeField, Range(0, 100)]
        private int musicVolume;
        [SerializeField, Range(0, 100)]
        private int soundVolume;

        // Mute volume controls
        [SerializeField]
        private bool muteMaster;
        [SerializeField]
        private bool muteMusic;
        [SerializeField]
        private bool muteSound;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnValidate()
        {
            UpdateMasterVolume(); 
            UpdateMusicVolume(); 
            UpdateSoundVolume();
        }

        public AudioClip Music
        {
            get => audioSource.clip;
            set => audioSource.clip = value;
        }

        public void PlayMusic()
        {
            if (audioSource.isPlaying)
            {
                return;
            }
            audioSource.Play();
        }

        public void PauseMusic()
        {
            audioSource.Pause();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public int MasterVolume
        {
            get => masterVolume;
            set
            {
                masterVolume = value;
                UpdateMasterVolume();
            }
        }

        public int MusicVolume
        {
            get => musicVolume;
            set
            {
                musicVolume = value;
                UpdateMusicVolume();
            }
        }

        public int SoundVolume
        {
            get => soundVolume;
            set
            {
                soundVolume = value;
                UpdateSoundVolume();
            }
        }

        public bool MuteMaster
        {
            get => muteMaster;
            set
            {
                muteMaster = value;
                UpdateMasterVolume();
            }
        }
        
        public bool MuteMusic
        {
            get => muteMusic;
            set
            {
                muteMusic = value;
                UpdateMusicVolume();
            }
        }
        
        public bool MuteSound
        {
            get => muteSound;
            set
            {
                muteSound = value;
                UpdateSoundVolume();
            }
        }

        private float VolumeToDecibels(int volume)
        {
            if (volume <= 0)
            {
                return minVolume;
            }
            return 20 * Mathf.Log10(volume / 100f);
        }

        private void UpdateMasterVolume()
        {
            if (muteMaster)
            {
                masterMixer.SetFloat(MasterVolumeId, minVolume);
            }
            else
            {
                masterMixer.SetFloat(MasterVolumeId, VolumeToDecibels(masterVolume));
            }
        }

        private void UpdateMusicVolume()
        {
            if (muteMusic)
            {
                masterMixer.SetFloat(MusicVolumeId, minVolume);
            }
            else
            {
                masterMixer.SetFloat(MusicVolumeId, VolumeToDecibels(musicVolume));
            }
        }
        
        private void UpdateSoundVolume()
        {
            if (muteSound)
            {
                masterMixer.SetFloat(SoundVolumeId, minVolume);
            }
            else
            {
                masterMixer.SetFloat(SoundVolumeId, VolumeToDecibels(soundVolume));
            }
        }
    }
}
