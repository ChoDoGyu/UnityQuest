using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    public static class ExpTable
    {
        /// <summary>
        /// 현재 레벨에서 다음 레벨까지 필요한 경험치를 반환합니다.
        /// 기본 식: 100 * (1.2)^(레벨 - 1)
        /// </summary>
        public static int GetRequiredExp(int level)
        {
            if (level < 1)
                return 100; // 최소 보정

            return Mathf.RoundToInt(100f * Mathf.Pow(1.2f, level - 1));
        }
    }
}
