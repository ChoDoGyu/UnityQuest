using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.UI
{
    /// <summary>
    /// 장비창에 존재하는 모든 EquipmentSlot을 관리하고,
    /// ItemType/EquipmentType에 따라 올바른 슬롯을 찾아준다.
    /// </summary>
    public class EquipmentSlotManager : MonoBehaviour
    {
        [Header("장비창 슬롯들")]
        [SerializeField] private List<EquipmentSlot> slots = new();

        /// <summary>
        /// EquipmentType에 해당하는 슬롯을 반환한다.
        /// 예: Armor, Weapon, Accessory
        /// </summary>
        public EquipmentSlot GetSlotByType(EquipmentType type)
        {
            foreach (var slot in slots)
            {
                if (slot.slotType == type)
                    return slot;
            }
            Debug.LogWarning($"{type} 타입의 슬롯을 찾을 수 없습니다.");
            return null;
        }

        /// <summary>
        /// 모든 슬롯을 초기화한다.
        /// </summary>
        public void ClearAllSlots()
        {
            foreach (var slot in slots)
            {
                slot.Clear();
            }
        }

        /// <summary>
        /// 현재 슬롯 상태를 디버그용으로 출력
        /// </summary>
        public void DebugSlots()
        {
            foreach (var slot in slots)
            {
                Debug.Log($"SlotType: {slot.slotType} / Item: {(slot.currentItem != null ? slot.currentItem.itemName : "없음")}");
            }
        }

    }
}