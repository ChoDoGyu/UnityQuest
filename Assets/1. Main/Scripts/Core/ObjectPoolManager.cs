using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Core
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        private Dictionary<GameObject, Queue<GameObject>> poolDict = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!poolDict.ContainsKey(prefab))
            {
                poolDict[prefab] = new Queue<GameObject>();
            }

            GameObject obj;

            if (poolDict[prefab].Count > 0)
            {
                obj = poolDict[prefab].Dequeue();
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }
            else
            {
                obj = Instantiate(prefab, position, rotation);
            }

            return obj;
        }

        public void ReturnToPool(GameObject prefab, GameObject obj)
        {
            obj.SetActive(false);
            poolDict[prefab].Enqueue(obj);
        }
    }
}
