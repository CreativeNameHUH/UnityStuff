using UnityEngine;

namespace Game
{
    public class Initialize : MonoBehaviour
    {
        void Start()
        {
            int targetFPS = PlayerPrefs.GetInt("IngameFPS", 60);
        
            Application.targetFrameRate = targetFPS;
        }
    }
}
