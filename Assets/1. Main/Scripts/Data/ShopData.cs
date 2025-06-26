using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/Shop/ShopData", fileName = "NewShopData")]
    public class ShopData : ScriptableObject
    {
        public List<ShopItem> itemsToSell = new();
    }

    [System.Serializable]
    public class ShopItem
    {
        public ItemData item;
        public int price;
    }
}