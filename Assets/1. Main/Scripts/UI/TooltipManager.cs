using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Main.Scripts.UI
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance { get; private set; }

        [SerializeField] private GameObject tooltipPanel;
        [SerializeField] private TextMeshProUGUI tooltipText;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            HideTooltip();
        }


        public void ShowTooltip(string text, Vector2 position)
        {
            tooltipPanel.SetActive(true);
            tooltipText.text = text;
            tooltipPanel.transform.position = position;
        }

        public void HideTooltip()
        {
            tooltipPanel.SetActive(false);
        }
    }
}