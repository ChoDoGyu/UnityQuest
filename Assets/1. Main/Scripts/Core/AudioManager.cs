using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

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

        [System.Serializable]
        public class SoundEntry
        {
            public string name;
            public AudioClip clip;
        }
    }
}
