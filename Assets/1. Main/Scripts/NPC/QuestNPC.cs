using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Core;
using Main.Scripts.Interfaces;

namespace Main.Scripts.NPC
{
    public class QuestNPC : MonoBehaviour, IInteractable
    {
        [Header("퀘스트 데이터")]
        public QuestData quest;

        [Header("사운드")]
        public string talkSFX = "TalkNPC";

        [Header("상호작용 거리")]
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
                GameManager.Instance.LogToConsole("너무 멀어서 NPC와 대화할 수 없습니다.");
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
                GameManager.Instance.LogToConsole("이미 완료한 퀘스트입니다.");
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
                GameManager.Instance.LogToConsole("아직 퀘스트 목표를 달성하지 못했습니다.");
            }
        }
    }
}