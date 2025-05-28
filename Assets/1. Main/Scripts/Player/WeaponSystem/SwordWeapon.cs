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
            // ���߿� �ִϸ��̼� Ÿ�̹� ������ ����
            weaponCollider.enabled = true;
            Invoke(nameof(DisableCollider), 0.3f); // Ÿ�� �ð� �� ��Ȱ��ȭ
        }

        private void DisableCollider()
        {
            weaponCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // ����: DamageReceiver ������Ʈ�� ������ ����
                if (other.TryGetComponent(out IDamageable target))
                {
                    target.TakeDamage(10); // �ӽ� ������ ��
                }
            }
        }
    }
}
