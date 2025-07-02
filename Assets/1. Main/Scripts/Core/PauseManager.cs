using UnityEngine;

namespace Main.Scripts.Core
{
    public class PauseManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance?.TogglePause();
            }
        }
    }
}
