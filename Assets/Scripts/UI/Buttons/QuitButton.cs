using UnityEngine;

namespace UI.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        public void OnButtonPress()
        {
            Application.Quit();
        }
    }
}
