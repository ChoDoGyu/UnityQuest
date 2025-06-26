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

        public virtual string GetTooltipText()
        {
            if (currentItem == null) return "";

            string result = $"<b>{currentItem.itemName}</b>";

            if (!string.IsNullOrEmpty(currentItem.description))
                result += $"\n{currentItem.description}";

            if (UIManager.Instance.isSellMode)
            {
                int sellPrice = Mathf.RoundToInt(currentItem.price * 0.5f);
                result += $"\n\n<color=yellow><size=90%>Sell Price: {sellPrice:N0} G</size></color>";
            }

            return result;
        }
    }
}
