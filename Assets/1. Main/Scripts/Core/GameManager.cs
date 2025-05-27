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

            if (SceneLoader.Instance == null)
                gameObject.AddComponent<SceneLoader>();
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
    }
}

