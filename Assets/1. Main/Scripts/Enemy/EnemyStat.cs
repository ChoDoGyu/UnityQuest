using UnityEngine;

namespace Main.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStat", menuName = "Game/EnemyStat")]
    public class EnemyStat : ScriptableObject
    {
        public float maxHP = 100f;
        public float moveSpeed = 3.5f;
        public float attackRange = 2f;
        public float detectRange = 10f;
        public float chaseLimitRange = 15f;
        public float attackCooldown = 2f;
        public float stunDuration = 1f;
    }
}
