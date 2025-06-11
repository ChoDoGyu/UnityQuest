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

                // ����ü �߻�
                GameObject projectile = GameObject.Instantiate(enemy.GetProjectilePrefab(), enemy.GetFirePoint().position, Quaternion.identity);
                Vector3 direction = (enemy.player.position - enemy.GetFirePoint().position).normalized;
                projectile.GetComponent<Rigidbody>().velocity = direction * 10f; // �ӵ� �ϵ��ڵ� �� ���߿� Statȭ ����

                enemy.GetAnimator().SetTrigger("Attack");
            }
        }

        public void ExitState(EnemyController enemy) { }
    }
}
