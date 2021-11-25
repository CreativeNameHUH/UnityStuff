using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class ButtonPress : MonoBehaviour
    {
        [Tooltip("This only works when buttonType = 1")]
        public string sceneName = "";
    
        [Tooltip("Button types: \n" +
                 "1 - Start \n" +
                 "2 - Quit \n" +
                 "3 - Settings")]
        public int buttonType = 1;

        private void OnMouseUp()
        {
            switch (buttonType)
            {
                case 1:
                    Application.targetFrameRate = 300;
                    SceneManager.LoadScene(sceneName);
                    break;

                case 2:
                    Application.Quit();
                    break;

                default:
                    break;
            }
        }
    }
}
