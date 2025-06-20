using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Data;
using Main.Scripts.Interfaces;

namespace Main.Scripts.UI
{
    public class ItemSlotBase : MonoBehaviour, ITooltipProvider
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

        public string GetTooltipText()
        {
            if (currentItem == null) return "";
            return $"{currentItem.itemName}\n{currentItem.description}";
        }
    }
}
