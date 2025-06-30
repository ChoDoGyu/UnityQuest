using UnityEngine;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// 플레이어가 보스방 입구에 진입했을 때 보스 컷신을 자동으로 시작하는 트리거입니다.
    /// </summary>
    public class BossEntranceTrigger : MonoBehaviour
    {
        [Tooltip("연결할 컷신 컨트롤러")]
        [SerializeField] private BossCutsceneController cutsceneController;

        private bool isPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            // 플레이어만 통과 시 작동하도록 태그 확인
            if (isPlayed) return;

            if (other.CompareTag("Player"))
            {
                isPlayed = true;
                if (cutsceneController != null)
                {
                    cutsceneController.StartBossCutscene();
                }
            }
        }
    }
}