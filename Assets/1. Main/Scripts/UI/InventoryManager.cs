using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Main.Scripts.Data;
using Main.Scripts.Player;

namespace Main.Scripts.UI
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [Header("슬롯 생성 관련")]
        [SerializeField] private InventorySlot slotPrefab;
        [SerializeField] private int slotCount = 20;
        [SerializeField] private Transform slotParent;

        [Header("UI 요소")]
        [SerializeField] private TextMeshProUGUI goldText;

        private readonly List<InventorySlot> slots = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            GenerateSlots();
        }

        private void Start()
        {
            PlayerWallet.Instance.OnGoldChanged += RefreshGoldUI;
            PlayerInventory.Instance.OnInventoryChanged += () =>
            {
                ShowItems(PlayerInventory.Instance.GetItems());
            };

            // 초기 상태 반영
            RefreshGoldUI(PlayerWallet.Instance.Gold);
            ShowItems(PlayerInventory.Instance.GetItems());
        }

        public void RefreshGoldUI(int gold)
        {
            if (goldText != null)
                goldText.text = $"{gold:N0} G";
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
                var newSlot = Instantiate(slotPrefab, slotParent);
                slots.Add(newSlot);
            }
        }

        public void ShowItems(IReadOnlyList<ItemData> items)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < items.Count)
                {
                    slots[i].SetItem(items[i]);
                }
                else
                {
                    slots[i].Clear();
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