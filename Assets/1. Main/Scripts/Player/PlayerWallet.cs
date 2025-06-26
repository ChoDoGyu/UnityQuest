using UnityEngine;
using System;
using Main.Scripts.Core;

namespace Main.Scripts.Player
{
    public class PlayerWallet : MonoBehaviour
    {
        public static PlayerWallet Instance { get; private set; }

        [SerializeField] private int startingGold = 300;
        private int gold;
        public int Gold => gold;

        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            gold = startingGold;
        }

        public bool CanAfford(int amount) => gold >= amount;

        public void AddGold(int amount)
        {
            gold += amount;
            OnGoldChanged?.Invoke(gold);
            GameManager.Instance.LogToConsole($"+{amount}G (보유: {gold})");
        }

        public void SpendGold(int amount)
        {
            if (!CanAfford(amount))
            {
                GameManager.Instance.LogToConsole("소지금이 부족합니다!");
                return;
            }

            gold -= amount;
            OnGoldChanged?.Invoke(gold);
            GameManager.Instance.LogToConsole($"-{amount}G (보유: {gold})");
        }
    }
}