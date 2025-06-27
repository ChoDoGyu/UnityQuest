using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    public enum QuestType
    {
        Main,
        Side,
        Tutorial
    }
    [CreateAssetMenu(menuName = "ScriptableObject/Quest/QuestData")]
    public class QuestData : ScriptableObject
    {
        [Header("�⺻ ����")]
        public string questId;
        public string questName;
        [TextArea] public string description;
        public QuestType questType;

        [Header("��ǥ ���")]
        public List<QuestObjective> objectives;

        [Header("����")]
        public int rewardGold;
        public List<ItemData> rewardItems;
    }

    [System.Serializable]
    public class QuestObjective
    {
        public string objectiveId;         // ��: "KillGoblin"
        public string description;         // ��: "��� 3���� óġ"
        public int requiredAmount = 1;     // ��ǥ ����
        public int currentAmount = 0;      // ���� �޼� ��

        public bool IsComplete => currentAmount >= requiredAmount;

        public void AddProgress(int amount = 1)
        {
            currentAmount += amount;
        }
    }
}