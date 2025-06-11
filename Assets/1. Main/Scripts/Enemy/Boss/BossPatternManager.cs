using UnityEngine;

namespace Main.Scripts.Enemy.Boss
{
    public class BossPatternManager : MonoBehaviour
    {
        private BossController boss;
        private int currentStage = 0;

        // ü�� �ۼ�Ʈ ���� (��: 70%, 40% ���� �� ���� ��ȯ)
        [SerializeField] private float stage2Threshold = 0.7f;
        [SerializeField] private float stage3Threshold = 0.4f;

        // �ʱ�ȭ
        public void Initialize(BossController controller)
        {
            boss = controller;
            currentStage = 1; // ������ Stage 1
        }

        // ü�� �ۼ�Ʈ�� ���� ���� ����
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
            Debug.Log("[BossPatternManager] Stage 2 ����");
            boss.ExecutePattern1(); // ��: ���� ���� ����
            boss.PlayRoarSound();
        }

        private void EnterStage3()
        {
            currentStage = 3;
            Debug.Log("[BossPatternManager] Stage 3 ����");
            boss.ExecutePattern2(); // ��: ����ȭ ����
            boss.PlayRoarSound();
        }
    }
}
