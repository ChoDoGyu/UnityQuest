using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Main.Scripts.Core
{
    /// <summary>
    /// 게임 전역에서 사용할 오디오 관리자
    /// BGM, 효과음(SFX), UI 사운드를 분리해서 관리하며
    /// AudioMixer와 연결되어 볼륨 조절이 가능함
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }


        [Header("AudioMixer 연결 (마스터 볼륨 조절용)")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("AudioSource (사운드 출력용)")]
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource uiSource;


        [Header("사운드 목록")]
        [SerializeField] private List<SoundEntry> bgmEntries = new();
        [SerializeField] private List<SoundEntry> sfxEntries = new();
        [SerializeField] private List<SoundEntry> uiEntries = new();

        // 사운드 이름으로 빠르게 찾기 위한 Dictionary
        private Dictionary<string, AudioClip> bgmDict;
        private Dictionary<string, AudioClip> sfxDict;
        private Dictionary<string, AudioClip> uiDict;

        private void Awake()
        {
            // 싱글톤 패턴 적용
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 리스트를 Dictionary로 변환
            bgmDict = ToDict(bgmEntries);
            sfxDict = ToDict(sfxEntries);
            uiDict = ToDict(uiEntries);

            // PlayerPrefs에서 이전 볼륨 설정 불러오기
            LoadVolumes();
        }

        /// <summary>
        /// SoundEntry 리스트를 Dictionary로 바꿔주는 함수
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
        /// BGM 재생
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
        /// 효과음 재생
        /// </summary>
        public void PlaySFX(string name)
        {
            if (sfxDict.TryGetValue(name, out var clip))
                sfxSource.PlayOneShot(clip);
            else
                Debug.LogWarning($"[AudioManager] SFX '{name}' not found.");
        }

        /// <summary>
        /// AudioClip 직접 재생 (스킬 등에서 사용)
        /// </summary>
        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// UI 클릭음 재생
        /// </summary>
        public void PlayUI(string name)
        {
            if (uiDict.TryGetValue(name, out var clip))
                uiSource.PlayOneShot(clip);
            else
                Debug.LogWarning($"[AudioManager] UI SFX '{name}' not found.");
        }

        // === 볼륨 설정 ===

        public void SetMasterVolume(float value) => SetVolume("MasterVolume", value);
        public void SetBGMVolume(float value) => SetVolume("BGMVolume", value);
        public void SetSFXVolume(float value) => SetVolume("SFXVolume", value);
        public void SetUIVolume(float value) => SetVolume("UIVolume", value);

        public void SetVolume(string parameterName, float volume)
        {
            // 0~1 → dB 변환
            float dB = Mathf.Lerp(-80f, 0f, volume); // volume: 0~1
            audioMixer.SetFloat(parameterName, dB);

            //Debug.Log($"[AudioManager] Set '{parameterName}' Volume: {volume:0.000} → {dB:0.0} dB");

            // PlayerPrefs 저장
            PlayerPrefs.SetFloat(parameterName, volume);
        }

        public float GetVolume(string key)
        {
            return PlayerPrefs.GetFloat(key, 1f);
        }

        /// <summary>
        /// 게임 시작 시 저장된 볼륨 값 불러오기
        /// </summary>
        private void LoadVolumes()
        {
            SetMasterVolume(GetVolume("MasterVolume"));
            SetBGMVolume(GetVolume("BGMVolume"));
            SetSFXVolume(GetVolume("SFXVolume"));
            SetUIVolume(GetVolume("UIVolume"));
        }

        /// <summary>
        /// Inspector에서 사운드 등록용
        /// </summary>
        [System.Serializable]
        public class SoundEntry
        {
            public string name;
            public AudioClip clip;
        }
    }
}
