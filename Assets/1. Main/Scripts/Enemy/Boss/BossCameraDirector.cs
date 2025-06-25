using UnityEngine;
using Cinemachine;
using Main.Scripts.Core;
using System.Collections;

namespace Main.Scripts.Enemy.Boss
{
    public class BossCameraDirector : MonoBehaviour
    {
        [Header("Camera Shake")]
        [SerializeField] private CinemachineImpulseSource impulseSource;

        [Header("Slow Motion")]
        [SerializeField] private float slowTimeScale = 0.3f;
        [SerializeField] private float slowDuration = 1.5f;

        [Header("HUD Control")]
        [SerializeField] private bool hideHUDDuringIntro = true;

        public void PlayBossIntro()
        {
            Debug.Log("[BossCameraDirector] Boss Intro ���� ����!");

            // 1) ī�޶� ����
            impulseSource.GenerateImpulse();

            // 2) HUD ����
            if (hideHUDDuringIntro)
                GameManager.Instance?.UIManager?.ShowPauseMenu(); // �ʿ� �� ���� HideHUD �޼���� ����

            // 3) ������ ȿ�� Coroutine ����
            StartCoroutine(PlaySlowMotion());
        }

        private IEnumerator PlaySlowMotion()
        {
            Time.timeScale = slowTimeScale;
            yield return new WaitForSecondsRealtime(slowDuration);
            Time.timeScale = 1f;

            // HUD ����
            if (hideHUDDuringIntro)
                GameManager.Instance?.UIManager?.HidePauseMenu(); // �ʿ� �� ���� ShowHUD �޼���� ����

            Debug.Log("[BossCameraDirector] Boss Intro ���� ����!");
        }
    }
}