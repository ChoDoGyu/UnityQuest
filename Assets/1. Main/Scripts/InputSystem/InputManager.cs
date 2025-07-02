using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.InputSystem
{
    /// <summary>
    /// �Է� Ȱ��ȭ ���θ� �����մϴ�. �Ͻ����� ���¿��� �Է��� ���� ���� ���˴ϴ�.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        private bool isPaused = false;

        /// <summary>
        /// �Ͻ����� ���� ����
        /// </summary>
        public void SetPaused(bool pause)
        {
            isPaused = pause;
        }

        /// <summary>
        /// ���� �Է��� ���Ǵ� �������� ��ȯ
        /// </summary>
        public bool IsInputEnabled()
        {
            return !isPaused;
        }
    }
}
