using UnityEngine;

namespace Main.Scripts.Enemy.Boss
{
    public class BossPatternManager : MonoBehaviour
    {
        private BossController boss;
        private int currentStage = 0;

        // 체력 퍼센트 기준 (예: 70%, 40% 이하 시 패턴 전환)
        [SerializeField] private float stage2Threshold = 0.7f;
        [SerializeField] private float stage3Threshold = 0.4f;

        // 초기화
        public void Initialize(BossController controller)
        {
            boss = controller;
            currentStage = 1; // 시작은 Stage 1
        }

        // 체력 퍼센트로 다음 패턴 결정
        public void EvaluateNextPattern(float hpRatio)
        {
            if (currentStage == 1 && hpRatio < stage2Threshold)
            {
                EnterStage2();
            }
            else if (currentStage == 2 && hpRatio < stage3Threshold)
            {
                EnterStage3();
            }
        }

        private void EnterStage2()
        {
            currentStage = 2;
            Debug.Log("[BossPatternManager] Stage 2 진입");
            boss.ExecutePattern1(); // 예: 대형 범위 공격
            boss.PlayRoarSound();
        }

        private void EnterStage3()
        {
            currentStage = 3;
            Debug.Log("[BossPatternManager] Stage 3 진입");
            boss.ExecutePattern2(); // 예: 광폭화 패턴
            boss.PlayRoarSound();
        }
    }
}
