using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace UI
{
    internal class Settings
    {
        public float Gravity = -50f;
        public float MaxSize = 200f;
        public float MinSize = 100f;
        
        public int NumberOfBlocks = 50;
    }
    public class GameSettings : MonoBehaviour
    {
        public TMP_InputField gravityInputField;
        public TMP_InputField maxSizeInputField;
        public TMP_InputField minSizeInputField;
        public TMP_InputField maxBlocksInputField;

        [Tooltip("Location of the settings.")]
        public string filePath = Directory.GetCurrentDirectory() + @"\game_settings.json";
            
        private Settings _settings;
        private LoadGameSettings _loadSettings;
        
        public void RestoreDefaults()
        {
            _settings.Gravity        = -50f;
            _settings.MaxSize        = 200f;
            _settings.MinSize        = 100f;
            _settings.NumberOfBlocks = 50;
            
            Restore();
            
            Debug.Log("Restored Defaults");
        }

        public void Save()
        {
            _settings.Gravity        = Convert.ToSingle(gravityInputField.text);
            _settings.MaxSize        = Convert.ToSingle(maxSizeInputField.text);
            _settings.MinSize        = Convert.ToSingle(minSizeInputField.text);
            _settings.NumberOfBlocks = Convert.ToInt32(maxBlocksInputField.text);
            
            SerializeSettings();
            _loadSettings.DestroyGameSettings();
            
            Debug.Log("Settings saved in: " + filePath);
        }

        private void Restore()
        {
            gravityInputField.text   = _settings.Gravity.ToString();
            maxSizeInputField.text   = _settings.MaxSize.ToString();
            minSizeInputField.text   = _settings.MinSize.ToString();
            maxBlocksInputField.text = _settings.NumberOfBlocks.ToString();
        }
        
        private void SerializeSettings()
        {
            string json = JsonConvert.SerializeObject(_settings);
            File.WriteAllText(filePath, json);
            
            Debug.Log("Settings serialized to: " + filePath);
        }

        private void GetObjectData()
        {
            GameObject nextObject = GameObject.Find("MainButtonManager");
            _loadSettings = nextObject.GetComponent<LoadGameSettings>();
            
            nextObject = GameObject.Find("MaxSize");
            maxSizeInputField = nextObject.GetComponentInChildren<TMP_InputField>();
            
            nextObject = GameObject.Find("MinSize");
            minSizeInputField = nextObject.GetComponentInChildren<TMP_InputField>();
            
            nextObject = GameObject.Find("Gravity");
            gravityInputField = nextObject.GetComponentInChildren<TMP_InputField>();
            
            nextObject = GameObject.Find("NumberOfBlocks");
            maxBlocksInputField = nextObject.GetComponentInChildren<TMP_InputField>();
        }

        private void DeserializeSettings(string json)
        {
            _settings = JsonConvert.DeserializeObject<Settings>(json);
            
            Debug.Log("Settings deserialized from: " + filePath);
        }
        
        private void Start()
        {
            GetObjectData();
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                DeserializeSettings(json);
                Restore();
            }
            else
            {
                _settings = new Settings();
            }
        }
    }
}