using Main.Scripts.Core;
using System.Collections.Generic;

namespace Main.Scripts.Player
{
    // 1. ���� ������ �����ϴ� enum(����ǥ)
    public enum StatType
    {
        HP,          // ü��
        Attack,      // ���ݷ�
        SkillPower,  // ��ų ����
        Defense,     // ����
        Critical,    // ġ��Ÿ Ȯ��
        Stamina,     // ���¹̳�(���)
        Exp,         // ����ġ
        Level        // ����
    }

    // 2. ���� �÷��̾��� ��� ������ �����ϴ� Ŭ����
    public class PlayerStat
    {
        // (1) StatType���� ���� �����ϴ� ����(��ųʸ�)
        private Dictionary<StatType, float> stats = new();

        // (2) ���� ���� �д� �Լ�
        public float GetStat(StatType type)
        {
            return stats.TryGetValue(type, out float value) ? value : 0f;
        }

        // (3) ���� ���� ���� �����ϴ� �Լ�
        public void SetStat(StatType type, float value)
        {
            stats[type] = value;
        }

        // (4) ���� ���� ����(���ϱ�/����)�ϴ� �Լ�
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

        // (5) ó�� ���� ��, �⺻�� ���� (���ϸ� ���ϴ� ������ �ٲ㵵 ��)
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
