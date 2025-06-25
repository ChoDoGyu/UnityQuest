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
            Debug.Log("[BossCameraDirector] Boss Intro 연출 시작!");

            // 1) 카메라 진동
            impulseSource.GenerateImpulse();

            // 2) HUD 숨김
            if (hideHUDDuringIntro)
                GameManager.Instance?.UIManager?.ShowPauseMenu(); // 필요 시 전용 HideHUD 메서드로 변경

            // 3) 느려짐 효과 Coroutine 시작
            StartCoroutine(PlaySlowMotion());
        }

        private IEnumerator PlaySlowMotion()
        {
            Time.timeScale = slowTimeScale;
            yield return new WaitForSecondsRealtime(slowDuration);
            Time.timeScale = 1f;

            // HUD 복구
            if (hideHUDDuringIntro)
                GameManager.Instance?.UIManager?.HidePauseMenu(); // 필요 시 전용 ShowHUD 메서드로 변경

            Debug.Log("[BossCameraDirector] Boss Intro 연출 종료!");
        }
    }
}