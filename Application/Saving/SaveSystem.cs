using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Samurai.Application.Configs;
using UnityEngine.Pool;

namespace Samurai.Application.Saving
{
    public class SaveSystem
    {
        public const string Autosave = "Autosave";
        internal const string LogTag = "Saves";
        private const string SaveSuffix = ".save";
        
        private readonly string _saveFolder;
        private readonly List<SessionSaves> _saves = new();

        public IReadOnlyList<SessionSaves> Saves => _saves;

        #region Lifecycle

        public SaveSystem()
        {
            var config = Definitions.Config<AppConfig>();
            _saveFolder = Path.Combine(UnityEngine.Application.persistentDataPath, config.SaveFolder);

            if (!Directory.Exists(_saveFolder))
            {
                Directory.CreateDirectory(_saveFolder);
            }

            using var obj = ListPool<string>.Get(out var saves);
            foreach (string directory in Directory.EnumerateDirectories(_saveFolder))
            {
                string path = Path.Combine(_saveFolder, directory);
                
                var files = Directory.EnumerateFiles(path).OrderByDescending(File.GetLastWriteTime);
                foreach (string filePath in files)
                {
                    if (filePath is null || !filePath.EndsWith(SaveSuffix))
                    {
                        continue;
                    }
                
                    saves.Add(Path.GetFileNameWithoutExtension(filePath));
                }
                
                string sessionId = Path.GetFileNameWithoutExtension(directory);
                _saves.Add(new SessionSaves(sessionId, saves));
                saves.Clear();
            }
        }

        #endregion Lifecycle

        #region Public
        
        public SaveState Load(string sessionId, string fileName)
        {
            Log.Debug($"Loading '{fileName}' for session '{sessionId}'.", LogTag);
            string path = GetSavePath(sessionId, fileName);
            if (!File.Exists(path))
            {
                Log.Debug($"Save file '{fileName}' for session '{sessionId}' doesn't exist.", LogTag);
                return null;
            }

            using var file = File.OpenRead(path);
            using var reader = new BinaryReader(file);
            string content = reader.ReadString();
            var state = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            Log.Debug($"Loaded '{fileName}' for session '{sessionId}'.", LogTag);
            
            return new SaveState(state);
        }

        public void Save(string sessionId, string fileName, IEnumerable<ISavable> savables)
        {
            Log.Debug($"Saving '{fileName}' for session '{sessionId}'.", LogTag);
            using var obj = DictionaryPool<string, string>.Get(out var state);
            foreach (var savable in savables)
            {
                string id = savable.Id;
                if (string.IsNullOrEmpty(id))
                {
                    Log.Warning("Savable with empty id. Skipping..", LogTag);
                    continue;
                }

                object entryState = savable.GetSave();
                if (!entryState.GetType().IsSerializable)
                {
                    Log.Error($"Savable's '{savable.Id}' state of type '{entryState.GetType().Name}' is not Serializable. Skipping..", LogTag);
                    continue;
                }

                string json = JsonConvert.SerializeObject(entryState);
                state[id] = json;
            }

            string sessionFolder = Path.Combine(_saveFolder, sessionId);
            if (!Directory.Exists(sessionFolder))
            {
                Directory.CreateDirectory(sessionFolder);
            }
            
            string path = GetSavePath(sessionId, fileName);
            using var file = File.Open(path, FileMode.Create);
            using var writer = new BinaryWriter(file);
            writer.Write(JsonConvert.SerializeObject(state));
            Log.Debug($"Saved '{fileName}' for session '{sessionId}'.", LogTag);
        }

        #endregion Public

        #region Private

        private string GetSavePath(string sessionId, string fileName)
        {
            string path = Path.Combine(_saveFolder, sessionId, fileName);
            return $"{path}{SaveSuffix}";
        }

        #endregion Private
    }
}