using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    public static class ExpTable
    {
        /// <summary>
        /// ���� �������� ���� �������� �ʿ��� ����ġ�� ��ȯ�մϴ�.
        /// �⺻ ��: 100 * (1.2)^(���� - 1)
        /// </summary>
        public static int GetRequiredExp(int level)
        {
            if (level < 1)
                return 100; // �ּ� ����

            return Mathf.RoundToInt(100f * Mathf.Pow(1.2f, level - 1));
        }
    }
}
