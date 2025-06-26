using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.Player;

namespace Main.Scripts.UI
{
    public class InventorySlot : ItemSlotBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (currentItem == null) return;
            DragHandler.Instance.StartDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragHandler.Instance.OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragHandler.Instance.EndDrag();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (currentItem == null) return;

            if (UIManager.Instance.isSellMode)
            {
                SellItem();
            }
            else
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    switch (currentItem.itemType)
                    {
                        case ItemType.Armor:
                            //직접 Equip 호출 X → 장비창 Armor Slot 찾아서 SetItem
                            var armorSlot = FindEquipmentSlot(EquipmentType.Armor);
                            armorSlot?.SetItem(currentItem);
                            break;

                        case ItemType.Weapon:
                            var weaponSlot = FindEquipmentSlot(EquipmentType.Weapon);
                            weaponSlot?.SetItem(currentItem);
                            break;

                        case ItemType.Accessory:
                            var accSlot = FindEquipmentSlot(EquipmentType.Accessory);
                            accSlot?.SetItem(currentItem);
                            break;

                        case ItemType.Potion:
                            GameManager.Instance.PlayerManager.SetEquippedPotion(currentItem);
                            break;
                    }

                    Clear();
                }
            }

        }

        // 장비창 슬롯 찾는 헬퍼
        private EquipmentSlot FindEquipmentSlot(EquipmentType type)
        {
            // 예: EquipmentSlot 모두 관리하는 Manager에서 찾아오기
            return GameObject.FindObjectOfType<EquipmentSlotManager>()
                             .GetSlotByType(type);
        }

        private void SellItem()
        {
            int sellPrice = Mathf.RoundToInt(currentItem.price * 0.5f); // 기본 50% 가격

            PlayerInventory.Instance.RemoveItem(currentItem);
            PlayerWallet.Instance.AddGold(sellPrice);
        }
    }
}