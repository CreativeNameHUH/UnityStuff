using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class Settings
    {
        public float Gravity = -50f;
        public float MaxSize = 200f;
        public float MinSize = 100f;
        
        public int NumberOfBlocks = 50;
    }
    public class Initialize : MonoBehaviour
    {
        public Settings GetSettings => _settings;
        
        private readonly string _filePath = Directory.GetCurrentDirectory() + @"\game_settings.json";

        private Settings _settings;
        
        private void Deserialize()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _settings = JsonConvert.DeserializeObject<Settings>(json);

                Debug.Log("Settings deserialized from: " + _filePath);
            }
            else
            {
                _settings = new Settings();

                Debug.Log("Couldn't deserialize from: " + _filePath);
            }
        }

        private void Awake()
        {
            int targetFPS = PlayerPrefs.GetInt("IngameFPS", 60);
            Deserialize();
        
            Application.targetFrameRate = targetFPS;
        }
        
    }
}
