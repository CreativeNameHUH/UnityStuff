using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class HideButtons : MonoBehaviour
    {
        [Tooltip("Buttons that will be hidden")]
        public Component[] buttonsToHide;
        
        private void Hide()
        {
            int index = 0;
            int length = buttonsToHide.Length;

            while (index < length)
            {
                buttonsToHide[index].transform.localPosition = new Vector3(5000f, 0f);
                index ++;
            }
            Debug.Log("All buttons are hidden");
        }
        
        public void OnButtonPress()
        {
            Hide();
        }
    }
}
