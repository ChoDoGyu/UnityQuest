using UnityEngine;
using TMPro;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// 퀘스트 1개 정보를 UI에 표시하는 슬롯 클래스입니다.
    /// </summary>
    public class QuestJournalSlot : MonoBehaviour
    {
        [Header("UI 요소")]
        [SerializeField] private TMP_Text questNameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text objectivesText;

        /// <summary>
        /// 슬롯에 퀘스트 데이터를 전달하여 UI 표시 내용을 갱신합니다.
        /// </summary>
        public void Init(QuestData quest)
        {
            questNameText.text = $"[{quest.questType}] {quest.questName}";
            descriptionText.text = quest.description;

            objectivesText.text = "";
            foreach (var obj in quest.objectives)
            {
                string status = obj.IsComplete ? "<color=green>완료</color>" : $"{obj.currentAmount}/{obj.requiredAmount}";
                objectivesText.text += $"- {obj.description} ({status})\n";
            }
        }
    }
}