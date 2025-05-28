using UnityEngine;
using Main.Scripts.Player.WeaponSystem;

namespace Main.Scripts.Interfaces
{
    public interface IWeapon
    {
        WeaponType WeaponType { get; }

        void Equip(Transform handTransform);  // 손에 장착
        void Unequip();                        // 해제
        void Attack();                         // 공격 실행
    }
}
