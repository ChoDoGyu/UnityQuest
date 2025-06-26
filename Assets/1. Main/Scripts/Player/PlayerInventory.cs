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
                GameManager.Instance.LogToConsole("인벤토리가 가득 찼습니다!");
                return false;
            }

            items.Add(item);
            OnInventoryChanged?.Invoke(); //UI 갱신 이벤트 발행
            GameManager.Instance.LogToConsole($"{item.itemName} 획득");
            return true;
        }

        public void RemoveItem(ItemData item)
        {
            if (items.Remove(item))
            {
                OnInventoryChanged?.Invoke(); //제거 시도에도 갱신
                GameManager.Instance.LogToConsole($"{item.itemName} 제거됨");
            }
        }

        public void ClearAll()
        {
            items.Clear();
            OnInventoryChanged?.Invoke(); //전체 초기화에도 갱신
            GameManager.Instance.LogToConsole("인벤토리 초기화 완료");
        }
    }
}