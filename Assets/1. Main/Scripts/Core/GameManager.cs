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

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentState { get; private set; } = GameState.Playing;

        [Header("Managed Systems")]
        [SerializeField] private InputManager inputManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private FXManager fxManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private PauseManager pauseManager;

        public AudioManager Audio => audioManager;
        public FXManager FX => fxManager;
        public PauseManager Pause => pauseManager;
        public PlayerManager PlayerManager => playerManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeManagers();
        }


        private void InitializeManagers()
        {
            if (inputManager == null)
                inputManager = FindObjectOfType<InputManager>();

            if (uiManager == null)
                uiManager = FindObjectOfType<UIManager>();

            if (playerManager == null)
                playerManager = FindObjectOfType<PlayerManager>();

            if (fxManager == null)
                fxManager = FindObjectOfType<FXManager>();

            if (audioManager == null)
                audioManager = FindObjectOfType<AudioManager>();

            if (SceneLoader.Instance == null)
                gameObject.AddComponent<SceneLoader>();

            if (pauseManager == null)
                pauseManager = FindObjectOfType<PauseManager>();
        }


        private void Start()
        {
            SetupSkillUI();
        }

        public void SetGameState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"Game State changed to: {newState}");
        }

        //UI 기능 위임
        public void UpdateHUD_HP(float ratio) => uiManager.UpdateHP(ratio);
        public void UpdateHUD_Stamina(float ratio) => uiManager.UpdateStamina(ratio);
        public void ToggleDebugConsole() => uiManager.ToggleConsole();
        public void LogToConsole(string msg) => uiManager.Log(msg);

        //Player 기능 위임
        public void TakeDamage(float amount) => playerManager.TakeDamage(amount);
        public void UseStamina(float amount) => playerManager.UseStamina(amount);
        public void RecoverStamina(float amount) => playerManager.RecoverStamina(amount);

        //스킬 UI 초기화
        public void SetupSkillUI()
        {
            var skillManager = playerManager.GetSkillManager();
            var skills = skillManager.GetEquippedSkills();

            // SkillManager를 UI까지 전달
            uiManager.InitializeSkillUI(skills, skillManager.UseSkill, skillManager);
        }


        // 레벨업 효과 호출 (FX + SFX)
        public void PlayLevelUpEffects(Vector3 position)
        {
            fxManager?.PlayEffect("LevelUp", position);
            audioManager?.PlaySFX("LevelUp");
        }
    }
}

