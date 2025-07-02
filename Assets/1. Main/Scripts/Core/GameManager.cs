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
    /// 게임 전체 흐름을 관리하는 GameManager 클래스입니다.
    /// 이 스크립트는 모든 씬에서 유지되며, 필요한 매니저들을 런타임에 동적으로 등록하거나 자동으로 찾아 작동합니다.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("전역 매니저 (공통)")]
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private FXManager fxManager;

        [Header("씬 매니저 (런타임에 찾음)")]
        private PlayerManager playerManager;
        private UIManager uiManager;
        private QuestManager questManager;
        private MapManager mapManager;
        private PauseManager pauseManager;
        private InputManager inputManager;

        [Header("상태 관리")]
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

            // 씬 로더 강제 생성
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
        /// 씬 내 필요한 매니저들을 자동으로 탐색하여 연결합니다.
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
                FX.PlayEffect("LevelUp", position); // FXManager에 등록된 이펙트 이름이 \"LevelUp\"일 경우
            }
        }
    }
}

