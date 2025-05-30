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

        //스킬 데이터를 SkillHUD에 전달
        public void SetSkillData(List<SkillData> skills, Action<SkillData> onUse, SkillManager skillManager)
        {
            skillHUD.SetupSlots(skills, onUse);

            //기존 이벤트 연결 제거 후 다시 연결
            skillManager.OnSkillUsed -= skillHUD.TriggerCooldown;
            skillManager.OnSkillUsed += skillHUD.TriggerCooldown;
        }
    }
}
