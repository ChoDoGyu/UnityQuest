using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Main.Scripts.Data;

namespace Main.Scripts.UI.Save
{
    public class SaveSlotUI : MonoBehaviour
    {
        [Header("UI 참조")]
        [SerializeField] private TextMeshProUGUI slotTitleText;
        [SerializeField] private TextMeshProUGUI saveTimeText;
        [SerializeField] private RawImage previewImage;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button selectButton;

        private int slotIndex; // 슬롯 번호 (0: 자동, 1~2: 수동)
        private Action<int> onDeleteClicked;
        private Action<int> onSelectClicked;

        /// <summary>
        /// 슬롯 UI에 표시할 정보 설정
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