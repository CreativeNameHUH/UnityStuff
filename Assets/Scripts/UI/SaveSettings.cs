using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SaveSettings : MonoBehaviour
    {
        private ResolutionManager _resolutionManager;

        public void SaveButton()
        {
            _resolutionManager.SaveSettings();
            _resolutionManager.Destroy();
            _resolutionManager.SettingsChanged = false;
        }

        public void RestoreButton()
        {
            _resolutionManager.RestoreSettings();
            _resolutionManager.Destroy();
            _resolutionManager.SettingsChanged = false;
        }
        private IEnumerator CountdownTimer()
        {
            TextMeshProUGUI countdownTimer = GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            if (countdownTimer == null) yield break;
        
            int timeLeft = Convert.ToInt32(countdownTimer.text);

            while (timeLeft >= 0)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
                countdownTimer.text = timeLeft.ToString();

                Debug.Log(timeLeft.ToString());
            }

            RestoreButton();
        }
        private void Start()
        {
            GameObject settingsManager = GameObject.Find("SettingsButtonManager");
            _resolutionManager = settingsManager.GetComponent<ResolutionManager>();
            _resolutionManager.DisableButtons();

            StartCoroutine(CountdownTimer());
        }
    }
}
