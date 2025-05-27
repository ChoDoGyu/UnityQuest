using UnityEngine;

namespace Main.Scripts.Player
{
    public class PlayerLook : MonoBehaviour
    {
        public void LookInDirection(Vector3 moveDirection)
        {
            if (moveDirection.sqrMagnitude <= 0.01f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}
