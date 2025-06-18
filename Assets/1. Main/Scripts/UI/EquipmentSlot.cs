using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.Player;

namespace Main.Scripts.UI
{
    public enum EquipmentType
    {
        Armor,
        Weapon,
        Accessory
    }
    public class EquipmentSlot : ItemSlotBase, IDropHandler
    {
        public EquipmentType slotType;


        public void OnDrop(PointerEventData eventData)
        {
            if (DragHandler.Instance.draggedSlot is InventorySlot invSlot && invSlot.currentItem != null)
            {
                // 슬롯에 아이템 세팅
                SetItem(invSlot.currentItem);

                // 현재 슬롯 타입에 맞게 실제 장착 실행
                switch (invSlot.currentItem.itemType)
                {
                    case ItemType.Armor:
                        GameManager.Instance.PlayerManager.EquipmentManager.EquipArmor((ArmorData)invSlot.currentItem);
                        break;

                    case ItemType.Weapon:
                        GameManager.Instance.PlayerManager.EquipmentManager.EquipWeapon((WeaponData)invSlot.currentItem);
                        break;

                    case ItemType.Accessory:
                        GameManager.Instance.PlayerManager.EquipmentManager.EquipAccessory((AccessoryData)invSlot.currentItem);
                        break;
                }

                // 인벤토리 슬롯 비움
                invSlot.Clear();
            }
        }

        public override void Clear()
        {
            // 슬롯 비울 때도 EquipmentManager에 해제 호출
            switch (slotType)
            {
                case EquipmentType.Armor:
                    GameManager.Instance.PlayerManager.EquipmentManager.UnequipArmor();
                    break;
                case EquipmentType.Weapon:
                    GameManager.Instance.PlayerManager.EquipmentManager.UnequipWeapon();
                    break;
                case EquipmentType.Accessory:
                    GameManager.Instance.PlayerManager.EquipmentManager.UnequipAccessory();
                    break;
            }

            base.Clear();
        }
    }
}