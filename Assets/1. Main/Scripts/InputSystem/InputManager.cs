using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.InputSystem
{
    /// <summary>
    /// 입력 활성화 여부를 제어합니다. 일시정지 상태에서 입력을 막기 위해 사용됩니다.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        private bool isPaused = false;

        /// <summary>
        /// 일시정지 상태 설정
        /// </summary>
        public void SetPaused(bool pause)
        {
            isPaused = pause;
        }

        /// <summary>
        /// 현재 입력이 허용되는 상태인지 반환
        /// </summary>
        public bool IsInputEnabled()
        {
            return !isPaused;
        }
    }
}
