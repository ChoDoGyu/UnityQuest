using UnityEngine;

namespace Main.Scripts.Data
{
    public enum ItemType
    {
        Gold,
        Potion,
    }
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public ItemType itemType;
        public Sprite icon;
        public GameObject worldPrefab; // 드롭용 Prefab 참조

        [TextArea]
        public string description;
    }
}
