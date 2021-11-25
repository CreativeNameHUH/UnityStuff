using System;
using TMPro;
using UI.Buttons;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    public class FPSTarget : MonoBehaviour
    {
        public TextMeshProUGUI gameFPSText;
        public TextMeshProUGUI menuFPSText;

        public Slider gameFPSSlider;
        public Slider menuFPSSlider;
        
        
        public int GameFPS
        {
            get => _gameFPS;
            set => _gameFPS = value;
        }
        public int MenuFPS
        {
            get => _menuFPS;
            set => _menuFPS = value;
        }

        private ResolutionManager _resolutionManager;
        private NewSceneButton _newScene;
    
        private int _gameFPS;
        private int _menuFPS;


        public void Restore()
        {
            gameFPSSlider.value = _gameFPS;
            menuFPSSlider.value = _menuFPS;
            Application.targetFrameRate = _menuFPS;
            
            TextChanged();
        }

        public void OnGameSliderChange()
        {
            ChangeGameFramerate(false);
        }

        public void OnGameButtonPress()
        {
            ChangeGameFramerate(true);
        }

        public void OnMenuSliderChange()
        {
            ChangeMenuFramerate(false);
        }

        public void OnMenuButtonPress()
        {
            ChangeMenuFramerate(true);
        }
        
        private void ChangeGameFramerate(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                _gameFPS++;
                gameFPSSlider.value = _gameFPS;
            }
            else
            {
                _gameFPS = Convert.ToInt32(gameFPSSlider.value);
            }

            _resolutionManager.SettingsChanged = true;
            TextChanged();
        }

        private void ChangeMenuFramerate(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                _menuFPS++;
                menuFPSSlider.value = _menuFPS;
            }
            else
            {
                _menuFPS = Convert.ToInt32(menuFPSSlider.value);
            }
            
            _resolutionManager.SettingsChanged = true;
            TextChanged();
        }

        private void TextChanged()
        {
            gameFPSText.text = "In-game framerate:\n"
                               + _gameFPS + " FPS";
            
            menuFPSText.text = "Menu framerate:\n"
                               + _menuFPS + " FPS";
        }

        private void Start()
        {
            GameObject settingsManager = GameObject.Find("SettingsButtonManager");

            _resolutionManager = settingsManager.GetComponent<ResolutionManager>();
            _resolutionManager.GetFPSTarget(this);
        }
    }
}
