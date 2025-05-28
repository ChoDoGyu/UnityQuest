using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.Core;
using Main.Scripts.UI;
using Main.Scripts.Player.WeaponSystem;
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

            playerStat = new PlayerStat(100, 50);
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
            playerStat.HP.OnValueChanged += GameManager.Instance.UpdateHUD_HP;
            playerStat.Stamina.OnValueChanged += GameManager.Instance.UpdateHUD_Stamina;

            playerStat.HP.Reset();
            playerStat.Stamina.Reset();
        }

        private void Update()
        {
            // 테스트 키
            if (Keyboard.current.hKey.wasPressedThisFrame)
                GameManager.Instance.TakeDamage(10);
            if (Keyboard.current.jKey.wasPressedThisFrame)
                GameManager.Instance.UseStamina(5);
            if (Keyboard.current.kKey.wasPressedThisFrame)
                GameManager.Instance.RecoverStamina(5);

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
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                playerWeaponManager.EquipWeapon(WeaponType.Sword);
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
                playerWeaponManager.EquipWeapon(WeaponType.Bow);
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
                playerWeaponManager.EquipWeapon(WeaponType.Staff);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            playerMovement.Move(input);
        }

        // 내부 호출은 여전히 허용
        public void TakeDamage(float amount) => playerStat.HP.Decrease(amount);
        public void UseStamina(float amount) => playerStat.Stamina.Decrease(amount);
        public void RecoverStamina(float amount) => playerStat.Stamina.Increase(amount);
    }
}
