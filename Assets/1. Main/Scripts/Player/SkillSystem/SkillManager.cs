using System;
using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Combat;
using Main.Scripts.Data;

namespace Main.Scripts.Player.SkillSystem
{
    public class SkillManager : MonoBehaviour
    {
        [Tooltip("�÷��̾ ���� ���� ���� ��ų��")]
        [SerializeField] private List<SkillData> equippedSkills = new();

        // �� ��ų�� ���� ��Ÿ���� �����ϴ� ��ųʸ�
        private Dictionary<SkillData, float> cooldownTimers = new();

        // ��ų�� ���Ǿ��� �� UI �� �ٸ� �ý��ۿ� �˷��ִ� �̺�Ʈ
        public event Action<SkillData> OnSkillUsed;

        private void Update()
        {
            // ��� ��ų�� ��Ÿ���� ���ҽ�Ŵ
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

        // �ܺο��� ��ų�� ����� �� ȣ���ϴ� �޼���
        public void UseSkill(SkillData skill)
        {
            if (cooldownTimers.TryGetValue(skill, out float cooldown) && cooldown > 0f)
            {
                Debug.Log($"{skill.skillName}�� ���� {cooldown:F1}�� ���ҽ��ϴ�.");
                return;
            }

            // ��Ÿ�� ����
            cooldownTimers[skill] = skill.cooldown;

            Fire(skill);
            // ���� ��ų �ߵ� ���� (������ �ܼ� ��¸�)
            Debug.Log($"{skill.skillName} �ߵ�!");

            // UI �� �ٸ� �ý��ۿ� �˸�
            OnSkillUsed?.Invoke(skill);
        }

        // ���� ��ü ������ ��ų�� ��ü�� �� ���
        public void SetSkills(List<SkillData> newSkills)
        {
            equippedSkills = newSkills;
            cooldownTimers.Clear();
            foreach (var skill in equippedSkills)
            {
                cooldownTimers[skill] = 0f;
            }
        }

        // �׽�Ʈ��: index��° ��ų�� ��� (UI ���� ����)
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
                // �߻� ���� = �÷��̾� �ٶ󺸴� ����
                Vector3 direction = transform.forward;

                // ������ ����
                GameObject go = Instantiate(skill.projectilePrefab, transform.position + direction * 1.0f, Quaternion.identity);

                // Projectile ������Ʈ �����ͼ� ����� �ӵ� ����
                var projectile = go.GetComponent<Projectile>();
                if (projectile != null)
                    projectile.Init(direction, skill.speed, skill.projectilePrefab);
            }
            else
            {
                Debug.Log($"{skill.skillName} �� ����ü ���� (���� �Ǵ� ��� ��ų)");
            }
        }

        public List<SkillData> GetEquippedSkills() => equippedSkills;
    }
}
