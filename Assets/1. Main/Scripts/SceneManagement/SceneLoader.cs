using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Main.Scripts.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [Header("Optional: 로딩씬 이름 (Inspector에서 설정 가능)")]
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

            // 1) 로딩씬 먼저 즉시 로드
            SceneManager.LoadScene(loadingSceneName);
            yield return null;

            // 2) 대상 씬 비동기 로드
            AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);

            while (!operation.isDone)
            {
                // TODO: 로딩 ProgressBar 업데이트 가능
                yield return null;
            }

            isLoading = false;
        }
    }
}
