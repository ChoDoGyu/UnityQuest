using System.Collections.Generic;
using UnityEngine;


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

        public void RegisterEnemy(EnemyController enemy)
        {
            if (!activeEnemies.Contains(enemy))
                activeEnemies.Add(enemy);
        }

        public void UnregisterEnemy(EnemyController enemy)
        {
            if (activeEnemies.Contains(enemy))
                activeEnemies.Remove(enemy);
        }

        public IReadOnlyList<EnemyController> GetAllEnemies() => activeEnemies;

        public void KillAllEnemies()
        {
            foreach (var enemy in activeEnemies)
            {
                enemy.TakeDamage(9999); // °­Á¦ Á×ÀÌ±â
            }
        }

        public int GetEnemyCount() => activeEnemies.Count;
    }
}
