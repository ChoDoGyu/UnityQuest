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
        /// �� ���Ͱ� �����Ǹ� ȣ��
        /// </summary>
        public void RegisterEnemy(EnemyController enemy)
        {
            if (!activeEnemies.Contains(enemy))
                activeEnemies.Add(enemy);

            //�����Ǹ� �̴ϸʿ� ������ �ڵ� ���
            if (GameManager.Instance != null && GameManager.Instance.MapManager != null)
            {
                GameManager.Instance.MapManager.RegisterIcon(enemy.transform, "Enemy");
            }
        }

        /// <summary>
        /// ���� ����
        /// </summary>
        public void UnregisterEnemy(EnemyController enemy)
        {
            if (activeEnemies.Contains(enemy))
                activeEnemies.Remove(enemy);

            //������ ���� ��� �ʿ� �� ���⼭ ȣ�� ����
            GameManager.Instance.MapManager.UnregisterIcon(enemy.transform);
        }

        public IReadOnlyList<EnemyController> GetAllEnemies() => activeEnemies;

        public void KillAllEnemies()
        {
            foreach (var enemy in activeEnemies)
            {
                enemy.TakeDamage(9999); // ���� ���̱�
            }
        }

        public int GetEnemyCount() => activeEnemies.Count;
    }
}
