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

        /// <summary>
        /// �ε������� ������ ���� �� �̸�
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

            //�� �ε� �� BGM �ڵ� ������ ���� �̺�Ʈ ���
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// ��� ������ �̵� ��û �� ���������� �ε����� ���� ��ģ��
        /// </summary>
        public void LoadScene(string targetSceneName)
        {
            if (!isLoading)
            {
                isLoading = true;
                NextSceneName = targetSceneName;
                SceneManager.LoadScene(loadingSceneName); // �ε������� ���� �̵�
            }
        }

        /// <summary>
        /// �� �ε� �Ϸ� �� BGM �ڵ� ���
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
