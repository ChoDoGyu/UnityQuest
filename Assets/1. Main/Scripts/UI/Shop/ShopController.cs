using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Data;
using Main.Scripts.Player;
using Main.Scripts.Core;

namespace Main.Scripts.UI.Shop
{
    public class ShopController : MonoBehaviour
    {
        public static ShopController Instance { get; private set; }

        [Header("UI 구성요소")]
        public GameObject shopPanel;
        public Transform itemSlotParent;
        public ShopItemSlot slotPrefab;

        private List<ShopItemSlot> spawnedSlots = new();
        private ShopData currentShop;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            shopPanel.SetActive(false);
        }

        public void OpenShop(ShopData shopData)
        {
            UIManager.Instance.isSellMode = true;

            currentShop = shopData;
            shopPanel.SetActive(true);

            // 슬롯이 한 번도 생성되지 않았을 때만 생성
            if (spawnedSlots.Count == 0)
                RefreshUI();
        }

        public void CloseShop()
        {
            shopPanel.SetActive(false);
        }


        public void BuyItem(ShopItem shopItem, int quantity)
        {
            int totalPrice = shopItem.price * quantity;

            if (!PlayerWallet.Instance.CanAfford(totalPrice))
            {
                GameManager.Instance.LogToConsole("골드가 부족합니다.");
                return;
            }

            if (!PlayerInventory.Instance.HasSpace(quantity))
            {
                GameManager.Instance.LogToConsole("인벤토리가 가득 찼습니다.");
                return;
            }

            PlayerWallet.Instance.SpendGold(totalPrice);

            for (int i = 0; i < quantity; i++)
                PlayerInventory.Instance.AddItem(shopItem.item);

        }

        private void RefreshUI()
        {
            // 기존 슬롯 제거 안 함 → 한 번만 생성
            for (int i = 0; i < currentShop.itemsToSell.Count; i++)
            {
                var shopItem = currentShop.itemsToSell[i];
                var slot = Instantiate(slotPrefab, itemSlotParent);
                slot.Setup(shopItem);
                spawnedSlots.Add(slot);
            }
        }
    }
}