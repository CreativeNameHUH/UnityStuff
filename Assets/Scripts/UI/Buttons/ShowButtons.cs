using UnityEngine;

namespace UI.Buttons
{
    public class ShowButtons : MonoBehaviour
    {
        public Component[] buttonsToShow;
        //public Component thisObject;

        private void Show()
        {
            int index = 0;
            int length = buttonsToShow.Length;

            while (index < length)
            {
                buttonsToShow[index].transform.localPosition = new Vector3(0f, 0f);
                index ++;
            }
            Debug.Log("Buttons are visible");
        }
        public void OnButtonPress()
        {
            Show();
        }
    }
}
