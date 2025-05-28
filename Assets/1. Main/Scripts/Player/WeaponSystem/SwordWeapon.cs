using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Player.WeaponSystem
{
    public class SwordWeapon : WeaponBase
    {
        private Collider weaponCollider;

        private void Awake()
        {
            weaponCollider = GetComponent<Collider>();
            weaponCollider.enabled = false;
        }

        public override void Attack()
        {
            // 나중에 애니메이션 타이밍 연동될 예정
            weaponCollider.enabled = true;
            Invoke(nameof(DisableCollider), 0.3f); // 타격 시간 후 비활성화
        }

        private void DisableCollider()
        {
            weaponCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // 예시: DamageReceiver 컴포넌트에 데미지 전달
                if (other.TryGetComponent(out IDamageable target))
                {
                    target.TakeDamage(10); // 임시 데미지 값
                }
            }
        }
    }
}
