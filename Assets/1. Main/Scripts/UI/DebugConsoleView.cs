using UnityEngine;
using TMPro;

namespace Main.Scripts.UI
{
    public class DebugConsoleView : MonoBehaviour
    {
        [SerializeField] private GameObject consolePanel;
        [SerializeField] private TMP_Text consoleText;

        private bool isVisible = false;

        private void Start()
        {
            consolePanel.SetActive(false);
        }

        public void ToggleConsole()
        {
            isVisible = !isVisible;
            consolePanel.SetActive(isVisible);
        }

        public void Log(string message)
        {
            consoleText.text += message + "\n";
        }
    }
}
