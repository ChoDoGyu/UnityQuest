using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.UI;
using Main.Scripts.Interfaces;

namespace Main.Scripts.NPC
{
    public class ShopNPC : MonoBehaviour, IInteractable
    {
        [Header("Shop ����")]
        public ShopData shopData;

        [Header("����")]
        public string talkSFX = "TalkNPC";

        [Header("�Ÿ� ����")]
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
                GameManager.Instance.LogToConsole("�ʹ� �־ ��ȣ�ۿ��� �� �����ϴ�.");
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