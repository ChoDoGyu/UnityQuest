using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Main.Scripts.SceneManagement
{
    public class LoadingSceneController : MonoBehaviour
    {
        [Header("UI 요소")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Sprite fieldLoadingSprite;
        [SerializeField] private Sprite dungeonLoadingSprite;
        [SerializeField] private Slider loadingSlider;

        private void Start()
        {
            SetupBackgroundImage();
            StartCoroutine(LoadNextScene());
        }

        private void SetupBackgroundImage()
        {
            string nextScene = SceneLoader.NextSceneName;

            if (nextScene == "FieldScene")
                backgroundImage.sprite = fieldLoadingSprite;
            else if (nextScene == "DungeonScene")
                backgroundImage.sprite = dungeonLoadingSprite;
            // 그 외: 기본 이미지 유지
        }

        private IEnumerator LoadNextScene()
        {
            string nextScene = SceneLoader.NextSceneName;
            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;

            while (op.progress < 0.9f)
            {
                loadingSlider.value = Mathf.Clamp01(op.progress / 0.9f);
                yield return null;
            }

            // 보정 시간 잠깐 주고 마무리
            yield return new WaitForSeconds(0.5f);
            loadingSlider.value = 1f;
            op.allowSceneActivation = true;
        }
    }

}