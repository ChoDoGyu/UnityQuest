using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private Transform cam;

        // 초기화 (카메라 전달)
        public void Initialize(Transform cameraTransform)
        {
            cam = cameraTransform;
        }

        // 체력 업데이트
        public void UpdateHP(float current, float max)
        {
            if (slider != null)
                slider.value = current / max;
        }

        private void LateUpdate()
        {
            if (cam != null)
            {
                // 체력바가 항상 카메라를 바라보도록 설정
                transform.forward = cam.forward;
            }
        }
    }
}
