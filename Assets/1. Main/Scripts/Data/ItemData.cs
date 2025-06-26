using UnityEngine;

namespace Main.Scripts.Data
{
    public enum ItemType
    {
        Gold,
        Potion,
        Armor,
        Weapon,
        Accessory
    }

    [CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObject/Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public ItemType itemType;
        public Sprite icon;
        public GameObject worldPrefab;
        public int price = 10;
        [TextArea] public string description;
    }
}
