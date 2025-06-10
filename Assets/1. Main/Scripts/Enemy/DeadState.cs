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

            // 추후 오브젝트 풀링을 고려해 SetActive 방식으로도 대체 가능
            GameObject.Destroy(enemy.gameObject);
        }

        public void UpdateState(EnemyController enemy) { }

        public void ExitState(EnemyController enemy) { }
    }
}
