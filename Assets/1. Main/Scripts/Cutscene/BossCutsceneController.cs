using System.Collections;
using UnityEngine;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// 보스 등장 연출 전용 컷신 컨트롤러입니다.
    /// 보스를 등장시키고 공통 컷신 연출을 실행합니다.
    /// </summary>
    public class BossCutsceneController : CutsceneControllerBase
    {
        [Header("보스 등장 연출 설정")]
        [Tooltip("보스 오브젝트 (연출 도중 활성화됨)")]
        [SerializeField] private GameObject bossObject;

        [Tooltip("컷신 시작 시 재생할 BGM 이름")]
        [SerializeField] private string bossBGM = "BossIntro";

        /// <summary>
        /// 보스 컷신을 시작합니다. (트리거나 코드에서 호출)
        /// </summary>
        public void StartBossCutscene()
        {
            StartCoroutine(BossSequence());
        }

        /// <summary>
        /// 보스 활성화 후 공통 컷신 루틴 실행
        /// </summary>
        private IEnumerator BossSequence()
        {
            // 보스를 먼저 활성화
            if (bossObject != null)
                bossObject.SetActive(true);

            // 공통 컷신 루틴 실행 (암전, Timeline, BGM 등)
            yield return PlayCutscene(bossBGM);
        }
    }
}