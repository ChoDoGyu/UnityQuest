using UnityEngine;

namespace Main.Scripts.UI
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector3 offset = new Vector3(0, 30, 0);

        private void LateUpdate()
        {
            if (playerTransform == null) return;
            transform.position = playerTransform.position + offset;
        }
    }
}