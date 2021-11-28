using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{ 
    public class TooMuchPink : MonoBehaviour
    {
        public Toggle toggle;
        public Button[] buttons;
        public TMP_Dropdown[] dropdowns;
        public Slider[] sliders;
        

        private void ChangeColor(float r, float g, float b, float a)
        {
            Color color = new Color(r, g, b, a);
            int index = 0;

            while (index < buttons.Length)
            {
                ColorBlock colorBlock = buttons[index].colors;
                colorBlock.normalColor = color;
                buttons[index].colors = colorBlock;

                index++;
            }
        }

        private void ChangeColor(float r, float g, float b)
        {
            Color color = new Color(r, g, b, 1f);

            foreach (Button button in buttons)
            {
                ColorBlock colorBlock = button.colors;
                colorBlock.normalColor = color;
                button.colors = colorBlock;
            }

            foreach (TMP_Dropdown dropdown in dropdowns)
            {
                ColorBlock colorBlock = dropdown.colors;
                colorBlock.normalColor = color;
                dropdown.colors = colorBlock;
            }
            
            foreach (Slider slider in sliders)
            {
                ColorBlock colorBlock = slider.colors;
                colorBlock.normalColor = color;
                slider.colors = colorBlock;
            }
        }
        public void MakeItPink()
        {
            if (toggle.isOn)
            {
                ChangeColor(1f, 0.6f, 0.95f);
            }
        }

        public void PleaseMakeItNormal()
        {
            if (!toggle.isOn)
            {
                ChangeColor(1f, 1f, 1f);
            }
        }
    }
}
