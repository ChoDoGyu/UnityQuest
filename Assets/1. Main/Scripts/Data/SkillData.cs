using UnityEngine;

namespace Main.Scripts.Data
{
    // Unity �����Ϳ��� ������ �� �ֵ��� �޴� �߰�
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObject/Skill/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("�⺻ ����")]
        public string skillName;
        public Sprite icon;
        public float cooldown;

        [Header("��ų �Ķ����")]
        public GameObject projectilePrefab;
        public float damage;
        public float speed;

        [Header("���� ����")]
        public AudioClip sfx;
    }
}
