using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class AttackState : IEnemyState
    {
        private float lastAttackTime = -Mathf.Infinity;

        public void EnterState(EnemyController enemy)
        {
            enemy.GetAgent().isStopped = true;
            enemy.GetAnimator().SetTrigger("Attack");
            lastAttackTime = Time.time;
        }

        public void UpdateState(EnemyController enemy)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distance > enemy.GetStat().attackRange)
            {
                enemy.TransitionToState(enemy.chaseState);
                return;
            }

            float cooldown = enemy.GetStat().attackCooldown;
            if (Time.time - lastAttackTime > cooldown)
            {
                enemy.GetAnimator().SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }

        public void ExitState(EnemyController enemy) { }
    }
}
