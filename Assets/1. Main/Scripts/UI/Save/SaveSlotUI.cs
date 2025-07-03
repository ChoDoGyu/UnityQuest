using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Save
{
    public class SaveSlotUI : MonoBehaviour
    {
        [Header("UI ����")]
        [SerializeField] private TextMeshProUGUI slotTitleText;
        [SerializeField] private TextMeshProUGUI saveTimeText;
        [SerializeField] private RawImage previewImage;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button selectButton;

        private int slotIndex; // ���� ��ȣ (0: �ڵ�, 1~2: ����)
        private Action<int> onDeleteClicked;
        private Action<int> onSelectClicked;

        /// <summary>
        /// ���� UI�� ǥ���� ���� ����
        /// </summary>
        public void SetData(SaveSlotData data, Action<SaveSlotData> onSelect, Action<SaveSlotData> onDelete)
        {
            slotTitleText.text = data.title;
            saveTimeText.text = data.saveTime;
            previewImage.texture = data.previewTexture;

            selectButton.onClick.RemoveAllListeners();
            deleteButton.onClick.RemoveAllListeners();

            selectButton.onClick.AddListener(() => onSelect?.Invoke(data));
            deleteButton.onClick.AddListener(() => onDelete?.Invoke(data));
        }
    }
}