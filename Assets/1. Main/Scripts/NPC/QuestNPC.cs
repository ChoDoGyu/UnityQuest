using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.Interfaces;

namespace Main.Scripts.NPC
{
    public class QuestNPC : MonoBehaviour, IInteractable
    {
        [Header("����Ʈ ������")]
        public QuestData quest;

        [Header("����")]
        public string talkSFX = "TalkNPC";

        [Header("��ȣ�ۿ� �Ÿ�")]
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
                GameManager.Instance.LogToConsole("�ʹ� �־ NPC�� ��ȭ�� �� �����ϴ�.");
                return;
            }

            GameManager.Instance.Audio?.PlaySFX(talkSFX);
            Interact();
        }

        public void Interact()
        {
            var qm = QuestManager.Instance;

            if (qm.IsCompleted(quest))
            {
                GameManager.Instance.LogToConsole("�̹� �Ϸ��� ����Ʈ�Դϴ�.");
                return;
            }

            if (!qm.IsInProgress(quest))
            {
                qm.AcceptQuest(quest);
            }
            else if (qm.IsObjectiveComplete(quest))
            {
                qm.CompleteQuest(quest, transform.position);
            }
            else
            {
                GameManager.Instance.LogToConsole("���� ����Ʈ ��ǥ�� �޼����� ���߽��ϴ�.");
            }
        }
    }
}