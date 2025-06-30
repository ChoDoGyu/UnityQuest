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

        public bool isSellMode = false; // 현재 판매 모드 여부

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
                QuestEventType.Accepted => $"[퀘스트 수락] {quest.questName}",
                QuestEventType.ObjectiveUpdated => $"[퀘스트 진행] {quest.questName}",
                QuestEventType.Completed => $"[퀘스트 완료] {quest.questName}",
                _ => ""
            };

            // 퀘스트 알림 출력
            questNotificationPanel?.Show(msg);

            // 저널 패널 새로고침
            UpdateQuestJournal();
        }

        public void UpdateQuestJournal()
        {
            if (questJournalPanel == null) return;

            // 현재 진행 중인 퀘스트 목록 가져오기
            var questList = GameManager.Instance.QuestManager.GetActiveQuests();

            // UI에 반영
            questJournalPanel.Refresh(questList);
        }

        /// <summary>
        /// HUD 전체를 숨깁니다. (보스 컷신 등 연출용)
        /// </summary>
        public void HideHUD()
        {
            if (hudView != null)
                hudView.gameObject.SetActive(false);
        }

        /// <summary>
        /// HUD 전체를 다시 보여줍니다. (컷신 종료 후 복구용)
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

        //옵션 UI 보여주기/숨기기 추가
        public void ShowOptionMenu() => optionMenuUI?.SetActive(true);
        public void HideOptionMenu() => optionMenuUI?.SetActive(false);

        //일시정지 UI 제어용 메서드
        public void ShowPauseMenu() => pauseMenuUI?.SetActive(true);
        public void HidePauseMenu() => pauseMenuUI?.SetActive(false);

        //스킬 UI 초기화 전달
        public void InitializeSkillUI(List<SkillData> skills, Action<SkillData> onUse, SkillManager skillManager)
        {
            hudView.SetSkillData(skills, onUse, skillManager);
        }
    }
}
