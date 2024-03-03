using System;
using System.Collections.Generic;
using Samurai.Application;
using Samurai.Application.Configs;
using Samurai.Application.Events;
using Samurai.Application.Pooling;
using Samurai.Application.Saving;
using Samurai.NSession.Example;

namespace Samurai.NSession
{
    public class Session
    {
        internal const string LogTag = "Session";
        
        #region Static

        private static Session _instance;

        public static ComponentPool Pool => App.Get<ComponentPool>();
        public static EventAggregator Events => Get<EventAggregator>();

        #region Lifecycle

        public static void Create(string sessionId, string saveName)
        {
            _instance = new Session(sessionId, saveName);
            _instance.Init();
            
            Log.Debug($"Session '{sessionId}' initialized.", LogTag);
        }

        public static void Dispose()
        {
            string sessionId = _instance._sessionId;

            bool isAutosaveEnabled = Definitions.Config<AppConfig>().EnableAutosaves;
            if (isAutosaveEnabled)
            {
                Get<SaveSystem>().Save(sessionId, SaveSystem.Autosave, _instance._savables);
            }

            _instance.DisposeInternal();
            _instance = null;
            
            Log.Debug($"Session '{sessionId}' disposed.", LogTag);
        }

        #endregion Lifecycle

        #region Access

        public static bool Exists()
        {
            return _instance != null;
        }

        public static void Add<T>(T obj)
        {
            if (Exists())
            {
                _instance._content[typeof(T)] = obj;
            }
        }

        public static T Get<T>()
        {
            if (!Exists())
            {
                return default;
            }
            
            if (_instance._content.TryGetValue(typeof(T), out var obj))
            {
                return (T)obj;
            }

            return App.Get<T>();
        }

        #endregion Access

        #region Saves

        public static void Register(ISavable savable)
        {
            _instance._savables.Add(savable);
        }

        public static void Save(string fileName)
        {
            var saves = Get<SaveSystem>();
            saves.Save(_instance._sessionId, fileName, _instance._savables);
        }

        public static bool HasSave()
        {
            return _instance._saveState is not null;
        }

        public static bool TryLoadState<T>(string id, out T state)
        {
            state = default;
            if (!HasSave())
            {
                return false;
            }

            return _instance._saveState.TryGet(id, out state);
        }

        public static T LoadState<T>(string id)
        {
            if (!HasSave())
            {
                return default;
            }

            return _instance._saveState.Get<T>(id);
        }

        #endregion Saves

        #endregion Static

        private readonly Dictionary<Type, object> _content = new();
        private readonly HashSet<ISavable> _savables = new();

        private readonly string _sessionId;
        private readonly SaveState _saveState;

        private Session(string sessionId, string saveName)
        {
            _sessionId = sessionId;
            if (!string.IsNullOrEmpty(saveName))
            {
                _saveState = App.Get<SaveSystem>().Load(sessionId, saveName);
            }
        }

        private void Init()
        {
            Add(new EventAggregator(App.Get<EventAggregator>()));
            
            // TODO: this is only an example, delete from here and replace with your stuff
            Add(new ExampleModel());
            Add(new ExampleSystem());
        }

        private void DisposeInternal()
        {
            foreach (object entry in _content.Values)
            {
                if (entry is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _content.Clear();
        }
    }
}
