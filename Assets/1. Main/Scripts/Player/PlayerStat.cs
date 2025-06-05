using Main.Scripts.Core;
using System.Collections.Generic;

namespace Main.Scripts.Player
{
    // 1. 스탯 종류를 정의하는 enum(종류표)
    public enum StatType
    {
        HP,          // 체력
        Attack,      // 공격력
        SkillPower,  // 스킬 위력
        Defense,     // 방어력
        Critical,    // 치명타 확률
        Stamina,     // 스태미나(기력)
        Exp,         // 경험치
        Level        // 레벨
    }

    // 2. 실제 플레이어의 모든 스탯을 관리하는 클래스
    public class PlayerStat
    {
        // (1) StatType별로 값을 저장하는 사전(딕셔너리)
        private Dictionary<StatType, float> stats = new();

        // (2) 스탯 값을 읽는 함수
        public float GetStat(StatType type)
        {
            return stats.TryGetValue(type, out float value) ? value : 0f;
        }

        // (3) 스탯 값을 직접 설정하는 함수
        public void SetStat(StatType type, float value)
        {
            stats[type] = value;
        }

        // (4) 스탯 값을 증감(더하기/빼기)하는 함수
        public void AddStat(StatType type, float amount)
        {
            if (!stats.ContainsKey(type)) stats[type] = 0;
            stats[type] += amount;
        }

        public void ApplyLevelUpBonus()
        {
            AddStat(StatType.HP, 10f);
            AddStat(StatType.Attack, 2f);
            AddStat(StatType.Stamina, 5f);
        }

        // (5) 처음 생성 시, 기본값 세팅 (원하면 원하는 값으로 바꿔도 됨)
        public PlayerStat()
        {
            stats[StatType.HP] = 100f;
            stats[StatType.Stamina] = 50f;
            stats[StatType.Attack] = 10f;
            stats[StatType.SkillPower] = 5f;
            stats[StatType.Defense] = 2f;
            stats[StatType.Critical] = 0.05f;
            stats[StatType.Exp] = 0f;
            stats[StatType.Level] = 1f;
        }
    }
}
