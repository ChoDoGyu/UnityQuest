using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Main.Scripts.Core;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// 보스 컷신, NPC 컷신 등의 공통 연출 처리를 담당하는 추상 베이스 클래스입니다.
    /// 암전(Fade), Timeline 실행, HUD 숨김/복구, BGM 전환 기능을 제공합니다.
    /// </summary>
    public class CutsceneControllerBase : MonoBehaviour
    {
        [Header("공통 연출 요소")]
        [Tooltip("전체 화면을 어둡게/밝게 조절하는 CanvasGroup (검정 이미지 포함)")]
        [SerializeField] protected CanvasGroup fadeCanvasGroup;


        [Tooltip("Timeline 재생용 PlayableDirector")]
        [SerializeField] protected PlayableDirector timelineDirector;

        [Tooltip("암전 또는 밝아짐에 걸리는 시간 (초)")]
        [SerializeField] protected float fadeDuration = 1f;

        protected virtual void Awake()
        {
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = 0f; // 처음엔 밝은 화면 상태
        }

        /// <summary>
        /// 컷신을 시작합니다. (BGM은 선택적으로 설정)
        /// </summary>
        /// <param name="bgmName">재생할 BGM 이름 (null이면 무시)</param>
        protected IEnumerator PlayCutscene(string bgmName = null)
        {
            // 1. HUD 숨김
            GameManager.Instance.UIManager.HideHUD();

            // 2. 암전 (Fade to black)
            yield return Fade(0f, 1f, fadeDuration);

            // 3. BGM 전환
            if (!string.IsNullOrEmpty(bgmName))
                GameManager.Instance.Audio?.PlayBGM(bgmName);

            // 4. Timeline 실행
            timelineDirector?.Play();

            // 5. Timeline이 재생되는 동안 대기
            if (timelineDirector != null)
                yield return new WaitForSeconds((float)timelineDirector.duration);

            // 6. 밝아짐 (Fade to clear)
            yield return Fade(1f, 0f, fadeDuration);

            // 7. HUD 복구
            GameManager.Instance.UIManager.ShowHUD();
        }


        /// <summary>
        /// CanvasGroup을 이용해 화면을 서서히 어둡게 또는 밝게 만듭니다.
        /// </summary>
        private IEnumerator Fade(float from, float to, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                float alpha = Mathf.Lerp(from, to, time / duration);
                if (fadeCanvasGroup != null)
                    fadeCanvasGroup.alpha = alpha;

                time += Time.deltaTime;
                yield return null;
            }

            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = to;
        }
    }
}