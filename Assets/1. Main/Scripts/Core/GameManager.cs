using UnityEngine;
using Main.Scripts.InputSystem;
using Main.Scripts.SceneManagement;

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
            if (InputManager.Instance == null)
            {
                gameObject.AddComponent<InputManager>();
            }

            if (SceneLoader.Instance == null)
            {
                gameObject.AddComponent<SceneLoader>();
            }
        }

        public void SetGameState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"Game State changed to: {newState}");
        }
    }
}

