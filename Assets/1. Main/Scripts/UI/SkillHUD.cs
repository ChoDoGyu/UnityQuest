using System;
using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.UI.HUD;

namespace Main.Scripts.UI
{
    public class SkillHUD : MonoBehaviour
    {
        [SerializeField] private SkillSlot slotPrefab;
        [SerializeField] private Transform slotParent;

        private readonly List<SkillSlot> slots = new();

        public void SetupSlots(List<SkillData> skills, Action<SkillData> onUse)
        {
            // 기존 슬롯 제거
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }
            slots.Clear();

            // 새로운 슬롯 생성
            foreach (var skill in skills)
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.Init(skill, onUse);
                slots.Add(slot);
            }
        }

        public void TriggerCooldown(SkillData usedSkill)
        {
            foreach (var slot in slots)
            {
                if (slot.name == usedSkill.skillName || slot.SkillId == usedSkill.name)
                {
                    slot.TriggerCooldown();
                }
            }
        }
    }
}
