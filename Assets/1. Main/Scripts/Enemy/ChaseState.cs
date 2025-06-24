using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class ChaseState : IEnemyState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.GetAnimator().SetBool("IsMoving", true);
            enemy.GetAgent().isStopped = false;
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.player == null)
            {
                enemy.TransitionToState(enemy.returnState);
                return;
            }

            float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distance < enemy.GetStat().attackRange)
            {
                enemy.TransitionToState(enemy.attackState);
            }
            else if (distance > enemy.GetStat().chaseLimitRange)
            {
                enemy.TransitionToState(enemy.returnState);
            }
            else
            {
                enemy.GetAgent().SetDestination(enemy.player.position);
            }
        }

        public void ExitState(EnemyController enemy)
        {
            enemy.GetAgent().isStopped = true;


        }
    }
}
