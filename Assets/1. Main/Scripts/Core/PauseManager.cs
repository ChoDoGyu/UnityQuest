using UnityEngine;
using UnityEngine.InputSystem;
using Main.Scripts.InputSystem;
using Main.Scripts.UI;

namespace Main.Scripts.Core
{
    public class PauseManager : MonoBehaviour
    {
        private bool isPaused = false;

        [SerializeField] private GameObject pauseMenuUI;

        public bool IsPaused => isPaused;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                TogglePause();
            }
        }

        private bool CanTogglePause()
        {
            return GameManager.Instance != null &&
                   GameManager.Instance.CurrentState != GameState.GameOver;
        }

        public void TogglePause()
        {
            if (isPaused) Resume();
            else Pause();
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0;

            UIManager.Instance.ShowPauseMenu(); // UIManager로 위임
            GameManager.Instance.SetGameState(GameState.Paused);
            InputManager.Instance.SetPaused(true);
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1;

            UIManager.Instance.HidePauseMenu(); // UIManager로 위임
            GameManager.Instance.SetGameState(GameState.Playing);
            InputManager.Instance.SetPaused(false);
        }


    }
}
