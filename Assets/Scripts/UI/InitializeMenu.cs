using UnityEngine;

namespace UI
{
    public class InitializeMenu : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("MenuFPS", 30);
        }
    }
}
