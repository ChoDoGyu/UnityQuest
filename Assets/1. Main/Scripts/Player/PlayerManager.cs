using UnityEngine;


namespace Main.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Module References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerLook playerLook;
        [SerializeField] private PlayerAnimatorController animatorController;
        // [SerializeField] private WeaponController weaponController; // 추후 확장

        private void Awake()
        {
            // 추후 외부에서 PlayerManager를 통해 하위 모듈에 접근하게 될 구조
            // 현재는 연결만 확인
        }
    }
}
