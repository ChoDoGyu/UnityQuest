using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Player;

namespace Main.Scripts.Core
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        // ���� ���� ���� ����Ʈ���� �����ϴ� ��ųʸ� (key: questId)
        private readonly Dictionary<string, QuestData> activeQuests = new();

        // �Ϸ��� ����Ʈ���� ID�� �����ϴ� �ؽü�
        private readonly HashSet<string> completedQuests = new();//HashSet<string>	�Ϸ� ���� üũ�� �� O(1) ����

        private void Awake()
        {
            // �̱��� ���� ����
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// ����Ʈ ���� ó��. �̹� ������ ����Ʈ�� �ߺ� ���� �Ұ�
        /// </summary>
        public void AcceptQuest(QuestData quest)
        {
            if (activeQuests.ContainsKey(quest.questId))
            {
                GameManager.Instance.LogToConsole("�̹� ������ ����Ʈ�Դϴ�.");
                return;
            }

            activeQuests.Add(quest.questId, quest);
            GameManager.Instance.LogToConsole($"[����Ʈ ����] {quest.questName}");
        }

        /// <summary>
        /// ����Ʈ ��ǥ ���൵ ������Ʈ.
        /// ���� óġ/������ ȹ�� �� ȣ���.
        /// </summary>
        public void UpdateObjective(string objectiveId, int amount = 1)
        {
            foreach (var quest in activeQuests.Values)
            {
                foreach (var obj in quest.objectives)
                {
                    // objectiveId�� ��ġ�ϰ� ���� �Ϸ���� ���� ��쿡�� ����
                    if (obj.objectiveId == objectiveId && !obj.IsComplete)
                    {
                        obj.AddProgress(amount);  // ���൵ ����

                        // UI �α� ���
                        GameManager.Instance.LogToConsole(
                            $"[����Ʈ ����] {obj.description} ({obj.currentAmount}/{obj.requiredAmount})"
                        );

                        // ��ü ��ǥ�� ��� �Ϸ�Ǿ����� üũ (����Ʈ �Ϸ� ����)
                        if (IsObjectiveComplete(quest))
                        {
                            GameManager.Instance.LogToConsole(
                                $"[����Ʈ �Ϸ� ����] {quest.questName} �� NPC���� �����ϼ���."
                            );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����Ʈ �Ϸ� ó��.
        /// - ���� ����
        /// - ȿ�� ���
        /// - ���� ������Ʈ
        /// </summary>
        public void CompleteQuest(QuestData quest, Vector3 position)
        {
            // ����ó��: ������ ����Ʈ�� �ƴϰų� ���� �Ϸ� ������ �� ä�� ���
            if (!activeQuests.ContainsKey(quest.questId)) return;
            if (!IsObjectiveComplete(quest)) return;

            // 1. ��� ����
            PlayerWallet.Instance.AddGold(quest.rewardGold);

            // 2. ������ ����
            foreach (var item in quest.rewardItems)
            {
                PlayerInventory.Instance.AddItem(item);
            }

            // 3. ȿ�� ��� (FX + ����)
            GameManager.Instance.FX?.PlayEffect("QuestComplete", position);
            GameManager.Instance.Audio?.PlaySFX("QuestComplete");

            // 4. �α� ���
            GameManager.Instance.LogToConsole($"[����Ʈ �Ϸ�] {quest.questName} ���� ���� �Ϸ�!");

            // 5. ����Ʈ ���� ����
            activeQuests.Remove(quest.questId);
            completedQuests.Add(quest.questId);
        }

        /// <summary>
        /// ����Ʈ�� ���� ���� ������ Ȯ��
        /// </summary>
        public bool IsInProgress(QuestData quest) => activeQuests.ContainsKey(quest.questId);

        /// <summary>
        /// ����Ʈ�� �Ϸ�Ǿ����� Ȯ��
        /// </summary>
        public bool IsCompleted(QuestData quest) => completedQuests.Contains(quest.questId);

        /// <summary>
        /// �ش� ����Ʈ�� ��� ��ǥ�� �Ϸ�Ǿ����� Ȯ��
        /// </summary>
        public bool IsObjectiveComplete(QuestData quest)//AND ���� �Ǵ� ���� ����
        {
            foreach (var obj in quest.objectives)
            {
                if (!obj.IsComplete) return false;
            }
            return true;
        }
    }
}