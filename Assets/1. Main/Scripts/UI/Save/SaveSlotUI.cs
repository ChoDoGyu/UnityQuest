using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
        public void SetData(int index, string title, string saveTime, Texture preview, Action<int> onDelete, Action<int> onSelect)
        {
            slotIndex = index;
            slotTitleText.text = title;
            saveTimeText.text = saveTime;
            previewImage.texture = preview;
            onDeleteClicked = onDelete;
            onSelectClicked = onSelect;
        }

        private void Awake()
        {
            deleteButton.onClick.AddListener(() =>
            {
                onDeleteClicked?.Invoke(slotIndex);
            });

            selectButton.onClick.AddListener(() =>
            {
                onSelectClicked?.Invoke(slotIndex);
            });
        }
    }
}