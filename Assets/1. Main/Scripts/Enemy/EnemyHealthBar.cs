using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private Transform cam;

        private bool isActive = true;

        // 초기화 (카메라 전달)
        public void Initialize(Transform cameraTransform)
        {
            cam = cameraTransform;
            isActive = true;
        }

        // 체력 업데이트
        public void UpdateHP(float current, float max)
        {
            if (slider != null)
                slider.value = current / max;
        }

        private void OnDisable()
        {
            isActive = false;
        }

        private void LateUpdate()
        {
            if (!isActive || cam == null) return;

            transform.forward = cam.forward;
        }
    }
}
