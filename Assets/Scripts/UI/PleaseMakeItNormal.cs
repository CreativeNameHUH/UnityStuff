using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PleaseMakeItNormal : MonoBehaviour
    {
        public Button[] thingsToMakePink;
        public int r, g, b;
        public void ChangeColor()
        {
            int index = 0;
            var color = new Color(r, g, b);
            
            while (index < thingsToMakePink.Length)
            {
                var colorBlock = thingsToMakePink[index].colors;
                colorBlock.normalColor = color;
                thingsToMakePink[index].colors = colorBlock;
            
                index ++;
            }
        }
    }
}
