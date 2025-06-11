using UnityEngine;

namespace Main.Scripts.Enemy
{
    public class RangedEnemyController : EnemyController
    {
        [Header("Ranged Attack Settings")]
        [SerializeField] private Transform firePoint;           // 투사체 발사 위치
        [SerializeField] private GameObject projectilePrefab;   // 투사체 프리팹

        protected override void Awake()
        {
            base.Awake();

            //공격 상태만 원거리 전용으로 덮어쓰기
            attackState = new RangedAttackState();
        }

        public Transform GetFirePoint() => firePoint;
        public GameObject GetProjectilePrefab() => projectilePrefab;
    }
}
