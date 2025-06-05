using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Core
{
    public class FXManager : MonoBehaviour
    {
        public static FXManager Instance { get; private set; }

        [SerializeField] private List<FXEntry> fxEntries = new();

        private Dictionary<string, GameObject> fxDict;

        private void Awake()
        {
            if (Instance == null) Instance = this;

            fxDict = new();
            foreach (var entry in fxEntries)
            {
                if (!fxDict.ContainsKey(entry.name))
                    fxDict.Add(entry.name, entry.prefab);
            }
        }

        public void PlayEffect(string name, Vector3 position)
        {
            if (fxDict.TryGetValue(name, out GameObject prefab))
            {
                GameObject fx = Instantiate(prefab, position, Quaternion.identity);
                Destroy(fx, 1.5f);
            }
            else
            {
                Debug.LogWarning($"FX '{name}' not found in FXManager.");
            }
        }

        [System.Serializable]
        public class FXEntry
        {
            public string name;
            public GameObject prefab;
        }
    }
}
