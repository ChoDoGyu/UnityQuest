using UnityEngine;

namespace Main.Scripts.Enemy.Boss
{
    public class BossPatternManager : MonoBehaviour
    {
        private BossController boss;
        private int currentStage = 0;

        [Header("Stage Thresholds")]
        [SerializeField] private float stage2Threshold = 0.7f;
        [SerializeField] private float stage3Threshold = 0.4f;

        [Header("Boss Camera Director")]
        [SerializeField] private BossCameraDirector bossCameraDirector;


        /// <summary>
        /// BossController에서 초기화 시 호출
        /// </summary>
        public void Initialize(BossController controller)
        {
            boss = controller;
            currentStage = 1; // 시작 Stage
        }

        /// <summary>
        /// BossController에서 체력 퍼센트 변화 시 호출
        /// </summary>
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
            Debug.Log("[BossPatternManager] Stage 2 진입!");

            // Stage 2 패턴 실행
            boss.ExecutePattern1();
            boss.PlayRoarSound();

            // Boss 전용 카메라 연출 트리거
            bossCameraDirector?.PlayBossIntro();
        }

        private void EnterStage3()
        {
            currentStage = 3;
            Debug.Log("[BossPatternManager] Stage 3 진입!");

            // Stage 3 패턴 실행
            boss.ExecutePattern2();
            boss.PlayRoarSound();

            // Stage 3도 필요하다면 추가 연출 호출
            bossCameraDirector?.PlayBossIntro();
        }
    }
}
