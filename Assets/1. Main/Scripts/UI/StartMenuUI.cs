using UnityEngine;
using Main.Scripts.SceneManagement;
using Main.Scripts.Core;
using System.Collections;

namespace Main.Scripts.UI
{
    /// <summary>
    /// ���� ������ New Game, Load Game, Option, Quit ��ư�� ó���ϴ� ���� UI Ŭ�����Դϴ�.
    /// </summary>
    public class StartMenuUI : MonoBehaviour
    {
        [Header("�� �̸� ����")]
        [Tooltip("���� ���� �� �̵��� �ʵ� �� �̸�")]
        [SerializeField] private string startSceneName = "FieldScene";

        [Header("UI �г�")]
        [Tooltip("�ҷ����� �г�")]
        [SerializeField] private GameObject loadGamePanel;

        [Tooltip("�ɼ� �г�")]
        [SerializeField] private GameObject optionPanel;


        /// <summary>
        /// 'New Game' ��ư Ŭ�� �� ȣ��˴ϴ�.
        /// �ڵ� ������ �����ϰ� �ε� ���� ���� ���� ������ �̵��մϴ�.
        /// </summary>
        public void OnClick_NewGame()
        {
            StartCoroutine(StartNewGameSequence());
        }

        private IEnumerator StartNewGameSequence()
        {
            Debug.Log($"GameManager.Instance = {GameManager.Instance}");
            Debug.Log($"GameManager.Instance.SaveDataManager = {GameManager.Instance?.SaveDataManager}");

            // �ڵ� ���� �Ϸ�� ������ ���
            yield return StartCoroutine(GameManager.Instance.SaveDataManager.SaveGameCoroutine(SaveDataManager.SaveSlotType.Auto));

            // ���� �Ϸ� �� �� �ε�
            SceneLoader.Instance.LoadScene(startSceneName);
        }

        /// <summary>
        /// 'Load Game' ��ư Ŭ�� �� ȣ��˴ϴ�.
        /// ����� ������ �ҷ����� �г��� Ȱ��ȭ�մϴ�.
        /// </summary>
        public void OnClick_LoadGame()
        {
            if (loadGamePanel != null)
                loadGamePanel.SetActive(true);
        }

        /// <summary>
        /// 'Option' ��ư Ŭ�� �� ȣ��˴ϴ�.
        /// �ɼ� �г��� ���� Ȱ��ȭ�մϴ�.
        /// </summary>
        public void OnClick_Option()
        {
            if (optionPanel != null)
                optionPanel.SetActive(true);
        }

        /// <summary>
        /// 'Quit' ��ư Ŭ�� �� ȣ��˴ϴ�.
        /// ������ �����ϸ�, �����Ϳ����� Play ��带 �����մϴ�.
        /// </summary>
        public void OnClick_Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }


        /// <summary>
        /// �г� �ݱ� ��ư���� ����� �� �ִ� ��ƿ��Ƽ �޼����Դϴ�.
        /// </summary>
        public void OnClick_ClosePanel(GameObject panel)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }
}