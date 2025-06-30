using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.Data;
using Main.Scripts.Player.SkillSystem;
using System;
using System.Collections.Generic;
using Main.Scripts.UI.Shop;
using Main.Scripts.UI.Quest;
using Main.Scripts.Core;

namespace Main.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("UI Views")]
        [SerializeField] private HUDView hudView;
        [SerializeField] private DebugConsoleView debugConsoleView;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private OptionManager optionManager;
        [SerializeField] private GameObject optionMenuUI;
        [SerializeField] private TooltipManager tooltipManager;
        [SerializeField] private ShopController shopController;
        [SerializeField] private QuestNotificationPanel questNotificationPanel;
        [SerializeField] private QuestJournalPanel questJournalPanel;

        [Header("Inventory & Equipment")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject equipmentPanel;


        private PlayerInputActions inputActions;
        private bool isInventoryOpen = false;

        public bool isSellMode = false; // ���� �Ǹ� ��� ����

        public OptionManager Option => optionManager;
        public TooltipManager Tooltip => tooltipManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.Player.Inventory.performed += OnToggleInventory;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            inputActions.Player.Inventory.performed -= OnToggleInventory;
        }

        private void Update()
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame)
            {
                ToggleConsole();
            }
        }

        private void OnToggleInventory(InputAction.CallbackContext ctx)
        {
            ToggleInventory();
        }

        public void ToggleInventory()
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel?.SetActive(isInventoryOpen);
            equipmentPanel?.SetActive(isInventoryOpen);
        }

        public void OpenShop(ShopData data)
        {
            shopController.OpenShop(data);
            inventoryPanel.SetActive(true);
        }

        public void CloseShop()
        {
            shopController.CloseShop();
            inventoryPanel.SetActive(false);

            isSellMode = false;
        }

        public void NotifyQuestUpdate(QuestEventType type, QuestData quest)
        {
            string msg = type switch
            {
                QuestEventType.Accepted => $"[����Ʈ ����] {quest.questName}",
                QuestEventType.ObjectiveUpdated => $"[����Ʈ ����] {quest.questName}",
                QuestEventType.Completed => $"[����Ʈ �Ϸ�] {quest.questName}",
                _ => ""
            };

            // ����Ʈ �˸� ���
            questNotificationPanel?.Show(msg);

            // ���� �г� ���ΰ�ħ
            UpdateQuestJournal();
        }

        public void UpdateQuestJournal()
        {
            if (questJournalPanel == null) return;

            // ���� ���� ���� ����Ʈ ��� ��������
            var questList = GameManager.Instance.QuestManager.GetActiveQuests();

            // UI�� �ݿ�
            questJournalPanel.Refresh(questList);
        }

        /// <summary>
        /// HUD ��ü�� ����ϴ�. (���� �ƽ� �� �����)
        /// </summary>
        public void HideHUD()
        {
            if (hudView != null)
                hudView.gameObject.SetActive(false);
        }

        /// <summary>
        /// HUD ��ü�� �ٽ� �����ݴϴ�. (�ƽ� ���� �� ������)
        /// </summary>
        public void ShowHUD()
        {
            if (hudView != null)
                hudView.gameObject.SetActive(true);
        }

        public void UpdateHP(float ratio) => hudView.UpdateHP(ratio);
        public void UpdateStamina(float ratio) => hudView.UpdateStamina(ratio);
        public void ToggleConsole() => debugConsoleView.ToggleConsole();
        public void Log(string msg) => debugConsoleView.Log(msg);

        //�ɼ� UI �����ֱ�/����� �߰�
        public void ShowOptionMenu() => optionMenuUI?.SetActive(true);
        public void HideOptionMenu() => optionMenuUI?.SetActive(false);

        //�Ͻ����� UI ����� �޼���
        public void ShowPauseMenu() => pauseMenuUI?.SetActive(true);
        public void HidePauseMenu() => pauseMenuUI?.SetActive(false);

        //��ų UI �ʱ�ȭ ����
        public void InitializeSkillUI(List<SkillData> skills, Action<SkillData> onUse, SkillManager skillManager)
        {
            hudView.SetSkillData(skills, onUse, skillManager);
        }
    }
}
