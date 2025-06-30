using UnityEngine;

namespace Main.Scripts.Cutscene
{
    /// <summary>
    /// �÷��̾ ������ �Ա��� �������� �� ���� �ƽ��� �ڵ����� �����ϴ� Ʈ�����Դϴ�.
    /// </summary>
    public class BossEntranceTrigger : MonoBehaviour
    {
        [Tooltip("������ �ƽ� ��Ʈ�ѷ�")]
        [SerializeField] private BossCutsceneController cutsceneController;

        private bool isPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            // �÷��̾ ��� �� �۵��ϵ��� �±� Ȯ��
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