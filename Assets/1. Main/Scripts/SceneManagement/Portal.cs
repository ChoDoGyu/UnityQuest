using UnityEngine;

namespace Main.Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [Header("��ǥ �� �̸�")]
        [SerializeField] private string targetSceneName = "DungeonScene";

        [Header("�÷��̾� �±�")]
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                Debug.Log($"[Portal] Player triggered portal �� Load {targetSceneName} with loading screen");
                SceneLoader.Instance.LoadScene(targetSceneName);
            }
        }

        private void Reset()
        {
            // �ڵ����� Collider Trigger�� ����
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }
    }
}