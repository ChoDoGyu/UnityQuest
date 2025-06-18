using UnityEngine;
using Main.Scripts.Player;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item/AccessoryData")]
    public class AccessoryData : ItemData
    {
        public StatType affectedStat;  // ex: HP, Stamina
        public float bonusAmount;      // ex: +50
    }
}