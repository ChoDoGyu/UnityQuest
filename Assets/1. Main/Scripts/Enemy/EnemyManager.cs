using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Core;


namespace Main.Scripts.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        private readonly List<EnemyController> activeEnemies = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// 새 몬스터가 스폰되면 호출
        /// </summary>
        public void RegisterEnemy(EnemyController enemy)
        {
            if (!activeEnemies.Contains(enemy))
                activeEnemies.Add(enemy);

            //스폰되면 미니맵에 아이콘 자동 등록
            if (GameManager.Instance != null && GameManager.Instance.MapManager != null)
            {
                GameManager.Instance.MapManager.RegisterIcon(enemy.transform, "Enemy");
            }
        }

        /// <summary>
        /// 몬스터 제거
        /// </summary>
        public void UnregisterEnemy(EnemyController enemy)
        {
            if (activeEnemies.Contains(enemy))
                activeEnemies.Remove(enemy);

            //아이콘 제거 기능 필요 시 여기서 호출 가능
            GameManager.Instance.MapManager.UnregisterIcon(enemy.transform);
        }

        public IReadOnlyList<EnemyController> GetAllEnemies() => activeEnemies;

        public void KillAllEnemies()
        {
            foreach (var enemy in activeEnemies)
            {
                enemy.TakeDamage(9999); // 강제 죽이기
            }
        }

        public int GetEnemyCount() => activeEnemies.Count;
    }
}
