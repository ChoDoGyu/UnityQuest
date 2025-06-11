using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class RangedAttackState : IEnemyState
    {
        private float lastAttackTime = -Mathf.Infinity;

        public void EnterState(EnemyController baseEnemy)
        {
            baseEnemy.GetAgent().isStopped = true;
            baseEnemy.GetAnimator().SetTrigger("Attack");
        }

        public void UpdateState(EnemyController baseEnemy)
        {
            if (!(baseEnemy is RangedEnemyController enemy)) return;

            float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distance > enemy.GetStat().attackRange)
            {
                enemy.TransitionToState(enemy.chaseState);
                return;
            }

            float cooldown = enemy.GetStat().attackCooldown;
            if (Time.time - lastAttackTime > cooldown)
            {
                lastAttackTime = Time.time;

                // 투사체 발사
                GameObject projectile = GameObject.Instantiate(enemy.GetProjectilePrefab(), enemy.GetFirePoint().position, Quaternion.identity);
                Vector3 direction = (enemy.player.position - enemy.GetFirePoint().position).normalized;
                projectile.GetComponent<Rigidbody>().velocity = direction * 10f; // 속도 하드코딩 → 나중에 Stat화 가능

                enemy.GetAnimator().SetTrigger("Attack");
            }
        }

        public void ExitState(EnemyController enemy) { }
    }
}
