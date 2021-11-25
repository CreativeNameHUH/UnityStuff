using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        [Tooltip("TextMeshPro containing level name")]
        public TextMeshProUGUI level;
    
        [Tooltip("Target FPS for the next scene")]
        public int targetFPS = 300;

        private string GetLevel()
        {
            return level.text switch
            {
                "1" => "Level1Scene",
                "2" => "Level1Scene",
                _ => "LevelSelectScene"
            };
        }
    
        public void OnButtonPress()
        {
            Application.targetFrameRate = targetFPS;
            Debug.Log("Load: " + GetLevel());
            SceneManager.LoadScene(GetLevel());
        }
    }
}
