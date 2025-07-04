using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Main.Scripts.SceneManagement
{
    public class LoadingSceneController : MonoBehaviour
    {
        [Header("UI ���")]
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
            // �� ��: �⺻ �̹��� ����
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

            // ���� �ð� ��� �ְ� ������
            yield return new WaitForSeconds(0.5f);
            loadingSlider.value = 1f;
            op.allowSceneActivation = true;
        }
    }

}