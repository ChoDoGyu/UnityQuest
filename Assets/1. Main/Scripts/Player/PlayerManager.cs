using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.Core;
using Main.Scripts.UI;
using Main.Scripts.Player.WeaponSystem;
using Main.Scripts.Player.SkillSystem;
using Main.Scripts.Data;
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
        [SerializeField] private SkillManager skillManager;
        [SerializeField] private EquipmentManager equipmentManager;

        public EquipmentManager EquipmentManager => equipmentManager;

        private PlayerInputActionsNamespace inputActions;
        private PlayerStat playerStat;

        //���� ���� ����
        [SerializeField] private ItemData equippedPotion;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // �ʼ� ������Ʈ ã��
            playerMovement = GetComponent<PlayerMovement>();
            playerLook = GetComponent<PlayerLook>();
            animatorController = GetComponent<PlayerAnimatorController>();
            skillManager = GetComponent<SkillManager>();
            equipmentManager = GetComponent<EquipmentManager>();

            // Stat ���� & ��� �Ŵ��� ����
            playerStat = new PlayerStat();
            equipmentManager.SetPlayerStat(playerStat);
        }

        private void OnEnable()
        {
            inputActions = new PlayerInputActionsNamespace();
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnMove;

            inputActions.Player.Potion.performed += OnUsePotion;
        }

        private void OnDisable()
        {
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnMove;

            inputActions.Player.Potion.performed -= OnUsePotion;
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
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            playerMovement.Move(input);
        }

        private void OnUsePotion(InputAction.CallbackContext context)
        {
            if (equippedPotion != null)
            {
                Debug.Log($"QŰ ���� ���: {equippedPotion.itemName}");
                UsePotion(equippedPotion);
                equippedPotion = null; // ��� �� ���� ��� (�ʿ��ϴٸ�)
            }
        }

        //�κ��丮 �Ǵ� ���� ���Կ��� ȣ���ؼ� ���� ����
        public void SetEquippedPotion(ItemData potion)
        {
            equippedPotion = potion;
        }

        public void UsePotion(ItemData potion)
        {
            Debug.Log($"���� ���: {potion.itemName}");
            // ����: HP ȸ��
            playerStat.AddStat(StatType.HP, 50); // ���ð�
            GameManager.Instance.UpdateHUD_HP(playerStat.GetStat(StatType.HP));
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
