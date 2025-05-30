using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Data;


namespace Main.Scripts.UI.HUD
{
    public class SkillSlot : MonoBehaviour
    {
        [SerializeField] private Image iconImage;        // ������ ǥ�ÿ�
        [SerializeField] private Image cooldownMask;     // ��Ÿ�� ����ũ
        [SerializeField] private Button button;          // Ŭ���� ��ư

        private SkillData skillData;
        private float cooldown;
        private float currentCooldown;
        private System.Action<SkillData> onClick;        // SkillManager�� ����Ǵ� �̺�Ʈ

        // �ܺο��� �� ������ ��ų ID�� ������ �� ��� (�񱳿�)
        public string SkillId => skillData?.skillName ?? "";

        private void Update()
        {
            // ��Ÿ�� ���� + ����ũ ä��� ������Ʈ
            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.deltaTime;
                cooldownMask.fillAmount = currentCooldown / cooldown;
            }
            else
            {
                cooldownMask.fillAmount = 0f;
            }
        }

        public void Init(SkillData data, System.Action<SkillData> clickCallback)
        {
            skillData = data;
            iconImage.sprite = data.icon;
            cooldown = data.cooldown;
            currentCooldown = 0f;

            onClick = clickCallback;
            button.onClick.AddListener(() => onClick?.Invoke(skillData));
        }

        public void TriggerCooldown()
        {
            currentCooldown = cooldown;
        }
    }
}
