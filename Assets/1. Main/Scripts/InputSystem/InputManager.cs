using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions inputActions;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

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

        private void BindInputEvents()
        {
            inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

            inputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += ctx => LookInput = Vector2.zero;
        }

        private void OnDestroy()
        {
            inputActions?.Dispose();
        }
    }
}
