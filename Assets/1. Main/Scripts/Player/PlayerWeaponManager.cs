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
        /// ���� ���� ��û (WeaponType�� ���� WeaponData + ������ �ڵ� ��Ī)
        /// </summary>
        public void EquipWeapon(WeaponType type, WeaponData data)
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
                Debug.LogError("������ ���⿡ IWeapon �������̽��� �����ϴ�.");
                return;
            }

            currentWeapon.Equip(weaponHoldPoint);
            currentWeaponData = data;

            // ���� Ÿ�� ��ġ ���� Ȯ��
            if (currentWeaponData.weaponType != type)
            {
                Debug.LogWarning($"WeaponData�� Ÿ��({currentWeaponData.weaponType})�� ��û�� Ÿ��({type})�� ��ġ���� �ʽ��ϴ�.");
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
