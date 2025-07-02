using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Main.Scripts.Core
{
    /// <summary>
    /// ���� �������� ����� ����� ������
    /// BGM, ȿ����(SFX), UI ���带 �и��ؼ� �����ϸ�
    /// AudioMixer�� ����Ǿ� ���� ������ ������
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }


        [Header("AudioMixer ���� (������ ���� ������)")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("AudioSource (���� ��¿�)")]
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource uiSource;


        [Header("���� ���")]
        [SerializeField] private List<SoundEntry> bgmEntries = new();
        [SerializeField] private List<SoundEntry> sfxEntries = new();
        [SerializeField] private List<SoundEntry> uiEntries = new();

        // ���� �̸����� ������ ã�� ���� Dictionary
        private Dictionary<string, AudioClip> bgmDict;
        private Dictionary<string, AudioClip> sfxDict;
        private Dictionary<string, AudioClip> uiDict;

        private void Awake()
        {
            // �̱��� ���� ����
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ����Ʈ�� Dictionary�� ��ȯ
            bgmDict = ToDict(bgmEntries);
            sfxDict = ToDict(sfxEntries);
            uiDict = ToDict(uiEntries);

            // PlayerPrefs���� ���� ���� ���� �ҷ�����
            LoadVolumes();
        }

        /// <summary>
        /// SoundEntry ����Ʈ�� Dictionary�� �ٲ��ִ� �Լ�
        /// </summary>
        private Dictionary<string, AudioClip> ToDict(List<SoundEntry> entries)
        {
            var dict = new Dictionary<string, AudioClip>();
            foreach (var entry in entries)
            {
                if (!dict.ContainsKey(entry.name))
                    dict.Add(entry.name, entry.clip);
            }
            return dict;
        }

        /// <summary>
        /// BGM ���
        /// </summary>
        public void PlayBGM(string name)
        {
            if (bgmDict.TryGetValue(name, out var clip))
            {
                if (bgmSource.clip == clip && bgmSource.isPlaying) return;

                bgmSource.clip = clip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
            else
            {
                Debug.LogWarning($"[AudioManager] BGM '{name}' not found.");
            }
        }


        /// <summary>
        /// ȿ���� ���
        /// </summary>
        public void PlaySFX(string name)
        {
            if (sfxDict.TryGetValue(name, out var clip))
                sfxSource.PlayOneShot(clip);
            else
                Debug.LogWarning($"[AudioManager] SFX '{name}' not found.");
        }

        /// <summary>
        /// AudioClip ���� ��� (��ų ��� ���)
        /// </summary>
        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// UI Ŭ���� ���
        /// </summary>
        public void PlayUI(string name)
        {
            if (uiDict.TryGetValue(name, out var clip))
                uiSource.PlayOneShot(clip);
            else
                Debug.LogWarning($"[AudioManager] UI SFX '{name}' not found.");
        }

        // === ���� ���� ===

        public void SetMasterVolume(float value) => SetVolume("MasterVolume", value);
        public void SetBGMVolume(float value) => SetVolume("BGMVolume", value);
        public void SetSFXVolume(float value) => SetVolume("SFXVolume", value);
        public void SetUIVolume(float value) => SetVolume("UIVolume", value);

        public void SetVolume(string parameterName, float volume)
        {
            // 0~1 �� dB ��ȯ
            float dB = Mathf.Lerp(-80f, 0f, volume); // volume: 0~1
            audioMixer.SetFloat(parameterName, dB);

            //Debug.Log($"[AudioManager] Set '{parameterName}' Volume: {volume:0.000} �� {dB:0.0} dB");

            // PlayerPrefs ����
            PlayerPrefs.SetFloat(parameterName, volume);
        }

        public float GetVolume(string key)
        {
            return PlayerPrefs.GetFloat(key, 1f);
        }

        /// <summary>
        /// ���� ���� �� ����� ���� �� �ҷ�����
        /// </summary>
        private void LoadVolumes()
        {
            SetMasterVolume(GetVolume("MasterVolume"));
            SetBGMVolume(GetVolume("BGMVolume"));
            SetSFXVolume(GetVolume("SFXVolume"));
            SetUIVolume(GetVolume("UIVolume"));
        }

        /// <summary>
        /// Inspector���� ���� ��Ͽ�
        /// </summary>
        [System.Serializable]
        public class SoundEntry
        {
            public string name;
            public AudioClip clip;
        }
    }
}
