using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.UI;
using Main.Scripts.Interfaces;

namespace Main.Scripts.NPC
{
    public class ShopNPC : MonoBehaviour, IInteractable
    {
        [Header("Shop 연결")]
        public ShopData shopData;

        [Header("사운드")]
        public string talkSFX = "TalkNPC";

        [Header("거리 제한")]
        public float interactionDistance = 2f;

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void OnMouseDown()
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > interactionDistance)
            {
                GameManager.Instance.LogToConsole("너무 멀어서 상호작용할 수 없습니다.");
                return;
            }

            Interact();
        }

        public void Interact()
        {
            GameManager.Instance.Audio?.PlaySFX(talkSFX);
            UIManager.Instance.OpenShop(shopData);
        }
    }
}