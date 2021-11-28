using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Gravity : MonoBehaviour
    {
        public Vector3 gravity = new Vector3(0, -45, 0);

        public TextMeshProUGUI infoButtonText;

        public Button increaseButton;
        public Button decreaseButton;
        
        private float _gravityMultiplier = 1f;
        private float _defaultGravity;

        private const string Text = "Gravity: ";

        public void IncreaseGravity()
        {
            if (_gravityMultiplier >= 256f)
                return;
            
            _gravityMultiplier *= 2f;
            gravity.y *= _gravityMultiplier;
            infoButtonText.text = Text + _gravityMultiplier + "x";
            Physics.gravity = gravity;
        }

        public void DecreaseGravity()
        {
            if (_gravityMultiplier <= 0.256f)
                return;
            
            _gravityMultiplier /= 2;
            gravity.y *= _gravityMultiplier;
            infoButtonText.text = Text + _gravityMultiplier + "x";
            Physics.gravity = gravity;
        }

        public void ResetGravity()
        {
            _gravityMultiplier = 1f;
            gravity.y = _defaultGravity;
            infoButtonText.text = Text + "1x";
            Physics.gravity = gravity;
        }
        private void Start()
        {
            _defaultGravity = gravity.y;
            Physics.gravity = gravity;
        }

        private void Update()
        {
            decreaseButton.interactable = _gravityMultiplier > 0.256f;
            increaseButton.interactable = _gravityMultiplier < 256f;
        }
    }
}
