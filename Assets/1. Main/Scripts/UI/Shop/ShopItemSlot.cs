using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Shop
{
    public class ShopItemSlot : ItemSlotBase, IPointerClickHandler
    {
        private ShopItem shopItem;
        public void Setup(ShopItem item)
        {
            shopItem = item;
            SetItem(item.item); // 아이콘 표시 등 base 처리
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShopController.Instance.BuyItem(shopItem, 1); //1개 즉시 구매
        }

        public override string GetTooltipText()
        {
            if (shopItem?.item == null)
                return "";

            string result = shopItem.item.itemName;

            if (!string.IsNullOrEmpty(shopItem.item.description))
                result += $"\n{shopItem.item.description}";

            result += $"\n<color=yellow>가격: {shopItem.price:N0} G</color>";

            return result;
        }
    }
}