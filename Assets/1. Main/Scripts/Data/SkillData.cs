using UnityEngine;

namespace Main.Scripts.Data
{
    // Unity 에디터에서 생성할 수 있도록 메뉴 추가
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObject/Skill/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("기본 정보")]
        public string skillName;
        public Sprite icon;
        public float cooldown;

        [Header("스킬 파라미터")]
        public GameObject projectilePrefab;
        public float damage;
        public float speed;

        [Header("사운드 설정")]
        public AudioClip sfx;
    }
}
