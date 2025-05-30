using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Player.WeaponSystem;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public WeaponType weaponType;
        public Sprite icon;  // �κ��丮/���â ������ (���û���)
        public List<SkillData> equippedSkills;
    }
}
