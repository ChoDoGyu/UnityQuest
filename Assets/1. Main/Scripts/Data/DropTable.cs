using UnityEngine;
using System.Collections.Generic;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewDropTable", menuName = "ScriptableObject/Item/DropTable")]
    public class DropTable : ScriptableObject
    {
        [System.Serializable]
        public class DropEntry
        {
            public ItemData item;
            [Range(0f, 1f)] public float dropChance;
        }

        public List<DropEntry> drops = new();
    }
}
