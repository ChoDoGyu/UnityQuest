using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.UI
{
    /// <summary>
    /// ���â�� �����ϴ� ��� EquipmentSlot�� �����ϰ�,
    /// ItemType/EquipmentType�� ���� �ùٸ� ������ ã���ش�.
    /// </summary>
    public class EquipmentSlotManager : MonoBehaviour
    {
        [Header("���â ���Ե�")]
        [SerializeField] private List<EquipmentSlot> slots = new();

        /// <summary>
        /// EquipmentType�� �ش��ϴ� ������ ��ȯ�Ѵ�.
        /// ��: Armor, Weapon, Accessory
        /// </summary>
        public EquipmentSlot GetSlotByType(EquipmentType type)
        {
            foreach (var slot in slots)
            {
                if (slot.slotType == type)
                    return slot;
            }
            Debug.LogWarning($"{type} Ÿ���� ������ ã�� �� �����ϴ�.");
            return null;
        }

        /// <summary>
        /// ��� ������ �ʱ�ȭ�Ѵ�.
        /// </summary>
        public void ClearAllSlots()
        {
            foreach (var slot in slots)
            {
                slot.Clear();
            }
        }

        /// <summary>
        /// ���� ���� ���¸� ����׿����� ���
        /// </summary>
        public void DebugSlots()
        {
            foreach (var slot in slots)
            {
                Debug.Log($"SlotType: {slot.slotType} / Item: {(slot.currentItem != null ? slot.currentItem.itemName : "����")}");
            }
        }

    }
}