using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputActionsNamespace = PlayerInputActions;

namespace Main.Scripts.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;  // 바닥 감지용 마스크

        private Camera mainCamera;
        private PlayerInputActionsNamespace inputActions;
        private Vector2 mouseDelta;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            inputActions = new PlayerInputActionsNamespace();
            inputActions.Player.Enable();
            inputActions.Player.Look.performed += OnLook;
            inputActions.Player.Look.canceled += OnLook;
        }

        private void OnDisable()
        {
            inputActions.Player.Look.performed -= OnLook;
            inputActions.Player.Look.canceled -= OnLook;
            inputActions.Player.Disable();
        }

        private void Update()
        {
            RotateToMouse();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            mouseDelta = context.ReadValue<Vector2>();
        }

        private void RotateToMouse()
        {
            if (mainCamera == null)
                return;

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
            {
                Vector3 targetPoint = hit.point;
                Vector3 direction = targetPoint - transform.position;
                direction.y = 0f;

                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
                }
            }
        }
    }
}
