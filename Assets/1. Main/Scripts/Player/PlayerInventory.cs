using UnityEngine;
using System.Collections.Generic;
using System;
using Main.Scripts.Data;
using Main.Scripts.Core;

namespace Main.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }

        [SerializeField] private int maxSlots = 20;
        private readonly List<ItemData> items = new();
        public IReadOnlyList<ItemData> GetItems() => items;

        public event Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public bool HasSpace() => items.Count < maxSlots;

        public bool HasSpace(int amount) => items.Count + amount <= maxSlots;

        public bool AddItem(ItemData item)
        {
            if (!HasSpace())
            {
                GameManager.Instance.LogToConsole("�κ��丮�� ���� á���ϴ�!");
                return false;
            }

            items.Add(item);
            OnInventoryChanged?.Invoke(); //UI ���� �̺�Ʈ ����
            GameManager.Instance.LogToConsole($"{item.itemName} ȹ��");
            return true;
        }

        public void RemoveItem(ItemData item)
        {
            if (items.Remove(item))
            {
                OnInventoryChanged?.Invoke(); //���� �õ����� ����
                GameManager.Instance.LogToConsole($"{item.itemName} ���ŵ�");
            }
        }

        public void ClearAll()
        {
            items.Clear();
            OnInventoryChanged?.Invoke(); //��ü �ʱ�ȭ���� ����
            GameManager.Instance.LogToConsole("�κ��丮 �ʱ�ȭ �Ϸ�");
        }
    }
}