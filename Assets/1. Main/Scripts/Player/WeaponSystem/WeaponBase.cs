using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Player.WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [SerializeField] protected WeaponType weaponType;
        public WeaponType WeaponType => weaponType;

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
