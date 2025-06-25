using UnityEngine;
using Main.Scripts.Core;
using Main.Scripts.Enemy;

namespace Main.Scripts.Enemy.Boss
{
    public class BossController : EnemyController
    {
        [Header("Boss FX / SFX Settings")]
        [SerializeField] private string roarSFXName = "BossRoar";
        [SerializeField] private string pattern1FXName = "FireNova";
        [SerializeField] private string pattern2FXName = "DarkBlast";

        [Header("Boss Pattern Manager")]
        [SerializeField] private BossPatternManager patternManager;

        private float maxHP;
        private float lastHPPercentage = 1f;

        protected override void Start()
        {
            base.Start();

            if (patternManager != null)
                patternManager.Initialize(this);

            maxHP = GetStat().maxHP;
        }


        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            float currentRatio = GetCurrentHP() / maxHP;

            // 체력 퍼센트 변화에 따른 패턴 평가
            if (patternManager != null && currentRatio < lastHPPercentage)
            {
                patternManager.EvaluateNextPattern(currentRatio);
                lastHPPercentage = currentRatio;
            }
        }

        public void PlayRoarSound()
        {
            GameManager.Instance?.Audio?.PlaySFX(roarSFXName);
        }

        public void PlayEffect(string fxName)
        {
            GameManager.Instance?.FX?.PlayEffect(fxName, transform.position);
        }

        // 패턴에서 호출할 수 있는 액션
        public void ExecutePattern1()
        {
            PlayEffect(pattern1FXName);
        }

        public void ExecutePattern2()
        {
            PlayEffect(pattern2FXName);
        }

        // EnemyController는 currentHP가 private이므로 override 필요
        public float GetCurrentHP()
        {
            var hpField = typeof(EnemyController).GetField("currentHP", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return hpField != null ? (float)hpField.GetValue(this) : 0f;
        }
    }
}