using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputActionsNamespace = PlayerInputActions;

namespace Main.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector2 moveInput;
        [SerializeField] private float moveSpeed = 5f;


        private PlayerInputActionsNamespace inputActions;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            inputActions = new PlayerInputActionsNamespace();
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnMove;
        }

        private void OnDisable()
        {
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnMove;
            inputActions.Player.Disable();
        }

        private void FixedUpdate()
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            //Debug.Log($"Move Input: {moveInput}");
        }
    }
}
