using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Main.Scripts.Core;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// ���� �ƽ�, NPC �ƽ� ���� ���� ���� ó���� ����ϴ� �߻� ���̽� Ŭ�����Դϴ�.
    /// ����(Fade), Timeline ����, HUD ����/����, BGM ��ȯ ����� �����մϴ�.
    /// </summary>
    public class CutsceneControllerBase : MonoBehaviour
    {
        [Header("���� ���� ���")]
        [Tooltip("��ü ȭ���� ��Ӱ�/��� �����ϴ� CanvasGroup (���� �̹��� ����)")]
        [SerializeField] protected CanvasGroup fadeCanvasGroup;


        [Tooltip("Timeline ����� PlayableDirector")]
        [SerializeField] protected PlayableDirector timelineDirector;

        [Tooltip("���� �Ǵ� ������� �ɸ��� �ð� (��)")]
        [SerializeField] protected float fadeDuration = 1f;

        protected virtual void Awake()
        {
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = 0f; // ó���� ���� ȭ�� ����
        }

        /// <summary>
        /// �ƽ��� �����մϴ�. (BGM�� ���������� ����)
        /// </summary>
        /// <param name="bgmName">����� BGM �̸� (null�̸� ����)</param>
        protected IEnumerator PlayCutscene(string bgmName = null)
        {
            // 1. HUD ����
            GameManager.Instance.UIManager.HideHUD();

            // 2. ���� (Fade to black)
            yield return Fade(0f, 1f, fadeDuration);

            // 3. BGM ��ȯ
            if (!string.IsNullOrEmpty(bgmName))
                GameManager.Instance.Audio?.PlayBGM(bgmName);

            // 4. Timeline ����
            timelineDirector?.Play();

            // 5. Timeline�� ����Ǵ� ���� ���
            if (timelineDirector != null)
                yield return new WaitForSeconds((float)timelineDirector.duration);

            // 6. ����� (Fade to clear)
            yield return Fade(1f, 0f, fadeDuration);

            // 7. HUD ����
            GameManager.Instance.UIManager.ShowHUD();
        }


        /// <summary>
        /// CanvasGroup�� �̿��� ȭ���� ������ ��Ӱ� �Ǵ� ��� ����ϴ�.
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