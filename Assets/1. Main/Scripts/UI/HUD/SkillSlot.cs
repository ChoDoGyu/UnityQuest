using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Data;


namespace Main.Scripts.UI.HUD
{
    public class SkillSlot : MonoBehaviour
    {
        [SerializeField] private Image iconImage;        // 아이콘 표시용
        [SerializeField] private Image cooldownMask;     // 쿨타임 마스크
        [SerializeField] private Button button;          // 클릭용 버튼

        private SkillData skillData;
        private float cooldown;
        private float currentCooldown;
        private System.Action<SkillData> onClick;        // SkillManager와 연결되는 이벤트

        // 외부에서 이 슬롯의 스킬 ID를 가져갈 때 사용 (비교용)
        public string SkillId => skillData?.skillName ?? "";

        private void Update()
        {
            // 쿨타임 감소 + 마스크 채우기 업데이트
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
