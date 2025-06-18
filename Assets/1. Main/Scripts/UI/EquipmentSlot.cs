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
                // ���Կ� ������ ����
                SetItem(invSlot.currentItem);

                // ���� ���� Ÿ�Կ� �°� ���� ���� ����
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

                // �κ��丮 ���� ���
                invSlot.Clear();
            }
        }

        public override void Clear()
        {
            // ���� ��� ���� EquipmentManager�� ���� ȣ��
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