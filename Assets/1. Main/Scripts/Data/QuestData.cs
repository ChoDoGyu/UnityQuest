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
        [Header("기본 정보")]
        public string questId;
        public string questName;
        [TextArea] public string description;
        public QuestType questType;

        [Header("목표 목록")]
        public List<QuestObjective> objectives;

        [Header("보상")]
        public int rewardGold;
        public List<ItemData> rewardItems;
    }

    [System.Serializable]
    public class QuestObjective
    {
        public string objectiveId;         // 예: "KillGoblin"
        public string description;         // 예: "고블린 3마리 처치"
        public int requiredAmount = 1;     // 목표 수량
        public int currentAmount = 0;      // 현재 달성 수

        public bool IsComplete => currentAmount >= requiredAmount;

        public void AddProgress(int amount = 1)
        {
            currentAmount += amount;
        }
    }
}