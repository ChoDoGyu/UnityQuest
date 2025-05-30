using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Player.WeaponSystem;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public WeaponType weaponType;
        public Sprite icon;  // 인벤토리/장비창 아이콘 (선택사항)
        public List<SkillData> equippedSkills;
    }
}
