using UnityEngine;

namespace Main.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator animator;
        private Vector3 previousPosition;
        private float speed;

        [SerializeField] private float smoothing = 10f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            previousPosition = transform.position;
        }

        private void Update()
        {
            Vector3 displacement = transform.position - previousPosition;
            Vector3 horizontal = new Vector3(displacement.x, 0f, displacement.z);
            float currentSpeed = horizontal.magnitude / Time.deltaTime;

            speed = Mathf.Lerp(speed, currentSpeed, Time.deltaTime * smoothing);
            animator.SetFloat("Speed", speed);

            previousPosition = transform.position;
        }
    }
}
