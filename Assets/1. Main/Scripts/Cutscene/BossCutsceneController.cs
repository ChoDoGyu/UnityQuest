using System.Collections;
using UnityEngine;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// ���� ���� ���� ���� �ƽ� ��Ʈ�ѷ��Դϴ�.
    /// ������ �����Ű�� ���� �ƽ� ������ �����մϴ�.
    /// </summary>
    public class BossCutsceneController : CutsceneControllerBase
    {
        [Header("���� ���� ���� ����")]
        [Tooltip("���� ������Ʈ (���� ���� Ȱ��ȭ��)")]
        [SerializeField] private GameObject bossObject;

        [Tooltip("�ƽ� ���� �� ����� BGM �̸�")]
        [SerializeField] private string bossBGM = "BossIntro";

        /// <summary>
        /// ���� �ƽ��� �����մϴ�. (Ʈ���ų� �ڵ忡�� ȣ��)
        /// </summary>
        public void StartBossCutscene()
        {
            StartCoroutine(BossSequence());
        }

        /// <summary>
        /// ���� Ȱ��ȭ �� ���� �ƽ� ��ƾ ����
        /// </summary>
        private IEnumerator BossSequence()
        {
            // ������ ���� Ȱ��ȭ
            if (bossObject != null)
                bossObject.SetActive(true);

            // ���� �ƽ� ��ƾ ���� (����, Timeline, BGM ��)
            yield return PlayCutscene(bossBGM);
        }
    }
}