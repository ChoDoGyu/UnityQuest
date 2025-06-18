using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class DragHandler : MonoBehaviour
    {
        public static DragHandler Instance;

        public Image dragIcon;
        [HideInInspector] public ItemSlotBase draggedSlot;
        private void Awake()
        {
            Instance = this;
            dragIcon.gameObject.SetActive(false);
        }

        public void StartDrag(ItemSlotBase slot)
        {
            draggedSlot = slot;
            dragIcon.sprite = slot.currentItem.icon;
            dragIcon.gameObject.SetActive(true);
        }

        public void OnDrag()
        {
            dragIcon.transform.position = Input.mousePosition;
        }

        public void EndDrag()
        {
            dragIcon.gameObject.SetActive(false);
            draggedSlot = null;
        }
    }
}