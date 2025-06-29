using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Main.Scripts.Core;

namespace Main.Scripts.SceneManagement
{
    /// <summary>
    /// 씬 전환을 담당하는 전역 관리자
    /// - 로딩씬 → 대상 씬 비동기 로딩
    /// - 씬 로드시 자동 BGM 재생
    /// </summary>
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

            //씬 로드 후 BGM 자동 설정을 위한 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// 씬 전환 요청 (로딩씬 → 대상 씬 비동기 로딩)
        /// </summary>
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

        /// <summary>
        /// 씬이 완전히 로드된 후 자동 호출됨
        /// → 씬 이름에 따라 BGM 자동 재생
        /// </summary>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            string name = scene.name;

            switch (name)
            {
                case "StartScene":
                    AudioManager.Instance?.PlayBGM("StartTheme");
                    break;
                case "FieldScene":
                    AudioManager.Instance?.PlayBGM("FieldTheme");
                    break;
                case "DungeonScene":
                    AudioManager.Instance?.PlayBGM("DungeonTheme");
                    break;
                default:
                    break;
            }
        }
    }
}
