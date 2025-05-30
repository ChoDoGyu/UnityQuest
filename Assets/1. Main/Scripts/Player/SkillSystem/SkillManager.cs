using System;
using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Combat;
using Main.Scripts.Data;

namespace Main.Scripts.Player.SkillSystem
{
    public class SkillManager : MonoBehaviour
    {
        [Tooltip("플레이어가 현재 장착 중인 스킬들")]
        [SerializeField] private List<SkillData> equippedSkills = new();

        // 각 스킬의 남은 쿨타임을 추적하는 딕셔너리
        private Dictionary<SkillData, float> cooldownTimers = new();

        // 스킬이 사용되었을 때 UI 등 다른 시스템에 알려주는 이벤트
        public event Action<SkillData> OnSkillUsed;

        private void Update()
        {
            // 모든 스킬의 쿨타임을 감소시킴
            foreach (var skill in equippedSkills)
            {
                if (cooldownTimers.ContainsKey(skill) && cooldownTimers[skill] > 0f)
                {
                    cooldownTimers[skill] -= Time.deltaTime;
                    if (cooldownTimers[skill] < 0f)
                        cooldownTimers[skill] = 0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                UseSkillByIndex(0);
        }

        // 외부에서 스킬을 사용할 때 호출하는 메서드
        public void UseSkill(SkillData skill)
        {
            if (cooldownTimers.TryGetValue(skill, out float cooldown) && cooldown > 0f)
            {
                Debug.Log($"{skill.skillName}은 아직 {cooldown:F1}초 남았습니다.");
                return;
            }

            // 쿨타임 갱신
            cooldownTimers[skill] = skill.cooldown;

            Fire(skill);
            // 실제 스킬 발동 로직 (지금은 단순 출력만)
            Debug.Log($"{skill.skillName} 발동!");

            // UI 등 다른 시스템에 알림
            OnSkillUsed?.Invoke(skill);
        }

        // 무기 교체 등으로 스킬을 교체할 때 사용
        public void SetSkills(List<SkillData> newSkills)
        {
            equippedSkills = newSkills;
            cooldownTimers.Clear();
            foreach (var skill in equippedSkills)
            {
                cooldownTimers[skill] = 0f;
            }
        }

        // 테스트용: index번째 스킬을 사용 (UI 연결 전용)
        public void UseSkillByIndex(int index)
        {
            if (index >= 0 && index < equippedSkills.Count)
            {
                UseSkill(equippedSkills[index]);
            }
        }

        private void Fire(SkillData skill)
        {
            if (skill.projectilePrefab != null)
            {
                // 발사 방향 = 플레이어 바라보는 방향
                Vector3 direction = transform.forward;

                // 프리팹 생성
                GameObject go = Instantiate(skill.projectilePrefab, transform.position + direction * 1.0f, Quaternion.identity);

                // Projectile 컴포넌트 가져와서 방향과 속도 설정
                var projectile = go.GetComponent<Projectile>();
                if (projectile != null)
                    projectile.Init(direction, skill.speed, skill.projectilePrefab);
            }
            else
            {
                Debug.Log($"{skill.skillName} 은 투사체 없음 (근접 또는 즉시 스킬)");
            }
        }

        public List<SkillData> GetEquippedSkills() => equippedSkills;
    }
}
