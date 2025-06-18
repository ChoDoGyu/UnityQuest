using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Data;
using Main.Scripts.Core;

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
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                switch (currentItem.itemType)
                {
                    case ItemType.Armor:
                        //���� Equip ȣ�� X �� ���â Armor Slot ã�Ƽ� SetItem
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

        // ���â ���� ã�� ����
        private EquipmentSlot FindEquipmentSlot(EquipmentType type)
        {
            // ��: EquipmentSlot ��� �����ϴ� Manager���� ã�ƿ���
            return GameObject.FindObjectOfType<EquipmentSlotManager>()
                             .GetSlotByType(type);
        }
    }
}