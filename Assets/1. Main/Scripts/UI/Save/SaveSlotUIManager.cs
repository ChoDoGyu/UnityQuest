using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Save
{
    public class SaveSlotUIManager : MonoBehaviour
    {
        [Header("���� ������ & �θ�")]
        [SerializeField] private SaveSlotUI slotPrefab;
        [SerializeField] private Transform slotParent;

        private List<SaveSlotUI> slotList = new();

        public void RefreshSlots(List<SaveSlotData> saveDataList)
        {
            // ���� ���� ����
            foreach (var slot in slotList)
                Destroy(slot.gameObject);
            slotList.Clear();

            // �� ���� ����
            foreach (var data in saveDataList)
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.SetData(data, OnClickSelect, OnClickDelete);
                slotList.Add(slot);
            }
        }

        // ���� ���� ��ư Ŭ��
        private void OnClickSelect(SaveSlotData data)
        {
            Debug.Log($"[�ҷ�����] ���� ���õ�: {data.title}");
            // ���� ������ �ҷ����� ó��
        }

        // ���� ���� ��ư Ŭ��
        private void OnClickDelete(SaveSlotData data)
        {
            Debug.Log($"[����] ���� ������: {data.title}");
            // ���� ó�� �� �ٽ� RefreshSlots ȣ��
        }
    }
}