using UnityEngine;
using UnityEngine.UI;

namespace UI
{ 
    public class TooMuchPink : MonoBehaviour
    {
        public Toggle toggle;
        public Button[] thingsToMakePink;

        private void ChangeColor(float r, float g, float b, float a)
        {
            Color color = new Color(r, g, b, a);
            int index = 0;

            while (index < thingsToMakePink.Length)
            {
                ColorBlock colorBlock = thingsToMakePink[index].colors;
                colorBlock.normalColor = color;
                thingsToMakePink[index].colors = colorBlock;

                index++;
            }
        }

        private void ChangeColor(float r, float g, float b)
        {
            Color color = new Color(r, g, b, 1f);
            int index = 0;

            while (index < thingsToMakePink.Length)
            {
                ColorBlock colorBlock = thingsToMakePink[index].colors;
                colorBlock.normalColor = color;
                thingsToMakePink[index].colors = colorBlock;

                index++;
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
