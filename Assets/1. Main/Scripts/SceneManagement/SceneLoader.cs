using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Main.Scripts.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

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

        public void LoadScene(string sceneName)
        {
            if (!isLoading)
            {
                StartCoroutine(LoadSceneAsync(sceneName));
            }
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            isLoading = true;

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }

            isLoading = false;
        }
    }
}
