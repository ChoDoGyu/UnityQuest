using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Core;
using UnityEngine.AI;

namespace Main.Scripts.Enemy
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject prefab;
        [HideInInspector] public int maxAlive;
        [HideInInspector] public int currentAlive;
    }
    public class EnemySpawnPoint : MonoBehaviour
    {
        [Header("���� ���� ����")]
        [SerializeField] private SpawnEntry[] spawnEntries;

        [Header("���� ����")]
        [SerializeField] private int maxAlive = 5;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private float spawnRadius = 3.0f; //���� �ݰ�

        private float lastSpawnTime;

        private void Start()
        {
            // ������ ��ǥġ ������
            int baseCount = maxAlive / spawnEntries.Length;
            int remainder = maxAlive % spawnEntries.Length;

            for (int i = 0; i < spawnEntries.Length; i++)
            {
                spawnEntries[i].maxAlive = baseCount + (i < remainder ? 1 : 0);
                spawnEntries[i].currentAlive = 0;
            }

            //�������ڸ��� maxAlive��ŭ �� ����
            int totalSpawned = 0;
            while (totalSpawned < maxAlive)
            {
                if (TrySpawn())
                    totalSpawned++;
                else
                    break; // �ĺ��� ������ ���� Ż��
            }

            lastSpawnTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - lastSpawnTime >= spawnInterval)
            {
                TrySpawn();
                lastSpawnTime = Time.time;
            }
        }

        /// <summary>
        /// �ĺ��� ������ �����ϰ� true ��ȯ, ������ false
        /// </summary>
        private bool TrySpawn()
        {
            // ������ �ĺ��� ������
            List<SpawnEntry> candidates = new();
            foreach (var entry in spawnEntries)
            {
                if (entry.currentAlive < entry.maxAlive)
                    candidates.Add(entry);
            }

            if (candidates.Count == 0) return false;

            //���� �ĺ� ����
            var selected = candidates[Random.Range(0, candidates.Count)];

            //�ݰ� �� ���� ��ġ ���
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            // 2) NavMesh ���̿� �°� ����
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 2.0f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
            }
            else
            {
                Debug.LogWarning("NavMesh ��ó���� ���ø� ����, ���� ��ġ ���");
                // spawnPos �״�� �ΰų� ���� �⺻�� ��� ����
            }

            // Ǯ���� ������
            GameObject monster = ObjectPoolManager.Instance.Spawn(selected.prefab, spawnPos, Quaternion.identity);

            var enemy = monster.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.OnReturnedToPool = () =>
                {
                    ObjectPoolManager.Instance.ReturnToPool(selected.prefab, monster);
                    selected.currentAlive--;
                };
            }

            selected.currentAlive++;
            return true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            //Editor���� Spawn Radius �ð�ȭ
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
#endif
    }
}