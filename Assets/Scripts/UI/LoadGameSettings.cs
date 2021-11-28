using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadGameSettings : MonoBehaviour
    {
        public GameObject gameSettings;
        public Button[] buttons;

        private GameObject _gameSettingsClone;
        
        public void OnClick()
        {
            _gameSettingsClone = gameSettings;
            
            foreach (Button button in buttons)
            {
                button.enabled = false;
            }
            
            gameSettings = Instantiate(gameSettings, GameObject.Find("Canvas").transform, true);
            gameSettings.transform.localScale = new Vector3(120, 1, 60);
            gameSettings.transform.localPosition = new Vector3(1, 1, -2);
        }

        public void DestroyGameSettings()
        {
            foreach (Button button in buttons)
            {
                button.enabled = true;
            }
            
            Destroy(gameSettings);
            gameSettings = _gameSettingsClone;
        }
    }
}
