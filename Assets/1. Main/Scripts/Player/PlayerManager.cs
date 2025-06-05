using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.Core;
using Main.Scripts.UI;
using Main.Scripts.Player.WeaponSystem;
using Main.Scripts.Player.SkillSystem;
using PlayerInputActionsNamespace = PlayerInputActions;


namespace Main.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [Header("Module References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerLook playerLook;
        [SerializeField] private PlayerAnimatorController animatorController;
        [SerializeField] private PlayerWeaponManager playerWeaponManager;
        [SerializeField] private SkillManager skillManager;

        private PlayerInputActionsNamespace inputActions;
        private PlayerStat playerStat;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            playerMovement = GetComponent<PlayerMovement>();
            playerLook = GetComponent<PlayerLook>();
            animatorController = GetComponent<PlayerAnimatorController>();
            playerWeaponManager = GetComponent<PlayerWeaponManager>();
            skillManager = GetComponent<SkillManager>();

            playerStat = new PlayerStat();
        }

        private void OnEnable()
        {
            inputActions = new PlayerInputActionsNamespace();
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnMove;
        }

        private void OnDisable()
        {
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnMove;
            inputActions.Player.Disable();
        }

        private void Start()
        {
            // HUD 갱신은 추후 이벤트 연동 방식으로 대체할 수 있음
            GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
            GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));
        }

        private void Update()
        {
            // 테스트 키
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                TakeDamage(10);
            }
            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                UseStamina(5);
            }
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                RecoverStamina(5);
            }
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                GainExp(50); // 경험치 50 획득
            }

            // 이동 방향에 따라 시선 회전
            if (playerMovement != null && playerLook != null)
            {
                Vector3 moveDir = playerMovement.CurrentMoveDirection;
                if (moveDir.sqrMagnitude > 0.01f)
                {
                    playerLook.LookInDirection(moveDir);
                }
            }

            // 무기 교체 입력
            //if (Keyboard.current.digit1Key.wasPressedThisFrame)
            //    playerWeaponManager.EquipWeapon(WeaponType.Sword);
            //if (Keyboard.current.digit2Key.wasPressedThisFrame)
            //    playerWeaponManager.EquipWeapon(WeaponType.Bow);
            //if (Keyboard.current.digit3Key.wasPressedThisFrame)
            //    playerWeaponManager.EquipWeapon(WeaponType.Staff);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            playerMovement.Move(input);
        }

        // 데미지/스태미나 조작 함수 → StatType 기반으로 변경
        public void TakeDamage(float amount)
        {
            playerStat.AddStat(StatType.HP, -amount);
            GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
        }

        public void UseStamina(float amount)
        {
            playerStat.AddStat(StatType.Stamina, -amount);
            GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));
        }

        public void RecoverStamina(float amount)
        {
            playerStat.AddStat(StatType.Stamina, amount);
            GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));
        }

        public void GainExp(float amount)
        {
            playerStat.AddStat(StatType.Exp, amount);
            float currentExp = playerStat.GetStat(StatType.Exp);
            float currentLevel = playerStat.GetStat(StatType.Level);

            int requiredExp = Main.Scripts.Data.ExpTable.GetRequiredExp((int)currentLevel);

            // 경험치가 다음 레벨 요구치를 초과하면 레벨업 처리
            if (currentExp >= requiredExp)
            {
                playerStat.AddStat(StatType.Level, 1);
                playerStat.SetStat(StatType.Exp, currentExp - requiredExp); // 남은 경험치는 유지

                Debug.Log($"레벨업! 현재 레벨: {playerStat.GetStat(StatType.Level)}");

                // 레벨업 시 스탯 증가
                playerStat.ApplyLevelUpBonus();

                GameManager.Instance.PlayLevelUpEffects(transform.position);

                // HUD 갱신
                GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
                GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));

                // TODO: 이펙트/사운드 재생 추가 예정
            }
        }

        public SkillManager GetSkillManager() => skillManager;
    }
}
