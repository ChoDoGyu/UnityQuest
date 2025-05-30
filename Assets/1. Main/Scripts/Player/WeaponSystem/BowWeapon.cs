using UnityEngine;
using Main.Scripts.Core;
using Main.Scripts.Combat;

namespace Main.Scripts.Player.WeaponSystem
{
    public class BowWeapon : WeaponBase
    {
        [Header("Bow Weapon Settings")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 15f;

        public override void Attack()
        {
            if (projectilePrefab == null || firePoint == null)
            {
                Debug.LogWarning("BowWeapon: projectilePrefab �Ǵ� firePoint�� �������� �ʾҽ��ϴ�.");
                return;
            }

            GameObject projectileGO = ObjectPoolManager.Instance.Spawn(projectilePrefab, firePoint.position, firePoint.rotation);

            if (projectileGO.TryGetComponent(out Projectile projectile))
            {
                projectile.Init(firePoint.forward, projectileSpeed, projectilePrefab);
            }
        }

        public override void Equip(Transform hand)
        {
            base.Equip(hand); // WeaponBase���� ��ġ ���� ����
        }

        public override void Unequip()
        {
            transform.SetParent(null);
        }
    }
}
