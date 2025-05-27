using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider staminaSlider;

        public void UpdateHP(float ratio)
        {
            hpSlider.SetValueWithoutNotify(ratio);
        }

        public void UpdateStamina(float ratio)
        {
            staminaSlider.SetValueWithoutNotify(ratio);
        }
    }
}
