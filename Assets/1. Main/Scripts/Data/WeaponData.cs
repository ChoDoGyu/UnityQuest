using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Player.WeaponSystem;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item/WeaponData")]
    public class WeaponData : ItemData
    {
        public WeaponType weaponType;
        public List<SkillData> equippedSkills;

        public float attackPower;
    }
}
