using System.Text.Json;

namespace Magna_TestApplication.services
{
    public class PlcConfigService
    {
        private readonly string _configPath;
        private readonly JsonSerializerOptions _options;

        // Key = LogPropertyName (or ParameterName for meta), Value = RegisterAddress
        public Dictionary<string, string> RegisterOverrides { get; private set; } = new();

        public PlcConfigService()
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            _configPath = Path.Combine(baseFolder, "plc_config.json");

            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            Load();
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    string json = File.ReadAllText(_configPath);
                    RegisterOverrides = JsonSerializer.Deserialize<Dictionary<string, string>>(json, _options)
                                        ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config load error: " + ex.Message);
                RegisterOverrides = new Dictionary<string, string>();
            }
        }

        public void Save(Dictionary<string, string> overrides)
        {
            try
            {
                RegisterOverrides = overrides;
                string json = JsonSerializer.Serialize(overrides, _options);

                // Atomic write
                string tempPath = _configPath + ".tmp";
                File.WriteAllText(tempPath, json);
                if (File.Exists(_configPath))
                    File.Replace(tempPath, _configPath, null);
                else
                    File.Move(tempPath, _configPath);

                Console.WriteLine("✔ PLC config saved to " + _configPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config save error: " + ex.Message);
                throw;
            }
        }

        public string GetConfigPath() => _configPath;
    }
}