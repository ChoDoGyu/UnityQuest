using UnityEngine;
using Main.Scripts.Interfaces;
using System.Collections;
using Main.Scripts.ItemSystem;

namespace Main.Scripts.Enemy
{
    public class DeadState : IEnemyState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.GetAnimator().SetTrigger("Die");
            enemy.GetAgent().isStopped = true;

            // 사망 처리 코루틴 시작
            enemy.StartCoroutine(DelayedDeath(enemy, 2f)); // ← 2초 후 제거
        }

        private IEnumerator DelayedDeath(EnemyController enemy, float delay)
        {
            yield return new WaitForSeconds(delay);

            // 드롭 처리 추가
            var dropHandler = enemy.GetComponent<EnemyDropHandler>();
            if (dropHandler != null)
            {
                dropHandler.DropItems();
            }

            //풀 반환 처리
            if (enemy.OnReturnedToPool != null)
            {
                enemy.OnReturnedToPool.Invoke();
            }
            else
            {
                // 풀 사용 안할 때만 Destroy
                GameObject.Destroy(enemy.gameObject);
            }
        }

        public void UpdateState(EnemyController enemy) { }

        public void ExitState(EnemyController enemy) { }
    }
}
