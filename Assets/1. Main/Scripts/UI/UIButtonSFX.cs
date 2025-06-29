using UnityEngine;
using UnityEngine.EventSystems;
using Main.Scripts.Core;

namespace Main.Scripts.UI
{
    /// <summary>
    /// UI ��ư Ŭ�� �� ������ ���带 �ڵ����� ����ϴ� ��ũ��Ʈ
    /// </summary>
    public class UIButtonSFX : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("AudioManager�� ��ϵ� UI ���� �̸� (��: 'Click')")]
        [SerializeField] private string sfxName = "Click";

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!string.IsNullOrEmpty(sfxName))
                AudioManager.Instance?.PlayUI(sfxName);
        }
    }
}