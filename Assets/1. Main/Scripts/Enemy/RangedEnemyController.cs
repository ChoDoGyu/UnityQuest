using UnityEngine;

namespace Main.Scripts.Enemy
{
    public class RangedEnemyController : EnemyController
    {
        [Header("Ranged Attack Settings")]
        [SerializeField] private Transform firePoint;           // ����ü �߻� ��ġ
        [SerializeField] private GameObject projectilePrefab;   // ����ü ������

        protected override void Awake()
        {
            base.Awake();

            //���� ���¸� ���Ÿ� �������� �����
            attackState = new RangedAttackState();
        }

        public Transform GetFirePoint() => firePoint;
        public GameObject GetProjectilePrefab() => projectilePrefab;
    }
}
