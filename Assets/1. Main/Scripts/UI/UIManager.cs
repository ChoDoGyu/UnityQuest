using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.Data;
using Main.Scripts.Player.SkillSystem;
using System;
using System.Collections.Generic;

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

        [Header("Inventory & Equipment")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject equipmentPanel;


        private PlayerInputActions inputActions;
        private bool isInventoryOpen = false;

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
