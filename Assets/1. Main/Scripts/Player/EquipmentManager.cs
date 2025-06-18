using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Player.SkillSystem;

namespace Main.Scripts.Player
{
    public class EquipmentManager : MonoBehaviour
    {
        [Header("플레이어 본체")]
        public Transform playerRoot;      // Bone 탐색용
        public Transform weaponHoldPoint; // 무기 Attach 위치

        [Header("플레이어 스탯")]
        [SerializeField] private PlayerStat playerStat;

        // 외형 & 데이터 캐싱
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

        //무기
        public void EquipWeapon(WeaponData weapon)
        {
            UnequipWeapon();

            // 외형: Resources 폴더 구조 예시
            GameObject prefab = Resources.Load<GameObject>($"Weapons/{weapon.weaponType}Weapon");
            if (prefab == null)
            {
                Debug.LogError($"Weapon Prefab {weapon.weaponType}Weapon not found in Resources!");
                return;
            }

            currentWeaponObject = Instantiate(prefab, weaponHoldPoint);
            currentWeaponData = weapon;

            // 스텟
            equippedAttackBonus = weapon.attackPower;
            playerStat.AddStat(StatType.Attack, equippedAttackBonus);

            // 스킬
            if (skillManager != null)
                skillManager.SetSkills(weapon.equippedSkills);

            Debug.Log($"무기 장착: {weapon.name} (공격력 +{equippedAttackBonus})");
        }

        public void UnequipWeapon()
        {
            if (currentWeaponObject != null)
                Destroy(currentWeaponObject);

            playerStat.AddStat(StatType.Attack, -equippedAttackBonus);
            equippedAttackBonus = 0;
            currentWeaponData = null;

            // 스킬 초기화 필요시 추가
            skillManager?.ClearSkills();

            Debug.Log("무기 해제 완료");
        }

        //방어구
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

            Debug.Log($"방어구 장착: {armor.name} (방어력 +{equippedDefenseBonus})");
        }
        public void UnequipArmor()
        {
            if (currentArmorObject != null)
                Destroy(currentArmorObject);

            playerStat.AddStat(StatType.Defense, -equippedDefenseBonus);
            equippedDefenseBonus = 0;
            currentArmorData = null;

            Debug.Log("방어구 해제 완료");
        }

        //장신구
        public void EquipAccessory(AccessoryData accessory)
        {
            UnequipAccessory();

            equippedAccessoryBonus = accessory.bonusAmount;
            playerStat.AddStat(accessory.affectedStat, equippedAccessoryBonus);

            currentAccessory = accessory;

            Debug.Log($"장신구 장착: {accessory.name} ({accessory.affectedStat} +{equippedAccessoryBonus})");
        }

        public void UnequipAccessory()
        {
            if (currentAccessory != null)
            {
                playerStat.AddStat(currentAccessory.affectedStat, -equippedAccessoryBonus);
                equippedAccessoryBonus = 0;
                currentAccessory = null;

                Debug.Log("장신구 해제 완료");
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

        //현재 장착된 데이터 가져오기 (Optional)
        public WeaponData GetEquippedWeapon() => currentWeaponData;
        public ArmorData GetEquippedArmor() => currentArmorData;
        public AccessoryData GetEquippedAccessory() => currentAccessory;
    }
}
