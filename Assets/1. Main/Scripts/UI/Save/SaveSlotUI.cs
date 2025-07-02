using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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