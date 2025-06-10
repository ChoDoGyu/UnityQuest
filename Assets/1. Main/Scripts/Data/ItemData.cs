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
        public GameObject worldPrefab; // ��ӿ� Prefab ����

        [TextArea]
        public string description;
    }
}
