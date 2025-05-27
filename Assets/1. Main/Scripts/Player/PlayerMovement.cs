using UnityEngine;

namespace Main.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector2 moveInput;
        [SerializeField] private float moveSpeed = 5f;

        public Vector3 CurrentMoveDirection { get; private set; } = Vector3.zero;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 input)
        {
            moveInput = input;
        }

        private void FixedUpdate()
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

            if (moveDir.sqrMagnitude > 0.01f)
            {
                CurrentMoveDirection = moveDir.normalized;

                rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                CurrentMoveDirection = Vector3.zero;
            }
        }
    }
}
