using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class ReturnState : IEnemyState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.GetAnimator().SetBool("IsMoving", true);
            enemy.GetAgent().isStopped = false;
            enemy.GetAgent().SetDestination(enemy.GetSpawnPosition());
        }

        public void UpdateState(EnemyController enemy)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.GetSpawnPosition());
            if (distance < 0.5f)
            {
                enemy.TransitionToState(enemy.idleState);
            }
        }

        public void ExitState(EnemyController enemy)
        {
            enemy.GetAgent().isStopped = true;
        }
    }
}
