using UnityEngine;
using Main.Scripts.Interfaces;
using Main.Scripts.Data;

namespace Main.Scripts.Player.WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [Header("무기 공통 설정")]
        [SerializeField] protected WeaponType weaponType;
        public WeaponType WeaponType => weaponType;

        [SerializeField] protected WeaponData weaponData;
        public WeaponData WeaponData => weaponData;

        protected Transform handTransform;

        public virtual void Equip(Transform hand)
        {
            handTransform = hand;
            transform.SetParent(handTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public virtual void Unequip()
        {
            transform.SetParent(null);
        }

        public abstract void Attack(); // 각 무기에서 반드시 구현해야 함
    }
}
