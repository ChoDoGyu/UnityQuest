using UnityEngine;
using Main.Scripts.Player.WeaponSystem;

namespace Main.Scripts.Interfaces
{
    public interface IWeapon
    {
        WeaponType WeaponType { get; }

        void Equip(Transform handTransform);  // �տ� ����
        void Unequip();                        // ����
        void Attack();                         // ���� ����
    }
}
