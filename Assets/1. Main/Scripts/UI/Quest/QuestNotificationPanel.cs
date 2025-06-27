using System.Collections;
using UnityEngine;
using TMPro;

namespace Main.Scripts.UI.Quest
{
    /// <summary>
    /// ����Ʈ �˸� �޽����� ȭ�鿡 ���� �ڵ����� ������� ó���ϴ� ������Ʈ�Դϴ�.
    /// </summary>
    public class QuestNotificationPanel : MonoBehaviour
    {
        [Header("UI ���")]
        [SerializeField] private TMP_Text messageText;       // �޽����� ǥ���� �ؽ�Ʈ
        [SerializeField] private CanvasGroup canvasGroup;    // ���� ���� �����

        private Coroutine currentRoutine;

        /// <summary>
        /// �ܺο��� ȣ���ϴ� �˸� ǥ�� �޼����Դϴ�.
        /// </summary>
        public void Show(string message)
        {
            messageText.text = message;

            // ���� �˸��� ���� ������� ���̸� �ߴ�
            if (currentRoutine != null)
                StopCoroutine(currentRoutine);

            // ���ο� �˸� ǥ�� �ڷ�ƾ ����
            currentRoutine = StartCoroutine(FadeRoutine());
        }

        /// <summary>
        /// �˸� ǥ�� �� ����� ó���� �ڷ�ƾ�Դϴ�.
        /// </summary>
        private IEnumerator FadeRoutine()
        {
            yield return Fade(0f, 1f, 0.3f);        // Fade In
            yield return new WaitForSeconds(2f);   // ���� �ð� ����
            yield return Fade(1f, 0f, 0.3f);        // Fade Out
        }

        /// <summary>
        /// CanvasGroup�� ���ĸ� ������ �����ϴ� �Լ��Դϴ�.
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