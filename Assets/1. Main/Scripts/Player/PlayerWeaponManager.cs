using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Player.WeaponSystem;
using Main.Scripts.Player.SkillSystem;
using Main.Scripts.Interfaces;
using Main.Scripts.Data;
using Main.Scripts.Core;

namespace Main.Scripts.Player
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        [Header("���� ���� ��ġ")]
        [SerializeField] private Transform weaponHoldPoint;

        [Header("���� ������ ���")]
        [SerializeField] private List<WeaponType> weaponTypes;


        private Dictionary<WeaponType, GameObject> weaponPrefabDict = new();
        private IWeapon currentWeapon;
        private GameObject currentWeaponObject;
        private WeaponData currentWeaponData;

        private SkillManager skillManager;


        private void Awake()
        {
            skillManager = GetComponent<SkillManager>();
            LoadWeaponPrefabs();
        }

        // Resources �������� ������ �ε�
        private void LoadWeaponPrefabs()
        {
            foreach (var type in weaponTypes)
            {
                var prefab = Resources.Load<GameObject>($"Weapons/{type}Weapon");
                if (prefab != null)
                {
                    weaponPrefabDict[type] = prefab;
                }
                else
                {
                    Debug.LogWarning($"{type}Weapon �������� ã�� �� �����ϴ�. Resources/Weapons ���� Ȯ��");
                }
            }
        }

        /// <summary>
        /// ������ ���ο��� WeaponData�� �������� ȥ�� ���
        /// </summary>
        public void EquipWeapon(WeaponType type)
        {
            if (!weaponPrefabDict.TryGetValue(type, out GameObject prefab))
            {
                Debug.LogError($"WeaponType {type}�� �ش��ϴ� �������� ��ϵǾ� ���� �ʽ��ϴ�.");
                return;
            }

            // ���� ���� ����
            if (currentWeaponObject != null)
            {
                Destroy(currentWeaponObject);
            }

            // ������ �ν��Ͻ�ȭ �� ����
            GameObject weaponGO = Instantiate(prefab, weaponHoldPoint);
            currentWeaponObject = weaponGO;

            currentWeapon = weaponGO.GetComponent<IWeapon>();
            if (currentWeapon == null)
            {
                Debug.LogError("IWeapon �������̽� ����");
                return;
            }

            currentWeapon.Equip(weaponHoldPoint);

            // ���⼭ WeaponData�� ������ ���ο��� �ڵ� ����
            WeaponBase baseWeapon = weaponGO.GetComponent<WeaponBase>();
            currentWeaponData = baseWeapon?.WeaponData;

            // ���� Ÿ�� ��ġ ���� Ȯ��
            if (currentWeaponData == null)
            {
                Debug.LogWarning("WeaponData�� �����տ� �����ϴ�.");
                return;
            }

            //SkillManager�� ���� ��ų �ݿ�
            skillManager.SetSkills(currentWeaponData.equippedSkills);

            //UI ���� (HUD ����)
            GameManager.Instance.SetupSkillUI();
        }

        // �ܺ� ���ٿ�
        public WeaponType GetCurrentWeaponType() => currentWeaponData?.weaponType ?? WeaponType.None;
        public IWeapon GetCurrentWeapon() => currentWeapon;
        public WeaponData GetCurrentWeaponData() => currentWeaponData;
    }
}
