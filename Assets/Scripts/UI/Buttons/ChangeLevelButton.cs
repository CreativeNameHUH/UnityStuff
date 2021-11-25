using System;
using TMPro;
using UnityEngine;

namespace UI.Buttons
{
    public class ChangeLevelButton : MonoBehaviour
    {
        [Tooltip("Number of levels available to play.")]
        public int numberOfLevels = 1;
        [Tooltip("TextMeshProUGUI containing level number.")]
        public TextMeshProUGUI level;

        private int _levelNumber;
    
        public void NextButtonPress()
        {
            if (_levelNumber >= numberOfLevels) return;
        
            _levelNumber++;
            level.text = _levelNumber.ToString();
        }

        public void PrevButtonPress()
        {
            if (_levelNumber <= 1) return;
        
            _levelNumber--;
            level.text = _levelNumber.ToString();
        }

        protected void Awake()
        {
            _levelNumber = Convert.ToInt32(level.text);
        }
    }
}
