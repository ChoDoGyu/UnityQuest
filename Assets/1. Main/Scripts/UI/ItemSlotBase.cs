using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Data;

namespace Main.Scripts.UI
{
    public class ItemSlotBase : MonoBehaviour
    {
        public Image iconImage;
        public ItemData currentItem;

        public virtual void SetItem(ItemData item)
        {
            currentItem = item;
            iconImage.sprite = item.icon;
            iconImage.enabled = true;
        }

        public virtual void Clear()
        {
            currentItem = null;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
