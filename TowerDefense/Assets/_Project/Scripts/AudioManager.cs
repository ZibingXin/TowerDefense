/* File Name: AudioManager.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the audio settings.
 * 
 */

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Mixer Settings")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup musicGroup;
        [SerializeField] private AudioMixerGroup sfxGroup;

        [Header("Music")]
        [SerializeField] private AudioSource musicSource;

        [Header("SFX")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private SFXData[] sfxLibrary;

        [Header("UI Bindings")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        [System.Serializable]
        public class SFXData
        {
            public string name;
            public AudioClip clip;
            [Range(0f, 1f)] public float volume = 1f;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            musicSource.outputAudioMixerGroup = musicGroup;
            sfxSource.outputAudioMixerGroup = sfxGroup;
        }

        public void PlayMusic(AudioClip musicClip)
        {
            if (musicSource.clip == musicClip && musicSource.isPlaying) return;

            musicSource.clip = musicClip;
            musicSource.Play();
        }

        public void PlaySFX(string sfxName, Vector3 pos)
        {
            SFXData data = System.Array.Find(sfxLibrary, x => x.name == sfxName);
            if (data == null)
            {
                Debug.LogWarning($"SFX {sfxName} not found!");
                return;
            }

            AudioSource.PlayClipAtPoint(data.clip, pos);
        }

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            audioMixer.SetFloat("MusicVol", VolumeToDB(volume));
            PlayerPrefs.SetFloat("MusicVol", volume);
        }

        public void SetSFXVolume()
        {
            float volume = sfxSlider.value;
            audioMixer.SetFloat("SFXVol", VolumeToDB(volume));
            PlayerPrefs.SetFloat("SFXVol", volume);
        }

        private float VolumeToDB(float volume)
        {
            return volume <= 0 ? -80f : Mathf.Log10(volume) * 20f;
        }
    }
}
