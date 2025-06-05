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
            // HUD ������ ���� �̺�Ʈ ���� ������� ��ü�� �� ����
            GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
            GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));
        }

        private void Update()
        {
            // �׽�Ʈ Ű
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
                GainExp(50); // ����ġ 50 ȹ��
            }

            // �̵� ���⿡ ���� �ü� ȸ��
            if (playerMovement != null && playerLook != null)
            {
                Vector3 moveDir = playerMovement.CurrentMoveDirection;
                if (moveDir.sqrMagnitude > 0.01f)
                {
                    playerLook.LookInDirection(moveDir);
                }
            }

            // ���� ��ü �Է�
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

        // ������/���¹̳� ���� �Լ� �� StatType ������� ����
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

            // ����ġ�� ���� ���� �䱸ġ�� �ʰ��ϸ� ������ ó��
            if (currentExp >= requiredExp)
            {
                playerStat.AddStat(StatType.Level, 1);
                playerStat.SetStat(StatType.Exp, currentExp - requiredExp); // ���� ����ġ�� ����

                Debug.Log($"������! ���� ����: {playerStat.GetStat(StatType.Level)}");

                // ������ �� ���� ����
                playerStat.ApplyLevelUpBonus();

                GameManager.Instance.PlayLevelUpEffects(transform.position);

                // HUD ����
                GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
                GameManager.Instance.UpdateHUD_Stamina(playerStat.GetStat(StatType.Stamina));

                // TODO: ����Ʈ/���� ��� �߰� ����
            }
        }

        public SkillManager GetSkillManager() => skillManager;
    }
}
