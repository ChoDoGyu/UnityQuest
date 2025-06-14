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
        [Header("무기 장착 위치")]
        [SerializeField] private Transform weaponHoldPoint;

        [Header("무기 프리팹 경로")]
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

        // Resources 폴더에서 프리팹 로드
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
                    Debug.LogWarning($"{type}Weapon 프리팹을 찾을 수 없습니다. Resources/Weapons 폴더 확인");
                }
            }
        }

        /// <summary>
        /// 프리팹 내부에서 WeaponData를 가져오는 혼합 방식
        /// </summary>
        public void EquipWeapon(WeaponType type)
        {
            if (!weaponPrefabDict.TryGetValue(type, out GameObject prefab))
            {
                Debug.LogError($"WeaponType {type}에 해당하는 프리팹이 등록되어 있지 않습니다.");
                return;
            }

            // 기존 무기 제거
            if (currentWeaponObject != null)
            {
                Destroy(currentWeaponObject);
            }

            // 프리팹 인스턴스화 및 장착
            GameObject weaponGO = Instantiate(prefab, weaponHoldPoint);
            currentWeaponObject = weaponGO;

            currentWeapon = weaponGO.GetComponent<IWeapon>();
            if (currentWeapon == null)
            {
                Debug.LogError("IWeapon 인터페이스 없음");
                return;
            }

            currentWeapon.Equip(weaponHoldPoint);

            // 여기서 WeaponData를 프리팹 내부에서 자동 추출
            WeaponBase baseWeapon = weaponGO.GetComponent<WeaponBase>();
            currentWeaponData = baseWeapon?.WeaponData;

            // 무기 타입 일치 여부 확인
            if (currentWeaponData == null)
            {
                Debug.LogWarning("WeaponData가 프리팹에 없습니다.");
                return;
            }

            //SkillManager에 무기 스킬 반영
            skillManager.SetSkills(currentWeaponData.equippedSkills);

            //UI 갱신 (HUD 갱신)
            GameManager.Instance.SetupSkillUI();
        }

        // 외부 접근용
        public WeaponType GetCurrentWeaponType() => currentWeaponData?.weaponType ?? WeaponType.None;
        public IWeapon GetCurrentWeapon() => currentWeapon;
        public WeaponData GetCurrentWeaponData() => currentWeaponData;
    }
}
