using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// 퀘스트 목록 전체를 관리하는 UI 패널입니다.
    /// </summary>
    public class QuestJournalPanel : MonoBehaviour
    {
        [SerializeField] private Transform slotParent;      // 슬롯들이 생성될 부모 오브젝트
        [SerializeField] private GameObject slotPrefab;     // 슬롯 프리팹

        /// <summary>
        /// 현재 수락 중인 퀘스트들을 UI에 표시합니다.
        /// </summary>
        public void Refresh(List<QuestData> activeQuests)
        {
            // 기존 슬롯 제거
            foreach (Transform child in slotParent)
            {
                Destroy(child.gameObject);
            }

            // 새로운 슬롯 생성
            foreach (var quest in activeQuests)
            {
                GameObject go = Instantiate(slotPrefab, slotParent);
                var slot = go.GetComponent<QuestJournalSlot>();
                slot.Init(quest);
            }
        }
    }
}