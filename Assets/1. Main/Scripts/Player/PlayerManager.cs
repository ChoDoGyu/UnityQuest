using UnityEngine;


namespace Main.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Module References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerLook playerLook;
        [SerializeField] private PlayerAnimatorController animatorController;
        // [SerializeField] private WeaponController weaponController; // ���� Ȯ��

        private void Awake()
        {
            // ���� �ܺο��� PlayerManager�� ���� ���� ��⿡ �����ϰ� �� ����
            // ����� ���Ḹ Ȯ��
        }
    }
}
