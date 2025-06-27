using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// ����Ʈ ��� ��ü�� �����ϴ� UI �г��Դϴ�.
    /// </summary>
    public class QuestJournalPanel : MonoBehaviour
    {
        [SerializeField] private Transform slotParent;      // ���Ե��� ������ �θ� ������Ʈ
        [SerializeField] private GameObject slotPrefab;     // ���� ������

        /// <summary>
        /// ���� ���� ���� ����Ʈ���� UI�� ǥ���մϴ�.
        /// </summary>
        public void Refresh(List<QuestData> activeQuests)
        {
            // ���� ���� ����
            foreach (Transform child in slotParent)
            {
                Destroy(child.gameObject);
            }

            // ���ο� ���� ����
            foreach (var quest in activeQuests)
            {
                GameObject go = Instantiate(slotPrefab, slotParent);
                var slot = go.GetComponent<QuestJournalSlot>();
                slot.Init(quest);
            }
        }
    }
}