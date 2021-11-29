using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Gravity : MonoBehaviour
    {
        [Tooltip("Gravity settings, they are replaced on start")]
        public Vector3 gravity = new Vector3(0, -50, 0);

        public TextMeshProUGUI infoButtonText;

        public Button          increaseButton;
        public Button          decreaseButton;
        
        private Initialize _gameSettings;
        
        private float _gravityMultiplier = 1f;
        private float _defaultGravity    = -50f;

        private const string Text        = "Gravity: ";

        public void IncreaseGravity()
        {
            if (_gravityMultiplier >= 256f)
                return;
            
            _gravityMultiplier *= 2f;
            SetGravity();
        }

        public void DecreaseGravity()
        {
            if (_gravityMultiplier <= 0.256f)
                return;
            
            _gravityMultiplier /= 2;
            SetGravity();
        }

        public void ResetGravity()
        {
            _gravityMultiplier = 1f;
            gravity.y = _defaultGravity;
            infoButtonText.text = Text + "1x";
            Physics.gravity = gravity;
        }

        private void SetGravity()
        {
            gravity.y *= _gravityMultiplier;
            infoButtonText.text = Text + _gravityMultiplier + "x";
            Physics.gravity = gravity;
        }
        private void Start()
        {
            _gameSettings = GetComponentInParent<Initialize>();
            _defaultGravity = _gameSettings.GetSettings.Gravity;
            gravity.y = _defaultGravity;
            Physics.gravity = gravity;
        }

        private void Update()
        {
            decreaseButton.interactable = _gravityMultiplier > 0.256f;
            increaseButton.interactable = _gravityMultiplier < 256f;
        }
    }
}
