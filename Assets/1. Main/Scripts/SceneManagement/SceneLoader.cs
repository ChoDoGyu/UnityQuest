using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Main.Scripts.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [Header("Optional: �ε��� �̸� (Inspector���� ���� ����)")]
        [SerializeField] private string loadingSceneName = "LoadingScene";

        private bool isLoading = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string targetSceneName)
        {
            if (!isLoading)
            {
                StartCoroutine(LoadSceneWithLoadingRoutine(targetSceneName));
            }
        }

        private IEnumerator LoadSceneWithLoadingRoutine(string targetSceneName)
        {
            isLoading = true;

            // 1) �ε��� ���� ��� �ε�
            SceneManager.LoadScene(loadingSceneName);
            yield return null;

            // 2) ��� �� �񵿱� �ε�
            AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);

            while (!operation.isDone)
            {
                // TODO: �ε� ProgressBar ������Ʈ ����
                yield return null;
            }

            isLoading = false;
        }
    }
}
