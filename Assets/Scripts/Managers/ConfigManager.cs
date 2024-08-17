using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Managers
{
    public class ConfigManager
    {
        public static ConfigManager _;
        private JObject _config;

        // constructor
        public ConfigManager()
        {
            _ = this;
            LoadAllConfigs();
        }

        private void LoadAllConfigs()
        {
            _config = new JObject();

            string configDirectory = Path.Combine(Application.dataPath, "Config");
            if (Directory.Exists(configDirectory))
            {
                string[] jsonFiles = Directory.GetFiles(configDirectory, "*.json");

                foreach (var file in jsonFiles)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    string json = File.ReadAllText(file);
                    JObject fileConfig = JObject.Parse(json);

                    _config[fileNameWithoutExtension] = fileConfig;
                }

                Debug.Log("All config files loaded successfully.");
            }
            else
            {
                Debug.LogError("Config directory not found at " + configDirectory);
            }
        }

        public T GetConfigValue<T>(string key)
        {
            JToken token = _config.SelectToken(key);
            if (token != null)
            {
                return token.Value<T>();
            }
            else
            {
                Debug.LogError("Key not found: " + key);
                return default(T);
            }
        }

        public static T Get<T>(string key)
        {
            return _.GetConfigValue<T>(key);
        }
    }
}
