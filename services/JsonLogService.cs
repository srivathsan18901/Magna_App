using Magna_TestApplication.Models;
using System.Text.Json;

namespace Magna_TestApplication.services
{
    public class JsonLogService
    {
        private readonly string _baseFolder;
        private readonly JsonSerializerOptions _options;

        private readonly string _ftLogPath;
        private readonly string _tetLogPath;

        // Cache to avoid reloading from disk on every query
        private List<FunctionalTestLogRecord> _ftCache = new();
        private List<TravelEnduranceLogRecord> _tetCache = new();

        private readonly object _lock = new();

        public JsonLogService()
        {
            // Logs folder next to the exe
            _baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(_baseFolder);

            _ftLogPath = Path.Combine(_baseFolder, "functional_test_logs.json");
            _tetLogPath = Path.Combine(_baseFolder, "travel_endurance_logs.json");

            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            // Load once at startup
            _ftCache = LoadFromFile<FunctionalTestLogRecord>(_ftLogPath);
            _tetCache = LoadFromFile<TravelEnduranceLogRecord>(_tetLogPath);
        }

        // ============================================================
        // FT LOGS
        // ============================================================
        public void AppendFtLog(FunctionalTestLogRecord log)
        {
            lock (_lock)
            {
                log.Id = _ftCache.Count == 0 ? 1 : _ftCache.Max(x => x.Id) + 1;
                _ftCache.Insert(0, log);           // newest first
                SaveToFile(_ftLogPath, _ftCache);
            }
        }

        public List<FunctionalTestLogRecord> GetFtLogs(DateTime from, DateTime to)
        {
            lock (_lock)
            {
                return _ftCache
                    .Where(x => x.LoggedAt >= from && x.LoggedAt <= to)
                    .OrderByDescending(x => x.LoggedAt)
                    .ToList();
            }
        }

        // ============================================================
        // TET LOGS
        // ============================================================
        public void AppendTetLog(TravelEnduranceLogRecord log)
        {
            lock (_lock)
            {
                log.Id = _tetCache.Count == 0 ? 1 : _tetCache.Max(x => x.Id) + 1;
                _tetCache.Insert(0, log);
                SaveToFile(_tetLogPath, _tetCache);
            }
        }

        public List<TravelEnduranceLogRecord> GetTetLogs(DateTime from, DateTime to)
        {
            lock (_lock)
            {
                return _tetCache
                    .Where(x => x.LoggedAt >= from && x.LoggedAt <= to)
                    .OrderByDescending(x => x.LoggedAt)
                    .ToList();
            }
        }

        // ============================================================
        // HELPERS
        // ============================================================
        private List<T> LoadFromFile<T>(string path)
        {
            try
            {
                if (!File.Exists(path)) return new List<T>();

                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json)) return new List<T>();

                return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load {path}: {ex.Message}");
                return new List<T>();
            }
        }

        private void SaveToFile<T>(string path, List<T> data)
        {
            try
            {
                // Write to temp first, then move — prevents corruption if the app crashes mid-write
                string tempPath = path + ".tmp";
                string json = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(tempPath, json);

                if (File.Exists(path))
                    File.Replace(tempPath, path, null);
                else
                    File.Move(tempPath, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save {path}: {ex.Message}");
            }
        }

        // Optional: manual reload (useful if logs are edited externally)
        public void ReloadAll()
        {
            lock (_lock)
            {
                _ftCache = LoadFromFile<FunctionalTestLogRecord>(_ftLogPath);
                _tetCache = LoadFromFile<TravelEnduranceLogRecord>(_tetLogPath);
            }
        }

        public string GetLogFolder() => _baseFolder;
    }
}