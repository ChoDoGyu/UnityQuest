using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.InputSystem;
using Main.Scripts.SceneManagement;
using Main.Scripts.UI;
using Main.Scripts.Player;

namespace Main.Scripts.Core
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    /// <summary>
    /// ���� ��ü �帧�� �����ϴ� GameManager Ŭ�����Դϴ�.
    /// �� ��ũ��Ʈ�� ��� ������ �����Ǹ�, �ʿ��� �Ŵ������� ��Ÿ�ӿ� �������� ����ϰų� �ڵ����� ã�� �۵��մϴ�.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("���� �Ŵ��� (����)")]
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private FXManager fxManager;

        [Header("�� �Ŵ��� (��Ÿ�ӿ� ã��)")]
        private PlayerManager playerManager;
        private UIManager uiManager;
        private QuestManager questManager;
        private MapManager mapManager;
        private PauseManager pauseManager;
        private InputManager inputManager;

        [Header("���� ����")]
        private GameState currentState = GameState.Playing;
        public GameState CurrentState => currentState;

        public AudioManager Audio => audioManager;
        public FXManager FX => fxManager;
        public PlayerManager PlayerManager => playerManager;
        public UIManager UIManager => uiManager;
        public QuestManager QuestManager => questManager;
        public MapManager MapManager => mapManager;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �� �δ� ���� ����
            if (FindObjectOfType<SceneLoader>() == null)
                gameObject.AddComponent<SceneLoader>();
        }

        private void Start()
        {
            InitializeManagers();
            SetupSkillUI();

            if (mapManager != null && playerManager != null)
            {
                mapManager.RegisterIcon(playerManager.transform, "Player");
            }
        }


        /// <summary>
        /// �� �� �ʿ��� �Ŵ������� �ڵ����� Ž���Ͽ� �����մϴ�.
        /// </summary>
        private void InitializeManagers()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
            questManager = FindObjectOfType<QuestManager>();
            mapManager = FindObjectOfType<MapManager>();
            pauseManager = FindObjectOfType<PauseManager>();
            inputManager = FindObjectOfType<InputManager>();
            audioManager = FindObjectOfType<AudioManager>();
            fxManager = FindObjectOfType<FXManager>();
        }

        public void SetupSkillUI()
        {
            if (playerManager == null || uiManager == null)
            {
                Debug.LogWarning("[GameManager] SetupSkillUI skipped (null reference)");
                return;
            }

            var skillManager = playerManager.GetSkillManager();
            var skills = skillManager.GetEquippedSkills();
            uiManager.InitializeSkillUI(skills, skillManager.UseSkill, skillManager);
        }

        public void SetGameState(GameState state)
        {
            currentState = state;

            switch (state)
            {
                case GameState.Playing:
                    Time.timeScale = 1f;
                    uiManager?.HidePauseMenu();
                    inputManager?.SetPaused(false);
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    uiManager?.ShowPauseMenu();
                    inputManager?.SetPaused(true);
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0f;
                    uiManager?.ShowGameOverMenu();
                    break;
            }
        }

        public void TogglePause()
        {
            if (currentState == GameState.Playing)
                SetGameState(GameState.Paused);
            else if (currentState == GameState.Paused)
                SetGameState(GameState.Playing);
        }

        public void TakeDamage(float damage) => playerManager?.TakeDamage(damage);
        public void UpdateHUD_HP(float ratio) => uiManager?.UpdateHP(ratio);
        public void UpdateHUD_Stamina(float ratio) => uiManager?.UpdateStamina(ratio);
        //public void UpdateHUD_Exp(float ratio) => uiManager?.UpdateExp(ratio);
        public void LogToConsole(string msg) => uiManager?.Log(msg);

        public void PlayLevelUpEffects(Vector3 position)
        {
            if (FX != null)
            {
                FX.PlayEffect("LevelUp", position); // FXManager�� ��ϵ� ����Ʈ �̸��� \"LevelUp\"�� ���
            }
        }
    }
}

