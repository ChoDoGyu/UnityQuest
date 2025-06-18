using UnityEngine;

namespace Main.Scripts.Data
{
    // Unity �����Ϳ��� ������ �� �ֵ��� �޴� �߰�
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObject/Skill/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("�⺻ ����")]
        public string skillName; // ��ų �̸�
        public Sprite icon; // ��ų UI�� ������

        [Header("���� �Ӽ�")]
        public float damage = 10f; // ���ط�
        public float cooldown = 1f; // ��Ÿ�� (�� ����)
        public float range = 10f; // ��ų�� ���� �Ÿ�
        public float speed = 10f; // ����ü �ӵ�

        [Header("����ü ������")]
        public GameObject projectilePrefab; // ȭ��, ���̾ ��

        [Header("��ų Ÿ��")]
        public SkillType skillType; // ��ų �з�
    }

    // � ������ ��ų���� �����ϴ� ������ (�ʿ� �� Ȯ�� ����)
    public enum SkillType
    {
        Arrow,
        Fireball,
        Heal,
        Buff,
        Lightning
    }
}
