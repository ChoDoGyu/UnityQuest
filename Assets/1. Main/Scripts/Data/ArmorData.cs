using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item/ArmorData")]
    public class ArmorData : ItemData
    {
        public string attachBoneName;
        public int defenseBonus;
    }
}
