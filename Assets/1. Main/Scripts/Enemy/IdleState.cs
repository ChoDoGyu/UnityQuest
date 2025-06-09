using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class IdleState : IEnemyState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.GetAnimator().SetBool("IsMoving", false);
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.player == null) return;

            float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distance < enemy.GetStat().detectRange)
            {
                enemy.TransitionToState(enemy.chaseState);
            }
        }

        public void ExitState(EnemyController enemy) { }
    }
}
