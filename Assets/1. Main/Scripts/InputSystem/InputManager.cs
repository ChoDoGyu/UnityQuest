using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions inputActions;

        public Vector2 MoveInput { get; private set; }

        private bool isPaused = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            inputActions = new PlayerInputActions();
            inputActions.Enable();

            BindInputEvents();
        }

        public void SetPaused(bool paused)
        {
            isPaused = paused;
            // 필요 시 여기서 추가적인 입력 비활성화 처리도 가능
        }

        private void BindInputEvents()
        {
            inputActions.Player.Move.performed += ctx =>
            {
                if (!isPaused)
                    MoveInput = ctx.ReadValue<Vector2>();
            };
            inputActions.Player.Move.canceled += ctx =>
            {
                if (!isPaused)
                    MoveInput = Vector2.zero;
            };
        }

        private void OnDestroy()
        {
            inputActions?.Dispose();
        }
    }
}
