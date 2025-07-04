using UnityEngine;
using Main.Scripts.SceneManagement;
using Main.Scripts.Core;
using System.Collections;

namespace Main.Scripts.UI
{
    /// <summary>
    /// 시작 씬에서 New Game, Load Game, Option, Quit 버튼을 처리하는 전용 UI 클래스입니다.
    /// </summary>
    public class StartMenuUI : MonoBehaviour
    {
        [Header("씬 이름 설정")]
        [Tooltip("게임 시작 시 이동할 필드 씬 이름")]
        [SerializeField] private string startSceneName = "FieldScene";

        [Header("UI 패널")]
        [Tooltip("불러오기 패널")]
        [SerializeField] private GameObject loadGamePanel;

        [Tooltip("옵션 패널")]
        [SerializeField] private GameObject optionPanel;


        /// <summary>
        /// 'New Game' 버튼 클릭 시 호출됩니다.
        /// 자동 저장을 수행하고 로딩 씬을 거쳐 게임 씬으로 이동합니다.
        /// </summary>
        public void OnClick_NewGame()
        {
            StartCoroutine(StartNewGameSequence());
        }

        private IEnumerator StartNewGameSequence()
        {
            Debug.Log($"GameManager.Instance = {GameManager.Instance}");
            Debug.Log($"GameManager.Instance.SaveDataManager = {GameManager.Instance?.SaveDataManager}");

            // 자동 저장 완료될 때까지 대기
            yield return StartCoroutine(GameManager.Instance.SaveDataManager.SaveGameCoroutine(SaveDataManager.SaveSlotType.Auto));

            // 저장 완료 후 씬 로딩
            SceneLoader.Instance.LoadScene(startSceneName);
        }

        /// <summary>
        /// 'Load Game' 버튼 클릭 시 호출됩니다.
        /// 저장된 게임을 불러오는 패널을 활성화합니다.
        /// </summary>
        public void OnClick_LoadGame()
        {
            if (loadGamePanel != null)
                loadGamePanel.SetActive(true);
        }

        /// <summary>
        /// 'Option' 버튼 클릭 시 호출됩니다.
        /// 옵션 패널을 직접 활성화합니다.
        /// </summary>
        public void OnClick_Option()
        {
            if (optionPanel != null)
                optionPanel.SetActive(true);
        }

        /// <summary>
        /// 'Quit' 버튼 클릭 시 호출됩니다.
        /// 게임을 종료하며, 에디터에서는 Play 모드를 중지합니다.
        /// </summary>
        public void OnClick_Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }


        /// <summary>
        /// 패널 닫기 버튼에서 사용할 수 있는 유틸리티 메서드입니다.
        /// </summary>
        public void OnClick_ClosePanel(GameObject panel)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }
}