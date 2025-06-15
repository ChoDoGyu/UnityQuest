using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Main.Scripts.UI
{
    public class RebindSlot : MonoBehaviour
    {
        [Header("Input Binding")]
        public InputActionReference actionRef;
        public int bindingIndex = 0;

        [Header("UI")]
        [SerializeField] private TMP_Text currentKeyText;
        [SerializeField] private TMP_InputField inputField;

        // ��� ���� ���� ����Ʈ (�ߺ� �˻��)
        public static List<RebindSlot> allSlots = new List<RebindSlot>();

        private void Awake()
        {
            allSlots.Add(this);
            inputField.onSelect.AddListener(_ => StartRebinding());
            UpdateKeyText();
        }

        private void OnDestroy()
        {
            allSlots.Remove(this);
        }

        private void StartRebinding()
        {
            currentKeyText.text = "�Է� ��� ��...";
            inputField.text = "";

            actionRef.action.Disable();

            actionRef.action.PerformInteractiveRebinding(bindingIndex)
                .WithCancelingThrough("<Keyboard>/escape")
                .OnComplete(op =>
                {
                    op.Dispose();
                    string newPath = actionRef.action.bindings[bindingIndex].effectivePath;

                    if (IsDuplicate(newPath))
                    {
                        currentKeyText.text = "�ߺ���";
                        RevertBinding();
                    }
                    else
                    {
                        currentKeyText.text = ToReadable(newPath);
                    }

                    actionRef.action.Enable();
                })
                .Start();
        }


        private bool IsDuplicate(string newPath)
        {
            foreach (var slot in allSlots)
            {
                if (slot == this) continue;
                string otherPath = slot.actionRef.action.bindings[slot.bindingIndex].effectivePath;
                if (newPath == otherPath)
                    return true;
            }
            return false;
        }

        private void RevertBinding()
        {
            actionRef.action.RemoveBindingOverride(bindingIndex);
            UpdateKeyText();
        }

        private void UpdateKeyText()
        {
            string path = actionRef.action.bindings[bindingIndex].effectivePath;
            currentKeyText.text = ToReadable(path);
        }

        private string ToReadable(string path)
        {
            return InputControlPath.ToHumanReadableString(path, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }
}