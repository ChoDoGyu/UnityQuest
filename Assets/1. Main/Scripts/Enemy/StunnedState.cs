using UnityEngine;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Enemy
{
    public class StunnedState : IEnemyState
    {
        private float enterTime;

        public void EnterState(EnemyController enemy)
        {
            enemy.GetAnimator().SetTrigger("Hit");
            enemy.GetAgent().isStopped = true;
            enterTime = Time.time;
        }

        public void UpdateState(EnemyController enemy)
        {
            float stunDuration = enemy.GetStat().stunDuration;

            if (Time.time - enterTime >= stunDuration)
            {
                enemy.RecoverFromStun();
            }
        }

        public void ExitState(EnemyController enemy)
        {
            enemy.GetAgent().isStopped = false;
        }
    }
}
