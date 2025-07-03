using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Save
{
    public class SaveSlotUIManager : MonoBehaviour
    {
        [Header("슬롯 프리팹 & 부모")]
        [SerializeField] private SaveSlotUI slotPrefab;
        [SerializeField] private Transform slotParent;

        private List<SaveSlotUI> slotList = new();

        public void RefreshSlots(List<SaveSlotData> saveDataList)
        {
            // 기존 슬롯 제거
            foreach (var slot in slotList)
                Destroy(slot.gameObject);
            slotList.Clear();

            // 새 슬롯 생성
            foreach (var data in saveDataList)
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.SetData(data, OnClickSelect, OnClickDelete);
                slotList.Add(slot);
            }
        }

        // 슬롯 선택 버튼 클릭
        private void OnClickSelect(SaveSlotData data)
        {
            Debug.Log($"[불러오기] 슬롯 선택됨: {data.title}");
            // 저장 데이터 불러오기 처리
        }

        // 슬롯 삭제 버튼 클릭
        private void OnClickDelete(SaveSlotData data)
        {
            Debug.Log($"[삭제] 슬롯 삭제됨: {data.title}");
            // 삭제 처리 및 다시 RefreshSlots 호출
        }
    }
}