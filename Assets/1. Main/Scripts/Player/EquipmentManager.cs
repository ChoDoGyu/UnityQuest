using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Player.SkillSystem;

namespace Main.Scripts.Player
{
    public class EquipmentManager : MonoBehaviour
    {
        [Header("�÷��̾� ��ü")]
        public Transform playerRoot;      // Bone Ž����
        public Transform weaponHoldPoint; // ���� Attach ��ġ

        [Header("�÷��̾� ����")]
        [SerializeField] private PlayerStat playerStat;

        // ���� & ������ ĳ��
        private GameObject currentWeaponObject;
        private WeaponData currentWeaponData;
        private float equippedAttackBonus = 0;

        private GameObject currentArmorObject;
        private ArmorData currentArmorData;
        private float equippedDefenseBonus = 0;

        private AccessoryData currentAccessory;
        private float equippedAccessoryBonus = 0;

        private SkillManager skillManager;

        private void Awake()
        {
            skillManager = GetComponent<SkillManager>();
        }

        public void SetPlayerStat(PlayerStat stat)
        {
            playerStat = stat;
        }

        //����
        public void EquipWeapon(WeaponData weapon)
        {
            UnequipWeapon();

            // ����: Resources ���� ���� ����
            GameObject prefab = Resources.Load<GameObject>($"Weapons/{weapon.weaponType}Weapon");
            if (prefab == null)
            {
                Debug.LogError($"Weapon Prefab {weapon.weaponType}Weapon not found in Resources!");
                return;
            }

            currentWeaponObject = Instantiate(prefab, weaponHoldPoint);
            currentWeaponData = weapon;

            // ����
            equippedAttackBonus = weapon.attackPower;
            playerStat.AddStat(StatType.Attack, equippedAttackBonus);

            // ��ų
            if (skillManager != null)
                skillManager.SetSkills(weapon.equippedSkills);

            Debug.Log($"���� ����: {weapon.name} (���ݷ� +{equippedAttackBonus})");
        }

        public void UnequipWeapon()
        {
            if (currentWeaponObject != null)
                Destroy(currentWeaponObject);

            playerStat.AddStat(StatType.Attack, -equippedAttackBonus);
            equippedAttackBonus = 0;
            currentWeaponData = null;

            // ��ų �ʱ�ȭ �ʿ�� �߰�
            skillManager?.ClearSkills();

            Debug.Log("���� ���� �Ϸ�");
        }

        //��
        public void EquipArmor(ArmorData armor)
        {
            UnequipArmor();

            Transform bone = FindBone(playerRoot, armor.attachBoneName);
            if (bone != null)
            {
                currentArmorObject = Instantiate(armor.worldPrefab, bone);
                currentArmorObject.transform.localPosition = Vector3.zero;
                currentArmorObject.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning($"Bone {armor.attachBoneName} not found!");
            }

            equippedDefenseBonus = armor.defenseBonus;
            playerStat.AddStat(StatType.Defense, equippedDefenseBonus);

            currentArmorData = armor;

            Debug.Log($"�� ����: {armor.name} (���� +{equippedDefenseBonus})");
        }
        public void UnequipArmor()
        {
            if (currentArmorObject != null)
                Destroy(currentArmorObject);

            playerStat.AddStat(StatType.Defense, -equippedDefenseBonus);
            equippedDefenseBonus = 0;
            currentArmorData = null;

            Debug.Log("�� ���� �Ϸ�");
        }

        //��ű�
        public void EquipAccessory(AccessoryData accessory)
        {
            UnequipAccessory();

            equippedAccessoryBonus = accessory.bonusAmount;
            playerStat.AddStat(accessory.affectedStat, equippedAccessoryBonus);

            currentAccessory = accessory;

            Debug.Log($"��ű� ����: {accessory.name} ({accessory.affectedStat} +{equippedAccessoryBonus})");
        }

        public void UnequipAccessory()
        {
            if (currentAccessory != null)
            {
                playerStat.AddStat(currentAccessory.affectedStat, -equippedAccessoryBonus);
                equippedAccessoryBonus = 0;
                currentAccessory = null;

                Debug.Log("��ű� ���� �Ϸ�");
            }
        }

        //Bone Finder
        private Transform FindBone(Transform root, string boneName)
        {
            foreach (Transform t in root.GetComponentsInChildren<Transform>())
            {
                if (t.name == boneName)
                    return t;
            }
            return null;
        }

        //���� ������ ������ �������� (Optional)
        public WeaponData GetEquippedWeapon() => currentWeaponData;
        public ArmorData GetEquippedArmor() => currentArmorData;
        public AccessoryData GetEquippedAccessory() => currentAccessory;
    }
}
