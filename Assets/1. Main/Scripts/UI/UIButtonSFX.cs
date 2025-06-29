using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Core;

namespace Main.Scripts.UI
{
    /// <summary>
    /// UI 버튼 클릭 시 지정된 사운드를 자동으로 재생하는 스크립트
    /// </summary>
    public class UIButtonSFX : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("AudioManager에 등록된 UI 사운드 이름 (예: 'Click')")]
        [SerializeField] private string sfxName = "Click";

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!string.IsNullOrEmpty(sfxName))
                AudioManager.Instance?.PlayUI(sfxName);
        }
    }
}