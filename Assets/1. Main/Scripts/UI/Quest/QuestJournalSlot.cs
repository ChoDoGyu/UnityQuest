using UnityEngine;
using TMPro;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// ����Ʈ 1�� ������ UI�� ǥ���ϴ� ���� Ŭ�����Դϴ�.
    /// </summary>
    public class QuestJournalSlot : MonoBehaviour
    {
        [Header("UI ���")]
        [SerializeField] private TMP_Text questNameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text objectivesText;

        /// <summary>
        /// ���Կ� ����Ʈ �����͸� �����Ͽ� UI ǥ�� ������ �����մϴ�.
        /// </summary>
        public void Init(QuestData quest)
        {
            questNameText.text = $"[{quest.questType}] {quest.questName}";
            descriptionText.text = quest.description;

            objectivesText.text = "";
            foreach (var obj in quest.objectives)
            {
                string status = obj.IsComplete ? "<color=green>�Ϸ�</color>" : $"{obj.currentAmount}/{obj.requiredAmount}";
                objectivesText.text += $"- {obj.description} ({status})\n";
            }
        }
    }
}