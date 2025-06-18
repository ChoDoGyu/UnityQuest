using UnityEngine;

namespace Main.Scripts.Data
{
    // Unity 에디터에서 생성할 수 있도록 메뉴 추가
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObject/Skill/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("기본 정보")]
        public string skillName; // 스킬 이름
        public Sprite icon; // 스킬 UI용 아이콘

        [Header("전투 속성")]
        public float damage = 10f; // 피해량
        public float cooldown = 1f; // 쿨타임 (초 단위)
        public float range = 10f; // 스킬이 닿을 거리
        public float speed = 10f; // 투사체 속도

        [Header("투사체 프리팹")]
        public GameObject projectilePrefab; // 화살, 파이어볼 등

        [Header("스킬 타입")]
        public SkillType skillType; // 스킬 분류
    }

    // 어떤 종류의 스킬인지 구분하는 열거형 (필요 시 확장 가능)
    public enum SkillType
    {
        Arrow,
        Fireball,
        Heal,
        Buff,
        Lightning
    }
}
