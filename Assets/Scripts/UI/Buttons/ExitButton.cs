using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class ExitButton : MonoBehaviour
    {
        public string sceneName = "Scenes/LevelSelectScene";
        public void OnClick()
        {
            SceneManager.LoadScene(sceneName);
            Application.targetFrameRate = PlayerPrefs.GetInt("MenuFPS", 30);
        }
    }
}
