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
            SetItem(item.item); // ������ ǥ�� �� base ó��
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShopController.Instance.BuyItem(shopItem, 1); //1�� ��� ����
        }

        public override string GetTooltipText()
        {
            if (shopItem?.item == null)
                return "";

            string result = shopItem.item.itemName;

            if (!string.IsNullOrEmpty(shopItem.item.description))
                result += $"\n{shopItem.item.description}";

            result += $"\n<color=yellow>����: {shopItem.price:N0} G</color>";

            return result;
        }
    }
}