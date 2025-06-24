using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private Transform cam;

        private bool isActive = true;

        // �ʱ�ȭ (ī�޶� ����)
        public void Initialize(Transform cameraTransform)
        {
            cam = cameraTransform;
            isActive = true;
        }

        // ü�� ������Ʈ
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
