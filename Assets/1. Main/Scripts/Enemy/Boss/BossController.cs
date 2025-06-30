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

        private bool isActivated = false;

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

            // ü�� �ۼ�Ʈ ��ȭ�� ���� ���� ��
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

        // ���Ͽ��� ȣ���� �� �ִ� �׼�
        public void ExecutePattern1()
        {
            PlayEffect(pattern1FXName);
        }

        public void ExecutePattern2()
        {
            PlayEffect(pattern2FXName);
        }

        // EnemyController�� currentHP�� private�̹Ƿ� override �ʿ�
        public float GetCurrentHP()
        {
            var hpField = typeof(EnemyController).GetField("currentHP", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return hpField != null ? (float)hpField.GetValue(this) : 0f;
        }

        /// <summary>
        /// �ƽ� ���� ȣ��Ǿ� ���� ������ �����մϴ�.
        /// </summary>
        public void Activate()
        {
            if (isActivated) return;
            isActivated = true;

            if (patternManager != null)
                patternManager.enabled = true;

            // ����: ���� �ִϸ��̼� Ʈ����
            var anim = GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("StartBattle");
        }
    }
}