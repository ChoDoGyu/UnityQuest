using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Main.Scripts.Data;
using Main.Scripts.Player.SkillSystem;

namespace Main.Scripts.UI
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider staminaSlider;

        [Header("Sub HUDs")]
        [SerializeField] private SkillHUD skillHUD;

        public void UpdateHP(float ratio)
        {
            hpSlider.SetValueWithoutNotify(ratio);
        }

        public void UpdateStamina(float ratio)
        {
            staminaSlider.SetValueWithoutNotify(ratio);
        }

        //��ų �����͸� SkillHUD�� ����
        public void SetSkillData(List<SkillData> skills, Action<SkillData> onUse, SkillManager skillManager)
        {
            skillHUD.SetupSlots(skills, onUse);

            //���� �̺�Ʈ ���� ���� �� �ٽ� ����
            skillManager.OnSkillUsed -= skillHUD.TriggerCooldown;
            skillManager.OnSkillUsed += skillHUD.TriggerCooldown;
        }
    }
}
