using UnityEngine;


namespace Main.Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerLook))]
    [RequireComponent(typeof(PlayerAnimatorController))]
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [Header("Module References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerLook playerLook;
        [SerializeField] private PlayerAnimatorController animatorController;

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

            if (playerMovement == null || playerLook == null || animatorController == null)
            {
                Debug.LogError("PlayerManager �ʱ�ȭ ����: �ʼ� ������Ʈ ����");
            }
        }

        // ���� �ʿ� �� ���� ���� � ��� (ex: ü�� ����, ���� ó�� ��)
    }
}
