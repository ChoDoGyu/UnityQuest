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

        /// <summary>
        /// 로딩씬에서 참조할 다음 씬 이름
        /// </summary>
        public static string NextSceneName { get; private set; }

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
        /// 대상 씬으로 이동 요청 → 내부적으로 로딩씬을 먼저 거친다
        /// </summary>
        public void LoadScene(string targetSceneName)
        {
            if (!isLoading)
            {
                isLoading = true;
                NextSceneName = targetSceneName;
                SceneManager.LoadScene(loadingSceneName); // 로딩씬으로 먼저 이동
            }
        }

        /// <summary>
        /// 씬 로드 완료 후 BGM 자동 재생
        /// </summary>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
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
            }
        }
    }
}
