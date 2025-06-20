using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Interfaces;

namespace Main.Scripts.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ITooltipProvider provider;

        private void Awake()
        {
            provider = GetComponent<ITooltipProvider>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (provider != null)
            {
                TooltipManager.Instance.ShowTooltip(
                    provider.GetTooltipText(),
                    Input.mousePosition
                );
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}