using System.Collections;
using UnityEngine;
using TMPro;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// 퀘스트 알림 메시지를 화면에 띄우고 자동으로 사라지게 처리하는 컴포넌트입니다.
    /// </summary>
    public class QuestNotificationPanel : MonoBehaviour
    {
        [Header("UI 요소")]
        [SerializeField] private TMP_Text messageText;       // 메시지를 표시할 텍스트
        [SerializeField] private CanvasGroup canvasGroup;    // 알파 투명도 제어용

        private Coroutine currentRoutine;

        /// <summary>
        /// 외부에서 호출하는 알림 표시 메서드입니다.
        /// </summary>
        public void Show(string message)
        {
            messageText.text = message;

            // 이전 알림이 아직 사라지는 중이면 중단
            if (currentRoutine != null)
                StopCoroutine(currentRoutine);

            // 새로운 알림 표시 코루틴 시작
            currentRoutine = StartCoroutine(FadeRoutine());
        }

        /// <summary>
        /// 알림 표시 및 사라짐 처리용 코루틴입니다.
        /// </summary>
        private IEnumerator FadeRoutine()
        {
            yield return Fade(0f, 1f, 0.3f);        // Fade In
            yield return new WaitForSeconds(2f);   // 일정 시간 유지
            yield return Fade(1f, 0f, 0.3f);        // Fade Out
        }

        /// <summary>
        /// CanvasGroup의 알파를 서서히 변경하는 함수입니다.
        /// </summary>
        private IEnumerator Fade(float from, float to, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = to;
        }
    }
}