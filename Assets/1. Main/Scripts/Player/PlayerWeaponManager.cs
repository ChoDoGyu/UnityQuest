using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Player.WeaponSystem;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Player
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        [SerializeField] private Transform weaponHoldPoint;

        private IWeapon currentWeapon;
        private GameObject currentWeaponObject;

        private Dictionary<WeaponType, GameObject> weaponPrefabs;

        private void Awake()
        {
            LoadWeaponPrefabs();
        }

        private void LoadWeaponPrefabs()
        {
            weaponPrefabs = new Dictionary<WeaponType, GameObject>
            {
                { WeaponType.Sword, Resources.Load<GameObject>("Weapons/Sword") },
                { WeaponType.Bow, Resources.Load<GameObject>("Weapons/Bow") },
                { WeaponType.Staff, Resources.Load<GameObject>("Weapons/Staff") }
            };
        }

        public void EquipWeapon(WeaponType type)
        {
            // 기존 무기 제거
            if (currentWeapon != null) currentWeapon.Unequip();
            if (currentWeaponObject != null) Destroy(currentWeaponObject);

            // 새 무기 장착
            if (weaponPrefabs.TryGetValue(type, out GameObject prefab))
            {
                GameObject newWeapon = Instantiate(prefab, weaponHoldPoint);
                currentWeaponObject = newWeapon;

                currentWeapon = newWeapon.GetComponent<IWeapon>();
                currentWeapon?.Equip(weaponHoldPoint);
            }
            else
            {
                Debug.LogWarning($"무기 프리팹 로드 실패: {type}");
            }
        }
    }
}
