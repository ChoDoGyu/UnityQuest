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
        [Header("스폰 종류 설정")]
        [SerializeField] private SpawnEntry[] spawnEntries;

        [Header("스폰 제한")]
        [SerializeField] private int maxAlive = 5;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private float spawnRadius = 3.0f; //스폰 반경

        private float lastSpawnTime;

        private void Start()
        {
            // 종류별 목표치 나누기
            int baseCount = maxAlive / spawnEntries.Length;
            int remainder = maxAlive % spawnEntries.Length;

            for (int i = 0; i < spawnEntries.Length; i++)
            {
                spawnEntries[i].maxAlive = baseCount + (i < remainder ? 1 : 0);
                spawnEntries[i].currentAlive = 0;
            }

            //시작하자마자 maxAlive만큼 다 스폰
            int totalSpawned = 0;
            while (totalSpawned < maxAlive)
            {
                if (TrySpawn())
                    totalSpawned++;
                else
                    break; // 후보가 없으면 안전 탈출
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
        /// 후보가 있으면 스폰하고 true 반환, 없으면 false
        /// </summary>
        private bool TrySpawn()
        {
            // 가능한 후보만 모으기
            List<SpawnEntry> candidates = new();
            foreach (var entry in spawnEntries)
            {
                if (entry.currentAlive < entry.maxAlive)
                    candidates.Add(entry);
            }

            if (candidates.Count == 0) return false;

            //랜덤 후보 선택
            var selected = candidates[Random.Range(0, candidates.Count)];

            //반경 내 랜덤 위치 계산
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            // 2) NavMesh 높이에 맞게 보정
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 2.0f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
            }
            else
            {
                Debug.LogWarning("NavMesh 근처에서 샘플링 실패, 원본 위치 사용");
                // spawnPos 그대로 두거나 안전 기본값 사용 가능
            }

            // 풀에서 꺼내기
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
            //Editor에서 Spawn Radius 시각화
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
#endif
    }
}