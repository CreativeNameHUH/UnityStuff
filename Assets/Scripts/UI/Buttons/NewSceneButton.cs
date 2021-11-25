using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class NewSceneButton : MonoBehaviour
    {
        [Tooltip("Name of the scene to load.")]
        public string sceneName = "";

        [Tooltip("True if the next scene is a menu.")]
        public bool menuScene = false;
        
        public int targetFPS = 300;

        public void OnButtonPress()
        {
            Application.targetFrameRate = targetFPS;
            Debug.Log("Load: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }

        private void Start()
        {
            if (menuScene)
            {
                targetFPS = PlayerPrefs.HasKey("MenuFPS") ? PlayerPrefs.GetInt("MenuFPS", 30) : 30;
            }
            else
            {
                targetFPS = PlayerPrefs.HasKey("IngameFPS") ? PlayerPrefs.GetInt("IngameFPS", 60) : 60;
            }
        }
    }
}
