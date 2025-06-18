using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Data;

namespace Main.Scripts.UI
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("Slot �ڵ� ���� ����")]
        [SerializeField] private InventorySlot slotPrefab;
        [SerializeField] private int slotCount = 20;

        [Header("Slot�� ������ �θ�")]
        [SerializeField] private Transform slotParent;

        public List<InventorySlot> slots = new();

        private void Awake()
        {
            GenerateSlots();
        }

        private void GenerateSlots()
        {
            foreach (var slot in slots)
            {
                if (slot != null) Destroy(slot.gameObject);
            }
            slots.Clear();

            for (int i = 0; i < slotCount; i++)
            {
                var newSlot = Instantiate(slotPrefab, slotParent); //Bag �ؿ� ����!
                slots.Add(newSlot);
            }
        }

        public void AddItem(ItemData item)
        {
            foreach (var slot in slots)
            {
                if (slot.currentItem == null)
                {
                    slot.SetItem(item);
                    return;
                }
            }

            Debug.LogWarning("�κ��丮�� ���� á���ϴ�!");
        }

        public void RemoveItem(ItemData item)
        {
            foreach (var slot in slots)
            {
                if (slot.currentItem == item)
                {
                    slot.Clear();
                    return;
                }
            }
        }

        public void SortItems()
        {
            var temp = new List<ItemData>();

            foreach (var slot in slots)
            {
                if (slot.currentItem != null)
                    temp.Add(slot.currentItem);
            }

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < temp.Count)
                    slots[i].SetItem(temp[i]);
                else
                    slots[i].Clear();
            }
        }
    }
}