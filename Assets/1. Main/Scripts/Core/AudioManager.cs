using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Main.Scripts.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }


        [Header("AudioMixer 연결")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("효과음 설정")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private List<SoundEntry> sfxEntries = new();

        private Dictionary<string, AudioClip> sfxDict;

        private void Awake()
        {
            if (Instance == null) Instance = this;

            sfxDict = new();
            foreach (var entry in sfxEntries)
            {
                if (!sfxDict.ContainsKey(entry.name))
                    sfxDict.Add(entry.name, entry.clip);
            }
        }


        //효과음 재생
        public void PlaySFX(string name)
        {
            if (sfxDict.TryGetValue(name, out AudioClip clip))
            {
                sfxSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"SFX '{name}' not found in AudioManager.");
            }
        }


        //AudioMixer 볼륨 설정
        public void SetMasterVolume(float value)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        }

        public void SetBGMVolume(float value)
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        }

        public void SetSFXVolume(float value)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        }

        //AudioMixer 볼륨 가져오기
        public float GetVolume(string exposedParam)
        {
            if (audioMixer.GetFloat(exposedParam, out float dB))
                return Mathf.Pow(10, dB / 20f);
            return 1f; // 기본값 1
        }

        [System.Serializable]
        public class SoundEntry
        {
            public string name;
            public AudioClip clip;
        }
    }
}
