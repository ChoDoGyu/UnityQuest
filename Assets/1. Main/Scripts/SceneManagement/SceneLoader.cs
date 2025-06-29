using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Main.Scripts.Core;

namespace Main.Scripts.SceneManagement
{
    /// <summary>
    /// �� ��ȯ�� ����ϴ� ���� ������
    /// - �ε��� �� ��� �� �񵿱� �ε�
    /// - �� �ε�� �ڵ� BGM ���
    /// </summary>
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

            //�� �ε� �� BGM �ڵ� ������ ���� �̺�Ʈ ���
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// �� ��ȯ ��û (�ε��� �� ��� �� �񵿱� �ε�)
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

        /// <summary>
        /// ���� ������ �ε�� �� �ڵ� ȣ���
        /// �� �� �̸��� ���� BGM �ڵ� ���
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
