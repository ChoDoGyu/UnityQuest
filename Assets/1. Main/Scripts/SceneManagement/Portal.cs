using UnityEngine;

namespace Main.Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [Header("목표 씬 이름")]
        [SerializeField] private string targetSceneName = "DungeonScene";

        [Header("플레이어 태그")]
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                Debug.Log($"[Portal] Player triggered portal → Load {targetSceneName} with loading screen");
                SceneLoader.Instance.LoadScene(targetSceneName);
            }
        }

        private void Reset()
        {
            // 자동으로 Collider Trigger로 설정
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }
    }
}