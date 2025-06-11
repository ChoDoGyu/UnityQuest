using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private Transform cam;

        // �ʱ�ȭ (ī�޶� ����)
        public void Initialize(Transform cameraTransform)
        {
            cam = cameraTransform;
        }

        // ü�� ������Ʈ
        public void UpdateHP(float current, float max)
        {
            if (slider != null)
                slider.value = current / max;
        }

        private void LateUpdate()
        {
            if (cam != null)
            {
                // ü�¹ٰ� �׻� ī�޶� �ٶ󺸵��� ����
                transform.forward = cam.forward;
            }
        }
    }
}
